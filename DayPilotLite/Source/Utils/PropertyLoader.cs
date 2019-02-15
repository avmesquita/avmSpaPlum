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
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace DayPilot.Web.Mvc.Utils
{
    internal class PropertyLoader
    {

        internal static string GetString(object dataItem, string propertyName, string dataFieldName)
        {
            if (String.IsNullOrEmpty(propertyName))
            {
                throw new NullReferenceException(dataFieldName + " property must be specified.");
            }

            object val;
            if (dataItem is DataRow)
            {
                DataRow dr = (DataRow) dataItem;
                if (dr.Table.Columns.Contains(propertyName))
                {
                    val = ((DataRow)dataItem)[propertyName];
                }
                else
                {
                    return null;
                }
            }
            else
            {
                val = DataBinder.GetPropertyValue(dataItem, propertyName, null);
            }

            return val.ToString();
        }


        internal static DateTime GetDateTime(object dataItem, string propertyName, string dataFieldName)
        {
            if (String.IsNullOrEmpty(propertyName))
            {
                throw new NullReferenceException(dataFieldName + " property must be specified.");
            }

            object val;
            if (dataItem is DataRow)
            {
                val = ((DataRow)dataItem)[propertyName];
            }
            else
            {
                val = DataBinder.GetPropertyValue(dataItem, propertyName, null);
            }

            DateTime result;
            if (DateTimeParser.IsParseable(val))
            {
                return DateTimeParser.Parse(val);
            }

            throw new FormatException(String.Format("Unable to convert '{0}' (from {1} column) to DateTime.", val, dataFieldName));
        }
    }
}
