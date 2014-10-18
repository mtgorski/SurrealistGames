
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