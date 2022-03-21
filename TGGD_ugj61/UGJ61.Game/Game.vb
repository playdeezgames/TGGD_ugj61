
Imports UGJ61.Data

Public Module Game
    Sub Start()
        Store.Reset()
        Dim locationId = LocationData.Create(LocationType.Lair)
        CreatePlayerCharacter(locationId)
        CreateLeftenant(locationId)
    End Sub

    Private Sub CreateLeftenant(locationId As Long)
        CharacterData.Create(CharacterType.Leftenant, locationId)
    End Sub

    Private Sub CreatePlayerCharacter(locationId As Long)
        Dim characterId = CharacterData.Create(CharacterType.Villain, locationId)
        CharacterStatisticData.Write(characterId, StatisticType.Villainy, 0)
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
