using System;
using System.Threading.Tasks;
using E_auction.Business.Contract.QueryHandlers;
using E_auction.Business.Directors;
using E_auction.Business.Exception;
using E_auction.Business.MessagePublishers;
using E_auction.Business.Models;
using E_auction.Business.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SellerApi.Controllers;

namespace e_auction_seller.Controller
{
    /// <summary>
    ///     Test class for <see cref="SellerController" />
    /// </summary>
    [TestClass]
    public class SellerControllerTests
    {
        #region Private Variables

        private Mock<ISellerDirector> _sellerDirector;
        private Mock<IMessagePublisher> _messagePublisher;
        private Mock<IQueryHandler> _iqueryHandler;
        private Mock<ISellerValidation> _isellerValidation;
        private SellerController _sellerController;
        private Mock<ILogger<SellerController>> _loggerMock;

        #endregion

        #region Initialize

        /// <summary>
        ///     Test initialize
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            _loggerMock = new Mock<ILogger<SellerController>>();
            _sellerDirector = new Mock<ISellerDirector>();
            _messagePublisher = new Mock<IMessagePublisher>();
            _iqueryHandler = new Mock<IQueryHandler>();
            _isellerValidation = new Mock<ISellerValidation>();
            _sellerController = new SellerController(_sellerDirector.Object, _iqueryHandler.Object,
                _isellerValidation.Object, _messagePublisher.Object, _loggerMock.Object);
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     To verify AddProductAsync with invalid category and throws bad request
        /// </summary>
        /// <returns>Awaitable task with no data</returns>
        [TestMethod]
        public async Task AddProductAsync_WithInvalidCategory_Throws_Bad_Request()
        {
            //Arrange
            var product = new ProductDetails
            {
                FirstName = "test456",
                LastName = "test789",
                ShortDescription = "s-description",
                DetailedDescription = "d-description",
                Category = "painting-123",
                City = "tenkasi",
                Address = "3/4 north street",
                BidEndDate = DateTime.Now,
                Email = "r@gmail.com",
                Name = "Vara-Product",
                StartingPrice = 1500,
                State = "test4",
                Phone = "6385132645"
            };

            //Act
            var actualResult = await _sellerController.AddProductAsync(product).ConfigureAwait(false);

            //Assert
            Assert.IsNotNull(actualResult);
            Assert.IsInstanceOfType(actualResult, typeof(BadRequestObjectResult));
        }


        /// <summary>
        ///     To verify AddProductAsync with invalid email and throws bad request
        /// </summary>
        /// <returns>Awaitable task with no data</returns>
        [TestMethod]
        public async Task AddProductAsync_WithInvalidEmail_Throws_Bad_Request()
        {
            //Arrange
            var product = new ProductDetails
            {
                FirstName = "test456",
                LastName = "test789",
                ShortDescription = "s-description",
                DetailedDescription = "d-description",
                Category = "painting",
                City = "tenkasi",
                Address = "3/4 north street",
                BidEndDate = DateTime.Now,
                Email = "r@@gmail.com",
                Name = "Vara-Product",
                StartingPrice = 1500,
                State = "test4",
                Phone = "6385132645"
            };

            //Act
            var actualResult = await _sellerController.AddProductAsync(product).ConfigureAwait(false);

            //Assert
            Assert.IsNotNull(actualResult);
            Assert.IsInstanceOfType(actualResult, typeof(BadRequestObjectResult));
        }

        /// <summary>
        ///     To verify AddProductAsync with invalid phone and throws bad request
        /// </summary>
        /// <returns>Awaitable task with no data</returns>
        [TestMethod]
        public async Task AddProductAsync_WithInvalidPhone_Throws_Bad_Request()
        {
            //Arrange
            var product = new ProductDetails
            {
                FirstName = "test456",
                LastName = "test789",
                ShortDescription = "s-description",
                DetailedDescription = "d-description",
                Category = "painting",
                City = "tenkasi",
                Address = "3/4 north street",
                BidEndDate = DateTime.Now,
                Email = "r@gmail.com",
                Name = "Vara-Product",
                StartingPrice = 1500,
                State = "test4",
                Phone = "6385132645123"
            };

            //Act
            var actualResult = await _sellerController.AddProductAsync(product).ConfigureAwait(false);

            //Assert
            Assert.IsNotNull(actualResult);
            Assert.IsInstanceOfType(actualResult, typeof(BadRequestObjectResult));
        }

        /// <summary>
        ///     To verify AddProductAsync with invalid bid end date and throws bad request
        /// </summary>
        /// <returns>Awaitable task with no data</returns>
        [TestMethod]
        public async Task AddProductAsync_WithInvalidBidEndDate_Throws_Bad_Request()
        {
            //Arrange
            var product = new ProductDetails
            {
                FirstName = "test456",
                LastName = "test789",
                ShortDescription = "s-description",
                DetailedDescription = "d-description",
                Category = "painting",
                City = "tenkasi",
                Address = "3/4 north street",
                BidEndDate = DateTime.Now,
                Email = "r@gmail.com",
                Name = "Vara-Product",
                StartingPrice = 1500,
                State = "test4",
                Phone = "6385132645"
            };

            _isellerValidation.Setup(x => x.IsValidProductAsync(It.IsAny<ProductDetails>()))
                .ThrowsAsync(new SellerException("Bid end date should be future date"));

            //Act
            var actualResult = await _sellerController.AddProductAsync(product);

            //Assert
            Assert.IsNotNull(actualResult);
            Assert.IsInstanceOfType(actualResult, typeof(BadRequestObjectResult));
        }

        /// <summary>
        ///     To verify AddProductAsync with valid product details and returns ok result
        /// </summary>
        /// <returns>Awaitable task with no data</returns>
        [TestMethod]
        public async Task AddProductAsync_WithValidData_Returns_OkResult()
        {
            //Arrange
            var product = new ProductDetails
            {
                FirstName = "test456",
                LastName = "test789",
                ShortDescription = "s-description",
                DetailedDescription = "d-description",
                Category = "painting",
                City = "tenkasi",
                Address = "3/4 north street",
                BidEndDate = DateTime.Now,
                Email = "r@gmail.com",
                Name = "Vara-Product",
                StartingPrice = 1500,
                State = "test4",
                Phone = "6385132645"
            };

            _isellerValidation.Setup(x => x.IsValidProductAsync(It.IsAny<ProductDetails>()))
                .ReturnsAsync(true);

            //Act
            var actualResult = await _sellerController.AddProductAsync(product);

            //Assert
            Assert.IsNotNull(actualResult);
            Assert.IsInstanceOfType(actualResult, typeof(OkObjectResult));
        }


        /// <summary>
        ///     To verify DeleteProductAsync with valid product and returns ok result
        /// </summary>
        /// <returns>Awaitable task with no data</returns>
        [TestMethod]
        public async Task DeleteProductAsync_WithInvalidBidEndDate_Returns_OkResult()
        {
            //Arrange
            var productId = Guid.NewGuid().ToString();
            
            _messagePublisher.Setup(x => x.PublisherAsync(It.IsAny<Task>())).Returns(Task.CompletedTask);

            //Act
            var actualResult = await _sellerController.DeleteProductAsync(productId);
           
            //Assert
            Assert.IsNotNull(actualResult);
            Assert.IsInstanceOfType(actualResult, typeof(OkObjectResult));
        }

        #endregion
    }
}