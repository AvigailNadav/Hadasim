CREATE DATABASE FamilyTree;
USE FamilyTree;

CREATE TABLE PeopleInfo(
	Person_Id Int PRIMARY KEY,
	Personal_Name VARCHAR(20),
	Family_Name VARCHAR(20),
	Gender VARCHAR(10),
	CONSTRAINT check_gender CHECK(Gender IN('male','female')),
	Father_Id Int,
	Mother_Id Int,
	Spouse_Id Int
	)
	INSERT INTO PeopleInfo(Person_Id,Personal_Name,Family_Name,Gender,Father_Id,Mother_Id,Spouse_Id)VALUES
	(1,'Ariel','Cohen','male',NULL,NULL,2),
	(2,'Michal','Cohen','female',NULL,NULL,NULL),
	(3,'Rachel','Cohen','female',1,2,NULL),
	(4, 'Miriam', 'Levi', 'female', 1, 2, 5),
    (5, 'Daniel', 'Cohen', 'male', NULL, NULL, NULL),
	(6, 'Tamar', 'Cohen', 'female', NULL, NULL, NULL),
    (7, 'Noa', 'Cohen', 'female', 5, 4, NULL),
    (8, 'Eitan', 'Levi', 'male', 5, 4, NULL);
	
	CREATE TABLE FamilyTree(
	Person_Id INT,
	Relative_Id INT, 
	Connection_Type VARCHAR(20),
	CONSTRAINT check_connection CHECK(Connection_Type IN('father','mother','brother','sister','son','daughter','spouse')),
	PRIMARY KEY (Person_Id,Relative_Id),
	FOREIGN KEY (Person_Id) REFERENCES PeopleInfo(Person_Id),
	FOREIGN KEY (Relative_Id) REFERENCES PeopleInfo(Person_Id)
	)

	INSERT INTO FamilyTree(Person_Id, Relative_Id,Connection_Type)

	SELECT Person_Id,Father_Id,'father'
	FROM PeopleInfo
	WHERE Father_Id IS NOT NULL

	UNION ALL 

	SELECT Person_Id,Mother_Id,'mother'
	FROM PeopleInfo
	WHERE Mother_Id IS NOT NULL

	UNION ALL

	SELECT Person_Id, Spouse_Id,'spouse'
	FROM PeopleInfo
	WHERE Spouse_Id IS NOT NULL

	UNION ALL

	SELECT Father_Id,Person_Id, 
		CASE Gender WHEN 'male' THEN 'son' ELSE 'doughter'
		END
	FROM PeopleInfo 
	WHERE Father_Id IS NOT NULL

	UNION ALL 

	SELECT Mother_Id,Person_Id, 
		CASE Gender WHEN 'male' THEN 'son' ELSE 'doughter'
		END
	FROM PeopleInfo 
	WHERE Mother_Id IS NOT NULL

	INSERT INTO FamilyTree(Person_Id, Relative_Id,Connection_Type)
	SELECT p1.Person_Id,p2.Person_Id,
		CASE p2.Gender WHEN 'male' THEN 'brother' ELSE 'sister' END
		FROM PeopleInfo p1
		JOIN PeopleInfo p2
			ON p1.Person_id<>p2.Person_Id
			AND p1.Father_Id=p2.Father_Id
			AND p1.Mother_Id=p2.Mother_id
		WHERE p1.Father_Id IS NOT NULL AND p1.Mother_Id IS NOT NULL
--הוספה לטבלה של כל האנשים
UPDATE P2
SET P2.Spouse_Id = P1.Person_Id
FROM PeopleInfo AS P1
JOIN PeopleInfo AS P2 ON P1.Spouse_Id = P2.Person_Id
WHERE P1.Spouse_Id IS NOT NULL 
  AND P2.Spouse_Id IS NULL;


--הוספה לטבלה של העץ משפחה, רשומה של הבן זוג
  INSERT INTO FamilyTree (Person_Id, Relative_Id, Connection_Type)
SELECT P1.Person_Id, P1.Spouse_Id, 'spouse'
FROM PeopleInfo AS P1
WHERE P1.Spouse_Id IS NOT NULL
  AND NOT EXISTS (
      SELECT 1
      FROM FamilyTree AS FT
      WHERE FT.Person_Id = P1.Person_Id
        AND FT.Relative_Id = P1.Spouse_Id
        AND FT.Connection_Type = 'spouse'
  );

-- הוספת הקשרים ההפוכים
INSERT INTO FamilyTree (Person_Id, Relative_Id, Connection_Type)
SELECT P1.Spouse_Id, P1.Person_Id, 'spouse'
FROM PeopleInfo AS P1
WHERE P1.Spouse_Id IS NOT NULL
  AND NOT EXISTS (
      SELECT 1
      FROM FamilyTree AS FT
      WHERE FT.Person_Id = P1.Spouse_Id
        AND FT.Relative_Id = P1.Person_Id
        AND FT.Connection_Type = 'spouse'
  );