CREATE DATABASE NoteTaker

CREATE TABLE Processes
(
	Id INT PRIMARY KEY IDENTITY
	,[Name] VARCHAR(50) NOT NULL
)
CREATE TABLE Notes
(
	Id INT PRIMARY KEY IDENTITY
	,Title NVARCHAR(100) NOT NULL
	,[Description] NVARCHAR(500) NOT NULL
	,Process INT NOT NULL 
	,CONSTRAINT FK_Notes_Processes FOREIGN KEY (Process) REFERENCES Processes(Id)
)

INSERT INTO Processes([Name]) VALUES
('New')
,('Waiting')
,('Running')
,('Done')

