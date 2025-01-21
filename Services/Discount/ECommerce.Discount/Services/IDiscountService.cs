using ECommerce.Discount.Dtos;

namespace ECommerce.Discount.Services
{
    public interface IDiscountService
    {
        Task<List<ResultDiscountCouponDto>> GetAllDiscountCouponAsync();
        Task CreateDiscountCouponAsync(CreateDiscountCouponDto createCouponDto);
        Task UpdateDiscountCouponAsync(UpdateDiscountCouponDto updateCouponDto);
        Task DeleteDiscountCouponAsync(int couponId);
        Task<GetByIdDiscountCouponDto> GetByIdDiscountCouponAsync(int couponId);

    }
}
