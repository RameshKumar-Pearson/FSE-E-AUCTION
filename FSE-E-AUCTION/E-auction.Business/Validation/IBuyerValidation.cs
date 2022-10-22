using E_auction.Business.ResponseModels;
using System.Threading.Tasks;
using SaveBuyerRequestModel = E_auction.Business.RequestModels.SaveBuyerRequestModel;

namespace E_auction.Business.Validation
{
    /// <summary>
    /// Interface class used to do the business and other validations against buyer details
    /// </summary>
    public interface IBuyerValidation
    {
        /// <summary>
        /// Method used to do the business validation of the buyer details
        /// </summary>
        /// <param name="saveBuyerRequestModel">Specifies to gets the <see cref="MongoBuyerResponse"/></param>
        /// <returns></returns>
        Task<bool> BusinessValidationAsync(SaveBuyerRequestModel saveBuyerRequestModel);
    }
}