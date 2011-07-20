using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BackboneDemo.Models;

namespace BackboneDemo.Controllers
{
    public class DemoController : Controller
    {
        private static Dictionary<int, ViewModel> DataBase = new Dictionary<int, ViewModel>();


        public ActionResult Index() { return View(); }

        [HttpGet]
        [NoCache]
        public string Ticket(ViewModel viewmodel) 
        {
            dynamic model = viewmodel;
            int? id = model.GetId();
            if (id.HasValue == false) { return GetAll(); }
            if (DataBase.ContainsKey(id.Value)) { return DataBase[id.Value].ToJson(); } 
            return null;
        }


        [AcceptVerbs(HttpVerbs.Put | HttpVerbs.Post)]//Post=New, Put=Update
        public string Ticket(ViewModel viewmodel, FormCollection data) 
        {
            dynamic model = viewmodel;
            int? id = model.GetId();
            if (id.HasValue == false && DataBase.Count > 0) { id = DataBase.Count + 1; }
            if (id.HasValue == false ) { id = 1; }
            model.id = id.Value;
            DataBase[id.Value] = model;
            return (new { id = id.Value }).ToJson();
        }

        private string GetAll() 
        {
            return '[' + string.Join(", ", DataBase.Values.Select(x => x.ToJson()).ToArray() ) + ']';
        }


    }
}
