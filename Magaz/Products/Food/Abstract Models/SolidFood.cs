namespace Magaz.Products.Food
{
    public abstract class SolidFood : Food
    {

        //todo добавить проверку на > 0 
        public decimal Kilograms { get; set; }
        
        protected SolidFood(string name, int code) : base(name, code)
        {
            Kilograms = 1;
        }

        protected SolidFood(string name, int code, decimal kilograms) : base(name, code)
        {
            Kilograms = kilograms;
        }

        public override string ToString()
        {
            return string.Concat(base.ToString(), $", measurement: {Kilograms.ToString()} kilo");
        }
    }
}