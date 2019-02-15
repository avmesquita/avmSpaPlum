/*
Copyright © 2005 - 2014 Annpoint, s.r.o.

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
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
/// Summary description for EventManager
/// </summary>
public class EventManager
{
    private Controller controller;
    private string key;

    public EventManager(Controller controller, string key)
    {
        this.controller = controller;
        this.key = key;

        if (this.controller.Session[key] == null)
        {
            this.controller.Session[key] = generateData();
        }
    }

    public DataTable Data
    {
        get { return (DataTable) controller.Session[key]; }
    }

    public DataTable FilteredData(DateTime start, DateTime end, string keyword)
    {
        string where = String.Format("NOT (([end] <= '{0:s}') OR ([start] >= '{1:s}')) and [text] like '%{2}%'", start, end, keyword);
        DataRow[] rows = Data.Select(where);
        DataTable filtered = Data.Clone();

        foreach (DataRow r in rows)
        {
            filtered.ImportRow(r);
        }

        return filtered;
    }

    public EventManager(Controller controller) : this(controller, "default")
    {
    }

    private DataTable generateData()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("id", typeof (string));
        dt.Columns.Add("text", typeof (string));
        dt.Columns.Add("start", typeof (DateTime));
        dt.Columns.Add("end", typeof (DateTime));

        dt.PrimaryKey = new DataColumn[] {dt.Columns["id"]};

        DataRow dr;

        dr = dt.NewRow();
        dr["id"] = 1;
        dr["start"] = Convert.ToDateTime("15:00");
        dr["end"] = Convert.ToDateTime("15:00");
        dr["text"] = "Event 1 (backslash test: TEST\\jname)";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["id"] = 2;
        dr["start"] = Convert.ToDateTime("16:00");
        dr["end"] = Convert.ToDateTime("17:00");
        dr["text"] = "Event 2";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["id"] = 3;
        dr["start"] = Convert.ToDateTime("14:15").AddDays(1);
        dr["end"] = Convert.ToDateTime("18:45").AddDays(1);
        dr["text"] = "Event 3";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["id"] = 4;
        dr["start"] = Convert.ToDateTime("16:30");
        dr["end"] = Convert.ToDateTime("17:30");
        dr["text"] = "Sales Dept. Meeting Once Again";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["id"] = 5;
        dr["start"] = Convert.ToDateTime("8:00");
        dr["end"] = Convert.ToDateTime("9:00");
        dr["text"] = "Event 4";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["id"] = 6;
        dr["start"] = Convert.ToDateTime("14:00");
        dr["end"] = Convert.ToDateTime("20:00");
        dr["text"] = "Event 6";
        dt.Rows.Add(dr);


        dr = dt.NewRow();
        dr["id"] = 7;
        dr["start"] = Convert.ToDateTime("11:00");
        dr["end"] = Convert.ToDateTime("13:14");
        dr["text"] = "Unicode test: 公曆 (requires Unicode fonts on the client side)";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["id"] = 8;
        dr["start"] = Convert.ToDateTime("13:14").AddDays(-1);
        dr["end"] = Convert.ToDateTime("14:05").AddDays(-1);
        dr["text"] = "Event 8";
        dt.Rows.Add(dr);


        dr = dt.NewRow();
        dr["id"] = 9;
        dr["start"] = Convert.ToDateTime("13:14").AddDays(7);
        dr["end"] = Convert.ToDateTime("14:05").AddDays(7);
        dr["text"] = "Event 9";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["id"] = 10;
        dr["start"] = Convert.ToDateTime("13:14").AddDays(-7);
        dr["end"] = Convert.ToDateTime("14:05").AddDays(-7);
        dr["text"] = "Event 10";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["id"] = 14;
        dr["start"] = Convert.ToDateTime("7:45:00");
        dr["end"] = Convert.ToDateTime("8:30:00");
        dr["text"] = "Event 14";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["id"] = 16;
        dr["start"] = Convert.ToDateTime("8:30:00").AddDays(1);
        dr["end"] = Convert.ToDateTime("9:00:00").AddDays(1);
        dr["text"] = "Event 16";
        dt.Rows.Add(dr);


        dr = dt.NewRow();
        dr["id"] = 17;
        dr["start"] = Convert.ToDateTime("8:00:00").AddDays(1);
        dr["end"] = Convert.ToDateTime("8:00:01").AddDays(1);
        dr["text"] = "Event 17";
        dt.Rows.Add(dr);

        return dt;
    }

    public void EventEdit(string id, string name)
    {
        DataRow dr = Data.Rows.Find(id);
        if (dr != null)
        {
            dr["text"] = name;
            Data.AcceptChanges();
        }
    }

    public void EventMove(string id, DateTime start, DateTime end)
    {
        DataRow dr = Data.Rows.Find(id);
        if (dr != null)
        {
            dr["start"] = start;
            dr["end"] = end;
            Data.AcceptChanges();
        }
    }

    public Event Get(string id)
    {
        DataRow dr = Data.Rows.Find(id);
        if (dr == null)
        {
            //return new Event();
            return null;
        }
        return new Event()
                   {
                       Id = (string) dr["id"],
                       Text = (string) dr["text"]
                   };
    }
    internal void EventCreate(DateTime start, DateTime end, string text, string id)
    {
        DataRow dr = Data.NewRow();

        dr["id"] = id;
        dr["start"] = start;
        dr["end"] = end;
        dr["text"] = text;

        Data.Rows.Add(dr);
        Data.AcceptChanges();
    }

    internal void EventCreate(DateTime start, DateTime end, string text)
    {
        EventCreate(start, end, text, Guid.NewGuid().ToString());
    }

    public class Event
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }

    public void EventDelete(string id)
    {
        DataRow dr = Data.Rows.Find(id);
        if (dr != null)
        {
            Data.Rows.Remove(dr);
            Data.AcceptChanges();
        }
    }
}
