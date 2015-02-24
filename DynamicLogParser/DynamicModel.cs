namespace DynamicLogParser
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Dynamic;

    public class DynamicModel : DynamicObject
    {
        private IDictionary<string, object> PropertyDictionary { get; set; }
        private const string ArrayPrefix = "ArrayIndex";

        public DynamicModel()
        {
            this.PropertyDictionary = new ConcurrentDictionary<string, object>();
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            var name = binder.Name.ToLower();
            return this.PropertyDictionary.TryGetValue(name, out result);
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            var name = binder.Name.ToLower();
            this.PropertyDictionary[name] = value;

            return true;
        }

        public bool TryCreateMember(string name, object value)
        {
            name = name.ToLower();
            this.PropertyDictionary[name] = value;

            return true;
        }

        public override bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object result)
        {
            var index = (int)indexes[0];
            var name = this.CreateArrayPropertyName(index);
            return this.PropertyDictionary.TryGetValue(name, out result);
        }

        public override bool TrySetIndex(SetIndexBinder binder, object[] indexes, object value)
        {
            var index = (int)indexes[0];
            var name = this.CreateArrayPropertyName(index);
            if (this.PropertyDictionary.ContainsKey(name))
            {
                this.PropertyDictionary[name] = value;
            }
            else
            {
                this.PropertyDictionary.Add(name, value);
            }

            return true;
        }

        private string CreateArrayPropertyName(int index)
        {
            if (index < 0)
            {
                throw new InvalidOperationException("Index must be greater than 0");
            }

            return string.Format("{0}{1}", ArrayPrefix, index);
        }
    }
}
