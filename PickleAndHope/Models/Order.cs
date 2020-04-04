using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PickleAndHope.Models
{
    // POST api/orders/
    // PUT api/orders/{id}/items

    public class Order
    {
        public int Id { get; set; }
        public List<LineItem> Items { get;set; }
        public string Recipient { get; set; }
        public decimal TotalPrice { get; set; }
        public int UserId { get; set; }
    }

    public class LineItem
    {
        public string PickleType { get; set; }
        public int Quantity {get;set;}
        public string Message { get; set; }
    }
}
