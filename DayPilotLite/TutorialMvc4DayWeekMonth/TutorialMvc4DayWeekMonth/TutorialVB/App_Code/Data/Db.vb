Imports System
Imports System.Collections.Generic
Imports System.Configuration
Imports System.Data
Imports System.Data.Common
Imports System.IO
Imports System.Linq
Imports System.Web

Namespace Data
	Public Class Db

		Private Shared ReadOnly Property ConnectionString() As String
			Get
				Dim mssql As Boolean = Not SqLiteFound()
				If mssql Then
					Return ConfigurationManager.ConnectionStrings("daypilot").ConnectionString
				End If

				If TryCast(HttpContext.Current.Session("cs"), String) Is Nothing Then
					HttpContext.Current.Session("cs") = GetNew()
				End If

				Return CStr(HttpContext.Current.Session("cs"))
			End Get
		End Property

		Private Shared ReadOnly Property Factory() As DbProviderFactory
			Get
				Return DbProviderFactories.GetFactory(FactoryName())
			End Get
		End Property

		Private Shared Function FactoryName() As String
			If SqLiteFound() Then
				Return "System.Data.SQLite"
			End If
			Return "System.Data.SqlClient"
		End Function

		Private Shared Function IdentityCommand() As String
			Select Case FactoryName()
				Case "System.Data.SQLite"
					Return "select last_insert_rowid();"
				Case "System.Data.SqlClient"
					Return "select @@identity;"
				Case Else
					Throw New NotSupportedException("Unsupported DB factory.")
			End Select
		End Function


		Private Shared Function GetNew() As String
			Dim today As String = Date.Today.ToString("yyyy-MM-dd")
			Dim guid As String = System.Guid.NewGuid().ToString()
			Dim dir As String = HttpContext.Current.Server.MapPath("~/App_Data/session/" & today & "/")
			Dim master As String = HttpContext.Current.Server.MapPath("~/App_Data/daypilot.sqlite")
			Dim path As String = dir & guid

			Directory.CreateDirectory(dir)
			File.Copy(master, path)

			Return String.Format("Data Source={0}", path)
		End Function

		Private Shared Function SqLiteFound() As Boolean
			Dim path As String = HttpContext.Current.Server.MapPath("~/bin/System.Data.SQLite.dll")
			Return File.Exists(path)
		End Function


		Public Shared Function CreateDataAdapter(ByVal [select] As String) As DbDataAdapter
			Dim da As DbDataAdapter = Factory.CreateDataAdapter()
			da.SelectCommand = CreateCommand([select])
			Return da
		End Function


		Public Shared Function CreateConnection() As DbConnection
			Dim connection As DbConnection = Factory.CreateConnection()
			connection.ConnectionString = ConnectionString
			Return connection
		End Function

		Public Shared Function CreateCommand(ByVal text As String) As DbCommand
			Dim command As DbCommand = Factory.CreateCommand()
			command.CommandText = text
			command.Connection = CreateConnection()

			Return command
		End Function

		Public Shared Function CreateCommand(ByVal text As String, ByVal connection As DbConnection) As DbCommand
			Dim command As DbCommand = Factory.CreateCommand()
			command.CommandText = text
			command.Connection = connection

			Return command
		End Function

		Public Shared Sub AddParameterWithValue(ByVal cmd As DbCommand, ByVal name As String, ByVal value As Object)
			Dim parameter = Factory.CreateParameter()
			parameter.Direction = ParameterDirection.Input
			parameter.ParameterName = name
			parameter.Value = value
			cmd.Parameters.Add(parameter)
		End Sub

		Public Shared Function GetIdentity(ByVal c As DbConnection) As Integer
			Dim cmd = CreateCommand(Db.IdentityCommand(), c)
			Return Convert.ToInt32(cmd.ExecuteScalar())
		End Function

	End Class
End Namespace