using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace JsonToCsv
{
    public static class JsonFlattener
    {
        public static Dictionary<string, object> Flatten(JToken token)
        {
            var dict = new Dictionary<string, object>();
            FlattenToken(token, string.Empty, dict);
            return dict;
        }

        private static void FlattenToken(JToken token, string prefix, Dictionary<string, object> dict)
        {
            switch (token.Type)
            {
                case JTokenType.Object: FlattenObject(token, prefix, dict); break;
                case JTokenType.Array: FlattenArray(token, prefix, dict); break;
                default: dict.Add(prefix, ((JValue)token).Value); break;
            }
        }

        private static void FlattenObject(JToken token, string prefix, Dictionary<string, object> dict)
        {
            foreach (var prop in token.Children<JProperty>())
            {
                FlattenToken(prop.Value, Join(prefix, prop.Name), dict);
            }
        }

        private static void FlattenArray(JToken token, string prefix, Dictionary<string, object> dict)
        {
            var index = 0;
            foreach (var value in token.Children())
            {
                FlattenToken(value, Join(prefix, index.ToString()), dict);
                index++;
            }
        }

        private static string Join(string prefix, string name)
        {
            return (string.IsNullOrEmpty(prefix) ? name : prefix + "." + name);
        }
    }
}
