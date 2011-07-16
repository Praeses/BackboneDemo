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

        public string Ticket(ViewModel viewmodel)
        {
            dynamic model = viewmodel;
            int? id = model.GetId();
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
            if (DataBase.ContainsKey(id)) { return DataBase[id].ToJson(); } 
            return null;
        }

        private string GetAll() 
        {

            return '[' + string.Join(", ", DataBase.Values.Select(x => x.ToJson()).ToArray() ) + ']';
        }

        private string Save(int? id, ViewModel viewmodel) 
        {
            dynamic model = viewmodel;
            if (id.HasValue == false && DataBase.Count > 0) { id = DataBase.Count + 1; }
            if (id.HasValue == false ) { id = 1; }
            model.id = id.Value;
            DataBase[id.Value] = model;
            return model.ToJson();
        }


    }
}
