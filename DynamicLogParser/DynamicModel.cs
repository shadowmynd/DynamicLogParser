// Solution: PersonalLibs
// Project: DynamicLogParser
// FileName: DynamicModel.cs
// 
// Author: Brandon Moller <brandon@shadowmynd.com>
// 
// Created: 02-23-2015 5:38 PM
// Modified: 02-23-2015 9:44 PM []
namespace DynamicLogParser
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Dynamic;

    public class DynamicModel : DynamicObject
    {
        public static int Count(DynamicModel model)
        {
            return model.CurrentPropertyIndex;
        }

        private int CurrentPropertyIndex { get; set; }
        private IDictionary<int, string> PropertyLocationDictionary { get; set; }
        private IDictionary<string, object> PropertyDictionary { get; set; }
        private const string ArrayPrefix = "ArrayIndex";

        public DynamicModel()
        {
            this.PropertyDictionary = new ConcurrentDictionary<string, object>();
            this.PropertyLocationDictionary = new ConcurrentDictionary<int, string>();
            this.CurrentPropertyIndex = 0;
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
            this.SetPropertyInsertDictionary(name);
            return true;
        }

        private void SetPropertyInsertDictionary(string name)
        {
            this.PropertyLocationDictionary[this.CurrentPropertyIndex] = name;
            this.CurrentPropertyIndex++;
        }

        public bool TryCreateMember(string name, object value)
        {
            name = name.ToLower();
            this.PropertyDictionary[name] = value;
            this.SetPropertyInsertDictionary(name);
            return true;
        }

        public override bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object result)
        {
            var index = (int)indexes[0];
            var name = this.CreateArrayPropertyName(index);
            if (!this.PropertyDictionary.TryGetValue(name, out result))
            {
                if (!this.PropertyLocationDictionary.TryGetValue(index, out name))
                {
                    return false;
                }
            }
            else
            {
                return true;
            }

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
