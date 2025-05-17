using System;

namespace BusinessEntities
{
    public class Product : IdObject
    {
        private string _name;
        private decimal _price;
        private int _stock;

        public string Name
        {
            get => _name;
            private set => _name = value;
        }

        public decimal Price
        {
            get => _price;
            private set => _price = value;
        }

        public int Stock
        {
            get => _stock;
            private set => _stock = value;
        }

        public void SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name), "Product name was not provided.");
            _name = name;
        }

        public void SetPrice(decimal price)
        {
            if (price < 0)
                throw new ArgumentOutOfRangeException(nameof(price), "Price cannot be negative.");
            _price = price;
        }

        public void SetStock(int stock)
        {
            if (stock < 0)
                throw new ArgumentOutOfRangeException(nameof(stock), "Stock cannot be negative.");
            _stock = stock;
        }
    }
}