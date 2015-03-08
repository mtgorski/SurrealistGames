using SurrealistGames.Models.Abstract;
using SurrealistGames.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurrealistGames.GameLogic.Factories.Interfaces
{
    public interface IContentRepositoryFactory
    {
        IContentRepository GetRepositoryFor<T>() where T : Content;
    }
}
