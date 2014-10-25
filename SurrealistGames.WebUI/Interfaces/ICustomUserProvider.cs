using System.Web.Mvc;

namespace SurrealistGames.WebUI.Interfaces
{
    public interface IUserUtility
    {
        bool IsLoggedIn(Controller controller);

        string GetAspId(Controller controller);
    }


}
