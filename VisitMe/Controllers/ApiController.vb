Imports System.Net
Imports System.Web.Http

Public Class ApiControllers
    Friend Shared ReadOnly RequestManager As New RequestManager()

    <RoutePrefix("api/request")>
    Public Class RequestController
        Inherits ApiController

        ' POST api/<controller>
        <Route("")>
        Public Function PostValue(<FromBody> Client As Patient) As Integer
            Return RequestManager.RequestVisit(Client)
        End Function
    End Class

    <RoutePrefix("api/check")>
    Public Class CheckController
        Inherits ApiController

        'Checks the status of a pending request
        <Route("{id:int}")>
        <HttpGet>
        Public Function GetValue(id As Integer) As Visitor
            Return RequestManager.GetStatus(id)
        End Function
    End Class

    <RoutePrefix("api/accept")>
    Public Class AcceptController
        Inherits ApiController

        'Accepts a locked visit request.
        <Route("{id:int}")>
        <HttpPost>
        Public Function Post(id As Integer, <FromBody> Visitor As Visitor) As VisitRequest
            Return RequestManager.AcceptRequest(id, Visitor)
        End Function
    End Class

    <RoutePrefix("api/reject")>
    Public Class RejectController
        Inherits ApiController

        'Rejects a locked visit request.
        <Route("{id:int}")>
        <HttpPost>
        Public Sub Post(id As Integer)
            RequestManager.RejectRequest(id)
        End Sub
    End Class


    <RoutePrefix("api/find")>
    Public Class FindController
        Inherits ApiController

        'Returns the oldest request within a certain distance from provided GPS coordinates
        'NOTE: SUPER, EXTRA-SPECIAL *NOT* SECURE IN THE SLIGHTEST
        <Route("{id:int:min(0)}")>
        <HttpPost>
        Public Function Post(id As Integer, <FromBody> Location As GPSLocation) As PendingRequestInfo
            Return RequestManager.GetRequestInRange(Location, id)
        End Function
    End Class
End Class