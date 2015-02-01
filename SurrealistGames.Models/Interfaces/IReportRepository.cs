using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurrealistGames.Models.Interfaces
{
    public interface IReportRepository
    {
        void Save(Report report);
        /// <summary>
        /// Valid reports include only a single report per user and do not
        /// include reports from users who are not allowed to report. 
        /// </summary>
        /// <param name="questionId"></param>
        /// <returns></returns>
        List<Report> GetValidReportsByQuestionId(int questionId);

        /// <summary>
        /// Valid reports include only a single report per user and do not
        /// include reports from users who are not allowed to report. 
        /// </summary>
        /// <param name="answerId"></param>
        /// <returns></returns>
        List<Report> GetValidReportsByAnswerId(int answerId);
    }
}
