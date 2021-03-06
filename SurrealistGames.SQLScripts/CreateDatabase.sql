use master
go

create database SurrealistGames
go

USE [SurrealistGames]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 1/25/2015 11:36:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 1/25/2015 11:36:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 1/25/2015 11:36:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 1/25/2015 11:36:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](128) NOT NULL,
	[RoleId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 1/25/2015 11:36:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) NOT NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO


create table Question
(
	[QuestionID] [int] identity(1, 1) primary key,
	[QuestionContent] varchar(300) not null,
	[ApprovingUserId] int null,
	[ApprovedOn] datetime null,
	[RemovingUserId] int null,
	[RemovedOn] datetime null
)

create table Answer
(
	[AnswerID] [int] identity(1, 1) primary key,
	[AnswerContent] varchar(300) not null,
	[ApprovingUserId] int null,
	[ApprovedOn] datetime null,
	[RemovingUserId] int null,
	[RemovedOn] datetime null
)

create table Report
(
	[ReportID] [int] identity(1, 1) primary key,
	[QuestionID] [int] null,
	[AnswerID] [int] null,
	[UserInfoId] [int] not null,
	[SubmittedOn] [datetime] default getdate()
)

create table RandomQuestion
(
	[RandomQuestionID] [int] not null primary key,
	[QuestionID] [int] not null,
)

create table RandomAnswer
(
	[RandomAnswerID] [int] not null primary key,
	[AnswerID] [int] not null
)	

create table UserInfo
(
	[UserInfoId] [int] not null Identity(1, 1) primary key,
	[Id] nvarchar(128) not null
)

create table SavedQuestionGameResult
(
	[SavedQuestionId] int Identity(1, 1) primary key,
	[QuestionId] int not null,
	[AnswerId] int not null,
	[UserInfoId] int not null
)

alter table Question
	add constraint FK_Question_ApprovingUserId_UserInfo_UserInfoId foreign key (ApprovingUserId)
	references UserInfo (UserInfoId)

alter table Question
	add constraint FK_Question_RemovingUserId_UserInfo_UserInfoId foreign key (RemovingUserId)
	references UserInfo (UserInfoId)

alter table Answer
	add constraint FK_Answer_ApprovingUserId_UserInfo_UserInfoId foreign key (ApprovingUserId)
	references UserInfo (UserInfoId)

alter table Answer
	add constraint FK_Answer_RemovingUserId_UserInfo_UserInfoId foreign key (RemovingUserId)
	references UserInfo (UserInfoId)

alter table Report
	add constraint FK_Report_QuestionID_Question_QuestionID foreign key (QuestionID)
	references Question (QuestionID)

alter table Report
	add constraint FK_Report_AnswerID_Answer_AnswerID foreign key (AnswerID)
	references Answer (AnswerID)

alter table Report
	add constraint FK_Report_UserId_User_UserID foreign key (UserInfoId)
	references UserInfo (UserInfoId)

alter table UserInfo
	add constraint FK_UserInfo_AspNetUsers foreign key (Id)
	references AspNetUsers (Id) 

alter table SavedQuestionGameResult
	add constraint FK_SavedQuestionGameResult_Question foreign key (QuestionId)
	references Question (QuestionId)

alter table SavedQuestionGameResult
	add constraint FK_SavedQuestionGameResult_Answer foreign key (AnswerId)
	references Answer (AnswerId)

alter table SavedQuestionGameResult
	add constraint FK_SavedQuestionGameResult_UserInfo foreign key (UserInfoId)
	references UserInfo (UserInfoId)
go

create procedure Question_GetRandom(
	@RandomQuestionId int
)
as
	select * from Question
	where QuestionId = (select QuestionId
								from RandomQuestion
								where RandomQuestionId = @RandomQuestionId)
go

create procedure Answer_GetRandom(
 @RandomAnswerId int
 )
as
	select * from Answer 
	where AnswerId = (select AnswerId 
								from RandomAnswer
								where RandomAnswerId = @RandomAnswerId)
go

create procedure RandomQuestion_MaxRandomId
as
	select Max(RandomQuestionId) as MaxRandomQuestionId from RandomQuestion;
go

create procedure RandomAnswer_MaxRandomId
as
	select Max(RandomAnswerId) as MaxRandomAnswerId from RandomAnswer;
go

create procedure Question_Insert
(
	@QuestionContent varchar(300)
) as
begin transaction
	insert into Question(QuestionContent)
	values (@QuestionContent);

	declare @NewId int
	set @NewId = SCOPE_IDENTITY()

	insert into RandomQuestion(QuestionID, RandomQuestionID)
	values (@NewId, (select Max(RandomQuestionID) + 1 
								from RandomQuestion ));
commit transaction

	select @NewId as QuestionId
go

create procedure Answer_Insert
(
	@AnswerContent varchar(300)
) as
begin transaction
	insert into Answer(AnswerContent)
	values (@AnswerContent);

	declare @NewId int
	set @NewId = SCOPE_IDENTITY()

	insert into RandomAnswer(AnswerID, RandomAnswerID)
	values(@NewId, (select Max(RandomAnswerID) + 1
								from RandomAnswer ));
commit transaction

	select @NewId as AnswerId
go


create procedure UserInfo_Insert
(
	@Id nvarchar(128)
)
as
begin transaction
	insert into UserInfo(Id)
	values (@Id)

	INSERT INTO [dbo].[AspNetUserRoles]
           ([UserId]
           ,[RoleId])
     VALUES
           (@Id
           ,'3') --automatically grant Report Role
commit transaction
go

create procedure SavedQuestion_Insert
(
	@QuestionId int,
	@AnswerId int,
	@UserInfoId int
)
as
declare @AlreadySaved bit

if not exists(select * from SavedQuestionGameResult 
							where QuestionId = @QuestionId
							and AnswerId = @QuestionId
							and UserInfoId = @UserInfoId)
begin
	begin transaction
		insert into SavedQuestionGameResult(QuestionId, AnswerId, UserInfoId)
		values (@QuestionId, @AnswerId, @UserInfoId)
	commit transaction
end
go

create procedure UserInfo_GetByAspId
(
	@AspId nvarchar(128)
)
as 
 select * from UserInfo where Id = @AspId
go

create procedure UserInfo_GetSavedQuestions
(
	@UserInfoId int
) 
as
	select sqgr.SavedQuestionId as SavedQuestionId, qp.QuestionContent as Question, qs.AnswerContent as Answer 
	from UserInfo ui
	inner join SavedQuestionGameResult sqgr on ui.UserInfoId = sqgr.UserInfoId
	inner join Question qp on sqgr.QuestionId = qp.QuestionID
	inner join Answer qs on sqgr.AnswerId = qs.AnswerID
	where ui.UserInfoId = @UserInfoId
go

create function UserOwnsResult(@UserInfoId int, @SavedQuestionId int)
RETURNS bit
as
begin
	declare @UserOwnsResult bit
	set @UserOwnsResult = 0
	if exists(select * from SavedQuestionGameResult
				where SavedQuestionId = @SavedQuestionId and UserInfoId = @UserInfoId)
	begin
		set @UserOwnsResult = 1
	end

	return(@UserOwnsResult)
end
go

create procedure SavedQuestionGameResult_Delete
(
	@SavedQuestionId int
)
as
	delete from SavedQuestionGameResult
		where SavedQuestionId = @SavedQuestionId
go

create procedure RandomQuestion_ResetIDsAfterDelete
as
	with randomRows as
	(
		select *, ROW_NUMBER() over(order by RandomQuestionID) as rowNumber 
		from RandomQuestion
	)

	update randomRows
	set RandomQuestionID = rowNumber
	where rowNumber != RandomQuestionID	
go

create procedure RandomAnswer_ResetIDsAfterDelete
as
	with randomRows as
	(
		select *, ROW_NUMBER() over(order by RandomAnswerID) as rowNumber 
		from RandomAnswer
	)

	update randomRows
	set RandomAnswerID = rowNumber
	where rowNumber != RandomAnswerID	
go

use SurrealistGames
go

INSERT INTO [dbo].[AspNetRoles]
           ([Id]
           ,[Name])
     VALUES
           ('1'
           ,'Admin'), ('2', 'Moderator'), ('3', 'Reporter')
GO

insert into Question(QuestionContent)
values 
		('What is the meaning of life?'),
		('What is the deepest thought man is capable of?'),
		('What is the best breakfast food?'),
		('What is man''s best friend?'),
		('What is the most important thing in the universe?');

insert into Answer(AnswerContent)
values
		('Nothing at all.'),
		('99 red balloons.'),
		('Poptarts.'),
		('Pain and suffering.'),
		('Mellow yellow.'),
		('Falling down 7 times, getting up 8.');

insert into RandomQuestion(RandomQuestionID, QuestionID)
values 
		(1, 1),
		(2, 2),
		(3, 3),
		(4, 4),
		(5, 5);

insert into RandomAnswer(RandomAnswerID, AnswerID)
values
		(1, 1),
		(2, 2),
		(3, 3),
		(4, 4),
		(5, 5),
		(6, 6);


INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'99ba7c1f-9091-443a-a9d1-78c0ac54b357', N'matt@example.com', 0, N'AHeA2/V7ZzW86cmaitaBcGbGGk8/wsg+jNsGEXOLXAqr//t/u9ToBgBehsrk4fEFUA==', N'26812078-4361-48e6-befa-e5b371fe9297', NULL, 0, 0, NULL, 0, 0, N'matt@example.com')
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'c69ab1cd-9eca-4dba-a01f-0e6e015d089e', N'mod@example.com', 0, N'AJKXUVqtImCw1x5Se1ki/ahuE9UjHtisLlm6d0xYhfGe1Il9F5Gg5SPl1UjLZJktWg==', N'62ae739f-34f2-40ba-822f-c26c284aaa84', NULL, 0, 0, NULL, 0, 0, N'mod@example.com')
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'ee9ec536-482c-4a08-8da0-7ffb6538ab1b', N'admin@example.com', 0, N'AJcxBd7DAbPq8mtQlZwQXZjHtNtYpy0thWGPfiTSelykWj8IHh2uB242/PC5HJ0L8Q==', N'05c2dc2b-d599-4402-9d33-a7ea65f47d01', NULL, 0, 0, NULL, 0, 0, N'admin@example.com')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'99ba7c1f-9091-443a-a9d1-78c0ac54b357', N'3')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'c69ab1cd-9eca-4dba-a01f-0e6e015d089e', N'3')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'ee9ec536-482c-4a08-8da0-7ffb6538ab1b', N'3')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'c69ab1cd-9eca-4dba-a01f-0e6e015d089e', N'2')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'ee9ec536-482c-4a08-8da0-7ffb6538ab1b', N'1')
GO
SET IDENTITY_INSERT [dbo].[UserInfo] ON 

GO
INSERT [dbo].[UserInfo] ([UserInfoId], [Id]) VALUES (1, N'99ba7c1f-9091-443a-a9d1-78c0ac54b357')
GO
INSERT [dbo].[UserInfo] ([UserInfoId], [Id]) VALUES (2, N'ee9ec536-482c-4a08-8da0-7ffb6538ab1b')
GO
INSERT [dbo].[UserInfo] ([UserInfoId], [Id]) VALUES (3, N'c69ab1cd-9eca-4dba-a01f-0e6e015d089e')
GO
SET IDENTITY_INSERT [dbo].[UserInfo] OFF
GO

declare @UserInfoId int
select @UserInfoId = UserInfoId 
	from UserInfo
	where UserInfo.Id = '99ba7c1f-9091-443a-a9d1-78c0ac54b357'

insert into SavedQuestionGameResult (UserInfoId, QuestionId, AnswerId)
values (@UserInfoId, 2, 1),
       (@UserInfoId, 5, 3),
       (@UserInfoId, 2, 4)

