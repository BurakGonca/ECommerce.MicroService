namespace ECommerce.Discount.Dtos
{
    public class CreateDiscountCouponDto
    {
        
        public string Code { get; set; }
        public int Rate { get; set; } //kupon yuzdesi
        public bool IsActive { get; set; }
        public DateTime ValidDate { get; set; } //gecerlilik tarihi
    }
}
