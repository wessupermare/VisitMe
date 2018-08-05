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

Public Structure Hospital
    <JsonProperty(PropertyName:="name")>
    Public Name As String
    <JsonProperty(PropertyName:="loc")>
    Public Coordinates As GPSLocation
    <JsonProperty(PropertyName:="addr")>
    Public Address As String
    <JsonProperty(PropertyName:="city")>
    Public City As String
    <JsonProperty(PropertyName:="state")>
    Public State As State
End Structure

Public Structure PendingRequestInfo
    <JsonProperty(PropertyName:="id")>
    Public ID As UInteger
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
    Idaho
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