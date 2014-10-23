
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