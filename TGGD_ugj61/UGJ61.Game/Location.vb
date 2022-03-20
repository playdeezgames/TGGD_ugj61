Imports UGJ61.Data

Public Class Location
    ReadOnly Property Id As Long
    Sub New(locationId As Long)
        Id = locationId
    End Sub
    ReadOnly Property LocationType As LocationType
        Get
            Return CType(LocationData.ReadLocationType(Id).Value, LocationType)
        End Get
    End Property
    ReadOnly Property Name As String
        Get
            Select Case LocationType
                Case LocationType.Lair
                    Return "Yer Lair"
                Case Else
                    Throw New NotImplementedException
            End Select
        End Get
    End Property
End Class
