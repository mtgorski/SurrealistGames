using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurrealistGames.GameLogic.Utility
{
    public class Config : IConfig   
    {
        public int ReportsDisabledOn
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["ReportsDisabledOn"]);
            }
        }
    }
}
