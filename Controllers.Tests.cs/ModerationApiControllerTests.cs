using Moq;
using NUnit.Framework;
using SurrealistGames.GameLogic.Helpers.Interfaces;
using SurrealistGames.Models;
using SurrealistGames.Models.Interfaces;
using SurrealistGames.WebUI.Controllers;
using SurrealistGames.WebUI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Controllers.Tests.cs
{
    [TestFixture]
    public class ModerationApiControllerTests
    {
        public ModerationApiController Target { get; set; }
        public Mock<IUserUtility> UserUtilityMock { get; set; }
        public Mock<IUserInfoRepo> UserInfoMock { get; set; }
        public Mock<IModerationHelper> ModerationHelperMock { get; set; }

        public RemoveContentRequest GivenRemovalRequest { get; set; }
        public RemoveContentResponse ExpectedRemovalResponse { get; set; }

        [SetUp]
        public void Setup()
        {
           
            UserUtilityMock = new Mock<IUserUtility>();
            UserUtilityMock.Setup(m => m.GetAspId(It.IsAny<ApiController>()))
                .Returns("userid");

            UserInfoMock = new Mock<IUserInfoRepo>();
            UserInfoMock.Setup(m => m.GetByAspId("userid"))
                .Returns(new UserInfo { Id = "userid", UserInfoId = 10 });

            ModerationHelperMock = new Mock<IModerationHelper>();
            
            GivenRemovalRequest = new RemoveContentRequest();
            ExpectedRemovalResponse = new RemoveContentResponse();

            Target = new ModerationApiController(UserInfoMock.Object, UserUtilityMock.Object, ModerationHelperMock.Object);
        }

        [Test]
        public void GivenRemovalRequest_WhenRemove_PassRequestToModerationHelper()
        {
            GivenRemovalRequest.QuestionId = 199;
            ModerationHelperMock.Setup(m => m.RemoveContent(It.Is<RemoveContentRequest>(
                r => r.QuestionId == 199 && r.RequestingUserId == 10)))
                .Returns(ExpectedRemovalResponse);

            var response = Target.RemoveContent(GivenRemovalRequest);

            Assert.AreSame(ExpectedRemovalResponse, response); 
        }

        [Test]
        public void RemoveContent_AuthorizesModeratorsAndAdmins()
        {
            Func<RemoveContentRequest, RemoveContentResponse> func = Target.RemoveContent;

            var attributeInfo = func.Method.CustomAttributes.FirstOrDefault(a => a.AttributeType == typeof(AuthorizeAttribute));

            Assert.IsNotNull(attributeInfo);

            var rolesValue = attributeInfo.NamedArguments.Where(arg => arg.MemberName == "Roles").FirstOrDefault().TypedValue.Value;
            var expectedAttributeArgument = "Admin, Moderator";

            Assert.AreEqual(expectedAttributeArgument, rolesValue);
        }
    }
}
