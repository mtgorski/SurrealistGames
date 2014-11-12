
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

	insert into RandomQuestionPrefix(QuestionPrefixID, RandomQuestionPrefixID)
	values (SCOPE_IDENTITY(), (select Max(RandomQuestionPrefixID) + 1 
								from RandomQuestionPrefix ));
commit transaction
go


create procedure QuestionSuffix_Insert
(
	@QuestionSuffixContent varchar(300)
) as
begin transaction
	insert into QuestionSuffix(QuestionSuffixContent)
	values (@QuestionSuffixContent);

	insert into RandomQuestionSuffix(QuestionSuffixID, RandomQuestionSuffixID)
	values(SCOPE_IDENTITY(), (select Max(RandomQuestionSuffixID) + 1
								from RandomQuestionSuffix ));
commit transaction
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
	inner join QuestionSuffix qs on sqgr.QuestionPrefixId = qs.QuestionSuffixID
	where ui.UserInfoId = @UserInfoId
go