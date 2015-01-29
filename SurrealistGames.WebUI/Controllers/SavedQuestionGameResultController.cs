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
using PagedList;
using SurrealistGames.Models;
using SurrealistGames.Models.Interfaces;
using SurrealistGames.WebUI.Interfaces;
using SurrealistGames.WebUI.Models;
using System.Threading.Tasks;

namespace SurrealistGames.WebUI.Controllers
{
    public class SavedQuestionGameResultController : Controller
    {
        private IUserInfoRepo _userInfoRepo;
        private ISavedQuestionGameResultRepo _savedQuestionGameResultRepo;
        private IUserUtility _userUtility;


        public SavedQuestionGameResultController(IUserInfoRepo userInfoRepo, ISavedQuestionGameResultRepo savedQuestionGameResultRepo, IUserUtility userUtility)
        {
            _userInfoRepo = userInfoRepo;
            _savedQuestionGameResultRepo = savedQuestionGameResultRepo;
            _userUtility = userUtility;
            
        }

        public JsonResult Post(int QuestionId, int AnswerId)
        {
            var result = new SaveQuestionGamePostResult();
            
            if (_userUtility.IsLoggedIn(this))
            {
                var itemToSave = new SavedQuestionGameResult()
                {
                    QuestionId = QuestionId,
                    AnswerId = AnswerId,
                    UserInfoId = _userInfoRepo.GetByAspId(_userUtility.GetAspId(this)).UserInfoId
                };
                _savedQuestionGameResultRepo.Save(itemToSave);

                result.LoggedIn = true;
                return Json(result);
            }

            result.LoggedIn = false;
            return Json(result);
        }

        [Authorize]
        public async Task<ViewResult> SavedResults(int page = 1, int pageSize = 5)
        {
            
            var userInfoId = GetUserInfoId();

            var favorites = await _savedQuestionGameResultRepo.GetAllSavedOutcomesByUserId(userInfoId);

            var model = new PagedList<UserSavedOutcomeView>(favorites, page, pageSize);

            return View("SavedResults", model);
        }

        private int GetUserInfoId()
        {
            return _userInfoRepo.GetByAspId(_userUtility.GetAspId(this)).UserInfoId;
        }

        [Authorize]
        public JsonResult Delete(int savedQuestionId)
        {
            var resultModel = new DeleteFavoriteResult()
            {
                IsResultOwnedByUser = false,
                IsUserLoggedIn = true
            };

            var userInfoId = GetUserInfoId();

            if (_savedQuestionGameResultRepo.UserOwnsSavedResult(userInfoId, savedQuestionId))
            {
                resultModel.IsResultOwnedByUser = true;

                _savedQuestionGameResultRepo.Delete(savedQuestionId);
            }

            return Json(resultModel);
        }
    }
}