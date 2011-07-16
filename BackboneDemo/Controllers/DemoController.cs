using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BackboneDemo.Controllers
{
    public class DemoController : Controller
    {
        private static Dictionary<int, dynamic> DataBase = new Dictionary<int, dynamic>();



        public ActionResult Index() { return View(); }

        public string Ticket(int? id, dynamic model)
        {
            switch (this.Request.RequestType)
            {
                case "GET":
                    return id.HasValue ? Get(id.Value) : GetAll();
                case "POST": //new
                case "PUT":  //update
                    return Save(id, model);
            }
            return null;
        }


        private string Get(int id) 
        {
            if (DataBase.ContainsKey(id)) return ToJson(DataBase[id]);
            return null;
        }

        private string GetAll() 
        {
            return DataBase.Values.ToList().ToJson();
        }

        private string Save(int? id, dynamic model) 
        {
            if (id.HasValue == false && DataBase.Count > 0) { id = DataBase.Keys.Max(); }
            if (id.HasValue == false ) { id = 1; }
            DataBase[id.Value] = model;
            return ToJson(model);
        }


        private string ToJson(dynamic model) { return ObjectExtentions.ToJson(model); }
    }
}
