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

CREATE TABLE FamilyTree(
	Person_Id INT,
	Relative_Id INT, 
	Connection_Type VARCHAR(20),
	CONSTRAINT check_connection CHECK(Connection_Type IN('father','mother','brother','sister','son','doughter','spouse')),
	PRIMARY KEY (Person_Id,Relative_Id),
	FOREIGN KEY (Person_Id) REFERENCES PeopleInfo(Person_Id),
	FOREIGN KEY (Relative_Id) REFERENCES PeopleInfo(Person_Id)
	)

