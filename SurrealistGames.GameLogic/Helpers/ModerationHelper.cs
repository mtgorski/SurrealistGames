using SurrealistGames.GameLogic.Factories.Interfaces;
using SurrealistGames.GameLogic.Helpers.Interfaces;
using SurrealistGames.Models;
using SurrealistGames.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurrealistGames.GameLogic.Helpers
{
    public class ModerationHelper : IModerationHelper
    {
        private readonly IContentRepositoryFactory _contentRepositoryFactory;

        public ModerationHelper(IContentRepositoryFactory contentRepositoryFactory)
        {
            _contentRepositoryFactory = contentRepositoryFactory;
        }

        public Models.RemoveContentResponse RemoveContent(Models.RemoveContentRequest request)
        {
            Type contentType = request.AnswerId.HasValue ? typeof(Answer) : typeof(Question);
            IContentRepository repo = _contentRepositoryFactory.GetRepositoryFor(contentType);
            var content = repo.GetContentById(request.ContentId);
            if(content.IsModerated)
            {
                return new RemoveContentResponse();
            }


            repo.Disable(request.ContentId);

            repo.Remove(request);

            return new RemoveContentResponse();
        }

        public Models.ApproveContentResponse ApproveContent(Models.ApproveContentRequest request)
        {
            Type contentType = request.ContentType;
            IContentRepository repo = _contentRepositoryFactory.GetRepositoryFor(contentType);

            var content = repo.GetContentById(request.ContentId);
            if(content.IsModerated)
            {
                return new ApproveContentResponse();
            }

            content.ApprovingUserId = request.RequestingUserId;
            content.ApprovedOn = DateTime.UtcNow;
            repo.Update(content);

            repo.AddToOutcomes(content);

            return new ApproveContentResponse();
        }
    }
}
