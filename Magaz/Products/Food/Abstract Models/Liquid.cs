namespace Magaz.Products.Food.Abstract_Models
{
    public abstract class Liquid : Food 
    {
        public decimal Gallons { get; set; }
        protected Liquid(string name, int code) : base(name, code)
        {
            Gallons = 1;
        }
        
        protected Liquid(string name, int code, decimal gallons) : base(name, code)
        {
            Gallons = gallons;
        }

        public override string ToString()
        {
            return base.ToString() + $", measurement: {Gallons.ToString()} gallons";
        }
    }
}