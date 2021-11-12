using System;
using System.Threading.Tasks;
using Discount.Grpc.Protos;

namespace Basket.Api.GrpcServvices
{
    public class DiscountGrpcService
    {
        private readonly DiscountProtoService.DiscountProtoServiceClient _serviceClient;

        public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient serviceClient)
        {
            _serviceClient = serviceClient ?? throw new ArgumentNullException(nameof(serviceClient));
        }

        public async Task<CouponModel> GetDiscount(string prductName)
        {
            var request = new GetDiscountRequest { ProductName = prductName };
            return await _serviceClient.GetDiscountAsync(request);
        }
    }
}
