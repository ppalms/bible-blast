IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190226013206_InitialMigration')
BEGIN
    CREATE TABLE [Award] (
        [AwardID] int NOT NULL,
        [Title] varchar(50) NOT NULL,
        [Description] varchar(255) NULL,
        [Cost] decimal(8, 2) NOT NULL,
        CONSTRAINT [PK_Award] PRIMARY KEY ([AwardID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190226013206_InitialMigration')
BEGIN
    CREATE TABLE [AwardQuestion] (
        [AwardID] int NOT NULL,
        [QuestionID] int NOT NULL,
        CONSTRAINT [PK_AwardQuestion] PRIMARY KEY ([AwardID], [QuestionID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190226013206_InitialMigration')
BEGIN
    CREATE TABLE [Category] (
        [CategoryID] int NOT NULL,
        [Name] varchar(50) NOT NULL,
        CONSTRAINT [PK_Category] PRIMARY KEY ([CategoryID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190226013206_InitialMigration')
BEGIN
    CREATE TABLE [Family] (
        [FamilyID] int NOT NULL IDENTITY,
        [DadName] varchar(50) NULL,
        [MomName] varchar(50) NULL,
        [DadCell] varchar(10) NULL,
        [MomCell] varchar(10) NULL,
        [HomePhone] varchar(50) NULL,
        [Address1] varchar(50) NULL,
        [Address2] varchar(50) NULL,
        [City] varchar(50) NULL,
        [State] varchar(50) NULL,
        [Zip] varchar(50) NULL,
        [NonParentName] varchar(50) NULL,
        [EmergencyPhone] varchar(50) NULL,
        [Email] varchar(125) NULL,
        [IsActive] bit NULL,
        CONSTRAINT [PK_Family] PRIMARY KEY NONCLUSTERED ([FamilyID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190226013206_InitialMigration')
BEGIN
    CREATE TABLE [Payment] (
        [PaymentID] int NOT NULL,
        [FamilyID] int NOT NULL,
        [Date] smalldatetime NOT NULL,
        [Ammount] decimal(8, 2) NOT NULL,
        CONSTRAINT [PK_Payment] PRIMARY KEY ([PaymentID], [FamilyID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190226013206_InitialMigration')
BEGIN
    CREATE TABLE [QuestionAnswer] (
        [KidID] int NOT NULL,
        [QuestionID] int NOT NULL,
        [Date] smalldatetime NOT NULL,
        [SubmittedBy] varchar(50) NULL,
        CONSTRAINT [PK_QuestionAnswer] PRIMARY KEY ([KidID], [QuestionID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190226013206_InitialMigration')
BEGIN
    CREATE TABLE [QuizScore] (
        [KidID] int NOT NULL,
        [Date] smalldatetime NOT NULL,
        [Points] tinyint NOT NULL,
        CONSTRAINT [PK_QuizScore] PRIMARY KEY ([KidID], [Date])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190226013206_InitialMigration')
BEGIN
    CREATE TABLE [Question] (
        [QuestionID] int NOT NULL,
        [CategoryID] int NOT NULL,
        [Title] varchar(50) NOT NULL,
        [Description] varchar(255) NULL,
        [Points] tinyint NOT NULL,
        CONSTRAINT [PK_Question] PRIMARY KEY ([QuestionID]),
        CONSTRAINT [FK_Question_Category] FOREIGN KEY ([CategoryID]) REFERENCES [Category] ([CategoryID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190226013206_InitialMigration')
BEGIN
    CREATE TABLE [Kid] (
        [KidID] int NOT NULL IDENTITY,
        [FirstName] varchar(50) NOT NULL,
        [LastName] varchar(50) NOT NULL,
        [Birthday] smalldatetime NULL,
        [IsActive] bit NULL,
        [DateRegistered] smalldatetime NULL,
        [FamilyID] int NOT NULL,
        [IsMale] bit NOT NULL,
        CONSTRAINT [PK_Kid] PRIMARY KEY NONCLUSTERED ([KidID]),
        CONSTRAINT [FK_Kid_Family] FOREIGN KEY ([FamilyID]) REFERENCES [Family] ([FamilyID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190226013206_InitialMigration')
BEGIN
    CREATE CLUSTERED INDEX [FamiyIDX] ON [Family] ([FamilyID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190226013206_InitialMigration')
BEGIN
    CREATE INDEX [IX_Kid_FamilyID] ON [Kid] ([FamilyID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190226013206_InitialMigration')
BEGIN
    CREATE CLUSTERED INDEX [KidIDX] ON [Kid] ([KidID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190226013206_InitialMigration')
BEGIN
    CREATE INDEX [IX_Question_CategoryID] ON [Question] ([CategoryID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190226013206_InitialMigration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190226013206_InitialMigration', N'2.2.1-servicing-10028');
END;

GO

