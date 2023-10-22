namespace Warehouse.DTO
{
    internal class ComboBoxDTO
    {
        public long id { get; set; }
        public string name { get; set; }

        public override string ToString()
        {
            return name;
        }
    }
}
