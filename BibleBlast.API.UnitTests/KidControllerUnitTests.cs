using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Moq;
using BibleBlast.API.Controllers;
using BibleBlast.API.DataAccess;
using BibleBlast.API.Models;
using AutoMapper;
using System.Security.Claims;
using BibleBlast.API.Dtos;
using Microsoft.AspNetCore.Http;

namespace BibleBlast.API.UnitTests
{
    [TestClass]
    public class KidControllerUnitTests
    {
        private Mock<IKidRepository> _kidRepoMock;
        private Mock<IMapper> _mapperMock;
        private KidsController _kidsController;

        [TestInitialize]
        public void Init()
        {
            _kidRepoMock = new Mock<IKidRepository>(MockBehavior.Strict);
            _mapperMock = new Mock<IMapper>(MockBehavior.Strict);

            var user = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, "19") }));
            _kidsController = new KidsController(_kidRepoMock.Object, _mapperMock.Object);
            _kidsController.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };
        }

        [TestMethod]
        public void GetById_UserIsParent_ReturnsOk()
        {
            var kid = new Kid
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Parents = new[]
                {
                    new UserKid { UserId = 19 },
                    new UserKid { UserId = 20 }
                }
            };

            _kidRepoMock.Setup(x => x.GetKid(1)).Returns(Task.FromResult(kid));

            var kidDetail = new KidDetail
            {
                Id = kid.Id,
                FirstName = kid.FirstName,
                LastName = kid.LastName,
                Parents = new[]
                {
                    new UserDetail { Id = 19, FirstName = "Leia" },
                    new UserDetail { Id = 20, FirstName = "Han" }
                }
            };

            _mapperMock.Setup(x => x.Map<KidDetail>(kid)).Returns(kidDetail);

            var expected = new OkObjectResult(kidDetail);
            var actual = _kidsController.Get(1).Result;

            Assert.IsInstanceOfType(actual, typeof(OkObjectResult));
            Assert.AreEqual(expected.Value, ((OkObjectResult)actual).Value);
        }

        [TestMethod]
        public void GetById_UserIsNotParent_ReturnsUnauthorized()
        {
            var kid = new Kid
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Parents = new[]
                {
                    new UserKid { UserId = 34 },
                    new UserKid { UserId = 35 }
                }
            };

            _kidRepoMock.Setup(x => x.GetKid(1)).Returns(Task.FromResult(kid));

            var kidDetail = new KidDetail
            {
                Id = kid.Id,
                FirstName = kid.FirstName,
                LastName = kid.LastName,
                Parents = new[]
                {
                    new UserDetail { Id = 34, FirstName = "Fred" },
                    new UserDetail { Id = 35, FirstName = "Ethel" }
                }
            };

            _mapperMock.Setup(x => x.Map<KidDetail>(kid)).Returns(kidDetail);

            var expected = new UnauthorizedResult();
            var result = _kidsController.Get(1).Result;

            Assert.IsInstanceOfType(result, typeof(UnauthorizedResult));
        }
    }
}
