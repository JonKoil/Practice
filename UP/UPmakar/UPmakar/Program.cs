using System;
using System.Collections.Generic;
using System.Globalization;

public sealed class Logger
{
    private static readonly Logger _instance = new Logger();
    private readonly List<string> _logs = new List<string>();

    private Logger() { }

    public static Logger Instance => _instance;

    public void Log(string message)
    {
        string logEntry = $"[{DateTime.Now:HH:mm:ss}] {message}";
        _logs.Add(logEntry);
        Console.WriteLine(logEntry);
    }

    public void PrintAllLogs()
    {
        Console.WriteLine("\n=== Полный лог системы ===");
        foreach (var log in _logs)
        {
            Console.WriteLine(log);
        }
    }
}

public interface ITransactionState
{
    void Process(Transaction transaction);
    void Cancel(Transaction transaction);
    string StatusName { get; }
}

public class PendingState : ITransactionState
{
    public string StatusName => "Ожидание";
    public void Process(Transaction transaction)
    {
        Logger.Instance.Log($"Транзакция {transaction.Id} переходит в статус [Выполнена]");
        transaction.SetState(new CompletedState());
    }

    public void Cancel(Transaction transaction)
    {
        Logger.Instance.Log($"Транзакция {transaction.Id} переходит в статус [Отменена]");
        transaction.SetState(new CanceledState());
    }
}

public class CompletedState : ITransactionState
{
    public string StatusName => "Выполнена";
    public void Process(Transaction transaction) =>
        Logger.Instance.Log($"Ошибка: Транзакция {transaction.Id} уже выполнена");

    public void Cancel(Transaction transaction) =>
        Logger.Instance.Log($"Транзакция {transaction.Id} отменена после выполнения");
}

public class CanceledState : ITransactionState
{
    public string StatusName => "Отменена";
    public void Process(Transaction transaction) =>
        Logger.Instance.Log($"Ошибка: Транзакция {transaction.Id} отменена и не может быть выполнена");

    public void Cancel(Transaction transaction) =>
        Logger.Instance.Log($"Транзакция {transaction.Id} уже отменена");
}

public interface IFeeStrategy
{
    decimal CalculateFee(decimal amount);
    string GetDescription();
}

public class PercentageFeeStrategy : IFeeStrategy
{
    private readonly decimal _percentage;

    public PercentageFeeStrategy(decimal percentage) => _percentage = percentage;

    public decimal CalculateFee(decimal amount) => amount * _percentage;
    public string GetDescription() => $"Комиссия {_percentage * 100}%";
}

public class FixedFeeStrategy : IFeeStrategy
{
    private readonly decimal _fixedAmount;

    public FixedFeeStrategy(decimal fixedAmount) => _fixedAmount = fixedAmount;

    public decimal CalculateFee(decimal amount) => _fixedAmount;
    public string GetDescription() => $"Фиксированная комиссия {_fixedAmount}";
}
public abstract class Transaction
{
    public Guid Id { get; } = Guid.NewGuid();
    public decimal Amount { get; }
    public ITransactionState State { get; private set; } = new PendingState();
    public IFeeStrategy FeeStrategy { get; set; }

    protected Transaction(decimal amount, IFeeStrategy feeStrategy)
    {
        Amount = amount;
        FeeStrategy = feeStrategy;
    }

    public void SetState(ITransactionState state) => State = state;
    public void Process() => State.Process(this);
    public void Cancel() => State.Cancel(this);

    public decimal CalculateTotal() => Amount - FeeStrategy.CalculateFee(Amount);

    public abstract string GetTransactionType();
    public abstract void Execute();

    public void PrintDetails()
    {
        Logger.Instance.Log($"\nДетали транзакции:");
        Logger.Instance.Log($"ID: {Id}");
        Logger.Instance.Log($"Тип: {GetTransactionType()}");
        Logger.Instance.Log($"Сумма: {Amount:N2}");
        Logger.Instance.Log($"Комиссия: {FeeStrategy.GetDescription()}");
        Logger.Instance.Log($"Итого: {CalculateTotal():N2}");
        Logger.Instance.Log($"Статус: {State.StatusName}");
    }
}

public class Payment : Transaction
{
    public string Recipient { get; }

    public Payment(decimal amount, string recipient, IFeeStrategy feeStrategy)
        : base(amount, feeStrategy) => Recipient = recipient;

    public override string GetTransactionType() => "Платёж";
    public override void Execute() =>
        Logger.Instance.Log($"Выполнен {GetTransactionType()} на {Amount:N2} для {Recipient}");
}

public class Transfer : Transaction
{
    public string FromAccount { get; }
    public string ToAccount { get; }

    public Transfer(decimal amount, string from, string to, IFeeStrategy feeStrategy)
        : base(amount, feeStrategy) => (FromAccount, ToAccount) = (from, to);

    public override string GetTransactionType() => "Перевод";
    public override void Execute() =>
        Logger.Instance.Log($"Выполнен {GetTransactionType()} {Amount:N2} с {FromAccount} на {ToAccount}");
}

public class Deposit : Transaction
{
    public string Account { get; }

    public Deposit(decimal amount, string account, IFeeStrategy feeStrategy)
        : base(amount, feeStrategy) => Account = account;

    public override string GetTransactionType() => "Пополнение";
    public override void Execute() =>
        Logger.Instance.Log($"Выполнено {GetTransactionType()} {Amount:N2} на счёт {Account}");
}

public interface ITransactionCommand
{
    void Execute();
    void Undo();
    Transaction Transaction { get; }
}

public class TransactionCommand : ITransactionCommand
{
    private readonly Action _undoAction;

    public TransactionCommand(Transaction transaction, Action undoAction)
    {
        Transaction = transaction;
        _undoAction = undoAction;
    }

    public Transaction Transaction { get; }

    public void Execute()
    {
        Logger.Instance.Log($"\n[Команда] Выполнение транзакции {Transaction.Id}");
        Transaction.Process();
        Transaction.Execute();
        Transaction.PrintDetails();
    }

    public void Undo()
    {
        Logger.Instance.Log($"\n[Команда] Отмена транзакции {Transaction.Id}");
        Transaction.Cancel();
        _undoAction?.Invoke();
        Transaction.PrintDetails();
    }
}

public interface ITransactionObserver
{
    void Update(Transaction transaction);
}

public class EmailNotifier : ITransactionObserver
{
    public void Update(Transaction transaction) =>
        Logger.Instance.Log($"Email: {transaction.GetTransactionType()} {transaction.Id} выполнена. Сумма: {transaction.Amount:N2}");
}

public class SmsNotifier : ITransactionObserver
{
    public void Update(Transaction transaction) =>
        Logger.Instance.Log($"SMS: Ваша операция {transaction.Id} завершена");
}

public class TransactionSubject
{
    private readonly List<ITransactionObserver> _observers = new List<ITransactionObserver>();

    public void Attach(ITransactionObserver observer) => _observers.Add(observer);
    public void Detach(ITransactionObserver observer) => _observers.Remove(observer);

    public void Notify(Transaction transaction)
    {
        foreach (var observer in _observers)
        {
            observer.Update(transaction);
        }
    }
}

public interface ITransactionProcessor
{
    void ProcessTransaction(Transaction transaction, Action undoAction);
    bool UndoLastTransaction();
}

public class TransactionProcessor : ITransactionProcessor
{
    private readonly TransactionSubject _subject = new TransactionSubject();
    private readonly Stack<ITransactionCommand> _transactionHistory = new Stack<ITransactionCommand>();

    public TransactionProcessor()
    {
        _subject.Attach(new EmailNotifier());
        _subject.Attach(new SmsNotifier());
    }

    public void ProcessTransaction(Transaction transaction, Action undoAction)
    {
        var command = new TransactionCommand(transaction, undoAction);
        command.Execute();
        _subject.Notify(transaction);
        _transactionHistory.Push(command);
    }

    public bool UndoLastTransaction()
    {
        if (_transactionHistory.Count == 0)
            return false;

        var lastCommand = _transactionHistory.Pop();
        lastCommand.Undo();
        return true;
    }
}

public class LoggingProcessorDecorator : ITransactionProcessor
{
    private readonly ITransactionProcessor _processor;

    public LoggingProcessorDecorator(ITransactionProcessor processor)
        => _processor = processor;

    public void ProcessTransaction(Transaction transaction, Action undoAction)
    {
        Logger.Instance.Log($"[Декоратор] Начало обработки {transaction.Id}");
        _processor.ProcessTransaction(transaction, undoAction);
        Logger.Instance.Log($"[Декоратор] Завершение обработки {transaction.Id}");
    }

    public bool UndoLastTransaction()
    {
        Logger.Instance.Log($"[Декоратор] Попытка отмены последней операции");
        return _processor.UndoLastTransaction();
    }
}

public class CachingProcessorDecorator : ITransactionProcessor
{
    private readonly ITransactionProcessor _processor;
    private readonly Dictionary<Guid, Transaction> _cache = new Dictionary<Guid, Transaction>();

    public CachingProcessorDecorator(ITransactionProcessor processor)
        => _processor = processor;

    public void ProcessTransaction(Transaction transaction, Action undoAction)
    {
        if (!_cache.ContainsKey(transaction.Id))
        {
            Logger.Instance.Log($"[Кэш] Транзакция {transaction.Id} отсутствует в кэше - обработка");
            _cache[transaction.Id] = transaction;
            _processor.ProcessTransaction(transaction, undoAction);
        }
        else
        {
            Logger.Instance.Log($"[Кэш] Транзакция {transaction.Id} уже обработана ранее");
            _cache[transaction.Id].PrintDetails();
        }
    }

    public bool UndoLastTransaction()
    {
        return _processor.UndoLastTransaction();
    }
}

public class BankingSystem
{
    private readonly ITransactionProcessor _processor;
    private readonly Dictionary<string, BankAccount> _accounts;

    public BankingSystem(Dictionary<string, BankAccount> accounts)
    {
        _accounts = accounts;
        ITransactionProcessor processor = new TransactionProcessor();
        processor = new LoggingProcessorDecorator(processor);
        processor = new CachingProcessorDecorator(processor);
        _processor = processor;
    }

    public void ProcessPayment(decimal amount, string accountNumber, string recipient)
    {
        if (!_accounts.TryGetValue(accountNumber, out var account))
            throw new ArgumentException("Счёт не найден");

        if (!account.Withdraw(amount))
            throw new InvalidOperationException("Недостаточно средств на счёте");

        var payment = new Payment(amount, recipient, new PercentageFeeStrategy(0.01m));
        _processor.ProcessTransaction(payment, () => account.Deposit(amount));
    }

    public void ProcessTransfer(decimal amount, string fromAccountNumber, string toAccountNumber)
    {
        if (!_accounts.TryGetValue(fromAccountNumber, out var fromAccount))
            throw new ArgumentException("Счёт отправителя не найден");

        if (!_accounts.TryGetValue(toAccountNumber, out var toAccount))
            throw new ArgumentException("Счёт получателя не найден");

        if (!fromAccount.Withdraw(amount))
            throw new InvalidOperationException("Недостаточно средств на счёте отправителя");

        var transfer = new Transfer(amount, fromAccountNumber, toAccountNumber, new FixedFeeStrategy(25));
        _processor.ProcessTransaction(transfer, () =>
        {
            fromAccount.Deposit(amount);
            toAccount.Withdraw(amount);
        });
        toAccount.Deposit(amount);
    }

    public void ProcessDeposit(decimal amount, string accountNumber)
    {
        if (!_accounts.TryGetValue(accountNumber, out var account))
            throw new ArgumentException("Счёт не найден");

        var deposit = new Deposit(amount, accountNumber, new FixedFeeStrategy(0));
        _processor.ProcessTransaction(deposit, () => account.Withdraw(amount));
        account.Deposit(amount);
    }

    public bool UndoLastTransaction()
    {
        return _processor.UndoLastTransaction();
    }
}

public class BankAccount
{
    public string AccountNumber { get; }
    public decimal Balance { get; private set; }
    public string Owner { get; }

    public BankAccount(string accountNumber, string owner, decimal initialBalance = 0)
    {
        AccountNumber = accountNumber;
        Owner = owner;
        Balance = initialBalance;
    }

    public void Deposit(decimal amount)
    {
        Balance += amount;
        Logger.Instance.Log($"Счёт {AccountNumber}: пополнение на {amount:N2}. Новый баланс: {Balance:N2}");
    }

    public bool Withdraw(decimal amount)
    {
        if (Balance >= amount)
        {
            Balance -= amount;
            Logger.Instance.Log($"Счёт {AccountNumber}: списание {amount:N2}. Новый баланс: {Balance:N2}");
            return true;
        }
        Logger.Instance.Log($"Ошибка: На счёте {AccountNumber} недостаточно средств");
        return false;
    }

    public override string ToString() =>
        $"Счёт {AccountNumber} ({Owner}) Баланс: {Balance:N2}";
}

class Program
{
    private static readonly Dictionary<string, BankAccount> _accounts = new Dictionary<string, BankAccount>();
    private static BankingSystem _bankSystem;

    static void Main()
    {
        CultureInfo.CurrentCulture = new CultureInfo("ru-RU");

        Logger.Instance.Log("=== Банковская система обработки транзакций ===");

        InitializeSampleData();
        _bankSystem = new BankingSystem(_accounts);

        while (true)
        {
            Console.WriteLine("\n=== Главное меню ===");
            Console.WriteLine("1. Управление счетами");
            Console.WriteLine("2. Операции с транзакциями");
            Console.WriteLine("3. Показать историю операций");
            Console.WriteLine("0. Выход");
            Console.Write("Выберите действие: ");

            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    ShowAccountManagementMenu();
                    break;

                case "2":
                    ShowTransactionMenu();
                    break;

                case "3":
                    ShowTransactionHistory();
                    break;

                case "0":
                    Logger.Instance.Log("Работа системы завершена");
                    return;

                default:
                    Logger.Instance.Log("Ошибка: Неверный ввод");
                    break;
            }
        }
    }

    private static void InitializeSampleData()
    {
        _accounts.Add("1001", new BankAccount("1001", "Иван Иванов", 50000));
        _accounts.Add("1002", new BankAccount("1002", "Петр Петров", 30000));
        _accounts.Add("1003", new BankAccount("1003", "Сидор Сидоров", 100000));
    }

    private static void ShowAccountManagementMenu()
    {
        while (true)
        {
            Console.WriteLine("\n=== Управление счетами ===");
            Console.WriteLine("1. Создать новый счёт");
            Console.WriteLine("2. Просмотреть все счета");
            Console.WriteLine("3. Найти счёт по номеру");
            Console.WriteLine("4. Пополнить счёт");
            Console.WriteLine("0. Назад");
            Console.Write("Выберите действие: ");

            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    CreateNewAccount();
                    break;

                case "2":
                    ShowAllAccounts();
                    break;

                case "3":
                    FindAccount();
                    break;

                case "4":
                    DepositToAccount();
                    break;

                case "0":
                    return;

                default:
                    Logger.Instance.Log("Ошибка: Неверный ввод");
                    break;
            }
        }
    }

    private static void ShowTransactionMenu()
    {
        while (true)
        {
            Console.WriteLine("\n=== Операции с транзакциями ===");
            Console.WriteLine("1. Создать платёж");
            Console.WriteLine("2. Создать перевод между счетами");
            Console.WriteLine("3. Отменить последнюю операцию");
            Console.WriteLine("0. Назад");
            Console.Write("Выберите действие: ");

            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    CreatePayment();
                    break;

                case "2":
                    CreateTransfer();
                    break;

                case "3":
                    if (!_bankSystem.UndoLastTransaction())
                    {
                        Logger.Instance.Log("Нет операций для отмены");
                    }
                    break;

                case "0":
                    return;

                default:
                    Logger.Instance.Log("Ошибка: Неверный ввод");
                    break;
            }
        }
    }

    private static void CreateNewAccount()
    {
        Console.Write("\nВведите номер счёта: ");
        var accountNumber = Console.ReadLine();

        if (_accounts.ContainsKey(accountNumber))
        {
            Logger.Instance.Log("Ошибка: Счёт с таким номером уже существует");
            return;
        }

        Console.Write("Введите имя владельца: ");
        var owner = Console.ReadLine();

        Console.Write("Введите начальный баланс: ");
        if (!decimal.TryParse(Console.ReadLine(), out var initialBalance) || initialBalance < 0)
        {
            Logger.Instance.Log("Ошибка: Некорректная сумма");
            return;
        }

        _accounts.Add(accountNumber, new BankAccount(accountNumber, owner, initialBalance));
        Logger.Instance.Log($"Счёт {accountNumber} для {owner} успешно создан");
    }

    private static void ShowAllAccounts()
    {
        Logger.Instance.Log("\n=== Список счетов ===");
        if (_accounts.Count == 0)
        {
            Logger.Instance.Log("Счета отсутствуют");
            return;
        }

        foreach (var account in _accounts.Values)
        {
            Logger.Instance.Log(account.ToString());
        }
    }

    private static void FindAccount()
    {
        Console.Write("\nВведите номер счёта: ");
        var accountNumber = Console.ReadLine();

        if (_accounts.TryGetValue(accountNumber, out var account))
        {
            Logger.Instance.Log(account.ToString());
        }
        else
        {
            Logger.Instance.Log("Счёт не найден");
        }
    }

    private static void DepositToAccount()
    {
        Console.Write("\nВведите номер счёта: ");
        var accountNumber = Console.ReadLine();

        if (!_accounts.TryGetValue(accountNumber, out var account))
        {
            Logger.Instance.Log("Ошибка: Счёт не найден");
            return;
        }

        Console.Write("Введите сумму пополнения: ");
        if (!decimal.TryParse(Console.ReadLine(), out var amount) || amount <= 0)
        {
            Logger.Instance.Log("Ошибка: Некорректная сумма");
            return;
        }

        try
        {
            _bankSystem.ProcessDeposit(amount, accountNumber);
            Logger.Instance.Log("Пополнение счёта выполнено успешно");
        }
        catch (Exception ex)
        {
            Logger.Instance.Log($"Ошибка: {ex.Message}");
        }
    }

    private static void CreatePayment()
    {
        Console.Write("\nВведите номер счёта для списания: ");
        var accountNumber = Console.ReadLine();

        Console.Write("Введите сумму платежа: ");
        if (!decimal.TryParse(Console.ReadLine(), out var amount) || amount <= 0)
        {
            Logger.Instance.Log("Ошибка: Некорректная сумма");
            return;
        }

        Console.Write("Введите получателя: ");
        var recipient = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(recipient))
        {
            Logger.Instance.Log("Ошибка: Получатель не может быть пустым");
            return;
        }

        try
        {
            _bankSystem.ProcessPayment(amount, accountNumber, recipient);
            Logger.Instance.Log("Платёж выполнен успешно");
        }
        catch (Exception ex)
        {
            Logger.Instance.Log($"Ошибка: {ex.Message}");
        }
    }

    private static void CreateTransfer()
    {
        Console.Write("\nВведите номер счёта отправителя: ");
        var fromAccountNumber = Console.ReadLine();

        Console.Write("Введите номер счёта получателя: ");
        var toAccountNumber = Console.ReadLine();

        Console.Write("Введите сумму перевода: ");
        if (!decimal.TryParse(Console.ReadLine(), out var amount) || amount <= 0)
        {
            Logger.Instance.Log("Ошибка: Некорректная сумма");
            return;
        }

        try
        {
            _bankSystem.ProcessTransfer(amount, fromAccountNumber, toAccountNumber);
            Logger.Instance.Log("Перевод выполнен успешно");
        }
        catch (Exception ex)
        {
            Logger.Instance.Log($"Ошибка: {ex.Message}");
        }
    }

    private static void ShowTransactionHistory()
    {
        Logger.Instance.Log("\n=== История операций ===");
        Logger.Instance.Log("Функция просмотра полной истории находится на ранней стадии разработки");
    }
}