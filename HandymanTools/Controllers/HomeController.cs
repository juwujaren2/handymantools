using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Data;
using System.Data.SqlClient;

namespace HandymanTools.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index ()
		{
			var mvcName = typeof(Controller).Assembly.GetName ();
			var isMono = Type.GetType ("Mono.Runtime") != null;

			ViewData ["Version"] = mvcName.Version.Major + "." + mvcName.Version.Minor + "." + mvcName.Version.Revision;
			ViewData ["Runtime"] = isMono ? "Mono" : ".NET";
			ViewData ["UserName"] = "Random User";
			var AzureSQLClientCSBuilder = new SqlConnectionStringBuilder ();
			//AzureSQLClientCSBuilder.TrustServerCertificate = true;
			AzureSQLClientCSBuilder.UserID = "web_site";
			AzureSQLClientCSBuilder.Password = "te22am-we!bdb";
			AzureSQLClientCSBuilder.DataSource = "handymantoolsteam22.database.windows.net";
			AzureSQLClientCSBuilder.InitialCatalog = "HandymanToolsTeam22";
			//AzureSQLClientCSBuilder.Encrypt = true;
			AzureSQLClientCSBuilder.PersistSecurityInfo = true;

			var AzureSQLConnection = new SqlConnection (AzureSQLClientCSBuilder.ToString ());
			AzureSQLConnection.Open ();
			if (AzureSQLConnection.State == ConnectionState.Open) {
				var SQLQuery = "SELECT * FROM test_table";
				var SQLCommand = new SqlCommand (SQLQuery, AzureSQLConnection);
				var dataAdapter = new SqlDataAdapter (SQLCommand);
				var returnDS = new DataSet ();
				dataAdapter.Fill (returnDS);
				if (returnDS.Tables.Count > 0) {
					ViewBag.returnDT = returnDS.Tables [0];
					return View (returnDS.Tables [0]);
				}
				else {
					ViewData ["DbErr"] = "No records returned";
					return View ();
				}
			} 
			else {
				ViewData ["DbErr"] = "Couldn't connect to the database.";	
				return View ();
			}


		}
	}
}

