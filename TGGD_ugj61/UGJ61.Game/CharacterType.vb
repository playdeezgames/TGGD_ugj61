Imports System.Runtime.CompilerServices

Public Enum CharacterType
    Villain
    Leftenant
    Minion
    LoveInterest
    Hero
End Enum
Public Module CharacterTypeExtensions
    <Extension()>
    Function CanExecutePlot(characterType As CharacterType) As Boolean
        Return characterType = CharacterType.Leftenant OrElse characterType = CharacterType.Minion
    End Function
End Module
