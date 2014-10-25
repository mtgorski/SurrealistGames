using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SurrealistGames.Data;
using SurrealistGames.Data.Mocks;
using SurrealistGames.GameLogic;
using SurrealistGames.Models.Interfaces;
using SurrealistGames.Repositories;
using SurrealistGames.Utility;
using SurrealistGames.WebUI.Interfaces;
using SurrealistGames.WebUI.Models;
using SurrealistGames.WebUI.Utility;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(SurrealistGames.WebUI.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(SurrealistGames.WebUI.App_Start.NinjectWebCommon), "Stop")]

namespace SurrealistGames.WebUI.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IRandomBehavior>().To<RandomBehavior>();
            kernel.Bind<IQuestionPrefixRepository>().To<SqlQuestionPrefixRepository>();
            kernel.Bind<IQuestionSuffixRepository>().To<SqlQuestionSuffixRepository>();
            kernel.Bind<IQuestionPrefixValidator>().To<QuestionValidator>();
            kernel.Bind<IQuestionPrefixFormatter>().To<QuestionFormatter>();
            kernel.Bind<IQuestionSuffixValidator>().To<AnswerValidator>();
            kernel.Bind<IQuestionSuffixFormatter>().To<AnswerFormatter>();
            kernel.Bind<IUserInfoRepo>().To<SqlUserInfoRepository>();
            kernel.Bind<IUserUtility>().To<UserUtility>();
            kernel.Bind<ISavedQuestionGameResultRepo>().To<SqlSavedQuestionGameResultRepo>();

        }        
    }
}
