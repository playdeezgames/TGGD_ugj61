Imports UGJ61.Data

Public Class Plot
    ReadOnly Property Id As Long
    Sub New(characterId As Long)
        Id = characterId
    End Sub
    Sub Convolute()
        Dim convolutedness = CharacterPlotData.Read(Id).Value
        CharacterPlotData.Write(Id, convolutedness + 1)
    End Sub
    Sub Cancel()
        CharacterStatisticData.Write(Id, StatisticType.Villainy, CharacterStatisticData.Read(Id, StatisticType.Villainy).Value \ 2)
        CharacterPlotData.Clear(Id)
    End Sub
End Class
