using System.Text.RegularExpressions;
using E_auction.Business.Contract.CommandHandlers;
using E_auction.Business.Directors;
using E_auction.Business.RequestModels;
using E_auction.Business.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using E_auction.Business.Exception;

namespace BuyerApi.Controllers
{
    /// <summary>
    /// Controller used to manages the buyer operations
    /// </summary>
    [Route("/e-auction/api/v1/buyer")]
    [ApiController]
    public class BuyerController : Controller
    {
        private readonly IBuyerDirector _buyerDirector;
        private readonly IBuyerValidation _buyerValidation;
        private readonly ILogger<BuyerController> _logger;
        private readonly ISaveBuyerCommandHandler _saveBuyerCommandHandler;


        /// <summary>
        /// Constructor for <see cref="BuyerController"/>
        /// </summary>
        /// <param name="buyerDirector">Specifies to gets the object instance for <see cref="IBuyerDirector"/></param>
        /// <param name="buyerValidation">Specifies to gets  <see cref="IBuyerValidation"/></param>
        /// <param name="logger">Specifies to gets the <see cref="ILogger"/></param>
        /// <param name="saveBuyerCommandHandler">Specifies to gets<see cref="ISaveBuyerCommandHandler"/></param>
        public BuyerController(IBuyerDirector buyerDirector,
            IBuyerValidation buyerValidation, ILogger<BuyerController> logger,
            ISaveBuyerCommandHandler saveBuyerCommandHandler)
        {
            _buyerValidation = buyerValidation;
            _buyerDirector = buyerDirector;
            _logger = logger;
            _saveBuyerCommandHandler = saveBuyerCommandHandler;
        }

        #region Public Methods

        /// <summary>
        /// Method used to add the bid amount for existing product
        /// </summary>
        /// <param name="buyerDetails">Specifies to gets <see cref="SaveBuyerRequestModel"/></param>
        /// <returns>Awaitable task with no data</returns>
        [Route("place-bid")]
        [HttpPost]
        public async Task<IActionResult> AddBidAsync([FromBody] SaveBuyerRequestModel buyerDetails)
        {
            try
            {
                _logger.LogInformation($"Add bid to the product started");

                if (!Regex.IsMatch(buyerDetails.Email, @"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}"))
                    return BadRequest("Please enter correct email");

                if (!Regex.IsMatch(buyerDetails.Phone, @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$"))
                    return BadRequest("Invalid Phone Number");

                if (await _buyerValidation.BusinessValidationAsync(buyerDetails))
                    await _saveBuyerCommandHandler.AddBidAsync(buyerDetails);

                _logger.LogInformation($"Add bid to the product completed");

                return Ok("Add bid to the product completed");
            }
            catch (BuyerException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("update-bid/{productId}/{buyerEmailId}/{newBidAmount}")]
        [HttpPut]
        public Task UpdateBidAsync(string productId, string buyerEmailId, int newBidAmount)
        {
            _logger.LogInformation($"Update bid for the product started{productId}");

            var updateResult = _buyerDirector.UpdateBidAsync(productId, buyerEmailId, newBidAmount);

            _logger.LogInformation($"Update bid for the product completed{productId}");

            return updateResult;
        }

        #endregion

    }
}