using Eshop.Domain.SeedWork;

namespace Eshop.Domain.Orders.Rules
{
    public class OrderProductPriceHasToBeInLimit : IBusinessRule
    {
        private const int MaxPrice = 15000;
        private readonly List<OrderProduct> _orderProducts;

        public OrderProductPriceHasToBeInLimit(List<OrderProduct> orderProducts)
        {
            _orderProducts = orderProducts;
        }

        public bool IsBroken() {
            return _orderProducts.Sum(product => product.TotalCost) > MaxPrice;
        }

        public string Message => "Total price of products in a single order can't be higher than " + MaxPrice;
    }
}