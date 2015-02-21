using SurrealistGames.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurrealistGames.Data
{
    public class EfReportRepository : IReportRepository
    {

        private IEfDbContext _context;

        public EfReportRepository(IEfDbContext context)
        {
            _context = context;
        }

        public void Save(Models.Report report)
        {
            _context.Reports.Add(report);
            _context.SaveChanges();

        }

        public List<Models.Report> GetReportsByQuestionId(int questionId)
        {
            return _context.Reports.Where(x => x.QuestionId == questionId).ToList();
        }

        public List<Models.Report> GetReportsByAnswerId(int answerId)
        {
            return _context.Reports.Where(x => x.AnswerId == answerId).ToList();
        }
    }
}
