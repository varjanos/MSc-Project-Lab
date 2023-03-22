using FloorPlanner.Web.Blazor.Interfaces;

namespace FloorPlanner.Web.Blazor.Services.UserProfile
{
    public interface IUserProfileClientService : IInitialize
    {
        public string GetUserName();
    }
}