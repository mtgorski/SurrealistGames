Surrealist Games
=========================

Surrealist games is a website that lets users participate in games inspired by surrealist artistic
practices. Surrealist artists used these games to generate insights through random chance.
In one variation, the group would write down questions on small sheets of paper which
were then thrown into a pile. Next, the group would write down answers on other sheets
of paper. The questions and answers were then combined, yielding results such as
"What is ugliness? Forgotten dreams."

The website will allow user to participate in the question/answer game as well as
an if/then variation. Users will enter a question which is then matched with
an answer written by another user and the question will be saved
for future games played by others (mutatis mutandis for entering answers). The user will then have the option to 
save the result to a list of their favorites or report the result for inappropriate
content/spam etc, but only if they have an account and are logged in. 

Website moderators will be able to view lists of reported submissions. They can
either remove them, approve them or prevent the user from making further reports. 

Website administrators will have moderator powers and in addition be able to approve
or dismiss moderators. 

Planning
-----------------
Stage 1: Implement the question/answer game (no need for reporting or saving.
 no real data layer)
	X create models for Q/A
	X create mock repos for handling storage of questions and answer
	X create UI for gathering and displaying questions/answers when the user is submitting
	X create UI for displaying questions/answers when user is submitting
	- enforce a uniform display format 
	X display outcomes in a mode that doesn't require users to give input
	X fix "What's" bug
	- improve display of the outcome (use a uniform layout by using a partial view or html helper extension?)
	- improve randomness of the outcomes
	X use Ninject