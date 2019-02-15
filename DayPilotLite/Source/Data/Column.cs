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
using System.Linq;
using System.Text;
using DayPilot.Web.Mvc.Json;

namespace DayPilot.Web.Mvc.Data
{
    /// <summary>
    /// Class representing calendar column in <see cref="ViewTypeEnum.Resources">resources view</see>.
    /// </summary>
    [Serializable]
    public class Column
    {
        private int width;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Column()
        {
            Children = new ColumnCollection();
            Date = DateTime.MinValue;
        }

        /// <summary>
        /// Constructor that sets the default values.
        /// </summary>
        /// <param name="name">Column name (visible).</param>
        /// <param name="id">Column value (id).</param>
        public Column(string name, string id)
        {
            Children = new ColumnCollection();
            Date = DateTime.MinValue;
            this.Name = name;
            this.Id = id;
        }

        /// <summary>
        /// Constructor that sets the default values.
        /// </summary>
        /// <param name="name">Column name (visible).</param>
        /// <param name="id">Column value (id).</param>
        /// <param name="date">Column date.</param>
        public Column(string name, string id, DateTime date)
        {
            Children = new ColumnCollection();
            this.Date = date.Date;
            this.Name = name;
            this.Id = id;
        }

        /// <summary>
        /// Column value (id).
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Column name (visible).
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Change the column date to override the default date in resources view.
        /// </summary>
        public DateTime Date { get; set; }

        ///<summary>
        ///Returns a <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
        ///</summary>
        ///
        ///<returns>
        ///A <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
        ///</returns>
        ///<filterpriority>2</filterpriority>
        public override string ToString()
        {
            return Name;
        }


        /// <summary>
        /// Get or set the column ToolTip.
        /// </summary>
        public string ToolTip { get; set; }


        /// <summary>
        /// A collection of child columns.
        /// </summary>
        public ColumnCollection Children { get; private set; }

        internal int getChildrenCount(int level)
        {
            int count = 0;

            if (Children.Count <= 0 || level <= 1)
            {
                return 1;
            }

            foreach (Column child in Children)
            {
                count += child.getChildrenCount(level - 1);
            }

            return count;
        }



        internal List<Column> getChildren(int level, bool inherit)
        {
            List<Column> list = new List<Column>();

            if (level <= 1)
            {
                list.Add(this);
                return list;
            }

            if (Children.Count == 0)
            {
                if (inherit)
                {
                    list.Add(this);
                }
                else
                {
                    list.Add(Column.Empty);
                }
                return list;
            }

            foreach (Column child in Children)
            {
                List<Column> subChildren = child.getChildren(level - 1, inherit);
                foreach (Column subChild in subChildren)
                {
                    list.Add(subChild);
                }
            }

            return list;
        }

        private static Column Empty = new Column();

        internal static Column FromJSON(JsonData input)
        {
            Column r = new Column();
            r.Name = (string) input["Name"];
            r.ToolTip = (string) input["ToolTip"];
            r.Id = (string) input["Value"];
            r.Date = (DateTime) input["Date"];

            r.Children.RestoreFromJSON(input["Children"]);

            return r;
        }
    }

}
