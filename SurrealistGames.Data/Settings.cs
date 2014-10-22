using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace SurrealistGames.Repositories
{
    public static class Settings
    {
        private static string _connectionString =
            @"Data Source=(localdb)\v11.0;Initial Catalog=SurrealistGamesDev;Integrated Security=True";

        public static string GetConnectionString()
        {
            if (string.IsNullOrEmpty(_connectionString))
            {
                _connectionString = ConfigurationManager.ConnectionStrings["QuestionAndAnswer"].ToString();
            }

            return _connectionString;
        }
    }
}