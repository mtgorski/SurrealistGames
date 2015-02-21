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
        /// </summary>
        /// <param name="questionId"></param>
        /// <returns></returns>
        List<Report> GetReportsByQuestionId(int questionId);

        /// <summary>
        /// </summary>
        /// <param name="answerId"></param>
        /// <returns></returns>
        List<Report> GetReportsByAnswerId(int answerId);
    }
}
