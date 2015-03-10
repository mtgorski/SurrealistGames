using Moq;
using NUnit.Framework;
using SurrealistGames.GameLogic.Factories.Interfaces;
using SurrealistGames.GameLogic.Helpers;
using SurrealistGames.Models;
using SurrealistGames.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic.Tests.cs
{
    [TestFixture]
    class ModerationHelperTests
    {
        public ModerationHelper Target { get; set; }
        public Mock<IContentRepository> AnswerRepositoryMock { get; set; }
        public Mock<IContentRepository> QuestionRepositoryMock { get; set; }
        public Mock<IContentRepositoryFactory> ContentRepositoryFactoryMock { get; set; } 

        [SetUp]
        public void SetUp()
        {
            AnswerRepositoryMock = new Mock<IContentRepository>();
            QuestionRepositoryMock = new Mock<IContentRepository>();
            ContentRepositoryFactoryMock = new Mock<IContentRepositoryFactory>();

            ContentRepositoryFactoryMock.Setup(m => m.GetRepositoryFor(It.Is<Type>(t => t == typeof(Question))))
                .Returns(QuestionRepositoryMock.Object);
            ContentRepositoryFactoryMock.Setup(m => m.GetRepositoryFor(It.Is<Type>(t => t == typeof(Answer))))
                .Returns(AnswerRepositoryMock.Object);

            Target = new ModerationHelper(ContentRepositoryFactoryMock.Object);
        }

        [Test]
        public void GivenRemovalRequestForQuestion_WhenRemove_DisablesQuestion()
        {
            var request = new RemoveContentRequest { QuestionId = 1789 };

            Target.RemoveContent(request);

            QuestionRepositoryMock.Verify(
                m => m.Disable(It.Is<int>(r => r == request.QuestionId.Value)));
        }

        [Test]
        public void GivenRemovalRequestForAnswer_WhenRemove_DisablesAnswer()
        {
            var request = new RemoveContentRequest { AnswerId = 10 };

            Target.RemoveContent(request);

            AnswerRepositoryMock.Verify(
                m => m.Disable(It.Is<int>(r => r == request.AnswerId.Value)));
        }

        [Test]
        public void GivenRemovalRequest_WhenRemove_SavesUpdatedContent()
        {
            var request = new RemoveContentRequest { AnswerId = 1500 };

            Target.RemoveContent(request);

            AnswerRepositoryMock.Verify(
                m => m.Remove(It.Is<RemoveContentRequest>(r => r == request)));
        }
    }
}
