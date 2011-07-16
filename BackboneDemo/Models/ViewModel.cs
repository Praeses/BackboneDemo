using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Dynamic;

namespace BackboneDemo.Models
{
    public class ViewModel : DynamicObject
    {

        Dictionary<string, object> _dictionary = new Dictionary<string, object>();


        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            _dictionary[binder.Name] = value;
            return true;
        }

        public override bool TryGetMember(GetMemberBinder binder,
            out object result)
        {
            return _dictionary.TryGetValue(binder.Name, out result);
        }

        public ViewModel SetValue(string key, object value)
        {
            _dictionary[key] = value;
            return this;
        }
        

        public bool HasMember(string member) { return _dictionary.ContainsKey(member); }

        public string ToJson() { return _dictionary.ToJson(); }

    }
}