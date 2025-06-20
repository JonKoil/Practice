SELECT DISTINCT T.Tname
FROM T, D, STD, S
WHERE STD.Tnum = T.Tnum AND STD.Dnum = D.Dnum AND STD.Snum = S.Snum
AND S.City = D.City;
GO
SELECT DISTINCT S.Sname
FROM S,STD
WHERE STD.Snum = S.Snum AND STD.Dnum = 'D1'
GO
SELECT DISTINCT S.City, D.City
FROM S, D, STD
WHERE STD.Snum = S.Snum AND STD.Dnum = D.Dnum;
GO
SELECT DISTINCT STD.Tnum
FROM STD, S
WHERE STD.Snum = S.Snum AND S.Status <> 50;