namespace Magaz.Products.Food
{
    public abstract class Food : Product
    {
        // Сюда можно дату создания, но тогда она будет у нас хранится для всей пачки продуктов с нашей реализации.
        protected Food(string name, int code) : base(name, code)
        {
            
        }
    }
}