using BuyerApi.RequestModels;
using BuyerApi.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerApi.Validation
{
    /// <summary>
    /// Interface class used to do the business and other validations against buyer details
    /// </summary>
    public interface IBuyerValidation
    {
        /// <summary>
        /// Method used to do the business vaidation of the buyer details
        /// </summary>
        /// <param name="saveBuyerRequestModel">Specifies to gets the <see cref="MongoBuyerResponse"/></param>
        /// <returns></returns>
        Task<bool> BusinessValidation(MongoBuyerResponse saveBuyerRequestModel);
    }
}
