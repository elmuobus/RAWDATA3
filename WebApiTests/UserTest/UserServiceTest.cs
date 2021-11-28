using WebApi.Domain.UserDomain;
using WebApi.Services.UserServices;
using Xunit;

namespace WebApiTests.UserTest
{
    public class UserServiceTest
    {
        [Fact]
        public void User_Object_HasDefaultValues()
        {
            var user = new User();
            Assert.Null(user.Username);
            Assert.Null(user.Password);
            Assert.Null(user.Salt);
            Assert.False(user.IsAdmin);
            Assert.False(user.IsAdult);
        }

        [Fact]
        public void CreateUser_ValidData_CreateUserAndReturnsNewObject()
        {
            var service = new UserBusinessLayer();
            var user = service.CreateUser("createTest", "1234", "key");
            Assert.Equal("createTest", user.Username);
            Assert.Equal("1234", user.Password);
            Assert.Equal("key", user.Salt);

            // cleanup
            service.DeleteUser(user.Username);
        }
        
        [Fact]
        public void CreateUser_ValidDataButAlreadyExisting_ReturnsNullObject()
        {
            var service = new UserBusinessLayer();
            var user = service.CreateUser("createTest", "1234", "key");
            Assert.Equal("createTest", user.Username);
            Assert.Equal("1234", user.Password);
            Assert.Equal("key", user.Salt);
            
            var sameUser = service.CreateUser("createTest", "1234", "key");
            Assert.Null(sameUser);

            // cleanup
            service.DeleteUser(user.Username);
        }
        
        [Fact]
        public void GetUser_ValidUsername_ReturnsUserObject()
        {
            var service = new UserBusinessLayer();
            var createUser = service.CreateUser("getTest", "1234abcd", "keyKey");
            Assert.NotNull(createUser);
            var user = service.GetUser("getTest");
            Assert.Equal("getTest", user.Username);
            Assert.Equal("1234abcd", user.Password);
            Assert.Equal("keyKey", user.Salt);
            
            // cleanup
            service.DeleteUser(user.Username);
        }
        
        [Fact]
        public void GetUser_InvalidUsername_ReturnsNullObject()
        {
            var service = new UserBusinessLayer();
            var user = service.GetUser("notExist");
            Assert.Null(user);
        }

        [Fact]
        public void DeleteUser_ValidUsername_RemoveTheUser()
        {
            var service = new UserBusinessLayer();
            var user = service.CreateUser("deleteTest", "1234", "key");
            var result = service.DeleteUser(user.Username);
            Assert.True(result);
            user = service.GetUser(user.Username);
            Assert.Null(user);
        }

        [Fact]
        public void DeleteUser_InvalidId_ReturnsFalse()
        {
            var service = new UserBusinessLayer();
            var result = service.DeleteUser("notExist");
            Assert.False(result);
        }
    } 
}