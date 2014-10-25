using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Moq;
using NUnit.Framework;
using SurrealistGames.Models;
using SurrealistGames.Models.Interfaces;
using SurrealistGames.WebUI.Controllers;
using SurrealistGames.WebUI.Models;

namespace Controllers.Tests.cs
{
    [TestFixture]
    class SavedQuestionGameResultTests
    {
        

        [Test]
        public void Post_WithUserLoggedIn_CallsSavedQuestionGameResultRepo()
        {
            
            var mockSavedQuestionRepo = new Mock<ISavedQuestionGameResultRepo>();

            var mockUser = new Mock<IUser>();
            mockUser.Setup(m => m.Id).Returns("5");

            var mockUserRepo = new Mock<IUserInfoRepo>();
            mockUserRepo.Setup(m => m.GetByAspId(It.Is<string>(x => x == "5"))).Returns(new UserInfo() {UserInfoId = 5}); 
            var controller = new SavedQuestionGameResultController(mockUserRepo.Object, mockSavedQuestionRepo.Object,
                mockUser.Object);

            //act
            controller.Post(4, 3);

            mockSavedQuestionRepo.Verify( x => x.
                Save(It.Is<SavedQuestionGameResult>((itemToSave) => itemToSave.QuestionPrefixId == 4 
                                                    && itemToSave.QuestionSuffixId == 3
                                                    && itemToSave.UserInfoId == 5)), Times.Once);

        }

        [Test]
        public void Post_WithUserLoggedIn_ReturnsSuccess()
        {
            var mockSavedQuestionRepo = new Mock<ISavedQuestionGameResultRepo>();

            var mockUser = new Mock<IUser>();
            mockUser.Setup(m => m.Id).Returns("5");

            var mockUserRepo = new Mock<IUserInfoRepo>();
            mockUserRepo.Setup(m => m.GetByAspId(It.Is<string>(x => x == "5"))).Returns(new UserInfo() { UserInfoId = 5 });
            var controller = new SavedQuestionGameResultController(mockUserRepo.Object, mockSavedQuestionRepo.Object,
                mockUser.Object);

            //act
            var result = controller.Post(4, 3).Data as SaveQuestionGamePostResult;

            Assert.AreEqual(result.LoggedIn, true);
        }
    }
}
