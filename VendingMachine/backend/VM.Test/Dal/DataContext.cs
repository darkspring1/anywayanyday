using System;
using System.Collections.Generic;
using System.Linq;

namespace VM.Test.Dal
{
    class DataContext
    {
        static readonly Dictionary<string, object> Data = new Dictionary<string, object>();

        static string GetKey(Type t)
        {
            return t.FullName;
        }

        public static List<T> GetData<T>()
        {
            var key = GetKey(typeof(T));

            object result = null;

            if (!Data.TryGetValue(key, out result))
            {
                var list = new List<T>();
                AddData(list);
                return list;
            }

            return (List<T>)result;
        }

        public static void AddData<T>(List<T> data)
        {
            var key = GetKey(typeof(T));
            Data.Add(key, data);
        }

        public static void Clear()
        {
            Data.Clear();
        }
    }
}