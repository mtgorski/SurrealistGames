using SurrealistGames.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurrealistGames.Models.Interfaces
{
    public interface IContentRepository
    {
        IEnumerable<Content> GetTopReportedAndUnmoderatedContent(int numberOfResults);

        void Disable(int contentId);

        Content GetContentById(int contentId);

        void Remove(RemoveContentRequest request);

        void Update(Content content);

        void AddToOutcomes(Content content);
    }
}
