use SurrealistGames
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


create table QuestionPrefix
(
	[QuestionPrefixID] [int] identity(1, 1) primary key,
	[QuestionPrefixContent] varchar(300) not null
)

create table QuestionSuffix
(
	[QuestionSuffixID] [int] identity(1, 1) primary key,
	[QuestionSuffixContent] varchar(300) not null
)

create table RandomQuestionPrefix
(
	[RandomQuestionPrefixID] [int] not null primary key,
	[QuestionPrefixID] [int] null,
)

create table RandomQuestionSuffix
(
	[RandomQuestionSuffixID] [int] not null primary key,
	[QuestionSuffixID] [int] null
)	

create table UserInfo
(
	[UserInfoId] [int] not null Identity(1, 1) primary key,
	[Id] nvarchar(128) not null
)

create table SavedQuestionGameResult
(
	[SavedQuestionId] int Identity(1, 1) primary key,
	[QuestionPrefixId] int not null,
	[QuestionSuffixId] int not null,
	[UserInfoId] int not null
)

alter table UserInfo
	add constraint FK_UserInfo_AspNetUsers foreign key (Id)
	references AspNetUsers (Id) 

alter table SavedQuestionGameResult
	add constraint FK_SavedQuestionGameResult_QuestionPrefix foreign key (QuestionPrefixId)
	references QuestionPrefix (QuestionPrefixId)

alter table SavedQuestionGameResult
	add constraint FK_SavedQuestionGameResult_QuestionSuffix foreign key (QuestionSuffixId)
	references QuestionSuffix (QuestionSuffixId)

alter table SavedQuestionGameResult
	add constraint FK_SavedQuestionGameResult_UserInfo foreign key (UserInfoId)
	references UserInfo (UserInfoId)
go

create procedure QuestionPrefix_GetRandom(
	@RandomQuestionPrefixId int
)
as
	select * from QuestionPrefix
	where QuestionPrefixId = (select QuestionPrefixId
								from RandomQuestionPrefix
								where RandomQuestionPrefixId = @RandomQuestionPrefixId)
go

create procedure QuestionSuffix_GetRandom(
 @RandomQuestionSuffixId int
 )
as
	select * from QuestionSuffix 
	where QuestionSuffixId = (select QuestionSuffixId 
								from RandomQuestionSuffix
								where RandomQuestionSuffixId = @RandomQuestionSuffixId)
go

create procedure RandomQuestionPrefix_MaxRandomId
as
	select Max(RandomQuestionPrefixId) as MaxRandomQuestionPrefixId from RandomQuestionPrefix;
go

create procedure RandomQuestionSuffix_MaxRandomId
as
	select Max(RandomQuestionSuffixId) as MaxRandomQuestionSuffixId from RandomQuestionSuffix;
go

create procedure QuestionPrefix_Insert
(
	@QuestionPrefixContent varchar(300)
) as
begin transaction
	insert into QuestionPrefix(QuestionPrefixContent)
	values (@QuestionPrefixContent);

	declare @NewId int
	set @NewId = SCOPE_IDENTITY()

	insert into RandomQuestionPrefix(QuestionPrefixID, RandomQuestionPrefixID)
	values (@NewId, (select Max(RandomQuestionPrefixID) + 1 
								from RandomQuestionPrefix ));
commit transaction

	select @NewId as QuestionPrefixId
go

create procedure QuestionSuffix_Insert
(
	@QuestionSuffixContent varchar(300)
) as
begin transaction
	insert into QuestionSuffix(QuestionSuffixContent)
	values (@QuestionSuffixContent);

	declare @NewId int
	set @NewId = SCOPE_IDENTITY()

	insert into RandomQuestionSuffix(QuestionSuffixID, RandomQuestionSuffixID)
	values(@NewId, (select Max(RandomQuestionSuffixID) + 1
								from RandomQuestionSuffix ));
commit transaction

	select @NewId as QuestionSuffixId
go


create procedure UserInfo_Insert
(
	@Id nvarchar(128)
)
as
begin transaction
	insert into UserInfo(Id)
	values (@Id)
commit transaction
go

create procedure SavedQuestion_Insert
(
	@QuestionPrefixId int,
	@QuestionSuffixId int,
	@UserInfoId int
)
as
declare @AlreadySaved bit

if not exists(select * from SavedQuestionGameResult 
							where QuestionPrefixId = @QuestionPrefixId
							and QuestionSuffixId = @QuestionPrefixId
							and UserInfoId = @UserInfoId)
begin
	begin transaction
		insert into SavedQuestionGameResult(QuestionPrefixId, QuestionSuffixId, UserInfoId)
		values (@QuestionPrefixId, @QuestionSuffixId, @UserInfoId)
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
	select sqgr.SavedQuestionId as SavedQuestionId, qp.QuestionPrefixContent as Question, qs.QuestionSuffixContent as Answer 
	from UserInfo ui
	inner join SavedQuestionGameResult sqgr on ui.UserInfoId = sqgr.UserInfoId
	inner join QuestionPrefix qp on sqgr.QuestionPrefixId = qp.QuestionPrefixID
	inner join QuestionSuffix qs on sqgr.QuestionSuffixId = qs.QuestionSuffixID
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

use SurrealistGames
go

insert into QuestionPrefix(QuestionPrefixContent)
values 
		('What is the meaning of life?'),
		('What is the deepest thought man is capable of?'),
		('What is the best breakfast food?'),
		('What is man''s best friend?'),
		('What is the most important thing in the universe?');

insert into QuestionSuffix(QuestionSuffixContent)
values
		('Nothing at all.'),
		('99 red balloons.'),
		('Poptarts.'),
		('Pain and suffering.'),
		('Mellow yellow.'),
		('Falling down 7 times, getting up 8.');

insert into RandomQuestionPrefix(RandomQuestionPrefixID, QuestionPrefixID)
values 
		(1, 1),
		(2, 2),
		(3, 3),
		(4, 4),
		(5, 5);

insert into RandomQuestionSuffix(RandomQuestionSuffixID, QuestionSuffixID)
values
		(1, 1),
		(2, 2),
		(3, 3),
		(4, 4),
		(5, 5),
		(6, 6);


insert into AspNetUsers (Id, Email, EmailConfirmed,
						 PasswordHash, SecurityStamp, UserName,
						  PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnabled,
						  AccessFailedCount)
values ('99ba7c1f-9091-443a-a9d1-78c0ac54b357', 'matt@example.com', 0, 
		'AHeA2/V7ZzW86cmaitaBcGbGGk8/wsg+jNsGEXOLXAqr//t/u9ToBgBehsrk4fEFUA==',
		'26812078-4361-48e6-befa-e5b371fe9297', 'matt@example.com', 0, 0, 0, 0)

insert into UserInfo(Id)
values ('99ba7c1f-9091-443a-a9d1-78c0ac54b357')

declare @UserInfoId int
select @UserInfoId = UserInfoId 
	from UserInfo
	where UserInfo.Id = '99ba7c1f-9091-443a-a9d1-78c0ac54b357'

insert into SavedQuestionGameResult (UserInfoId, QuestionPrefixId, QuestionSuffixId)
values (@UserInfoId, 2, 1),
       (@UserInfoId, 5, 3),
       (@UserInfoId, 2, 4)
