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

namespace DayPilot.Web.Mvc.Json
{
    /// <summary>
    /// Wrapper to make the API compatible with the Java version. 
    /// For internal use.
    /// </summary>
    public class JSONObject
    {
        private JsonData _data;
        
        public JSONObject(JsonData data)
        {
            _data = data;
        }

        public JSONObject()
        {
            _data = new JsonData();
        }

        public string getString(string key)
        {
            return (string) _data[key];
        }

        public string optString(string key)
        {
            if (_data[key] != null && _data[key].IsString)
            {
                return getString(key);
            }
            return null;
        }

        public JSONObject getJSONObject(string key)
        {
            return new JSONObject(_data[key]);
        }

        public JSONObject optJSONObject(string key)
        {
            if (_data[key] != null && _data[key].IsObject)
            {
                return getJSONObject(key);
            }
            return null;
        }

        public JSONArray optJSONArray(string key)
        {
            if (_data[key] != null && _data[key].IsArray)
            {
                return getJSONArray(key);
            }
            return null;
        }

        public int getInt(string key)
        {
            return (int) _data[key];
        }

        public int optInt(string key)
        {
            if (_data[key] != null && _data[key].IsInt)
            {
                return getInt(key);
            }
            return 0;
        }

        public bool getBoolean(string key)
        {
            return (bool) _data[key];
        }

        public bool optBoolean(string key)
        {
            if (_data[key] != null && _data[key].IsBoolean)
            {
                return getBoolean(key);
            }
            return false;
        }

        public JSONArray getJSONArray(string key)
        {
            return new JSONArray(_data[key]);
        }

        public bool isNull(string key)
        {
            return _data[key] == null;
        }

        public JsonData getJsonData(string key)
        {
            return _data[key];
        }

        public JsonData optJsonData(string key)
        {
            return _data[key];
        }

        public DateTime getDateTime(string key)
        {
            return Convert.ToDateTime(getString(key));
        }

        public void put(string key, string value)
        {
            _data[key] = value;
        }

        public void put(string key, DateTime value)
        {
            put(key, value.ToString("s"));
        }

        public void put(string key, int value)
        {
            _data[key] = value;
        }

        public JsonData ToJsonData()
        {
            return _data;
        }

        public Hashtable ToHashtable()
        {
            Hashtable result = new Hashtable();
            foreach (string key in _data.Keys)
            {
                switch (_data[key].GetJsonType())
                {
                    case JsonType.None:
                        break;
                    case JsonType.Object:
                        result[key] = new JSONObject(_data[key]).ToHashtable();
                        break;
                    case JsonType.Array:
                        result[key] = new JSONArray(_data[key]).ToList();
                        break;
                    case JsonType.String:
                        result[key] = (string) _data[key];
                        break;
                    case JsonType.Int:
                        result[key] = (int) _data[key];
                        break;
                    case JsonType.Long:
                        result[key] = (long) _data[key];
                        break;
                    case JsonType.Double:
                        result[key] = (double) _data[key];
                        break;
                    case JsonType.Boolean:
                        result[key] = (bool) _data[key];
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                
            }
            return result;
        }
    }
}