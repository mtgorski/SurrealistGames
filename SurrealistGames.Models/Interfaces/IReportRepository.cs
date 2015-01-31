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
        List<Report> GetReportsByQuestionId(int questionId);

        List<Report> GetReportsByAnswerId(int p);
    }
}
