Imports System.Web.Http

Public Module WebApiConfig
    Public Sub Register(configuration As HttpConfiguration)
        configuration.Routes.MapHttpRoute("API Default", "api/{controller}/{id}", New With {.id = RouteParameter.Optional})
    End Sub
End Module