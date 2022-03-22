Imports System.Runtime.CompilerServices

Public Enum CharacterType
    Villain
    Leftenant
    Henchman
End Enum
Public Module CharacterTypeExtensions
    <Extension()>
    Function CanExecutePlot(characterType As CharacterType) As Boolean
        Return characterType = CharacterType.Leftenant OrElse characterType = CharacterType.Henchman
    End Function
End Module
