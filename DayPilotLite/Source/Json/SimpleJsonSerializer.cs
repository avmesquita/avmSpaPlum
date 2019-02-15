/*
Copyright © 2005 - 2016 Annpoint, s.r.o.

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.

-------------------------------------------------------------------------

NOTE: Reuse requires the following acknowledgement (see also NOTICE):
This product includes DayPilot (http://www.daypilot.org) developed by Annpoint, s.r.o.
*/


using System;
using System.Collections;
using System.Text;

namespace DayPilot.Web.Mvc.Json
{
    /// <summary>
    /// Class for serializing simple objects to JSON. Supports null, int, double, bool, DateTime, string, IDictionary, and IList.
    /// </summary>
    public class SimpleJsonSerializer
    {
        private StringBuilder sb = new StringBuilder();

        /// <summary>
        /// Serializes an object to a JSON string. Unknown classes are serialized using ToString().
        /// </summary>
        /// <param name="obj">Supports null, int, double, bool, DateTime, string, IDictionary, and IList.</param>
        /// <returns></returns>
        public static string Serialize(object obj)
        {
            SimpleJsonSerializer main = new SimpleJsonSerializer();
            main.serializeObject(obj);
            return main.sb.ToString();
        }

        private void serializeObject(object obj)
        {
            if (obj == null) // null
            {
                sb.Append("null");
                return;
            }

            if (obj is int) // integer number
            {
                sb.Append(obj);
                return;
            }

            if (obj is long) // integer number
            {
                sb.Append(obj);
                return;
            }

            if (obj is double)
            {
                sb.Append(obj);
                return;
            }

            if (obj is bool)
            {
                sb.Append(obj.ToString().ToLower());
                return;
            }

            if (obj is DateTime)
            {
                DateTime dt = (DateTime)obj;
                serialize(dt.ToString("s"));
                return;
            }

            if (obj is string)
            {
                serialize(obj as string);
                return;
            }

            if (obj is JsonData)
            {
                serialize(obj as JsonData);
                return;
            }

            if (obj is IDictionary)
            {
                serialize(obj as IDictionary);
                return;
            }

            if (obj is IList)
            {
                serialize(obj as IList);
                return;
            }

            // all other object serialized using ToString() (or throw an exception?)
            serialize(obj.ToString());

        }

        private void serialize(JsonData data)
        {
            switch (data.GetJsonType())
            {
                case JsonType.Array:
                    serialize(data as IList);
                    return;
                case JsonType.Object:
                    serialize(data as IDictionary);
                    return;
                case JsonType.Boolean:
                    serializeObject((bool)data);
                    return;
                case JsonType.Double:
                    serializeObject((double)data);
                    return;
                case JsonType.Int:
                    serializeObject((int)data);
                    return;
                case JsonType.Long:
                    serializeObject((long)data);
                    return;
                case JsonType.String:
                    serialize((string)data);
                    return;
                case JsonType.None:
                    serializeObject(null);
                    return;
            }

        }

        private void serialize(IDictionary dict)
        {
            sb.Append("{");

            bool first = true;
            foreach (string key in dict.Keys)
            {
                if (!first)
                {
                    sb.Append(",");
                }

                serialize(key);
                sb.Append(":");
                serializeObject(dict[key]);
                first = false;
            }

            sb.Append("}");

        }

        private void serialize(string str)
        {
            if (str == null)
            {
                sb.Append("null");
                return;
            }
            sb.Append("\"");
            sb.Append(EscapeString(str));
            sb.Append("\"");
        }

        // t n r f b 
        public static string EscapeString(string str)
        {
            return
                str.Replace("\\", "\\\\")
                    .Replace("\t", "\\t")
                    .Replace("\n", "\\n")
                    .Replace("\r", "\\r")
                    .Replace("\f", "\\f")
                    .Replace("\b", "\\b")
                    .Replace("\'", "\\'")
                    .Replace("\"", "\\\"");
        }

        private void serialize(IList list)
        {
            bool first = true;

            sb.Append("[");
            foreach (object o in list)
            {
                if (!first)
                {
                    sb.Append(",");
                }

                serializeObject(o);

                first = false;
            }
            sb.Append("]");
        }
    }
}