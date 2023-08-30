using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using WebAppAssign14.Models;

namespace WebAppAssign14.Controllers
{
    public class PlayersController : Controller
    {
        string conString = ConfigurationManager.ConnectionStrings["PlayersConStr"].ConnectionString;
        static SqlConnection con;
        static SqlCommand cmd;
        static SqlDataReader srdr;
        // GET: Players
        public ActionResult Index()
        {
            List<Players> player = new List<Players>();
            try
            {
                con = new SqlConnection(conString);
                cmd = new SqlCommand("Select * from Players");
                cmd.Connection = con;
                con.Open();
                srdr = cmd.ExecuteReader();
                while (srdr.Read())
                {
                    player.Add(
                        new Players
                        {
                            PId = (int)(srdr["PId"]),
                            FName = (string)srdr["FName"],
                            Lname = (string)(srdr["Lname"]),
                            Num = (int)(srdr["Num"]),
                            position = (string)(srdr["position"]),
                            Team = (string)(srdr["Team"]),
                        });
                }
            }
            catch (Exception ex)

            {
                TempData["error"] = ex.Message;
                return View("Error");
            }

            return View(player);
        }



        // GET: Players/Details/5
        public ActionResult Details(int id)
        {
            Players player = new Players();
            try
            {
                con = new SqlConnection(conString);
                cmd = new SqlCommand("select * from Players", con);
                con.Open();
                srdr = cmd.ExecuteReader();

                while (srdr.Read())
                {
                    player = (new Players
                    {
                        PId = (int)srdr["PId"],
                        FName = (string)srdr["FName"],
                        Lname = (string)srdr["Lname"],
                        Num = (int)srdr["Num"],
                        position = (string)srdr["position"],
                        Team = (string)srdr["Team"]
                    });
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return View("Error");
            }
            finally
            {
                con.Close();
            }
            return View(player);
        }
    
       
        
        // GET: Players/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Players/Create
        [HttpPost]
        public ActionResult Create(Players player)
        {


            try
            {
                con = new SqlConnection(conString);
                cmd = new SqlCommand("insert into Players values (@id, @fn, @ln, @jn, @pos, @team)", con);

                cmd.Parameters.AddWithValue("@id", player.PId);
                cmd.Parameters.AddWithValue("@fn", player.FName);
                cmd.Parameters.AddWithValue("@ln", player.Lname);
                cmd.Parameters.AddWithValue("@jn", player.Num);
                cmd.Parameters.AddWithValue("@pos", player.position);
                cmd.Parameters.AddWithValue("@team", player.Team);

                con.Open();
                cmd.ExecuteNonQuery();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return View("Error");

            }
            finally
            {
                con.Close();
            }
        }
           
        

        // GET: Players/Edit/5
        public ActionResult Edit(int id)
        {
            Players player = new Players();
            try
            {
                con = new SqlConnection(conString);
                cmd = new SqlCommand("select * from Players where PId = @id", con);
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                srdr = cmd.ExecuteReader();

                while (srdr.Read())
                {
                    player = (new Players
                    {
                        PId = (int)(srdr["PId"]),
                        FName = (string)srdr["FName"],
                        Lname = (string)(srdr["Lname"]),
                        Num = (int)(srdr["Num"]),
                        position = (string)(srdr["position"]),
                        Team = (string)(srdr["Team"]),

                    });
                }
            }
            catch(Exception ex)
            {
                TempData["error"] = ex.Message;
                return View("Error");
            }
            finally
            {
                con.Close();
            }
            return View(player);


        }

        // POST: Players/Edit/5
        [HttpPost]
        public ActionResult Edit(int pid, Players player)
        {
            try
            {
                con = new SqlConnection(conString);
                cmd = new SqlCommand("update Player set FName = @fn, Lname = @ln, Num = @jn, position = @pos, Team = @team where PId = @pid", con);

                cmd.Parameters.AddWithValue("@id", player.PId);
                cmd.Parameters.AddWithValue("@fn", player.FName);
                cmd.Parameters.AddWithValue("@ln", player.Lname);
                cmd.Parameters.AddWithValue("@jn", player.Num);
                cmd.Parameters.AddWithValue("@pos", player.position);
                cmd.Parameters.AddWithValue("@team", player.Team);
                con.Open();
                cmd.ExecuteNonQuery();

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                TempData["error"] = ex.Message;
                return View("Error");

            }
            finally
            {
                con.Close();
            }   
        }
        

        // GET: Players/Delete/5
        public ActionResult Delete(int id )
        {
            Players player = new Players();
            try
            {
                con = new SqlConnection(conString);
                cmd = new SqlCommand("select * from Players where PId = @id", con);
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                srdr = cmd.ExecuteReader();
                while (srdr.Read())
                {
                    player = (new Players
                    {
                        PId = (int)srdr["PlayerId"],
                        FName = (string)srdr["FName"],
                        Lname = (string)srdr["Lname"],
                        Num = (int)srdr["Num"],
                        position = (string)srdr["position"],
                        Team = (string)srdr["Team"]
                    });
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return View("Error");
            }
            finally
            {
                con.Close();
            }
            return View(player);
        }


        // POST: Players/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Players player)
        {
            try
            {
                con = new SqlConnection(conString);
                cmd = new SqlCommand("delete from Players where PId = @id", con);

                cmd.Parameters.AddWithValue("@id", player.PId);

                con.Open();
                cmd.ExecuteNonQuery();
                return RedirectToAction("Index");
            }
            catch (Exception ex)

            {
                TempData["error"] = ex.Message;
                return View("Error");
            }
            finally
            {
                con.Close();
            }
        }                           
    }
}
