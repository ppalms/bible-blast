using AutoMapper;
using BibleBlast.API.Controllers;
using BibleBlast.API.DataAccess;
using BibleBlast.API.Dtos;
using BibleBlast.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BibleBlast.API.UnitTests
{
    [TestClass]
    public class UsersControllerUnitTests
    {
        private UsersController _controller;
        private Mock<IUserRepository> _userRepoMock;
        private Mock<UserManager<User>> _userManagerMock;
        private Mock<IMapper> _mapperMock;

        [TestInitialize]
        public void Init()
        {
            _userRepoMock = new Mock<IUserRepository>();
            _userManagerMock = new Mock<UserManager<User>>(new Mock<IUserStore<User>>().Object, null, null, null, null, null, null, null, null);
            _mapperMock = new Mock<IMapper>();
            _controller = new UsersController(_userRepoMock.Object, _userManagerMock.Object, _mapperMock.Object);
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
            const int userId = 19;
            var updatedUser = new UserUpdateRequest
            {
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

            var actual = _controller.UpdateUser(userId, updatedUser);

            Assert.IsInstanceOfType(actual.Result, typeof(NoContentResult));
        }

        [TestMethod]
        public void UpdateUser_FailToPersist_ThrowsException()
        {
            const int userId = 19;
            var updatedUser = new UserUpdateRequest
            {
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

            Assert.ThrowsExceptionAsync<System.Exception>(() => _controller.UpdateUser(userId, updatedUser));
        }
    }
}
