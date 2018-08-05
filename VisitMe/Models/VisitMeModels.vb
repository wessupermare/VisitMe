Imports Newtonsoft.Json

Public Structure Patient
    <JsonProperty(PropertyName:="name")>
    Public Name As String
    <JsonProperty(PropertyName:="room")>
    Public Room As String
    <JsonProperty(PropertyName:="info")>
    Public AdditionalLocationInformation As String
    <JsonProperty(PropertyName:="phone")>
    Public PhoneNumber As String
    <JsonProperty(PropertyName:="hospital")>
    Public Hospital As Hospital
End Structure

Public Structure Visitor
    <JsonProperty(PropertyName:="name")>
    Public Name As String
    <JsonProperty(PropertyName:="phone")>
    Public PhoneNumber As String
    <JsonProperty(PropertyName:="title")>
    Public Title As String
    <JsonProperty(PropertyName:="church")>
    Public Church As String
End Structure

Public Structure PendingRequestInfo
    <JsonProperty(PropertyName:="id")>
    Public ID As Integer
    <JsonProperty(PropertyName:="hospital")>
    Public Hospital As Hospital
End Structure

Public Class VisitRequest
    <JsonProperty(PropertyName:="patient")>
    Public ReadOnly Property Client As Patient
    <JsonProperty(PropertyName:="time")>
    Public ReadOnly Property Time As Date

    Public Sub New(Visitee As Patient)
        Client = Visitee
        Time = Date.UtcNow
    End Sub
End Class

Public Structure GPSLocation
    <JsonProperty(PropertyName:="lat")>
    Public Latitude As Double
    <JsonProperty(PropertyName:="lon")>
    Public Longitude As Double

    Public Shared Operator =(left As GPSLocation, right As GPSLocation) As Boolean
        Return left.Latitude = right.Latitude AndAlso left.Longitude = right.Longitude
    End Operator

    Public Shared Operator <>(left As GPSLocation, right As GPSLocation) As Boolean
        Return Not left = right
    End Operator
End Structure

Public Enum State
    Alabama
    Alaska
    Arizona
    Arkansas
    California
    Colorado
    Connecticut
    Delaware
    Florida
    Georgia
    Hawaii
    idaho
    Illinois
    Indiana
    Iowa
    Kansas
    Kentucky
    Louisiana
    Maine
    Maryland
    Massachusetts
    Michigan
    Minnesota
    Mississippi
    Missouri
    Montana
    Nebraska
    Nevada
    New_Hampshire
    New_Jersey
    New_Mexico
    New_York
    North_Carolina
    North_Dakota
    Ohio
    Oklahoma
    Oregon
    Pennsylvania
    Rhode_Island
    South_Carolina
    South_Dakota
    Tennessee
    Texas
    Utah
    Vermont
    Virginia
    Washington
    West_Virginia
    Wisconsin
    Wyoming
End Enum