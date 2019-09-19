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
using System.Collections.Generic;
using System;

namespace BibleBlast.API.UnitTests
{
    [TestClass]
    public class KidControllerUnitTests
    {
        private const int _userId = 19;
        private Mock<IKidRepository> _kidRepoMock;
        private Mock<IMemoryRepository> _memoryRepoMock;
        private Mock<IMapper> _mapperMock;
        private KidsController _kidsController;

        [TestInitialize]
        public void Init()
        {
            _kidRepoMock = new Mock<IKidRepository>(MockBehavior.Strict);
            _memoryRepoMock = new Mock<IMemoryRepository>(MockBehavior.Strict);
            _mapperMock = new Mock<IMapper>(MockBehavior.Strict);

            _kidsController = new KidsController(_kidRepoMock.Object, _memoryRepoMock.Object, _mapperMock.Object);
            _kidsController.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, _userId.ToString()) })) }
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

            _kidRepoMock.Setup(x => x.GetKidWithChildEntities(1)).Returns(Task.FromResult(kid));

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
            _kidRepoMock.Setup(x => x.GetKidWithChildEntities(1)).ReturnsAsync((Kid)null);

            var result = _kidsController.Get(1).Result;

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void UpsertCompletedMemories_AsMember_ReturnsBadRequest()
        {
            const int kidId = 29;

            var requestParams = new[] {
                new KidMemoryRequest
                {
                    MemoryId= 23,
                    DateCompleted = new DateTime(2019, 3, 18)
                },
                new KidMemoryRequest
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

            _kidsController.AddUserClaim(ClaimTypes.Role, UserRoles.Member);

            var actual = _kidsController.UpsertCompletedMemories(kidId, requestParams).Result;

            Assert.IsInstanceOfType(actual, typeof(BadRequestResult));
        }

        [TestMethod]
        public void UpsertCompletedMemories_AsCoach_ReturnsNoContent()
        {
            const int kidId = 29;

            var requestParams = new[] {
                new KidMemoryRequest
                {
                    MemoryId= 23,
                    DateCompleted = new DateTime(2019, 3, 18)
                },
                new KidMemoryRequest
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

            _kidRepoMock.Setup(x => x.GetKid(kidId)).ReturnsAsync(new Kid { Id = kidId });

            _kidRepoMock.Setup(x => x.UpsertCompletedMemories(kidMemories)).ReturnsAsync(true);

            _kidsController.AddUserClaim(ClaimTypes.Role, UserRoles.Coach);

            var actual = _kidsController.UpsertCompletedMemories(kidId, requestParams).Result;

            Assert.IsInstanceOfType(actual, typeof(NoContentResult));
        }

        [TestMethod]
        public void DeleteCompletedMemories_ReturnNoContent()
        {
            const int kidId = 29;

            var requestParams = new[] {
                new KidMemoryRequest
                {
                    MemoryId= 23,
                    DateCompleted = new DateTime(2019, 3, 18)
                },
                new KidMemoryRequest
                {
                    MemoryId= 24,
                    DateCompleted = new DateTime(2019, 3, 19)
                },
            };

            _kidRepoMock.Setup(x => x.DeleteCompletedMemories(kidId, new[] { 23, 24 })).ReturnsAsync(true);
            _kidRepoMock.Setup(x => x.GetKid(kidId)).ReturnsAsync(new Kid { Id = kidId, FirstName = "Roland", LastName = "Deschain" });

            var actual = _kidsController.DeleteCompletedMemories(kidId, requestParams).Result;

            Assert.IsInstanceOfType(actual, typeof(NoContentResult));
        }
    }
}
