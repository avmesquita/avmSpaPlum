Imports System
Imports System.Collections.Generic
Imports System.Configuration
Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports System.Linq
Imports System.Web
Imports System.Web.Mvc

Namespace Data
	''' <summary>
	''' Summary description for EventManager
	''' </summary>
	Public Class EventManager
		Public Class [Event]
			Public Property Id() As String
			Public Property Text() As String
			Public Property Start() As Date
			Public Property [End]() As Date
		End Class


		Public Function FilteredData(ByVal start As Date, ByVal [end] As Date) As DataTable
			Dim da As DbDataAdapter = Db.CreateDataAdapter("SELECT * FROM [event] WHERE NOT (([eventend] <= @start) OR ([eventstart] >= @end))")
			Db.AddParameterWithValue(da.SelectCommand, "start", start)
			Db.AddParameterWithValue(da.SelectCommand, "end", [end])

			Dim dt As New DataTable()
			da.Fill(dt)

			Return dt
		End Function

		Public Sub EventEdit(ByVal id As String, ByVal name As String, ByVal start As Date, ByVal [end] As Date)
			Using con = Db.CreateConnection()
				con.Open()

				Dim cmd = Db.CreateCommand("UPDATE [event] SET [name] = @name, [eventstart] = @start, [eventend] = @end WHERE [id] = @id", con)
				Db.AddParameterWithValue(cmd, "id", id)
				Db.AddParameterWithValue(cmd, "start", start)
				Db.AddParameterWithValue(cmd, "end", [end])
				Db.AddParameterWithValue(cmd, "name", name)
				cmd.ExecuteNonQuery()
			End Using
		End Sub

		Public Sub EventMove(ByVal id As String, ByVal start As Date, ByVal [end] As Date)
			Using con = Db.CreateConnection()
				con.Open()

				Dim cmd = Db.CreateCommand("UPDATE [event] SET [eventstart] = @start, [eventend] = @end WHERE [id] = @id", con)
				Db.AddParameterWithValue(cmd, "id", id)
				Db.AddParameterWithValue(cmd, "start", start)
				Db.AddParameterWithValue(cmd, "end", [end])
				cmd.ExecuteNonQuery()
			End Using

		End Sub

		Public Function [Get](ByVal id As String) As [Event]
			Dim da = Db.CreateDataAdapter("SELECT * FROM [event] WHERE id = @id")
			Db.AddParameterWithValue(da.SelectCommand, "id", id)
			Dim dt As New DataTable()
			da.Fill(dt)
			If dt.Rows.Count > 0 Then
				Dim dr As DataRow = dt.Rows(0)
				Return New [Event] With {.Id = id, .Text = CStr(dr("name")), .Start = CDate(dr("eventstart")), .End = CDate(dr("eventend"))}
			End If
			Return Nothing
		End Function

		Public Sub EventCreate(ByVal start As Date, ByVal [end] As Date, ByVal name As String)
			Using con = Db.CreateConnection()
				con.Open()

				Dim cmd = Db.CreateCommand("INSERT INTO [event] (eventstart, eventend, name) VALUES (@start, @end, @name)", con)
				Db.AddParameterWithValue(cmd, "start", start)
				Db.AddParameterWithValue(cmd, "end", [end])
				Db.AddParameterWithValue(cmd, "name", name)
				cmd.ExecuteNonQuery()
			End Using
		End Sub


		Public Sub EventDelete(ByVal id As String)
			Using con = Db.CreateConnection()
				con.Open()

				Dim cmd = Db.CreateCommand("DELETE FROM [event] WHERE id = @id", con)
				Db.AddParameterWithValue(cmd, "id", id)
				cmd.ExecuteNonQuery()

			End Using
		End Sub
	End Class

End Namespace