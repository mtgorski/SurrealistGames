using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Moq;
using NUnit.Framework;
using PagedList;
using SurrealistGames.Models;
using SurrealistGames.Models.Interfaces;
using SurrealistGames.WebUI.Controllers;
using SurrealistGames.WebUI.Interfaces;
using SurrealistGames.WebUI.Models;
using System.Reflection;

namespace Controllers.Tests.cs
{
    [TestFixture]
    class SavedQuestionGameResultTests
    {
        public Mock<ISavedQuestionGameResultRepo> FakeSavedOutcomeRepo { get; set; }

        public Mock<IUserUtility> FakeUserUtility { get; set; }

        public Mock<IUserInfoRepo> FakeUserInfoRepo { get; set; }

        public SavedQuestionGameResultController ControllerUnderTest { get; set; }

        [SetUp]
        public void SetUp()
        {
            FakeSavedOutcomeRepo = new Mock<ISavedQuestionGameResultRepo>();
            FakeUserUtility = new Mock<IUserUtility>();
            FakeUserInfoRepo = new Mock<IUserInfoRepo>();
            ControllerUnderTest = new SavedQuestionGameResultController(FakeUserInfoRepo.Object,
                FakeSavedOutcomeRepo.Object, FakeUserUtility.Object);
        }

        private void SetUpLoggedInUser(string aspId, int userInfoId)
        {
            FakeUserUtility.Setup(m => m.IsLoggedIn(It.IsAny<Controller>())).Returns(true);
            FakeUserUtility.Setup(m => m.GetAspId(It.IsAny<Controller>())).Returns(aspId);

            FakeUserInfoRepo.Setup(m => m.GetByAspId(It.Is<string>(x => x == aspId)))
                .Returns(new UserInfo()
                {
                    Id = aspId,
                    UserInfoId = userInfoId
                });
        }

        [Test]
        public void Post_WithUserLoggedIn_CallsSavedQuestionGameResultRepo()
        {
            SetUpLoggedInUser("5", 5);

            //act
            ControllerUnderTest.Post(4, 3);

            FakeSavedOutcomeRepo.Verify( x => x.
                Save(It.Is<SavedQuestionGameResult>((itemToSave) => itemToSave.QuestionPrefixId == 4 
                                                    && itemToSave.QuestionSuffixId == 3
                                                    && itemToSave.UserInfoId == 5)), Times.Once);

        }

        [Test]
        public void Post_WithUserLoggedIn_ReturnsSuccess()
        {
            SetUpLoggedInUser("5", 5);

            //act
            var result = ControllerUnderTest.Post(4, 3).Data as SaveQuestionGamePostResult;

            Assert.AreEqual(result.LoggedIn, true);
        }

        [Test]
        public void SavedResults_CallsGetAllByUserIdOnGameStorageRepo()
        {
            SetUpLoggedInUser("test", 3);
            FakeSavedOutcomeRepo.Setup(m => m.GetAllSavedOutcomesByUserId(It.IsAny<int>()))
                .Returns(new List<UserSavedOutcomeView>());
            ControllerUnderTest.SavedResults();

            FakeSavedOutcomeRepo
                .Verify(m => m.GetAllSavedOutcomesByUserId( It.Is<int>(x => x == 3)));
        }

        [Test]
        public void SavedResults_RendersSavedResultsView()
        {
            SetUpLoggedInUser("test", 5);
            FakeSavedOutcomeRepo.Setup(m => m.GetAllSavedOutcomesByUserId(It.IsAny<int>()))
                .Returns(new List<UserSavedOutcomeView>());

            var result = ControllerUnderTest.SavedResults();

            Assert.AreEqual(result.ViewName, "SavedResults");
        }

        [Test]
        public void SavedResults_PassesPagedListOfOutcomesToView()
        {
            SetUpLoggedInUser("test", 5);
            var model = new List<UserSavedOutcomeView>();
            FakeSavedOutcomeRepo.Setup(m => m.GetAllSavedOutcomesByUserId(5)).Returns(model);

            var result = ControllerUnderTest.SavedResults();

            var viewModel = result.Model as PagedList<UserSavedOutcomeView>;
            Assert.IsNotNull(viewModel);
        }

        [Test]
        public void Delete_ReturnsJsonResultWithDataCastableToDeleteFavoriteResult()
        {
            SetUpLoggedInUser("test", 5);
            var result = ControllerUnderTest.Delete(3);

            Assert.IsInstanceOf<DeleteFavoriteResult>(result.Data);
        }

        [Test]
        public void Delete_OnResultOwnedByUser_CallsSavedResultRepoDelete()
        {
            SetUpLoggedInUser("test", 5);
            FakeSavedOutcomeRepo.Setup(m => m.UserOwnsSavedResult(It.Is<int>(y => y == 5), It.Is<int>(y => y == 10)))
                .Returns(true);

            ControllerUnderTest.Delete(10);

            FakeSavedOutcomeRepo.Verify(m => m.Delete(It.Is<int>(x => x == 10)));
        }

        [Test]
        public void Delete_OnResultOwnedByUser_ReturnsResponseWithUserOwnedTrue()
        {
            SetUpLoggedInUser("test", 5);
            FakeSavedOutcomeRepo.Setup(m => m.UserOwnsSavedResult(It.Is<int>(y => y == 5), It.Is<int>(y => y == 10)))
                .Returns(true);

            var result = ControllerUnderTest.Delete(10);
            var resultModel = result.Data as DeleteFavoriteResult;

            Assert.IsTrue(resultModel.IsResultOwnedByUser);
        }

        [Test]
        public void Delete_OnResultNotOwnedByUser_DoesNotCallSavedResultRepoDelte()
        {
            SetUpLoggedInUser("test", 5);
            FakeSavedOutcomeRepo.Setup(m => m.UserOwnsSavedResult(It.Is<int>(i => i == 5), It.Is<int>(i => i == 10)))
                .Returns(false);

            var result = ControllerUnderTest.Delete(10);

            FakeSavedOutcomeRepo.Verify(m => m.Delete(10), Times.Never);
        }

        [Test]
        public void Delete_OnResultNotOwnedByUser_ReturnsJsonResultWithOwnedFalse()
        {
            SetUpLoggedInUser("test", 5);
            FakeSavedOutcomeRepo.Setup(m => m.UserOwnsSavedResult(It.Is<int>(i => i == 5), It.Is<int>(i => i == 10)))
                .Returns(false);

            var result = ControllerUnderTest.Delete(10);
            var resultModel = result.Data as DeleteFavoriteResult;

            Assert.IsFalse(resultModel.IsResultOwnedByUser);
        }

        [Test]
        public void Delete_HasAuthorizeAttribute()
        {
            Func<int, JsonResult> deleteFunc = ControllerUnderTest.Delete;

            var authorizeAttributes = deleteFunc.Method.GetCustomAttributes(typeof(AuthorizeAttribute), false);
            
            Assert.Greater(authorizeAttributes.Length, 0);
        }


    }
}
