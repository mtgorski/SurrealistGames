using AutoMapper;
using Ninject;
using Ninject.Activation.Caching;
using Ninject.Modules;
using Ninject.Web.Common;
using SurrealistGames.Data;
using SurrealistGames.GameLogic;
using SurrealistGames.GameLogic.AutoMapper;
using SurrealistGames.GameLogic.Factories;
using SurrealistGames.GameLogic.Factories.Interfaces;
using SurrealistGames.GameLogic.Helpers;
using SurrealistGames.GameLogic.Helpers.Interfaces;
using SurrealistGames.GameLogic.Utility;
using SurrealistGames.Models.Interfaces;
using SurrealistGames.Utility;
using SurrealistGames.WebUI.Interfaces;
using SurrealistGames.WebUI.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurrealistGames.WebUI
{
    public class SurrealistGamesModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => Kernel);
            Kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

            Kernel.Bind<IRandomBehavior>().To<RandomBehavior>();
            Kernel.Bind<IQuestionRepository>().To<EfQuestionRepository>();
            Kernel.Bind<IAnswerRepository>().To<EfAnswerRepository>();
            Kernel.Bind<IQuestionValidator>().To<QuestionValidator>();
            Kernel.Bind<IQuestionFormatter>().To<QuestionFormatter>();
            Kernel.Bind<IAnswerValidator>().To<AnswerValidator>();
            Kernel.Bind<IAnswerFormatter>().To<AnswerFormatter>();
            Kernel.Bind<IUserInfoRepo>().To<EfUserInfoRepository>();
            Kernel.Bind<IUserUtility>().To<UserUtility>();
            Kernel.Bind<ISavedQuestionGameResultRepo>().To<EfSavedQuestionGameResultRepository>();
            Kernel.Bind<IEfDbContext>().To<EfDbContext>().InRequestScope();
            Kernel.Bind<IReportRepository>().To<EfReportRepository>().InRequestScope();
            Kernel.Bind<IReportHelper>().To<ReportHelper>();
            Kernel.Bind<IConfig>().To<Config>().InSingletonScope();
            Kernel.Bind<IContentRepositoryFactory>().To<ContentRepositoryFactory>();
            Kernel.Bind<IModerationHelper>().To<ModerationHelper>();
            Kernel.Rebind<IKernel>().ToMethod(_ => Kernel).InSingletonScope();
    
            AutoMapperConfiguration.Register();
            Kernel.Bind<IMappingEngine>().ToConstant(Mapper.Engine);
        }
    }
}