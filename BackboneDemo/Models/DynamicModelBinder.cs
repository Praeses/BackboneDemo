using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BackboneDemo.Models
{
    public class DynamicModelBinder : IModelBinder
    {


        #region IModelBinder Members

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            dynamic view_model = new ViewModel();

            Action<string> TrySetValue = (field) => {
                var value = bindingContext.ValueProvider.GetValue(field);
                if ( value.IsNotNull() ) { view_model.SetValue(field, value.RawValue); }
            };

            TrySetValue("id");

            controllerContext.RequestContext.HttpContext.Request
                .QueryString.AllKeys.ToList().ForEach(x => TrySetValue(x));

            controllerContext.RequestContext.HttpContext.Request
                .Form.AllKeys.ToList().ForEach(x => TrySetValue(x));
            
            return view_model;
        }

        #endregion
    }
}