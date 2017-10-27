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

    [HttpPost("/stylist/list")]
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

    [HttpGet("/{name}/{id}/client/list")]
    public ActionResult ViewClientList(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();

      Stylist selectedStylist = Stylist.Find(id);
      List<Client> stylistClients = selectedStylist.GetClients();

      model.Add("stylist", selectedStylist);
      model.Add("client", stylistClients);

      return View("StylistDetail", model);
    }

    [HttpGet("/stylist/{id}/client/add")]
    public ActionResult AddClient(int id)
    {
      Stylist selectedStylist = Stylist.Find(id);

      return View(selectedStylist);
    }

    [HttpPost("/stylist/{id}/client/list")]
    public ActionResult AddClientViewClientList(int id)
    {
      Client newClient = new Client(Request.Form["client-name"], id);
      newClient.Save();
      Dictionary<string, object> model = new Dictionary<string, object>();

      Stylist selectedStylist = Stylist.Find(id);
      List<Client> stylistClients = selectedStylist.GetClients();

      model.Add("stylist", selectedStylist);
      model.Add("client", stylistClients);

      return View("StylistDetail", model);
    }

    [HttpGet("/client/{id}/edit")]
    public ActionResult ClientEdit(int id)
    {
      Client thisClient = Client.Find(id);

      return View(thisClient);
    }

    [HttpPost("/client/{id}/edit")]
    public ActionResult ClientEditConfirm(int id)
    {
      Client thisClient = Client.Find(id);
      thisClient.UpdateName(Request.Form["new-name"]);

      return RedirectToAction("Index");
    }

    [HttpGet("/{name}/{id}/client/delete")]
    public ActionResult ClientDelete(int id)
    {
      // Cuisine is selected as an object
      Client thisClient = Client.Find(id);
      thisClient.DeleteClient();

      return RedirectToAction("Index");
    }
  }
}
