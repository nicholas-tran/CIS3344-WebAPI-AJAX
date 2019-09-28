using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5ClassLibrary
{
    public class Home
    {
        public int HomeOwnershipID{ get; set; }
        public int OwnerID { get; set; }
        public int Block { get; set; }
        public int Lot { get; set; }
        public DateTime DateOfSale { get; set; }
        public decimal SalePrice { get; set; }
        public string Address { get; set; }

        public TaxInformation TaxInformation { get; set; }

        public Home()
        {

        }
    }
}
