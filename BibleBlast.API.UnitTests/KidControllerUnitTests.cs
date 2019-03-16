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
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;

namespace BibleBlast.API.UnitTests
{
    [TestClass]
    public class KidControllerUnitTests
    {
        private const int _userId = 19;
        private Mock<IKidRepository> _kidRepoMock;
        private Mock<IMapper> _mapperMock;
        private KidsController _kidsController;

        [TestInitialize]
        public void Init()
        {
            _kidRepoMock = new Mock<IKidRepository>(MockBehavior.Strict);
            _mapperMock = new Mock<IMapper>(MockBehavior.Strict);

            var user = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, _userId.ToString()) }));
            _kidsController = new KidsController(_kidRepoMock.Object, _mapperMock.Object);
            _kidsController.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };
        }

        [TestMethod]
        public void GetById_MemberUser_IsParent_ReturnsOk()
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

            _kidRepoMock.Setup(x => x.GetKid(1, _userId)).Returns(Task.FromResult(kid));

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
        public void GetById_MemberUser_IsNotParent_ReturnsUnauthorized()
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

            _kidRepoMock.Setup(x => x.GetKid(1, _userId)).Returns(Task.FromResult(kid));

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

            var result = _kidsController.Get(1).Result;

            Assert.IsInstanceOfType(result, typeof(UnauthorizedResult));
        }

        [TestMethod]
        public void GetCompletedMemories_MemberUser_IsParent_HasMemories_ReturnsOk()
        {
            const int kidId = 324;

            var kidMemories = new Collection<KidMemory>
            {
                new KidMemory
                {
                    Memory = new Memory { Id = 1, Name = "The Lords Prayer", Description = "Matthew 6:9-13" },
                    Kid = new Kid { Id = 1, Parents = new [] { new UserKid { KidId = 1, UserId = _userId } } }
                },
                new KidMemory
                {
                    Memory = new Memory { Id = 2, Name = "The 12 Apostles", Description = "Matthew 10:2-4" },
                    Kid = new Kid { Id = 1, Parents = new [] { new UserKid { KidId = 1, UserId = _userId } } }
                },
                new KidMemory
                {
                    Memory = new Memory { Id = 3, Name = "The 4 Brothers of Jesus", Description = "Matthew 13:55" },
                    Kid = new Kid { Id = 1, Parents = new [] { new UserKid { KidId = 1, UserId = _userId } } }
                },
            };

            _kidRepoMock.Setup(x => x.GetCompletedMemories(kidId, _userId)).ReturnsAsync(kidMemories);

            var expected = new Collection<CompletedMemory>
            {
                new CompletedMemory { Name = "The Lords Prayer", Description = "Matthew 6:9-13" },
                new CompletedMemory { Name = "The 12 Apostles", Description = "Matthew 10:2-4" },
                new CompletedMemory { Name = "The 4 Brothers of Jesus", Description = "Matthew 13:55" },
            };

            _mapperMock.Setup(x => x.Map<IEnumerable<CompletedMemory>>(kidMemories)).Returns(expected);

            var actual = _kidsController.GetCompletedMemeories(kidId).Result;

            Assert.IsInstanceOfType(actual, typeof(OkObjectResult));
            Assert.IsInstanceOfType(((OkObjectResult)actual).Value, typeof(ICollection<CompletedMemory>));
            CollectionAssert.AreEquivalent(expected, ((OkObjectResult)actual).Value as Collection<CompletedMemory>);
        }

        [TestMethod]
        public void GetCompletedMemories_NoMemories_ReturnsEmpty()
        {
            const int kidId = 324;

            _kidsController.HttpContext.User.AddIdentity(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Role, UserRoles.Admin) }));

            _kidRepoMock.Setup(x => x.GetCompletedMemories(kidId, _userId)).ReturnsAsync(Enumerable.Empty<KidMemory>());

            _mapperMock.Setup(x => x.Map<IEnumerable<CompletedMemory>>(It.IsAny<IEnumerable<KidMemory>>())).Returns(new List<CompletedMemory>());

            var actual = _kidsController.GetCompletedMemeories(kidId).Result;

            Assert.IsInstanceOfType(actual, typeof(OkObjectResult));
            Assert.IsInstanceOfType(((OkObjectResult)actual).Value, typeof(ICollection<CompletedMemory>));
            Assert.AreEqual(0, (((OkObjectResult)actual).Value as ICollection<CompletedMemory>).Count);
        }

        [TestMethod]
        public void GetCompletedMemories_MemberUser_IsNotParent_ReturnsUnauthorized()
        {
            const int kidId = 324;

            var kidMemories = new Collection<KidMemory>
            {
                new KidMemory
                {
                    Memory = new Memory { Id = 1, Name = "The Lords Prayer", Description = "Matthew 6:9-13" },
                    Kid = new Kid { Id = 1, Parents = new [] { new UserKid { KidId = 1, UserId = 789 } } }
                },
                new KidMemory
                {
                    Memory = new Memory { Id = 2, Name = "The 12 Apostles", Description = "Matthew 10:2-4" },
                    Kid = new Kid { Id = 1, Parents = new [] { new UserKid { KidId = 1, UserId = 789 } } }
                },
            };

            _kidRepoMock.Setup(x => x.GetCompletedMemories(kidId, _userId)).ReturnsAsync(kidMemories);

            var actual = _kidsController.GetCompletedMemeories(kidId).Result;

            Assert.IsInstanceOfType(actual, typeof(UnauthorizedResult));
        }

        [TestMethod]
        public void GetCompletedMemories_CoachUser_IsNotParent_ReturnsOk()
        {
            const int kidId = 324;

            _kidsController.HttpContext.User.AddIdentity(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Role, UserRoles.Coach) }));

            var kidMemories = new Collection<KidMemory>
            {
                new KidMemory
                {
                    Memory = new Memory { Id = 1, Name = "The Lords Prayer", Description = "Matthew 6:9-13" },
                    Kid = new Kid { Id = 1, Parents = new [] { new UserKid { KidId = 1, UserId = 789 } } }
                },
                new KidMemory
                {
                    Memory = new Memory { Id = 2, Name = "The 12 Apostles", Description = "Matthew 10:2-4" },
                    Kid = new Kid { Id = 1, Parents = new [] { new UserKid { KidId = 1, UserId = 789 } } }
                },
            };

            _kidRepoMock.Setup(x => x.GetCompletedMemories(kidId, _userId)).ReturnsAsync(kidMemories);

            var expected = new Collection<CompletedMemory>
            {
                new CompletedMemory { Name = "The Lords Prayer", Description = "Matthew 6:9-13" },
                new CompletedMemory { Name = "The 12 Apostles", Description = "Matthew 10:2-4" },
            };

            _mapperMock.Setup(x => x.Map<IEnumerable<CompletedMemory>>(kidMemories)).Returns(expected);

            var actual = _kidsController.GetCompletedMemeories(kidId).Result;

            Assert.IsInstanceOfType(actual, typeof(OkObjectResult));
            Assert.IsInstanceOfType(((OkObjectResult)actual).Value, typeof(ICollection<CompletedMemory>));
            CollectionAssert.AreEquivalent(expected, ((OkObjectResult)actual).Value as Collection<CompletedMemory>);
        }
    }
}
