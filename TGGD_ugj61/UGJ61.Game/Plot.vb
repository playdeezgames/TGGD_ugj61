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
    Private Sub ProcessSuccess(ByRef results As List(Of PlotResult), convolutedness As Long)
        Dim character As New Character(Id)
        Dim hasHero = character.Location.HasCharacterType(CharacterType.Hero)
        Dim hasLoveInterest = character.Location.HasCharacterType(CharacterType.LoveInterest)
        If convolutedness >= 20 AndAlso Not hasHero Then
            results.Add(PlotResult.CaptureHero)
            CharacterData.Create(CharacterType.Hero, character.Location.Id)
            convolutedness -= 20
        End If
        If convolutedness >= 10 AndAlso Not hasLoveInterest Then
            results.Add(PlotResult.CaptureLoveInterest)
            CharacterData.Create(CharacterType.LoveInterest, character.Location.Id)
            convolutedness -= 10
        End If
        If convolutedness >= 5 Then
            results.Add(PlotResult.GainMinions)
        End If
        While convolutedness >= 5
            convolutedness -= 5
            CharacterData.Create(CharacterType.Minion, character.Location.Id)
        End While
        If convolutedness > 0 Then
            results.Add(PlotResult.GainVillainy)
            character.ChangeStatistic(StatisticType.Villainy, convolutedness)
        End If
    End Sub
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
            ProcessSuccess(results, convolutedness)
        Else
            results.Add(PlotResult.Failure)
        End If
        Return results
    End Function
End Class
