using Moq;
using Ninject.MockingKernel.Moq;
using NUnit.Framework;
using SurrealistGames.WebUI.Controllers;
using SurrealistGames.WebUI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SurrealistGames.Models;
using System.Net;
using System.Reflection;
using System.Web.Http;
using SurrealistGames.GameLogic.Helpers;
using SurrealistGames.GameLogic.Helpers.Interfaces;
using SurrealistGames.Models.Interfaces;

namespace Controllers.Tests.cs
{
    [TestFixture]
    class ReportControllerTests
    {
        public ReportApiController Target { get; set; }
        public Mock<IReportHelper> ReportHelperMock { get; set; }
        public Mock<IUserUtility> UserUtilityMock { get; set; }
        public Mock<IUserInfoRepo> UserInfoRepoMock { get; set; }

        public ReportRequest GivenReportRequest { get; set; }
        public ReportResponse ExpectedReportResponse { get; set; }

        [SetUp]
        public void SetUp()
        {
            ReportHelperMock = new Mock<IReportHelper>();
            UserUtilityMock = new Mock<IUserUtility>();
            UserInfoRepoMock = new Mock<IUserInfoRepo>();
            ExpectedReportResponse = new ReportResponse();
            GivenReportRequest = new ReportRequest();

            Target = new ReportApiController(ReportHelperMock.Object, UserUtilityMock.Object,
                UserInfoRepoMock.Object);
        }

        private void SetUpUser(int userId)
        {
            UserUtilityMock.Setup(m => m.GetAspId(It.IsAny<ReportApiController>()))
                .Returns("aspId");
            UserInfoRepoMock.Setup(m => m.GetByAspId(It.Is<string>(s => s == "aspId")))
                .Returns(new UserInfo() { Id = "aspId", UserInfoId = userId });
        }

        [Test]
        public void ReportApiController_HasAuthorizeAttribute()
        {
            Func<ReportRequest, ReportResponse> func = Target.Post;

            var attribute = func.Method.GetCustomAttribute(typeof(AuthorizeAttribute));
            Assert.IsNotNull(attribute);
        }

        [Test]
        public void GivenReportRequest_WhenPost_ReturnResultOfReportHelper()
        {
            SetUpUser(19);

            ReportHelperMock.Setup(m => m.MakeReport(It.Is<ReportRequest>(r => r == GivenReportRequest)))
                .Returns(ExpectedReportResponse);

            var actualResponse = Target.Post(GivenReportRequest);

            Assert.AreSame(ExpectedReportResponse, actualResponse);
        }

        [Test]
        public void GivenReportRequest_WhenPost_SetUserIdOfReport()
        {
            SetUpUser(19);

            var response = Target.Post(GivenReportRequest);

            ReportHelperMock.Verify(m => m.MakeReport(It.Is<ReportRequest>(r => r.UserInfoId == 19)));
        }
    
    }
}
