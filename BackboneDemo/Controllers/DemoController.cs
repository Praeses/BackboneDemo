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
        private static Dictionary<int, Dictionary<string, Object>> DataBase 
            = new Dictionary<int, Dictionary<string, Object>>();



        public ActionResult Index() { return View(); }

        public string Ticket(ViewModel model)
        {
            dynamic test = new ViewModel();
            //int? id = model.IsNotNull() && model.ContainsKey("id") ? (int?)model["id"] : null;
            //switch (this.Request.RequestType)
            //{
            //    case "GET":
            //        return id.HasValue ? Get(id.Value) : GetAll();
            //    case "POST": //new
            //    case "PUT":  //update
            //        return Save(id, model);
            //}
            return null;
        }


        private string Get(int id) 
        {
            if (DataBase.ContainsKey(id)) DataBase[id].ToJson();
            return null;
        }

        private string GetAll() 
        {
            return DataBase.Values.ToList().ToJson();
        }

        private string Save(int? id, Dictionary<string, Object> model) 
        {
            if (id.HasValue == false && DataBase.Count > 0) { id = DataBase.Keys.Max(); }
            if (id.HasValue == false ) { id = 1; }
            DataBase[id.Value] = model;
            model["id"] = id.Value;
            return model.ToJson();
        }


    }
}
