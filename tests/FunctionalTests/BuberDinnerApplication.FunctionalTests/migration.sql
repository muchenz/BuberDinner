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
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20230109212859_InitialCreate'
)
BEGIN
    CREATE TABLE [Menus] (
        [Id] uniqueidentifier NOT NULL,
        [Name] nvarchar(100) NOT NULL,
        [Description] nvarchar(100) NOT NULL,
        [AverageRating_Value] float NOT NULL,
        [AverageRating_NumRatings] int NOT NULL,
        [HostId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_Menus] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20230109212859_InitialCreate'
)
BEGIN
    CREATE TABLE [MenuIds] (
        [Id] int NOT NULL IDENTITY,
        [DinnerId] uniqueidentifier NOT NULL,
        [MenuId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_MenuIds] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_MenuIds_Menus_MenuId] FOREIGN KEY ([MenuId]) REFERENCES [Menus] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20230109212859_InitialCreate'
)
BEGIN
    CREATE TABLE [MenuReviewIds] (
        [Id] int NOT NULL IDENTITY,
        [ReviewId] uniqueidentifier NOT NULL,
        [MenuId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_MenuReviewIds] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_MenuReviewIds_Menus_MenuId] FOREIGN KEY ([MenuId]) REFERENCES [Menus] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20230109212859_InitialCreate'
)
BEGIN
    CREATE TABLE [MenuSections] (
        [MenuSectionId] uniqueidentifier NOT NULL,
        [MenuId] uniqueidentifier NOT NULL,
        [Name] nvarchar(100) NOT NULL,
        [Description] nvarchar(100) NOT NULL,
        CONSTRAINT [PK_MenuSections] PRIMARY KEY ([MenuSectionId], [MenuId]),
        CONSTRAINT [FK_MenuSections_Menus_MenuId] FOREIGN KEY ([MenuId]) REFERENCES [Menus] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20230109212859_InitialCreate'
)
BEGIN
    CREATE TABLE [MenuItems] (
        [MenuItemId] uniqueidentifier NOT NULL,
        [MenuSectionId] uniqueidentifier NOT NULL,
        [MenuId] uniqueidentifier NOT NULL,
        [Name] nvarchar(100) NOT NULL,
        [Description] nvarchar(100) NOT NULL,
        CONSTRAINT [PK_MenuItems] PRIMARY KEY ([MenuItemId], [MenuSectionId], [MenuId]),
        CONSTRAINT [FK_MenuItems_MenuSections_MenuSectionId_MenuId] FOREIGN KEY ([MenuSectionId], [MenuId]) REFERENCES [MenuSections] ([MenuSectionId], [MenuId]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20230109212859_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_MenuIds_MenuId] ON [MenuIds] ([MenuId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20230109212859_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_MenuItems_MenuSectionId_MenuId] ON [MenuItems] ([MenuSectionId], [MenuId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20230109212859_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_MenuReviewIds_MenuId] ON [MenuReviewIds] ([MenuId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20230109212859_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_MenuSections_MenuId] ON [MenuSections] ([MenuId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20230109212859_InitialCreate'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230109212859_InitialCreate', N'8.0.11');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20230430222014_After_MenuReview_Menu_Host_Guest_Dinner_Bill_User'
)
BEGIN
    DROP TABLE [MenuIds];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20230430222014_After_MenuReview_Menu_Host_Guest_Dinner_Bill_User'
)
BEGIN
    CREATE TABLE [Bills] (
        [Id] uniqueidentifier NOT NULL,
        [DinnerId] uniqueidentifier NOT NULL,
        [GuestId] uniqueidentifier NOT NULL,
        [HostId] uniqueidentifier NOT NULL,
        [Price_Amount] decimal(18,2) NOT NULL,
        [Price_Currency] int NOT NULL,
        CONSTRAINT [PK_Bills] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20230430222014_After_MenuReview_Menu_Host_Guest_Dinner_Bill_User'
)
BEGIN
    CREATE TABLE [Dinners] (
        [Id] uniqueidentifier NOT NULL,
        [Name] nvarchar(100) NOT NULL,
        [Description] nvarchar(100) NOT NULL,
        [StartDatetime] datetime2 NOT NULL,
        [EndDatetime] datetime2 NOT NULL,
        [StartedDatetime] datetime2 NOT NULL,
        [EndedDatetime] datetime2 NOT NULL,
        [Status] int NOT NULL,
        [IsPublic] bit NOT NULL,
        [MaxGests] int NOT NULL,
        [Price_Amount] decimal(18,2) NOT NULL,
        [Price_Currency] int NOT NULL,
        [HostId] uniqueidentifier NOT NULL,
        [MenuId] uniqueidentifier NOT NULL,
        [ImageUrl] nvarchar(max) NOT NULL,
        [Location_Name] nvarchar(max) NOT NULL,
        [Location_Address] nvarchar(max) NOT NULL,
        [Location_Latitude] float NOT NULL,
        [Location_Longitude] float NOT NULL,
        CONSTRAINT [PK_Dinners] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20230430222014_After_MenuReview_Menu_Host_Guest_Dinner_Bill_User'
)
BEGIN
    CREATE TABLE [Guests] (
        [Id] uniqueidentifier NOT NULL,
        [FirstName] nvarchar(100) NOT NULL,
        [LastName] nvarchar(100) NOT NULL,
        [ProfileImage] nvarchar(100) NOT NULL,
        [AverageRating_Value] float NOT NULL,
        [AverageRating_NumRatings] int NOT NULL,
        CONSTRAINT [PK_Guests] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20230430222014_After_MenuReview_Menu_Host_Guest_Dinner_Bill_User'
)
BEGIN
    CREATE TABLE [Hosts] (
        [Id] uniqueidentifier NOT NULL,
        [FirstName] nvarchar(100) NOT NULL,
        [LastName] nvarchar(100) NOT NULL,
        [ProfileImage] nvarchar(100) NOT NULL,
        [AverageRating_Value] float NOT NULL,
        [AverageRating_NumRatings] int NOT NULL,
        [UserId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_Hosts] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20230430222014_After_MenuReview_Menu_Host_Guest_Dinner_Bill_User'
)
BEGIN
    CREATE TABLE [MenuDinnerIds] (
        [Id] int NOT NULL IDENTITY,
        [DinnerId] uniqueidentifier NOT NULL,
        [MenuId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_MenuDinnerIds] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_MenuDinnerIds_Menus_MenuId] FOREIGN KEY ([MenuId]) REFERENCES [Menus] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20230430222014_After_MenuReview_Menu_Host_Guest_Dinner_Bill_User'
)
BEGIN
    CREATE TABLE [MenuReviews] (
        [Id] uniqueidentifier NOT NULL,
        [Rating_Value] float NOT NULL,
        [Comment] nvarchar(200) NOT NULL,
        [HostId] uniqueidentifier NOT NULL,
        [MenuId] uniqueidentifier NOT NULL,
        [GuestId] uniqueidentifier NOT NULL,
        [DinnerId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_MenuReviews] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20230430222014_After_MenuReview_Menu_Host_Guest_Dinner_Bill_User'
)
BEGIN
    CREATE TABLE [Users] (
        [Id] uniqueidentifier NOT NULL,
        [FirstName] nvarchar(100) NOT NULL,
        [LastName] nvarchar(100) NOT NULL,
        [Email] nvarchar(100) NOT NULL,
        [Password] nvarchar(100) NOT NULL,
        CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20230430222014_After_MenuReview_Menu_Host_Guest_Dinner_Bill_User'
)
BEGIN
    CREATE TABLE [Reservations] (
        [ReservationId] uniqueidentifier NOT NULL,
        [DinnerId] uniqueidentifier NOT NULL,
        [GuestCount] int NOT NULL,
        [ReservationStatus] int NOT NULL,
        [GuestId] uniqueidentifier NOT NULL,
        [BillId] uniqueidentifier NOT NULL,
        [ArrivalDateTime] datetime2 NOT NULL,
        [CreatedDateTime] datetime2 NOT NULL,
        [UpdatedDateTime] datetime2 NOT NULL,
        CONSTRAINT [PK_Reservations] PRIMARY KEY ([ReservationId], [DinnerId]),
        CONSTRAINT [FK_Reservations_Dinners_DinnerId] FOREIGN KEY ([DinnerId]) REFERENCES [Dinners] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20230430222014_After_MenuReview_Menu_Host_Guest_Dinner_Bill_User'
)
BEGIN
    CREATE TABLE [DinnerRatings] (
        [RatingId] uniqueidentifier NOT NULL,
        [GuestId] uniqueidentifier NOT NULL,
        [HostId] uniqueidentifier NOT NULL,
        [DinnerId] uniqueidentifier NOT NULL,
        [Rating_Value] float NOT NULL,
        CONSTRAINT [PK_DinnerRatings] PRIMARY KEY ([RatingId], [GuestId]),
        CONSTRAINT [FK_DinnerRatings_Guests_GuestId] FOREIGN KEY ([GuestId]) REFERENCES [Guests] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20230430222014_After_MenuReview_Menu_Host_Guest_Dinner_Bill_User'
)
BEGIN
    CREATE TABLE [GuestBillIds] (
        [Id] int NOT NULL IDENTITY,
        [BillId] uniqueidentifier NOT NULL,
        [GuestId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_GuestBillIds] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_GuestBillIds_Guests_GuestId] FOREIGN KEY ([GuestId]) REFERENCES [Guests] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20230430222014_After_MenuReview_Menu_Host_Guest_Dinner_Bill_User'
)
BEGIN
    CREATE TABLE [GuestMenuReviewIds] (
        [Id] int NOT NULL IDENTITY,
        [MenuReviewId] uniqueidentifier NOT NULL,
        [GuestId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_GuestMenuReviewIds] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_GuestMenuReviewIds_Guests_GuestId] FOREIGN KEY ([GuestId]) REFERENCES [Guests] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20230430222014_After_MenuReview_Menu_Host_Guest_Dinner_Bill_User'
)
BEGIN
    CREATE TABLE [GuestPastDinnerIds] (
        [Id] int NOT NULL IDENTITY,
        [PastDinnerId] uniqueidentifier NOT NULL,
        [GuestId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_GuestPastDinnerIds] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_GuestPastDinnerIds_Guests_GuestId] FOREIGN KEY ([GuestId]) REFERENCES [Guests] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20230430222014_After_MenuReview_Menu_Host_Guest_Dinner_Bill_User'
)
BEGIN
    CREATE TABLE [GuestPendingDinnerIds] (
        [Id] int NOT NULL IDENTITY,
        [PendingDinnerId] uniqueidentifier NOT NULL,
        [GuestId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_GuestPendingDinnerIds] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_GuestPendingDinnerIds_Guests_GuestId] FOREIGN KEY ([GuestId]) REFERENCES [Guests] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20230430222014_After_MenuReview_Menu_Host_Guest_Dinner_Bill_User'
)
BEGIN
    CREATE TABLE [GuestUpcomingDinnerIds] (
        [Id] int NOT NULL IDENTITY,
        [UpcomingDinnerId] uniqueidentifier NOT NULL,
        [GuestId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_GuestUpcomingDinnerIds] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_GuestUpcomingDinnerIds_Guests_GuestId] FOREIGN KEY ([GuestId]) REFERENCES [Guests] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20230430222014_After_MenuReview_Menu_Host_Guest_Dinner_Bill_User'
)
BEGIN
    CREATE TABLE [GuestUserIds] (
        [Id] int NOT NULL IDENTITY,
        [UserId] uniqueidentifier NOT NULL,
        [GuestId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_GuestUserIds] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_GuestUserIds_Guests_GuestId] FOREIGN KEY ([GuestId]) REFERENCES [Guests] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20230430222014_After_MenuReview_Menu_Host_Guest_Dinner_Bill_User'
)
BEGIN
    CREATE TABLE [HostDinnerIds] (
        [Id] int NOT NULL IDENTITY,
        [DinnerId] uniqueidentifier NOT NULL,
        [HostId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_HostDinnerIds] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_HostDinnerIds_Hosts_HostId] FOREIGN KEY ([HostId]) REFERENCES [Hosts] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20230430222014_After_MenuReview_Menu_Host_Guest_Dinner_Bill_User'
)
BEGIN
    CREATE TABLE [HostMenuIds] (
        [Id] int NOT NULL IDENTITY,
        [MenuId] uniqueidentifier NOT NULL,
        [HostId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_HostMenuIds] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_HostMenuIds_Hosts_HostId] FOREIGN KEY ([HostId]) REFERENCES [Hosts] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20230430222014_After_MenuReview_Menu_Host_Guest_Dinner_Bill_User'
)
BEGIN
    CREATE INDEX [IX_DinnerRatings_GuestId] ON [DinnerRatings] ([GuestId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20230430222014_After_MenuReview_Menu_Host_Guest_Dinner_Bill_User'
)
BEGIN
    CREATE INDEX [IX_GuestBillIds_GuestId] ON [GuestBillIds] ([GuestId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20230430222014_After_MenuReview_Menu_Host_Guest_Dinner_Bill_User'
)
BEGIN
    CREATE INDEX [IX_GuestMenuReviewIds_GuestId] ON [GuestMenuReviewIds] ([GuestId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20230430222014_After_MenuReview_Menu_Host_Guest_Dinner_Bill_User'
)
BEGIN
    CREATE INDEX [IX_GuestPastDinnerIds_GuestId] ON [GuestPastDinnerIds] ([GuestId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20230430222014_After_MenuReview_Menu_Host_Guest_Dinner_Bill_User'
)
BEGIN
    CREATE INDEX [IX_GuestPendingDinnerIds_GuestId] ON [GuestPendingDinnerIds] ([GuestId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20230430222014_After_MenuReview_Menu_Host_Guest_Dinner_Bill_User'
)
BEGIN
    CREATE INDEX [IX_GuestUpcomingDinnerIds_GuestId] ON [GuestUpcomingDinnerIds] ([GuestId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20230430222014_After_MenuReview_Menu_Host_Guest_Dinner_Bill_User'
)
BEGIN
    CREATE INDEX [IX_GuestUserIds_GuestId] ON [GuestUserIds] ([GuestId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20230430222014_After_MenuReview_Menu_Host_Guest_Dinner_Bill_User'
)
BEGIN
    CREATE INDEX [IX_HostDinnerIds_HostId] ON [HostDinnerIds] ([HostId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20230430222014_After_MenuReview_Menu_Host_Guest_Dinner_Bill_User'
)
BEGIN
    CREATE INDEX [IX_HostMenuIds_HostId] ON [HostMenuIds] ([HostId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20230430222014_After_MenuReview_Menu_Host_Guest_Dinner_Bill_User'
)
BEGIN
    CREATE INDEX [IX_MenuDinnerIds_MenuId] ON [MenuDinnerIds] ([MenuId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20230430222014_After_MenuReview_Menu_Host_Guest_Dinner_Bill_User'
)
BEGIN
    CREATE INDEX [IX_Reservations_DinnerId] ON [Reservations] ([DinnerId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20230430222014_After_MenuReview_Menu_Host_Guest_Dinner_Bill_User'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230430222014_After_MenuReview_Menu_Host_Guest_Dinner_Bill_User', N'8.0.11');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231230102755_Outbox'
)
BEGIN
    CREATE TABLE [OutboxMessages] (
        [Id] uniqueidentifier NOT NULL,
        [Type] nvarchar(max) NOT NULL,
        [Content] nvarchar(max) NOT NULL,
        [OccureredOnUtc] datetime2 NOT NULL,
        [ProcessedOnUtc] datetime2 NULL,
        [Error] nvarchar(max) NULL,
        CONSTRAINT [PK_OutboxMessages] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231230102755_Outbox'
)
BEGIN
    CREATE INDEX [IX_OutboxMessages_ProcessedOnUtc] ON [OutboxMessages] ([ProcessedOnUtc]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20231230102755_Outbox'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20231230102755_Outbox', N'8.0.11');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240622000827_SoftDelete'
)
BEGIN
    ALTER TABLE [Menus] ADD [DeletedOnTime] datetime2 NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240622000827_SoftDelete'
)
BEGIN
    ALTER TABLE [Menus] ADD [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240622000827_SoftDelete'
)
BEGIN
    EXEC(N'CREATE INDEX [IX_Menus_IsDeleted] ON [Menus] ([IsDeleted]) WHERE [IsDeleted] = 0');
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240622000827_SoftDelete'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240622000827_SoftDelete', N'8.0.11');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240813134322_Add_AditEntry_forAuditInterceptor'
)
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Menus]') AND [c].[name] = N'DeletedOnTime');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Menus] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [Menus] DROP COLUMN [DeletedOnTime];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240813134322_Add_AditEntry_forAuditInterceptor'
)
BEGIN
    CREATE TABLE [AuditEntries] (
        [Id] uniqueidentifier NOT NULL,
        [MetaData] nvarchar(max) NOT NULL,
        [StartTimeUtc] datetime2 NOT NULL,
        [EndTimeUtc] datetime2 NOT NULL,
        [Succeeded] bit NOT NULL,
        [ErrorMesage] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_AuditEntries] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240813134322_Add_AditEntry_forAuditInterceptor'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240813134322_Add_AditEntry_forAuditInterceptor', N'8.0.11');
END;
GO

COMMIT;
GO

