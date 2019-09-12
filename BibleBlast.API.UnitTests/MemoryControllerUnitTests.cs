using System.Security.Claims;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BibleBlast.API.Controllers;
using Moq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using BibleBlast.API.DataAccess;
using Microsoft.AspNetCore.Http;
using System;
using BibleBlast.API.Models;
using System.Collections.Generic;

namespace BibleBlast.API.UnitTests
{
    [TestClass]
    public class MemoryControllerUnitTests
    {
        private const int _userId = 19;
        private Mock<IMemoryRepository> _memoryRepoMock;
        private Mock<IMapper> _mapperMock;
        private MemoriesController _memoriesController;

        [TestInitialize]
        public void Init()
        {
            _memoryRepoMock = new Mock<IMemoryRepository>(MockBehavior.Strict);
            _mapperMock = new Mock<IMapper>(MockBehavior.Strict);

            _memoriesController = new MemoriesController(_memoryRepoMock.Object, _mapperMock.Object);
            _memoriesController.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, _userId.ToString()) })) }
            };
        }

        [TestMethod]
        public void GetCompletedMemeories_InvalidDateRange_ReturnsInvalid()
        {
            _memoriesController.HttpContext.User.AddIdentity(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Role, UserRoles.Coach) }));

            var actual = _memoriesController.GetCompletedMemeories(new CompletedMemoryParams
            {
                FromDate = new DateTime(2019, 8, 23),
                ToDate = new DateTime(2018, 1, 1),
            }).Result;

            Assert.IsInstanceOfType(actual, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public void GetCompletedMemeories_ValidDateRange_ReturnsOk()
        {
            _memoriesController.HttpContext.User.AddIdentity(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Role, UserRoles.Coach) }));

            CompletedMemoryParams queryParams = new CompletedMemoryParams
            {
                FromDate = new DateTime(2019, 8, 23),
                ToDate = new DateTime(2020, 1, 1),
            };

            var kidMemories = new List<KidMemory> { new KidMemory { KidId = 324, MemoryId = 12, DateCompleted = new DateTime(2019, 8, 24), Memory = new Memory { Id = 12, CategoryId = 1 } } };

            _memoryRepoMock
                .Setup(x => x.GetCompletedMemories(queryParams))
                .ReturnsAsync(kidMemories);

            var actual = _memoriesController.GetCompletedMemeories(queryParams).Result;

            Assert.IsInstanceOfType(actual, typeof(OkObjectResult));
        }
    }
}
