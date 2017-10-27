using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalonApp.Controllers
{
  public class HomeController : Controller
  {
    [HttpGet("/")]
    public ActionResult Index()
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      List<Stylist> stylistList = Stylist.GetAll();
      List<Client> stylistClients = Client.GetAll();
      model.Add("stylist", stylistList);
      model.Add("client", stylistClients);

      return View("Index", model);
    }

    [HttpPost("/")]
    public ActionResult WriteStylists()
    {
      Stylist newStylist = new Stylist(Request.Form["stylist-name"], Request.Form["stylist-specialty"]);
      newStylist.Save();
      List<Stylist> allStylists = Stylist.GetAll();

      return View("Index", allStylists);
    }

    [HttpGet("/stylist/add")]
    public ActionResult AddStylist()
    {
      return View();
    }

    [HttpGet("/{name}/{id}/clientlist")]
    public ActionResult ViewClientList(int id)
    {

      Dictionary<string, object> model = new Dictionary<string, object>();

      Stylist selectedStylist = Stylist.Find(id);
      List<Client> stylistClients = selectedStylist.GetClients();

      model.Add("stylist", selectedStylist);
      model.Add("client", stylistClients);

      // Return the restaurant list
      return View("StylistDetail", model);
    }
  }
}
