﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurrealistGames.Models.Interfaces
{
    public interface IUserInfoRepo
    {
        void Add(UserInfo item);
        UserInfo GetByAspId(string aspNetId);
    }
}
