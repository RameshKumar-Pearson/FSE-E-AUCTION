using System;
using System.Threading.Tasks;
using BuyerApi.Controllers;
using E_auction.Business.Contract.CommandHandlers;
using E_auction.Business.Directors;
using E_auction.Business.Models;
using E_auction.Business.RequestModels;
using E_auction.Business.Validation;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace E_Auction.UnitTests.ControllerTests
{
    /// <summary>
    ///  Test class for <see cref="BuyerController"/>
    /// </summary>
    [TestClass]
    public class BuyerControllerTests
    {
        #region Private Variables

        private Mock<ITopicProducer<KafkaBuyerEventCreate>> _topicProducer;
        private Mock<IBuyerDirector> _mockBuyerDirector;
        private Mock<IBuyerValidation> _mockBuyerValidation;
        private Mock<ILogger<BuyerController>> _mockLogger;
        private Mock<ISaveBuyerCommandHandler> _mockSaveBuyerCommandHandler;
        private BuyerController _buyerController;

        #endregion

        #region Initialize

        /// <summary>
        ///     Test initialize
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            _mockBuyerDirector = new Mock<IBuyerDirector>();
            _mockBuyerValidation = new Mock<IBuyerValidation>();
            _mockLogger = new Mock<ILogger<BuyerController>>();
            _mockSaveBuyerCommandHandler = new Mock<ISaveBuyerCommandHandler>();
            _topicProducer = new Mock<ITopicProducer<KafkaBuyerEventCreate>>();
            _buyerController = new BuyerController(_mockBuyerDirector.Object, _topicProducer.Object,
                _mockBuyerValidation.Object, _mockLogger.Object, _mockSaveBuyerCommandHandler.Object);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// To verify AddBidAsync with valid date and returns success result
        /// </summary>
        /// <returns>Awaitable task with no data</returns>
        [TestMethod]
        public async Task AddBidAsync_WithValidData_ReturnsSuccessResult()
        {
            //Arrange
            var buyerRequest = new SaveBuyerRequestModel
            {
                City = "Tenkasi",
                Address = "3/4 north street",
                Email = "r@gmail.com",
                BidAmount = 1500,
                FirstName = "Ramesh",
                LastName = "Kumar",
                Phone = "6385132645",
                Pin = 627814,
                ProductId = Guid.NewGuid().ToString(),
                State = "Tamilnadu"
            };

            //Act 
            _mockBuyerValidation.Setup(x => x.BusinessValidationAsync(It.IsAny<SaveBuyerRequestModel>()))
                .ReturnsAsync(true);

            var actualResult = await _buyerController.AddBidAsync(buyerRequest);

            //Assert
            Assert.IsNotNull(actualResult);
        }

        /// <summary>
        /// To verify AddBidAsync within- valid date and throws bad request
        /// </summary>
        /// <returns>Awaitable task with no data</returns>
        [TestMethod]
        public async Task AddBidAsync_WithInValidData_Throws_BadRequest()
        {
            //Arrange
            var buyerRequest = new SaveBuyerRequestModel
            {
                City = "Tenkasi",
                Address = "3/4 north street",
                Email = "r@@gmail.com",
                BidAmount = 1500,
                FirstName = "Ramesh",
                LastName = "Kumar",
                Phone = "6385132645",
                Pin = 627814,
                ProductId = Guid.NewGuid().ToString(),
                State = "Tamilnadu"
            };

            var actualResult = await _buyerController.AddBidAsync(buyerRequest);

            //Assert
            Assert.IsNotNull(actualResult);
            Assert.IsInstanceOfType(actualResult, typeof(BadRequestObjectResult));
        }

        #endregion
    }
}