Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Net
Imports System.Web.Http
Imports System.Web.Mvc
Imports Data
Imports DayPilot.Web.Mvc.Json
Imports TutorialCS

Public Class EventController
	Inherits Controller

	Public Function Edit(ByVal id As String) As ActionResult
		Dim e = If((New EventManager()).Get(id), New EventManager.Event())
		Return View(e)
	End Function

	<AcceptVerbs(HttpVerbs.Post)> _
	Public Function Edit(ByVal form As FormCollection) As ActionResult
		Dim start As Date = Convert.ToDateTime(form("Start"))
		Dim [end] As Date = Convert.ToDateTime(form("End"))
		CType(New EventManager(), EventManager).EventEdit(form("Id"), form("Text"), start, [end])
		Return JavaScript(SimpleJsonSerializer.Serialize("OK"))
	End Function


	Public Function Create() As ActionResult
		Return View(New EventManager.Event With {.Start = Convert.ToDateTime(Request.QueryString("start")), .End = Convert.ToDateTime(Request.QueryString("end"))})
	End Function

	<AcceptVerbs(HttpVerbs.Post)> _
	Public Function Create(ByVal form As FormCollection) As ActionResult
		Dim start As Date = Convert.ToDateTime(form("Start"))
		Dim [end] As Date = Convert.ToDateTime(form("End"))
		CType(New EventManager(), EventManager).EventCreate(start, [end], form("Text"))
		Return JavaScript(SimpleJsonSerializer.Serialize("OK"))
	End Function
End Class
