Imports System.Web
Imports System.Web.Mvc
Imports System.Web.UI

Partial Public Class _Default
	Inherits System.Web.UI.Page

		Public Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
			' Change the current path so that the Routing handler can correctly interpret
			' the request, then restore the original path so that the OutputCache module
			' can correctly process the response (if caching is enabled).

			Dim originalPath As String = Request.Path
			HttpContext.Current.RewritePath(Request.ApplicationPath, False)
			Dim httpHandler As IHttpHandler = New MvcHttpHandler()
			httpHandler.ProcessRequest(HttpContext.Current)
			HttpContext.Current.RewritePath(originalPath, False)
		End Sub
End Class
