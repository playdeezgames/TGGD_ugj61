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
    ReadOnly Property StackedCharacters As Dictionary(Of CharacterType, List(Of Character))
        Get
            Dim result As New Dictionary(Of CharacterType, List(Of Character))
            For Each character In Characters
                If result.ContainsKey(character.CharacterType) Then
                    result(character.CharacterType).Add(character)
                Else
                    result.Add(character.CharacterType, New List(Of Character) From {character})
                End If
            Next
            Return result
        End Get
    End Property
    Function HasCharacterType(characterType As CharacterType) As Boolean
        Return Characters.Any(Function(character) character.CharacterType = characterType)
    End Function
    ReadOnly Property UnderConstruction As Boolean
        Get
            Dim constructionNeeded = LocationStatisticData.Read(Id, StatisticType.ConstructionNeeded)
            Return constructionNeeded IsNot Nothing AndAlso constructionNeeded.Value > 0
        End Get
    End Property
End Class
