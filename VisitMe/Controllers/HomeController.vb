Imports System.Net.Http
Imports System.Threading.Tasks

<RequireHttps>
Partial Public Class HomeController
    Inherits Controller

    Function About() As ActionResult
        ViewData("Message") = "Your application description page."

        Return View()
    End Function

    Function Contact() As ActionResult
        ViewData("Message") = "Your contact page."

        Return View()
    End Function
End Class
