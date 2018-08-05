Imports System.Net
Imports System.Web.Http

Public Class ApiControllers
    Friend Shared ReadOnly RequestManager As New RequestManager()

    <Route("api/[controller]")>
    Public Class VisitController
        Inherits ApiController

        ' POST api/<controller>
        Public Function PostValue(<FromBody> Client As Patient) As UInteger
            Return RequestManager.RequestVisit(Client)
        End Function
    End Class

    <Route("api/[controller]")>
    Public Class CheckController
        Inherits ApiController

        'Checks the status of a pending request
        <HttpGet>
        Public Function GetValue(<FromBody> Id As UInteger) As Visitor
            Return RequestManager.GetStatus(Id)
        End Function
    End Class

    <Route("api/[controller]")>
    Public Class AcceptController
        Inherits ApiController

        'Accepts a locked visit request.
        <HttpPost>
        Public Function Post(<FromBody> Id As UInteger, Visitor As Visitor) As VisitRequest
            Return RequestManager.AcceptRequest(Id, Visitor)
        End Function
    End Class

    <Route("api/[controller]")>
    Public Class RejectController
        Inherits ApiController

        'Rejects a locked visit request.
        <HttpPost>
        Public Sub Post(<FromBody> Id As UInteger)
            RequestManager.RejectRequest(Id)
        End Sub
    End Class


    <Route("api/[controller]")>
    Public Class FindController
        Inherits ApiController

        'Returns the oldest request within a certain distance from provided GPS coordinates
        'NOTE: SUPER, EXTRA-SPECIAL *NOT* SECURE IN THE SLIGHTEST
        <HttpGet>
        Public Function GetValue(<FromBody> Location As GPSLocation, Range As UInteger) As PendingRequestInfo
            Return RequestManager.GetRequestInRange(Location, Range)
        End Function
    End Class
End Class