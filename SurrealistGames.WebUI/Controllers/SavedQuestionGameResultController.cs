using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Http.Results;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SurrealistGames.Models;
using SurrealistGames.Models.Interfaces;
using SurrealistGames.WebUI.Models;

namespace SurrealistGames.WebUI.Controllers
{
    public class SavedQuestionGameResultController : Controller
    {
        private IUserInfoRepo _userInfoRepo;
        private ISavedQuestionGameResultRepo _savedQuestionGameResultRepo;
        private IUser _aspUser;


        public SavedQuestionGameResultController(IUserInfoRepo userInfoRepo, ISavedQuestionGameResultRepo savedQuestionGameResultRepo, Microsoft.AspNet.Identity.IUser user)
        {
            _userInfoRepo = userInfoRepo;
            _savedQuestionGameResultRepo = savedQuestionGameResultRepo;
            _aspUser = user;
            
        }

        public JsonResult Post(int questionPrefixId, int questionSuffixId)
        {
            var result = new SaveQuestionGamePostResult();

            if (_aspUser != null)
            {
                var itemToSave = new SavedQuestionGameResult()
                {
                    QuestionPrefixId = questionPrefixId,
                    QuestionSuffixId = questionSuffixId,
                    UserInfoId = _userInfoRepo.GetByAspId(_aspUser.Id).UserInfoId
                };
                _savedQuestionGameResultRepo.Save(itemToSave);

                result.LoggedIn = true;
                return Json(result);
            }

            result.LoggedIn = false;
            return Json(result);
        }
    }
}