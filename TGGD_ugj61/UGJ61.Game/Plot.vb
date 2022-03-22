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
    ReadOnly Property CanExecute As Boolean
        Get
            Dim convolutedness = CharacterPlotData.Read(Id).Value
            Dim minions = New Character(Id).Location.Characters.Where(Function(character) character.CharacterType.CanExecutePlot)
            Return minions.Count * 2 >= convolutedness
        End Get
    End Property
    Function Execute() As List(Of PlotResult)
        Dim convolutedness = CharacterPlotData.Read(Id).Value
        Dim minionCount = New Character(Id).Location.Characters.Where(Function(character) character.CharacterType.CanExecutePlot).Count
        Dim generator As New Dictionary(Of Boolean, Integer) From
            {
                {True, minionCount},
                {False, CInt(convolutedness)}
            }
        CharacterPlotData.Clear(Id)
        Dim results As New List(Of PlotResult)
        If RNG.FromGenerator(generator) Then
            results.Add(PlotResult.Success)
        Else
            results.Add(PlotResult.Failure)
        End If
        Return results
    End Function
End Class
