Imports System.Web.Http
Imports System.Web.Optimization

Public Class MvcApplication
    Inherits HttpApplication

    Protected Sub Application_Start()
        AreaRegistration.RegisterAllAreas()
        WebApiConfig.Register(GlobalConfiguration.Configuration)
        FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters)
        RouteConfig.RegisterRoutes(RouteTable.Routes)
        BundleConfig.RegisterBundles(BundleTable.Bundles)
    End Sub
End Class
