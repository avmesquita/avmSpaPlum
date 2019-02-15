using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Data
{
    /// <summary>
    /// Summary description for EventManager
    /// </summary>
    public class EventManager
    {
        public class Event
        {
            public string Id { get; set; }
            public string Text { get; set; }
            public DateTime Start { get; set; }
            public DateTime End { get; set; }
        }
        
        
        public DataTable FilteredData(DateTime start, DateTime end)
        {
            DbDataAdapter da = Db.CreateDataAdapter("SELECT * FROM [event] WHERE NOT (([eventend] <= @start) OR ([eventstart] >= @end))");
            Db.AddParameterWithValue(da.SelectCommand, "start", start);
            Db.AddParameterWithValue(da.SelectCommand, "end", end);

            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt;
        }

        public void EventEdit(string id, string name, DateTime start, DateTime end)
        {
            using (var con = Db.CreateConnection())
            {
                con.Open();

                var cmd = Db.CreateCommand("UPDATE [event] SET [name] = @name, [eventstart] = @start, [eventend] = @end WHERE [id] = @id", con);
                Db.AddParameterWithValue(cmd, "id", id);
                Db.AddParameterWithValue(cmd, "start", start);
                Db.AddParameterWithValue(cmd, "end", end);
                Db.AddParameterWithValue(cmd, "name", name);
                cmd.ExecuteNonQuery();
            }
        }

        public void EventMove(string id, DateTime start, DateTime end)
        {
            using (var con = Db.CreateConnection())
            {
                con.Open();

                var cmd = Db.CreateCommand("UPDATE [event] SET [eventstart] = @start, [eventend] = @end WHERE [id] = @id", con);
                Db.AddParameterWithValue(cmd, "id", id);
                Db.AddParameterWithValue(cmd, "start", start);
                Db.AddParameterWithValue(cmd, "end", end);
                cmd.ExecuteNonQuery();
            } 
            
        }

        public Event Get(string id)
        {
            var da = Db.CreateDataAdapter("SELECT * FROM [event] WHERE id = @id");
            Db.AddParameterWithValue(da.SelectCommand, "id", id);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                return new Event
                {
                    Id = id,
                    Text = (string)dr["name"],
                    Start = (DateTime)dr["eventstart"],
                    End = (DateTime)dr["eventend"]
                };
            }
            return null;
        }

        public void EventCreate(DateTime start, DateTime end, string name)
        {
            using (var con = Db.CreateConnection())
            {
                con.Open();

                var cmd = Db.CreateCommand("INSERT INTO [event] (eventstart, eventend, name) VALUES (@start, @end, @name)", con);
                Db.AddParameterWithValue(cmd, "start", start);
                Db.AddParameterWithValue(cmd, "end", end);
                Db.AddParameterWithValue(cmd, "name", name);
                cmd.ExecuteNonQuery();
            }
        }


        public void EventDelete(string id)
        {
            using (var con = Db.CreateConnection())
            {
                con.Open();

                var cmd = Db.CreateCommand("DELETE FROM [event] WHERE id = @id", con);
                Db.AddParameterWithValue(cmd, "id", id);
                cmd.ExecuteNonQuery();

            }
        }
    }

}