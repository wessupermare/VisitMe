@Imports Microsoft.AspNet.Identity

@If Request.IsAuthenticated
    @Using Html.BeginForm("LogOff", "Account", FormMethod.Post, New With { .id = "logoutForm", .class = "navbar-right" })
        @Html.AntiForgeryToken()
        @<ul class="nav navbar-nav navbar-right mr-auto">
            <li>
                @Html.ActionLink("Hello " + User.Identity.GetUserName() + "!", "Index", "Manage", routeValues:=Nothing, htmlAttributes:=New With {.title = "Manage", .class = "nav-link"})
            </li>
            <li><a href="javascript:document.getElementById('logoutForm').submit()" class="nav-link">Log off</a></li>
        </ul>
    End Using
Else
    @<ul class="nav navbar-nav navbar-right mr-auto">
        <li>@Html.ActionLink("Register", "Register", "Account", routeValues:=Nothing, htmlAttributes:=New With {.id = "registerLink", .class = "nav-link"})</li>
        <li>@Html.ActionLink("Log in", "Login", "Account", routeValues:=Nothing, htmlAttributes:=New With {.id = "loginLink", .class = "nav-link"})</li>
    </ul>
End If

