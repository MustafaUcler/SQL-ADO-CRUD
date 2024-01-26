using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Databas
{
    //OrderItem lagrar och hantera information om enskilda artiklar som beställts och används bara för AddCustmerandOrder methoden
    public class OrderItem
    {
        // 'OrderId' representerar orderns ID.
        public int OrderId { get; set; }
        //  'ProductId' representerar produktens ID i ordern.
        public int ProductId { get; set; }
        // 'UnitPrice' representerar enhetspriset för produkten i ordern. 
        public decimal UnitPrice { get; set; }
        //  'Quantity' representerar antalet av produkten som beställts. 
        public int Quantity { get; set; }
        //  'Discount' representerar rabatten som tillämpas på produkten.
        public float Discount { get; set; }
    }

}
