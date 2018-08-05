Imports System.Threading
Imports System.Threading.Tasks

Public NotInheritable Class RequestManager
    Private Const TIMEOUT = 30 'Minutes

    Private PendingRequests As New Dictionary(Of Integer, VisitRequest)
    Private LockedRequests As New Dictionary(Of Integer, VisitRequest)
    Private AcceptedRequests As New Dictionary(Of Integer, (Request As VisitRequest, Visitor As Visitor))

    Public Function RequestVisit(Client As Patient) As Integer
        If Client.Hospital.Coordinates = New GPSLocation Then
            Client.Hospital = (From h As Hospital In (From h As Hospital In Hospitals
                                                      Select h Where Client.Hospital.Name Like h.Name)
                               Select h Where Not Client.Hospital.ZIP = 0 OrElse Client.Hospital.ZIP = h.ZIP).FirstOrDefault
        End If
        Return RequestVisit(New VisitRequest(Client))
    End Function

    Private Function RequestVisit(Request As VisitRequest) As Integer
        Dim rand As New Random()
        Dim id As Integer = rand.Next()
        While PendingRequests.ContainsKey(id) OrElse LockedRequests.ContainsKey(id)
            id = rand.Next
        End While
        PendingRequests.Add(id, Request)
        Return id
    End Function

    Public Function GetRequestInRange(Location As GPSLocation, Range As Integer) As PendingRequestInfo
        Dim sorted = From pair In PendingRequests
                     Order By pair.Value.Time
        For Each request In sorted
            If DistanceFromGPS(Location, request.Value.Client.Hospital.Coordinates) <= Range Then
                LockedRequests.Add(request.Key, request.Value)
                PendingRequests.Remove(request.Key)
                Task.Run(Sub() ForceTimeout(request.Key, New TimeSpan(0, TIMEOUT, 0)))
                Return New PendingRequestInfo With {.ID = request.Key, .Hospital = request.Value.Client.Hospital}
            End If
        Next
        Return Nothing
    End Function

    Private Sub ForceTimeout(id As Integer, timeout As TimeSpan)
        Dim sw = Stopwatch.StartNew()
        Do Until sw.Elapsed >= timeout
            Thread.Sleep(New TimeSpan(0, 0, 5))
            If Not LockedRequests.ContainsKey(id) Then Return
        Loop
        PendingRequests.Add(id, LockedRequests(id))
        LockedRequests.Remove(id)
    End Sub

    Public Function AcceptRequest(id As Integer, visitor As Visitor) As VisitRequest
        If LockedRequests.ContainsKey(id) Then
            Dim retval = LockedRequests(id)
            AcceptedRequests.Add(id, (retval, visitor))
            LockedRequests.Remove(id)
            Return retval
        Else
            Return Nothing
        End If
    End Function

    Public Sub RejectRequest(id As Integer)
        If LockedRequests.ContainsKey(id) Then
            PendingRequests.Add(id, LockedRequests(id))
            LockedRequests.Remove(id)
        End If
    End Sub

    Public Function GetStatus(id As Integer) As Visitor
        If AcceptedRequests.ContainsKey(id) Then
            Return AcceptedRequests(id).Visitor
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Returns distance between two GPS points in miles.
    ''' </summary>
    Private Function DistanceFromGPS(point1 As GPSLocation, point2 As GPSLocation) As Double
        Dim degToRad = Function(degrees#) degrees * Math.PI / 180

        Dim earthRadius% = 3959

        Dim delta As New GPSLocation With
            {
                .Latitude = degToRad(Math.Abs(point2.Latitude - point1.Latitude)),
                .Longitude = degToRad(Math.Abs(point2.Longitude - point1.Longitude))
            }

        Dim a = Math.Sin(delta.Latitude / 2) * Math.Sin(delta.Latitude / 2) +
                Math.Sin(delta.Longitude / 2) * Math.Sin(delta.Longitude / 2) *
                Math.Cos(point1.Latitude) * Math.Cos(point2.Latitude)
        Dim c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a))
        Return earthRadius * c
    End Function
End Class