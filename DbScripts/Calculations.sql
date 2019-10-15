IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('Calculations'))
BEGIN
	CREATE TABLE [dbo].[Calculations]
	(
		[Id] BIGINT IDENTITY(1,1) NOT NULL,
		[UserId] BIGINT NOT NULL,
		[FirstNumber] VARCHAR(MAX) NOT NULL,
		[SecondNumber] VARCHAR(MAX) NOT NULL,
		[Summation] VARCHAR(MAX) NOT NULL,
		[CalculatedOn] DATETIME NOT NULL DEFAULT GETDATE(),
		CONSTRAINT PK_Calculations_Id PRIMARY KEY CLUSTERED (Id),
		CONSTRAINT FK_Calculations_Users_Id FOREIGN KEY(UserId) REFERENCES [dbo].[Users] (Id)
	)
END
GO
