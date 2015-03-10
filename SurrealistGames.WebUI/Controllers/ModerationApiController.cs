using SurrealistGames.GameLogic.Helpers.Interfaces;
using SurrealistGames.Models;
using SurrealistGames.Models.Interfaces;
using SurrealistGames.WebUI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SurrealistGames.WebUI.Controllers
{
    public class ModerationApiController : ApiController
    {
        private readonly IUserUtility _userUtility;
        private readonly IUserInfoRepo _userInfoRepo;
        private readonly IModerationHelper _moderationHelper;

        public ModerationApiController(IUserInfoRepo userInfoRepo, IUserUtility userUtility, IModerationHelper moderationHelper)
        {
            _userUtility = userUtility;
            _userInfoRepo = userInfoRepo;
            _moderationHelper = moderationHelper;
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Moderator")]
        public RemoveContentResponse RemoveContent([FromBody]RemoveContentRequest request)
        {
            var aspUserId = _userUtility.GetAspId(this);
            request.RequestingUserId = _userInfoRepo.GetByAspId(aspUserId).UserInfoId;

            return _moderationHelper.RemoveContent(request);
        }
    }
}