using Ninject;
using SurrealistGames.GameLogic.Factories.Interfaces;
using SurrealistGames.Models;
using SurrealistGames.Models.Abstract;
using SurrealistGames.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurrealistGames.GameLogic.Factories
{
    public class ContentRepositoryFactory : IContentRepositoryFactory
    {
        private readonly IKernel _kernel;

        public ContentRepositoryFactory(IKernel kernel)
	    {
            _kernel = kernel;
	    }

        public Models.Interfaces.IContentRepository GetRepositoryFor<TContent>() where TContent : Content
        {
            if(typeof(TContent) == typeof(Answer))
            {
                return _kernel.Get<IAnswerRepository>();
            }

            if(typeof(TContent) == typeof(Question))
            {
                return _kernel.Get<IQuestionRepository>();
            }

            throw new ArgumentException("Unsupported type");
        }
    }
}
