Imports System.Net.Http
Imports System.Threading.Tasks

Partial Public Class HomeController
    Function Index() As ActionResult
        Return View()
    End Function

    Function IndexFromPRI(pri As PendingRequestInfo) As ActionResult
        Return View("Index", pri)
    End Function

    Function PatientInfo(visitdata As VisitRequest) As ActionResult
        Return View("Visit", visitdata.Client)
    End Function

    Public Async Function CreateVisitAsync(name$, zip%, hospital$, room$, info$) As Task(Of ActionResult)
        Dim client As New Patient With {
            .Name = name,
            .Hospital = New Hospital With {.Name = hospital, .ZIP = zip},
            .Room = room,
            .AdditionalLocationInformation = info
        }

        Dim result As UInteger = 0

        Await Task.Run(Sub() result = (New ApiControllers.RequestController).PostValue(client))
        Return IndexFromPRI(New PendingRequestInfo With {.ID = result})
    End Function

    Public Function VisitFound(data As String) As ActionResult
        Dim pri As PendingRequestInfo = Newtonsoft.Json.JsonConvert.DeserializeObject(Of PendingRequestInfo)(data)
        Return IndexFromPRI(pri)
    End Function

    Public Async Function AnswerRequestAsync(name As String, church As String, title As String, phone As String, action As String, ID As Integer) As Task(Of ActionResult)
        If name IsNot Nothing AndAlso church IsNot Nothing AndAlso title IsNot Nothing AndAlso phone IsNot Nothing AndAlso action = "accept" Then
            Dim reqres As VisitRequest = Await OffloadAsync(Function() (New ApiControllers.AcceptController).Post(ID, New Visitor With {.Name = name, .Church = church, .Title = title, .PhoneNumber = phone}))
            Return PatientInfo(reqres)
        Else    'Reject
            Await OffloadAsync(Function()
                                   Call (New ApiControllers.RejectController).Post(ID)
                                   Return ""
                               End Function)
            Return Index()
        End If
    End Function

    Private Async Function OffloadAsync(Of T)(routine As Func(Of T)) As Task(Of T)
        Dim result As T
        Await Task.Run(Sub() result = routine.Invoke)
        Return result
    End Function
End Class