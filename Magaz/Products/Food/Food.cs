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

    class Bread : Food
    {
        public Bread(string name) : base(name)
        {
        }

        public Bread(string name, int code) : base(name, code)
        {
        }
        
    }

    class Meat : Food
    {
        public Meat(string name) : base(name)
        {
        }

        public Meat(string name, int code) : base(name, code)
        {
        }
    }

    class Fish : Food
    {
        public Fish(string name) : base(name)
        {
        }

        public Fish(string name, int code) : base(name, code)
        {
        }
    }
}