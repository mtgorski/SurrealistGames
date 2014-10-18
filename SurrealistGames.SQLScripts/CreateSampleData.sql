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