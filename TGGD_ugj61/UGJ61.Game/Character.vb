Imports UGJ61.Data

Public Class Character
    ReadOnly Property Id As Long
    Sub New(characterId As Long)
        Id = characterId
    End Sub
    ReadOnly Property Location As Location
        Get
            Return New Location(CharacterData.ReadLocationId(Id).Value)
        End Get
    End Property
    ReadOnly Property CharacterType As CharacterType
        Get
            Return CType(CharacterData.ReadCharacterType(Id).Value, CharacterType)
        End Get
    End Property
    ReadOnly Property Name As String
        Get
            Select Case CharacterType
                Case CharacterType.Leftenant
                    Return "Yer trusty leftenant"
                Case CharacterType.Villain
                    Return "The Villain(you)"
                Case Else
                    Throw New NotImplementedException
            End Select
        End Get
    End Property
    ReadOnly Property CanInteract As Boolean
        Get
            Return Location.Characters.Any(Function(x) x.Id <> Id)
        End Get
    End Property
End Class
