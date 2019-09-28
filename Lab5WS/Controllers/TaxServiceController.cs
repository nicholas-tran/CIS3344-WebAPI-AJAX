using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Lab5ClassLibrary;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Utilities;

namespace Lab5WS.Controllers
{
    //[Produces("application/json")]
    [Route("api/[controller]")]
    public class TaxServiceController : Controller
    {
        // GET: api/TaxService/GetTaxInformation/block/lot
       [HttpGet("GetTaxInformation/{block}/{lot}")]
       public TaxInformation GetTaxInformation(int block, int lot)
        {
            DBConnect objDB = new DBConnect();
            string sqlCommand = "SELECT * FROM TaxInformation JOIN HomeOwnership ON TaxInformation.HomeOwnershipID = HomeOwnership.HomeOwnershipID WHERE Block = '" + block + "'AND Lot = '" + lot + "'";
            DataSet ds = objDB.GetDataSet(sqlCommand);
            DataRow record;
            TaxInformation taxInformation = new TaxInformation();
            if (ds.Tables[0].Rows.Count != 0)
            {
                record = ds.Tables[0].Rows[0];
                taxInformation.TaxInformationID = Convert.ToInt32(record["TaxInformationID"].ToString());
                taxInformation.HomeOwnershipID = Convert.ToInt32(record["HomeOwnershipID"].ToString());
                taxInformation.AccessedValue = Convert.ToDecimal(record["AccessedValue"].ToString());
                taxInformation.AdditionalValue = Convert.ToDecimal(record["AdditionalValue"].ToString());
                taxInformation.LandValue = Convert.ToDecimal(record["LandValue"].ToString());
                taxInformation.TaxAmount = Convert.ToDecimal(record["TaxAmount"].ToString());
                taxInformation.TaxRate = Convert.ToDecimal(record["TaxRate"].ToString());
            }
            return taxInformation;
        }

        // GET: api/TaxService/GetTaxInformationByAddress/address
        [HttpGet("GetTaxInformationByAddress/{address}")]
        public TaxInformation GetTaxInformationByAddress(string address)
        {
            DBConnect objDB = new DBConnect();
            string sqlCommand = "SELECT * FROM TaxInformation JOIN HomeOwnership ON TaxInformation.HomeOwnershipID = HomeOwnership.HomeOwnershipID WHERE Address = '" + address + "'";
            DataSet ds = objDB.GetDataSet(sqlCommand);
            DataRow record;
            TaxInformation taxInformation = new TaxInformation();
            if (ds.Tables[0].Rows.Count != 0)
            {
                record = ds.Tables[0].Rows[0];
                taxInformation.TaxInformationID = Convert.ToInt32(record["TaxInformationID"].ToString());
                taxInformation.HomeOwnershipID = Convert.ToInt32(record["HomeOwnershipID"].ToString());
                taxInformation.AccessedValue = Convert.ToDecimal(record["AccessedValue"].ToString());
                taxInformation.AdditionalValue = Convert.ToDecimal(record["AdditionalValue"].ToString());
                taxInformation.LandValue = Convert.ToDecimal(record["LandValue"].ToString());
                taxInformation.TaxAmount = Convert.ToDecimal(record["TaxAmount"].ToString());
                taxInformation.TaxRate = Convert.ToDecimal(record["TaxRate"].ToString());
            }
            return taxInformation;
        }

        // GET: api/TaxService/GetHomeandTaxInformationByOwnerName/ownerName
        [HttpGet("GetHomeandTaxInformationByOwnerName/{ownerName}")]
        public List<Home> GetHomeandTaxInformationByOwnerName(string ownerName)
        {

            DBConnect objDB = new DBConnect();
            string sqlCommand = "SELECT * FROM HomeOwnership JOIN TaxInformation ON HomeOwnerShip.HomeOwnershipID = TaxInformation.HomeOwnershipID JOIN Owner ON HomeOwnerShip.OwnerID = Owner.OwnerID WHERE OwnerName = '" + ownerName + "'";
            DataSet ds = objDB.GetDataSet(sqlCommand);
            List<Home> homes = new List<Home>();
            Home home;
            TaxInformation taxInformation = new TaxInformation();
            foreach (DataRow record in ds.Tables[0].Rows)
            {
                home = new Home();
                taxInformation = new TaxInformation();
                home.HomeOwnershipID = Convert.ToInt32(record["HomeOwnershipID"].ToString());
                home.OwnerID = Convert.ToInt32(record["OwnerID"].ToString());
                home.Address = record["Address"].ToString();
                home.Lot = Convert.ToInt32(record["Lot"].ToString());
                home.Block = Convert.ToInt32(record["Block"].ToString());
                home.DateOfSale = Convert.ToDateTime(record["DateOfSale"].ToString());
                home.SalePrice = Convert.ToDecimal(record["SalePrice"].ToString());
                home.TaxInformation = taxInformation;
                home.TaxInformation.TaxInformationID = Convert.ToInt32(record["TaxInformationID"].ToString());
                home.TaxInformation.HomeOwnershipID = Convert.ToInt32(record["HomeOwnershipID"].ToString());
                home.TaxInformation.AccessedValue = Convert.ToDecimal(record["AccessedValue"].ToString());
                home.TaxInformation.AdditionalValue = Convert.ToDecimal(record["AdditionalValue"].ToString());
                home.TaxInformation.LandValue = Convert.ToDecimal(record["LandValue"].ToString());
                home.TaxInformation.TaxAmount = Convert.ToDecimal(record["TaxAmount"].ToString());
                home.TaxInformation.TaxRate = Convert.ToDecimal(record["TaxRate"].ToString());
                homes.Add(home);
            }
            return homes;
        }
        // PUT: api/TaxService/UpdateHomeValues
        [HttpPut("UpdateHomeValues")]
        public Boolean UpdateHomeValues([FromBody] Home home)
        {
            DBConnect objDB = new DBConnect();
            string sqlCommand = "UPDATE TaxInformation SET AccessedValue = '" + home.TaxInformation.AccessedValue + "', LandValue= '" + home.TaxInformation.LandValue + "', AdditionalValue= '" + home.TaxInformation.AdditionalValue + "'";  
            if(objDB.DoUpdate(sqlCommand) > 0)
            {
                return true;
            }
            return false;
        }
        // PUT: api/TaxService/UpdateTaxAmount
        [HttpPut("UpdateTaxAmount")]
        public Boolean UpdateTaxAmount([FromBody] Home home)
        {
            DBConnect objDB = new DBConnect();
            string sqlCommand = "SELECT * FROM TaxInformation WHERE HomeOwnershipID = '" + home.HomeOwnershipID + "'";
            DataSet ds = objDB.GetDataSet(sqlCommand);
            TaxInformation taxInformation = new TaxInformation();
            DataRow record;
            if (ds.Tables[0].Rows.Count != 0)
            {
                record = ds.Tables[0].Rows[0];
                taxInformation.TaxInformationID = Convert.ToInt32(record["TaxInformationID"].ToString());
                taxInformation.HomeOwnershipID = Convert.ToInt32(record["HomeOwnershipID"].ToString());
                taxInformation.AccessedValue = Convert.ToDecimal(record["AccessedValue"].ToString());
                taxInformation.AdditionalValue = Convert.ToDecimal(record["AdditionalValue"].ToString());
                taxInformation.LandValue = Convert.ToDecimal(record["LandValue"].ToString());
                taxInformation.TaxAmount = Convert.ToDecimal(record["TaxAmount"].ToString());
                taxInformation.TaxRate = Convert.ToDecimal(record["TaxRate"].ToString());
            }
            decimal taxAmount = (taxInformation.AccessedValue + taxInformation.AdditionalValue + taxInformation.LandValue) * taxInformation.TaxRate;
            taxInformation.TaxAmount = taxAmount;
            objDB = new DBConnect();
            sqlCommand = "UPDATE TaxInformation SET TaxAmount = '" + taxInformation.TaxAmount + "'";
            if(objDB.DoUpdate(sqlCommand) > 0)
            {
                return true;
            }
            return false;
        }
    }
}
