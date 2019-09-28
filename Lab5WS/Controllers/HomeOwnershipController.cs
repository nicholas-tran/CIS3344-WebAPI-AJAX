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
    public class HomeOwnershipController : Controller
    {
        // GET: api/HomeOwnership/GetAllHomes
        [HttpGet("GetAllHomes")]
        public List<Home> GetAllHomes()
        {
            DBConnect objDB = new DBConnect();
            string sqlCommand = "SELECT * FROM HomeOwnership";
            DataSet ds = objDB.GetDataSet(sqlCommand);
            List<Home> homes = new List<Home>();
            Home home;
            foreach (DataRow record in ds.Tables[0].Rows)
            {
                home = new Home();
                home.HomeOwnershipID = Convert.ToInt32(record["HomeOwnershipID"].ToString());
                home.OwnerID = Convert.ToInt32(record["OwnerID"].ToString());
                home.Address = record["Address"].ToString();
                home.Lot = Convert.ToInt32(record["Lot"].ToString());
                home.Block = Convert.ToInt32(record["Block"].ToString());
                home.DateOfSale = Convert.ToDateTime(record["DateOfSale"].ToString());
                home.SalePrice = Convert.ToDecimal(record["SalePrice"].ToString());
                homes.Add(home);
            }
            return homes;
        }

        // GET: api/HomeOwnership/GetAllHomeIDs
        [HttpGet("GetAllHomeIDs")]
        public List<Home> GetAllHomeIDs()
        {
            DBConnect objDB = new DBConnect();
            string sqlCommand = "SELECT HomeOwnershipID FROM HomeOwnership";
            DataSet ds = objDB.GetDataSet(sqlCommand);
            List<Home> homes = new List<Home>();
            Home home;
            foreach (DataRow record in ds.Tables[0].Rows)
            {
                home = new Home();
                home.HomeOwnershipID = Convert.ToInt32(record["HomeOwnershipID"].ToString());
                homes.Add(home);
            }
            return homes;
        }

        // GET: api/HomeOwnership/GetHomeOwnershipID/block/lot
        [HttpGet("GetHomeOwnershipID/{block}/{lot}")]
        public Home GetHomeOwnershipID(int block, int lot)
        {
            DBConnect objDB = new DBConnect();
            string sqlCommand = "SELECT HomeOwnershipID FROM HomeOwnership WHERE Block = '" + block + "' AND Lot = '" + lot + "'";
            DataSet ds = objDB.GetDataSet(sqlCommand);
            DataRow record;
            Home home = new Home();
            if (ds.Tables[0].Rows.Count != 0)
            {
                record = ds.Tables[0].Rows[0];
                home.HomeOwnershipID = Convert.ToInt32(record["HomeOwnershipID"].ToString());
            }
            return home;
        }

        // GET: api/HomeOwnership/GetAllHomeBlock
        [HttpGet("GetAllHomeBlock")]
        public List<Home> GetAllHomeBlock()
        {
            DBConnect objDB = new DBConnect();
            string sqlCommand = "SELECT Block FROM dbo.HomeOwnership GROUP BY Block";
            DataSet ds = objDB.GetDataSet(sqlCommand);
            List<Home> homes = new List<Home>();
            Home home;
            foreach (DataRow record in ds.Tables[0].Rows)
            {
                home = new Home();
                home.Block = Convert.ToInt32(record["Block"].ToString());
                homes.Add(home);
            }
            return homes;
        }

        // GET: api/HomeOwnership/GetAllHomeLot
        [HttpGet("GetAllHomeLot")]
        public List<Home> GetAllHomeLot()
        {
            DBConnect objDB = new DBConnect();
            string sqlCommand = "SELECT Lot FROM dbo.HomeOwnership GROUP BY Lot";
            DataSet ds = objDB.GetDataSet(sqlCommand);
            List<Home> homes = new List<Home>();
            Home home;
            foreach (DataRow record in ds.Tables[0].Rows)
            {
                home = new Home();
                home.Lot = Convert.ToInt32(record["Lot"].ToString());
                homes.Add(home);
            }
            return homes;
        }

        // GET: api/HomeOwnership/GetAllHomeAddress
        [HttpGet("GetAllHomeAddress")]
        public List<Home> GetAllHomeAddress()
        {
            DBConnect objDB = new DBConnect();
            string sqlCommand = "SELECT Address FROM dbo.HomeOwnership";
            DataSet ds = objDB.GetDataSet(sqlCommand);
            List<Home> homes = new List<Home>();
            Home home;
            foreach (DataRow record in ds.Tables[0].Rows)
            {
                home = new Home();
                home.Address = record["Address"].ToString();
                homes.Add(home);
            }
            return homes;
        }

        // GET: api/HomeOwnership/GetAllHomeOwners
        [HttpGet("GetAllHomeOwners")]
        public List<Owner> GetAllHomeOwners()
        {
            DBConnect objDB = new DBConnect();
            string sqlCommand = "SELECT OwnerID, OwnerName FROM Owner";
            DataSet ds = objDB.GetDataSet(sqlCommand);
            List<Owner> owners = new List<Owner>();
            Owner owner;
            foreach (DataRow record in ds.Tables[0].Rows)
            {
                owner = new Owner();
                owner.OwnerID = Convert.ToInt32(record["OwnerID"].ToString());
                owner.OwnerName = record["OwnerName"].ToString();
                owners.Add(owner);
            }
            return owners;
        }

        // GET: api/HomeOwnership/GetHome/homeOwnershipID
        [HttpGet("GetHome/{homeOwnershipID}")]
        public Home GetHome(int homeOwnershipID)
        {
            DBConnect objDB = new DBConnect();
            string sqlCommand = "SELECT * FROM HomeOwnership WHERE HomeOwnershipID = '" + homeOwnershipID + "'";
            DataSet ds = objDB.GetDataSet(sqlCommand);
            DataRow record;
            Home home = new Home();
            if (ds.Tables[0].Rows.Count != 0)
            {
                record = ds.Tables[0].Rows[0];
                home.HomeOwnershipID = Convert.ToInt32(record["HomeOwnershipID"].ToString());
                if (record["OwnerID"].ToString() == "")
                {
                    home.OwnerID = -1;
                }
                else
                {
                    home.OwnerID = Convert.ToInt32(record["OwnerID"].ToString());
                }
                home.Address = record["Address"].ToString();
                home.Lot = Convert.ToInt32(record["Lot"].ToString());
                home.Block = Convert.ToInt32(record["Block"].ToString());
                home.DateOfSale = Convert.ToDateTime(record["DateOfSale"].ToString());
                home.SalePrice = Convert.ToDecimal(record["SalePrice"].ToString());
            }
            return home;
        }

        // POST: api/HomeOwnership/AddHome
        [HttpPost("AddHome")]
        public Boolean AddHome([FromBody] Home home)
        {
            Home checkHome = new Home();
            checkHome.Lot = Convert.ToInt32(home.Lot.ToString());
            checkHome.Block = Convert.ToInt32(home.Block.ToString());
            DBConnect objDB = new DBConnect();
            string sqlCommand = "SELECT Lot, Block FROM HomeOwnership WHERE Lot = '" + checkHome.Lot + "' AND Block = '" + checkHome.Block + "'";
            DataSet ds;
            ds = objDB.GetDataSet(sqlCommand);
            if (ds.Tables[0].Rows.Count != 0)
            {
                if (ds.Tables[0].Rows[0]["Lot"].ToString() == checkHome.Lot.ToString() && ds.Tables[0].Rows[0]["Block"].ToString() == checkHome.Block.ToString())
                {
                    return false;
                }
            }
            Home newHome = new Home();
            newHome.Lot = Convert.ToInt32(home.Lot.ToString());
            newHome.Block = Convert.ToInt32(home.Block.ToString());
            newHome.Address = home.Lot + home.Block + " " + home.Address.ToString();
            objDB = new DBConnect();
            sqlCommand = "INSERT INTO HomeOwnership (Lot, Block, Address) VALUES ('" + newHome.Lot + "', '" + newHome.Block + "', '" + newHome.Address + "')";
            if (objDB.DoUpdate(sqlCommand) > 0)
            {
                sqlCommand = "INSERT INTO TaxInformation DEFAULT VALUES";
                if (objDB.DoUpdate(sqlCommand) > 0)
                {
                    sqlCommand = "UPDATE TaxInformation SET HomeOwnershipID = TaxInformationID WHERE TaxInformationID IN (SELECT max(TaxInformationID) FROM TaxInformation)";
                    if (objDB.DoUpdate(sqlCommand) > 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        // PUT: api/HomeOwnership/UpdateHome
        [HttpPut("UpdateHome")]
        public Boolean UpdateHome([FromBody] Home home)
        {
            Home updateHome = new Home();
            updateHome.HomeOwnershipID = Convert.ToInt32(home.HomeOwnershipID.ToString());
            updateHome.OwnerID = Convert.ToInt32(home.OwnerID.ToString());
            updateHome.DateOfSale = Convert.ToDateTime(home.DateOfSale.ToString());
            updateHome.SalePrice = Convert.ToDecimal(home.SalePrice.ToString());
            DBConnect objDB = new DBConnect();
            string sqlCommand = "UPDATE HomeOwnership SET OwnerID = '" + updateHome.OwnerID + "', DateOfSale ='" + DateTime.Today + "', SalePrice = '" + updateHome.SalePrice + "'WHERE HomeOwnershipID= '" + updateHome.HomeOwnershipID + "'";
            if (objDB.DoUpdate(sqlCommand) > 0)
            {
                return true;
            }
            return false;
        }

        // DELETE: api/HomeOwnership/DeleteHome
        [HttpDelete("DeleteHome")]
        public Boolean DeleteHome([FromBody] Home home)
        {
            Home deleteHome = new Home();
            deleteHome.HomeOwnershipID = home.HomeOwnershipID;
            DBConnect objDB = new DBConnect();
            string sqlCommand = "DELETE FROM HomeOwnership WHERE HomeOwnershipID = '" + deleteHome.HomeOwnershipID + "'";
            if (objDB.DoUpdate(sqlCommand) > 0)
            {
                return true;
            }
            return false;
        }
    }

}
