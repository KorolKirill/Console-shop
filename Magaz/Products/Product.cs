namespace Magaz
{
    public class ProductInformation
    {
        public string Name { get; set; }
        public int Code { get; set; }

        public ProductInformation(string name)
        {
            Name = name;
        }

        public ProductInformation(int code)
        {
            Code = code;
        }

        public ProductInformation(string name = null, int code = default)
        {
            Name = name;
            Code = code;
        }
    }
    public abstract class Product
    {
        public string Name { get; private set; }
        public int Code { get; private set; }
        public Product(string name, int code)
        {
            Name = name;
            Code = code;
        }

        public override string ToString()
        {
            return $"name: {Name}, code: {Code.ToString()}";
        }
        public bool Equals(ProductInformation product)
        {
            if (product.Name == null)
            {
                return Code.Equals(product.Code);
            }
            return Name.ToLower().Equals(product.Name.ToLower()) || Code.Equals(product.Code);
        }

    }
    
    public class ProductData
    {
        public Product Product { get; set; }
        public decimal Quantity { get; set; }
        
        public ProductData( Product product, decimal quantity = 0)
        {
            Product = product;
            Quantity = quantity;
        }

        public override string ToString()
        {
            return $"{Product.ToString()}, quantity: {Quantity.ToString()}";
        }
    }
}