using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Web.Mvc;

namespace DudeFinder.Controllers
{
    public class PartyController : Controller
    {
        // GET: Main
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Join(string id)
        {
            double lat = 0.0;
            double lng = 0.0;

            using (MySqlConnection sqlconn = new MySqlConnection(ConfigurationManager.ConnectionStrings["AzureDB"].ConnectionString))
            {
                sqlconn.Open();
                string sql_command = "SELECT lat,lng FROM parties WHERE partyid = @id";
                MySqlCommand cmd = new MySqlCommand(sql_command, sqlconn);
                cmd.Parameters.AddWithValue("@id", id);
                var mr = cmd.ExecuteReader();
                if (!mr.Read())
                    return RedirectToAction("NotFound");
                else
                {
                    lat = mr.GetDouble(0);
                    lng = mr.GetDouble(1);
                    mr.Close();
                }
                sqlconn.Close();
            }

            ViewBag.PartyID = id;
            ViewBag.Lat = lat;
            ViewBag.Lng = lng;

            return View();
        }

        public ActionResult NotFound()
        {
            return View();
        }

        public ActionResult Create(double lat, double lng)
        {
            string partyid = MD5Gen.ConvertStringtoMD5(Guid.NewGuid().ToString());
            ViewBag.PartyID = partyid;
            ViewBag.PartyUrl = Url.Action("Join", "Party", new { id = partyid }, "http");
            ViewBag.Lat = lat;
            ViewBag.Lng = lng;

            WebClient wc = new WebClient();
            string add_url = String.Format("http://nominatim.openstreetmap.org/reverse?format=json&lat={0}&lon={1}", lat, lng);
            string add_json = wc.DownloadString(new Uri(add_url));

            Dictionary<string, object> address = JsonConvert.DeserializeObject<Dictionary<string, object>>(add_json);
            ViewBag.Address = (string)address["display_name"];

            using (MySqlConnection sqlconn = new MySqlConnection(ConfigurationManager.ConnectionStrings["AzureDB"].ConnectionString))
            {
                sqlconn.Open();
                string sql_command = "INSERT INTO parties (partyid,lat,lng) VALUES (@id,@lat,@lng)";
                MySqlCommand cmd = new MySqlCommand(sql_command, sqlconn);
                cmd.Parameters.AddWithValue("@id", partyid);
                cmd.Parameters.AddWithValue("@lat", lat);
                cmd.Parameters.AddWithValue("@lng", lng);
                cmd.ExecuteNonQuery();
                sqlconn.Close();
            }

            return View();
        }
    }
}