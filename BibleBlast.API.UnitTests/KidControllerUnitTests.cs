using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Moq;
using BibleBlast.API.Controllers;
using BibleBlast.API.DataAccess;
using BibleBlast.API.Models;

namespace BibleBlast.API.UnitTests
{
    [TestClass]
    public class KidControllerUnitTests
    {
        private Mock<IKidRepository> _kidRepoMock;
        private KidsController _kidsController;

        [TestInitialize]
        public void Init()
        {
            _kidRepoMock = new Mock<IKidRepository>(MockBehavior.Strict);
            _kidsController = new KidsController(_kidRepoMock.Object);
        }

        [TestMethod]
        public void GetById_ReturnsOk()
        {
            var kid = new Kid
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe"
            };

            _kidRepoMock.Setup(x => x.GetKid(1)).Returns(Task.FromResult(kid));

            var expected = new OkObjectResult(kid);

            var actual = _kidsController.Get(1).Result;
            Assert.IsInstanceOfType(actual, typeof(OkObjectResult));
            Assert.AreEqual(expected.Value, ((OkObjectResult)actual).Value);
        }
    }
}
