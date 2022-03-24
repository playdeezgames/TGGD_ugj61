Imports UGJ61.Data

Public Module Characters
    Function Exists(characterType As CharacterType) As Boolean
        Return CharacterData.ReadCountByCharacterType(characterType) > 0
    End Function
    Function AllCharactersOfCharacterType(characterType As CharacterType) As List(Of Character)
        Return CharacterData.ReadForCharacterType(characterType).Select(Function(x) New Character(x)).ToList
    End Function
End Module
