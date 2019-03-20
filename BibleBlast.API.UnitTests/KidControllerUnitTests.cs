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
using System;

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

        [TestCleanup]
        public void Cleanup()
        {
            _kidRepoMock.VerifyAll();
            _mapperMock.VerifyAll();
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
        public void GetById_MemberUser_IsNotParent_ReturnsNotFound()
        {
            _kidRepoMock.Setup(x => x.GetKid(1, _userId)).ReturnsAsync((Kid)null);

            var result = _kidsController.Get(1).Result;

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
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
        public void GetCompletedMemories_NoMemories_ReturnsNotFound()
        {
            const int kidId = 324;

            _kidsController.HttpContext.User.AddIdentity(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Role, UserRoles.Admin) }));

            _kidRepoMock.Setup(x => x.GetCompletedMemories(kidId, _userId)).ReturnsAsync(Enumerable.Empty<KidMemory>());

            var actual = _kidsController.GetCompletedMemeories(kidId).Result;

            Assert.IsInstanceOfType(actual, typeof(NotFoundResult));
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

        [TestMethod]
        public void AddCompletedMemories_ReturnNoContent()
        {
            const int kidId = 29;

            var requestParams = new[] {
                new KidMemoryParams
                {
                    MemoryId= 23,
                    DateCompleted = new DateTime(2019, 3, 18)
                },
                new KidMemoryParams
                {
                    MemoryId= 24,
                    DateCompleted = new DateTime(2019, 3, 19)
                },
            };

            var kidMemories = new[]
            {
                new KidMemory { KidId = kidId, MemoryId = 23, DateCompleted = new DateTime(2019, 3, 18)},
                new KidMemory { KidId = kidId, MemoryId = 24, DateCompleted = new DateTime(2019, 3, 19)},
            };

            _mapperMock.Setup(x => x.Map<IEnumerable<KidMemory>>(requestParams)).Returns(kidMemories);

            _kidRepoMock.Setup(x => x.GetCompletedMemories(kidId, _userId)).ReturnsAsync((Enumerable.Empty<KidMemory>()));
            _kidRepoMock.Setup(x => x.AddCompletedMemories(kidMemories)).ReturnsAsync(true);

            var actual = _kidsController.AddCompletedMemories(kidId, requestParams).Result;

            Assert.IsInstanceOfType(actual, typeof(NoContentResult));
        }

        [TestMethod]
        public void AddCompletedMemories_KidMemoryExists_ReturnNoContent()
        {
            const int kidId = 29;

            var requestParams = new[] {
                new KidMemoryParams
                {
                    MemoryId= 23,
                    DateCompleted = new DateTime(2019, 3, 18)
                },
                new KidMemoryParams
                {
                    MemoryId= 24,
                    DateCompleted = new DateTime(2019, 3, 19)
                },
            };

            var kidMemories = new[]
            {
                new KidMemory { KidId = kidId, MemoryId = 23, DateCompleted = new DateTime(2019, 3, 18) },
                new KidMemory { KidId = kidId, MemoryId = 24, DateCompleted = new DateTime(2019, 3, 19) },
            };

            _mapperMock.Setup(x => x.Map<IEnumerable<KidMemory>>(requestParams)).Returns(kidMemories);

            _kidRepoMock.Setup(x => x.GetCompletedMemories(kidId, _userId))
                .ReturnsAsync(new[] { new KidMemory { KidId = kidId, MemoryId = 23, DateCompleted = new DateTime(2019, 3, 18) } });

            _kidRepoMock.Setup(x => x
                .AddCompletedMemories(new[] { new KidMemory { KidId = kidId, MemoryId = 24, DateCompleted = new DateTime(2019, 3, 19) } }))
                .ReturnsAsync(true);

            var actual = _kidsController.AddCompletedMemories(kidId, requestParams).Result;

            Assert.IsInstanceOfType(actual, typeof(NoContentResult));

            _kidRepoMock.Verify(x => x.GetCompletedMemories(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
            _kidRepoMock.Verify(x => x.AddCompletedMemories(It.IsAny<IEnumerable<KidMemory>>()), Times.Once);
        }

        [TestMethod]
        public void DeleteCompletedMemories_ReturnNoContent()
        {
            const int kidId = 29;

            var requestParams = new[] {
                new KidMemoryParams
                {
                    MemoryId= 23,
                    DateCompleted = new DateTime(2019, 3, 18)
                },
                new KidMemoryParams
                {
                    MemoryId= 24,
                    DateCompleted = new DateTime(2019, 3, 19)
                },
            };

            _kidRepoMock.Setup(x => x.DeleteCompletedMemories(kidId, new[] { 23, 24 })).ReturnsAsync(true);

            var actual = _kidsController.DeleteCompletedMemories(kidId, requestParams).Result;

            Assert.IsInstanceOfType(actual, typeof(NoContentResult));
        }
    }
}
