using System.Security.Claims;
using AutoMapper;
using BibleBlast.API.Controllers;
using BibleBlast.API.DataAccess;
using BibleBlast.API.Dtos;
using BibleBlast.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BibleBlast.API.UnitTests
{
    [TestClass]
    public class UsersControllerUnitTests
    {
        private UsersController _usersController;
        private Mock<IUserRepository> _userRepoMock;
        private Mock<UserManager<User>> _userManagerMock;
        private Mock<IMapper> _mapperMock;

        [TestInitialize]
        public void Init()
        {
            _userRepoMock = new Mock<IUserRepository>();
            _userManagerMock = new Mock<UserManager<User>>(new Mock<IUserStore<User>>().Object, null, null, null, null, null, null, null, null);
            _mapperMock = new Mock<IMapper>();

            _usersController = new UsersController(_userRepoMock.Object, _userManagerMock.Object, _mapperMock.Object);
            _usersController.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext()
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, "27") }))
                }
            };
        }

        [TestCleanup]
        public void Cleanup()
        {
            _userRepoMock.VerifyAll();
            _userManagerMock.VerifyAll();
            _mapperMock.VerifyAll();
        }

        [TestMethod]
        public void UpdateUser_Success_ReturnNoContent()
        {
            _usersController.AddUserClaim(ClaimTypes.Role, UserRoles.Coach);

            const int userId = 19;
            var updatedUser = new UserUpdateRequest
            {
                Id = userId,
                FirstName = "Bob",
                LastName = "Belcher",
                OrganizationId = 2,
                UserRole = "Coach",
            };

            var user = new User { Id = userId, FirstName = "Robert", LastName = "Belcher", OrganizationId = 2 };

            _userRepoMock.Setup(x => x.GetUser(userId, true)).ReturnsAsync(user);
            _mapperMock.Setup(x => x.Map(updatedUser, user));
            _userManagerMock.Setup(x => x.GetRolesAsync(user)).ReturnsAsync(new[] { "Coach" });
            _userRepoMock.Setup(x => x.SaveAll()).ReturnsAsync(true);

            var actual = _usersController.UpdateUser(userId, updatedUser);

            Assert.IsInstanceOfType(actual.Result, typeof(NoContentResult));
        }

        [TestMethod]
        public void UpdateUser_FailToPersist_ThrowsException()
        {
            _usersController.AddUserClaim(ClaimTypes.Role, UserRoles.Coach);

            const int userId = 19;
            var updatedUser = new UserUpdateRequest
            {
                Id = userId,
                FirstName = "Bob",
                LastName = "Belcher",
                OrganizationId = 2,
                UserRole = "Coach",
            };

            var user = new User { Id = userId, FirstName = "Robert", LastName = "Belcher", OrganizationId = 2 };

            _userRepoMock.Setup(x => x.GetUser(userId, true)).ReturnsAsync(user);
            _mapperMock.Setup(x => x.Map(updatedUser, user));
            _userManagerMock.Setup(x => x.GetRolesAsync(user)).ReturnsAsync(new[] { "Coach" });
            _userRepoMock.Setup(x => x.SaveAll()).ReturnsAsync(false);

            Assert.ThrowsExceptionAsync<System.Exception>(() => _usersController.UpdateUser(userId, updatedUser));
        }

        [TestMethod]
        public void UpdateUser_UserIdDoesNotMatchRequest_ReturnsBadRequest()
        {
            const int userId = 19;
            var updatedUser = new UserUpdateRequest
            {
                Id = 37,
                FirstName = "Bob",
                LastName = "Belcher",
                OrganizationId = 2,
                UserRole = "Coach",
            };

            var user = new User { Id = userId, FirstName = "Robert", LastName = "Belcher", OrganizationId = 2 };

            var actual = _usersController.UpdateUser(userId, updatedUser);

            Assert.IsInstanceOfType(actual.Result, typeof(BadRequestResult));
        }

        [TestMethod]
        public void DeleteUser_CurrentUser_Coach_NotInOrganization_ReturnsUnauthorized()
        {
            _usersController.AddUserClaim(ClaimTypes.Role, UserRoles.Coach);
            _usersController.AddUserClaim("organizationId", 2);

            var user = new User { Id = 19, OrganizationId = 1 };
            _userRepoMock.Setup(x => x.GetUser(19, true)).ReturnsAsync(user);

            var actual = _usersController.DeleteUser(19);

            Assert.IsInstanceOfType(actual.Result, typeof(UnauthorizedObjectResult));
        }

        [TestMethod]
        public void DeleteUser_CurrentUser_Admin_NotInOrganization_ReturnsNoContent()
        {
            _usersController.AddUserClaim(ClaimTypes.Role, UserRoles.Admin);
            _usersController.AddUserClaim("organizationId", 2);

            var user = new User { Id = 19, OrganizationId = 1, UserRoles = new[] { new UserRole { RoleId = 1, UserId = 19 } } };
            _userRepoMock.Setup(x => x.GetUser(19, true)).ReturnsAsync(user);
            _userManagerMock.Setup(x => x.DeleteAsync(user)).ReturnsAsync(IdentityResult.Success);

            var actual = _usersController.DeleteUser(19);

            Assert.IsInstanceOfType(actual.Result, typeof(NoContentResult));
        }
    }
}
