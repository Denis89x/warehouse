using System.Collections.Generic;
using System.Collections.Specialized;

namespace Warehouse.Storage
{
    internal class ComboBoxOrder
    {
        public string Title { get; set; }
        public int Quantity { get; set; }

        public static OrderedDictionary dicrtionaryWithId1 = new OrderedDictionary(); 
        public static Dictionary<string, int> dicrtionaryWithName = new Dictionary<string, int>(); 
    }
}
