using System;

namespace BusinessEntities
{
    public class Order : IdObject
    {
        private string _customerName;
        private Guid _productId;
        private int _quantity;
        private DateTime _orderDate;

        public string CustomerName
        {
            get => _customerName;
            private set => _customerName = value;
        }

        public Guid ProductId
        {
            get => _productId;
            private set => _productId = value;
        }

        public int Quantity
        {
            get => _quantity;
            private set => _quantity = value;
        }

        public DateTime OrderDate
        {
            get => _orderDate;
            private set => _orderDate = value;
        }

        public void SetCustomerName(string customerName)
        {
            if (string.IsNullOrEmpty(customerName))
                throw new ArgumentNullException(nameof(customerName), "Customer name was not provided.");
            _customerName = customerName;
        }

        public void SetProductId(Guid productId)
        {
            if (productId == Guid.Empty)
                throw new ArgumentException("ProductId must be a valid GUID.", nameof(productId));
            _productId = productId;
        }

        public void SetQuantity(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be greater than zero.");
            _quantity = quantity;
        }

        public void SetOrderDate(DateTime orderDate)
        {
            _orderDate = orderDate;
        }
    }
}