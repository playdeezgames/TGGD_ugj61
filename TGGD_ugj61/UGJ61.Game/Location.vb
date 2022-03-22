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
    ReadOnly Property Characters As List(Of Character)
        Get
            Return CharacterData.ReadForLocationId(Id).Select(Function(characterId) New Character(characterId)).ToList
        End Get
    End Property
    Function HasCharacterType(characterType As CharacterType) As Boolean
        Return Characters.Any(Function(character) character.CharacterType = characterType)
    End Function
End Class
