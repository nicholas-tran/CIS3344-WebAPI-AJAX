using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5ClassLibrary
{
    public class TaxInformation
    {
        public int TaxInformationID { get; set; }
        public int HomeOwnershipID { get; set; }
        public decimal AccessedValue { get; set; }
        public decimal LandValue { get; set; }
        public decimal AdditionalValue { get; set; }
        public decimal TaxRate { get; set; }
        public decimal TaxAmount { get; set; }

        public TaxInformation()
        {

        }
    }
}
