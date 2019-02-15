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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DayPilot.Web.Mvc.Json;

namespace DayPilot.Web.Mvc.Data
{
    /// <summary>
    /// Collection of columns for <see cref="ViewTypeEnum.Resources">Resources</see> view.
    /// </summary>
    public class ColumnCollection : CollectionBase
    {
        internal bool designMode;

        /// <summary>
        /// Gets the specified <see cref="Column">Column</see>.
        /// </summary>
        /// <param name="index">Item index</param>
        /// <returns>Column at the specified position.</returns>
        public Column this[int index]
        {
            get
            {
                return ((Column)List[index]);
            }
            set
            {
                List[index] = value;
            }
        }

        /// <summary>
        /// Converts ColumnCollection to ArrayList.
        /// </summary>
        /// <returns>ArrayList with ColumnCollection items.</returns>
        public ArrayList ToArrayList()
        {
            ArrayList retArray = new ArrayList();
            for (int i = 0; i < this.Count; i++)
            {
                retArray.Add(this[i]);
            }

            return retArray;
        }

        /// <summary>
        /// Adds a new <see cref="Column">Column</see> to the collection.
        /// </summary>
        /// <param name="value">Column to be added.</param>
        /// <returns></returns>
        public int Add(Column value)
        {
            return (List.Add(value));
        }

        /// <summary>
        /// Adds a new <see cref="Column">Column</see> to the collection.
        /// </summary>
        /// <param name="name">Column name</param>
        /// <param name="id">Column id</param>
        /// <returns></returns>
        public int Add(string name, string id)
        {
            return Add(new Column(name, id));
        }

        /// <summary>
        /// Adds a new <see cref="Column">Column</see> to the collection.
        /// </summary>
        /// <param name="name">Column name</param>
        /// <param name="id">Column id</param>
        /// <param name="date">Column date</param>
        /// <returns></returns>
        public int Add(string name, string id, DateTime date)
        {
            return Add(new Column(name, id, date));
        }
        /// <summary>
        /// Determines the index of a specific item in the collection.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int IndexOf(Column value)
        {
            return (List.IndexOf(value));
        }

        /// <summary>
        /// Inserts a new column at the specified position.
        /// </summary>
        /// <param name="index">New column position.</param>
        /// <param name="value">Column to be added.</param>
        public void Insert(int index, Column value)
        {
            List.Insert(index, value);
        }

        /// <summary>
        /// Removes a column from the collection.
        /// </summary>
        /// <param name="value">Column to be removed.</param>
        public void Remove(Column value)
        {
            List.Remove(value);
        }


        /// <summary>
        /// Determines whether the collection contains a specified column.
        /// </summary>
        /// <param name="value">Column to be found.</param>
        /// <returns>True if the collection contains the column</returns>
        public bool Contains(Column value)
        {
            return (List.Contains(value));
        }


        /// <summary>
        /// Creates a new collection from an ArrayList.
        /// </summary>
        /// <param name="items">ArrayList that contains the new columns.</param>
        public ColumnCollection(ArrayList items)
            : base()
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i] is Column)
                {
                    this.Add((Column)items[i]);
                }
            }
        }

        /// <summary>
        /// Creates a new ColumnCollection.
        /// </summary>
        public ColumnCollection()
            : base()
        { }

        /// <summary>
        /// Returns number of columns in the collection.
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public int GetColumnCount(int level)
        {
            int count = 0;
            foreach (Column c in List)
            {
                count += c.getChildrenCount(level);
            }

            return count;
        }

        /// <summary>
        /// Returns the columns at the specified level.
        /// </summary>
        /// <param name="level">Column level</param>
        /// <param name="inherit">True if the column details should be inherited from the parent (if there is no column on the specified level).</param>
        /// <returns></returns>
        public List<Column> GetColumns(int level, bool inherit)
        {
            List<Column> list = new List<Column>();

            foreach (Column c in List)
            {
                List<Column> children = c.getChildren(level, inherit);
                list.AddRange(children);
                //                foreach (Column child in children)
                //                {
                //                    list.Add(child);
                //                }

            }
            return list;
        }


        internal void RestoreFromJSON(JsonData input)
        {
            Clear();

            if (input == null || !input.IsArray)
            {
                return;
            }

            for (int i = 0; i < input.Count; i++)
            {
                JsonData c = input[i];
                Add(Column.FromJSON(c));
            }
        }
    }

}
