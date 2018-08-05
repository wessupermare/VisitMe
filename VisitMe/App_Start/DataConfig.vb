Imports Newtonsoft.Json
Imports VisitMe.State

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
    <JsonProperty(PropertyName:="zip")>
    Public ZIP As UInteger

    Public Overrides Function ToString() As String
        Return Name
    End Function
End Structure

Public Module DataConfig
    Public Hospitals As New HashSet(Of Hospital) From {
        Create("Torrance Memorial Medical Center", 33.81, -118.3, "3330 Lomita Blvd", "Torrance", California, 90505),
        Create("Providence Little Company of Mary Medical Center Torrance", 34.05, -118.3, "4101 Torrance Blvd", "Torrance", California, 90503)
    }

    Private Function Create(name$, lat#, lon#, address$, city$, state As State, zip As UInteger) As Hospital
        Return New Hospital With {.Name = name, .Coordinates = New GPSLocation() With {.Latitude = lat, .Longitude = lon}, .Address = address, .City = city, .State = state, .ZIP = zip}
    End Function
End Module