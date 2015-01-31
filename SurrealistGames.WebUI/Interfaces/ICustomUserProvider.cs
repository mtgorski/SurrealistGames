using System.Web.Http;
using System.Web.Mvc;

namespace SurrealistGames.WebUI.Interfaces
{
    public interface IUserUtility
    {
        bool IsLoggedIn(Controller controller);

        bool IsLoggedIn(ApiController controller);

        string GetAspId(Controller controller);
    }


}
