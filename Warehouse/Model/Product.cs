namespace Warehouse.Model
{
    internal class Product
    {
        public long id { get; set; }   
        public string title { get; set; }

        public Product()
        {
           
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
