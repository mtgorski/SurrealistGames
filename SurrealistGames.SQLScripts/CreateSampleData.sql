use SurrealistGamesDev
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
