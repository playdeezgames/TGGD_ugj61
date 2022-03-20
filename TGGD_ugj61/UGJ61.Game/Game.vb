
Imports UGJ61.Data

Public Module Game
    Sub Start()
        Store.Reset()
        CreatePlayerCharacter()
    End Sub

    Private Sub CreatePlayerCharacter()
        Dim locationId = LocationData.Create(LocationType.Lair)
        Dim characterId = CharacterData.Create(CharacterType.Villain, locationId)
        PlayerData.Write(characterId)
    End Sub

    Sub Finish()
        Store.ShutDown()
    End Sub
    Public Event PlaySfx As Action(Of Sfx)
    Sub Play(sfx As Sfx)
        RaiseEvent PlaySfx(sfx)
    End Sub
End Module