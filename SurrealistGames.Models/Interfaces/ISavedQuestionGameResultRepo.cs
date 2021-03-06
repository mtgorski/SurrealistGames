﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurrealistGames.Models.Interfaces
{
    public interface ISavedQuestionGameResultRepo
    {
        void Save(SavedQuestionGameResult item);
        Task<List<UserSavedOutcomeView>> GetAllSavedOutcomesByUserId(int userInfoid);
        bool UserOwnsSavedResult(int userInfoId, int savedQuestionId);

        void Delete(int savedQuestionId);
    }
}
