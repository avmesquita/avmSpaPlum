Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.Mvc
Imports System.Web.Routing

Namespace MvcApplication1
	' Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	' visit http://go.microsoft.com/?LinkId=9394801

	Partial Public Class MvcApplication
		Inherits System.Web.HttpApplication

		Public Shared Sub RegisterRoutes(ByVal routes As RouteCollection)
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}")

			routes.MapRoute("Default", "{controller}/{action}/{id}", New With {Key .controller = "Home", Key .action = "Index", Key .id = ""}) ' Parameter defaults -  URL with parameters -  Route name

		End Sub

		Protected Sub Application_Start()
			RegisterRoutes(RouteTable.Routes)
		End Sub
	End Class
End Namespace