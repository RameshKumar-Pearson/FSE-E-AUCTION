using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_auction.Business.Models
{
    public class ProductResponse
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ProductId { get; set; }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string SellerId { get; set; }
    }
}
