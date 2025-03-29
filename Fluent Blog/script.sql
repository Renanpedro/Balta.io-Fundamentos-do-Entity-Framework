IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
CREATE TABLE [Category] (
    [Id] int NOT NULL IDENTITY,
    [Name] NVARCHAR(80) NOT NULL,
    [Slug] VARCHAR(80) NOT NULL,
    CONSTRAINT [PK_Category] PRIMARY KEY ([Id])
);

CREATE TABLE [Role] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Slug] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Role] PRIMARY KEY ([Id])
);

CREATE TABLE [Tag] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Slug] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Tag] PRIMARY KEY ([Id])
);

CREATE TABLE [User] (
    [Id] int NOT NULL IDENTITY,
    [Name] NVARCHAR(80) NOT NULL,
    [Email] nvarchar(max) NOT NULL,
    [PasswordHash] nvarchar(max) NOT NULL,
    [Bio] nvarchar(max) NOT NULL,
    [Image] nvarchar(max) NOT NULL,
    [Slug] VARCHAR(80) NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY ([Id])
);

CREATE TABLE [Post] (
    [Id] int NOT NULL IDENTITY,
    [Title] nvarchar(max) NOT NULL,
    [Summary] nvarchar(max) NOT NULL,
    [Body] nvarchar(max) NOT NULL,
    [Slug] nvarchar(450) NOT NULL,
    [CreateDate] datetime2 NOT NULL,
    [LastUpdateDate] SMALLDATETIME NOT NULL DEFAULT (GETDATE()),
    [CategoryId] int NOT NULL,
    [AuthorId] int NOT NULL,
    CONSTRAINT [PK_Post] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Post_Author] FOREIGN KEY ([AuthorId]) REFERENCES [User] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Post_Category] FOREIGN KEY ([CategoryId]) REFERENCES [Category] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [UserRole] (
    [RoleId] int NOT NULL,
    [UserId] int NOT NULL,
    CONSTRAINT [PK_UserRole] PRIMARY KEY ([RoleId], [UserId]),
    CONSTRAINT [FK_UserRole_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Role] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_UserRole_UserId] FOREIGN KEY ([UserId]) REFERENCES [User] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [PostTag] (
    [PostId] int NOT NULL,
    [TagId] int NOT NULL,
    CONSTRAINT [PK_PostTag] PRIMARY KEY ([PostId], [TagId]),
    CONSTRAINT [FK_PostTag_PostId] FOREIGN KEY ([PostId]) REFERENCES [Tag] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_PostTag_TagId] FOREIGN KEY ([TagId]) REFERENCES [Post] ([Id]) ON DELETE CASCADE
);

CREATE UNIQUE INDEX [IX_Category_Slug] ON [Category] ([Slug]);

CREATE INDEX [IX_Post_AuthorId] ON [Post] ([AuthorId]);

CREATE INDEX [IX_Post_CategoryId] ON [Post] ([CategoryId]);

CREATE UNIQUE INDEX [IX_Post_Slug] ON [Post] ([Slug]);

CREATE INDEX [IX_PostTag_TagId] ON [PostTag] ([TagId]);

CREATE UNIQUE INDEX [IX_User_Slug] ON [User] ([Slug]);

CREATE INDEX [IX_UserRole_UserId] ON [UserRole] ([UserId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250328233312_InitialCreation', N'9.0.3');

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250328234205_Migration2', N'9.0.3');

COMMIT;
GO

