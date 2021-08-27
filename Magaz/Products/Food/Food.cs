namespace Magaz.Products.Food
{
    public abstract class Food : Product
    {
        protected Food(string name) : base(name)
        {
        }

        protected Food(string name, int code) : base(name, code)
        {
            
        }
    }
}