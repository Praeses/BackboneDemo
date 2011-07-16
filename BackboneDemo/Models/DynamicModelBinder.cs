using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.Web.Script.Serialization;

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
            
            var request = controllerContext.RequestContext.HttpContext.Request;
            if( request.IsAjaxRequest() )
            {
                try
                {
                    byte[] bytes = new byte[request.TotalBytes];
                    var original_position = request.InputStream.Position;
                    request.InputStream.Position = 0;
                    request.InputStream.Read(bytes, 0, request.TotalBytes);
                    request.InputStream.Position = original_position;
                    string data = Encoding.UTF8.GetString(bytes);

                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    var dictionary = serializer.Deserialize<Dictionary<string, object>>(data);
                    dictionary.Keys.ToList().ForEach(x => TrySetValue( x ));
                }
                catch{}

            }
            
            return view_model;
        }

        #endregion
    }
}