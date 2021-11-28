using WebApi.Services.UserServices;

namespace WebApiTests.UserTest
{
    public static class UserUtils
    {
        public static void InitUser(string userName)
        {
            var service = new UserBusinessLayer();
            var user = service.GetUser(userName);

            if (user != null)
            {
                service.DeleteUser(user.Username);
            }

            service.CreateUser(userName, "1234", "key");
        }
        
        public static void DeleteUser(string userName)
        {
            var service = new UserBusinessLayer();
            var user = service.GetUser(userName);

            if (user != null)
            {
                service.DeleteUser(user.Username);
            }
        }
    }
}