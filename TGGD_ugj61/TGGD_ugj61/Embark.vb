Imports Spectre.Console
Imports UGJ61.Game

Module Embark
    Private Const EmoteText = "Emote"
    Private Const GameMenuText = "Game Menu"
    Private Const InteractText = "Interact..."
    Private Const StatisticsText = "Statistics"
    Private Const HatchPlot = "Hatch a Villainous Plot"
    Private Const ExecutePlot = "Execute Villainous Plot"
    Private Const CancelPlot = "Cancel Villainous Plot"
    Private Const ConvolutePlot = "Convolute Villanous Plot"
    Sub Run()
        Game.Start()
        AnsiConsole.WriteLine()
        AnsiConsole.MarkupLine("So, you embarked. Good for you!")
        Dim done = False
        While Not done
            AnsiConsole.WriteLine()
            Dim character As New PlayerCharacter()
            AnsiConsole.MarkupLine($"Location: {character.Location.Name}")
            ShowCharacters(character.Location)
            Dim prompt As New SelectionPrompt(Of String) With {
                .Title = "[olive]Now what?[/]"
            }
            If character.CanInteract Then
                prompt.AddChoice(InteractText)
            End If
            If character.CurrentPlot Is Nothing Then
                prompt.AddChoice(HatchPlot)
            Else
                'prompt.AddChoice(ExecutePlot)
                prompt.AddChoice(ConvolutePlot)
                'prompt.AddChoice(CancelPlot)
            End If
            prompt.AddChoice(EmoteText)
            prompt.AddChoice(StatisticsText)
            prompt.AddChoice(GameMenuText)
            Select Case AnsiConsole.Prompt(prompt)
                Case HatchPlot
                    HandleHatchingPlot(character)
                Case ConvolutePlot
                    HandleConvolutingPlot(character)
                Case ExecutePlot
                    'TODO
                Case StatisticsText
                    ShowStatistics(character)
                Case InteractText
                    InteractMenu.Run(character)
                Case GameMenuText
                    done = GameMenu.Run()
                Case EmoteText
                    EmoteMenu.Run()
                Case Else
                    Throw New NotImplementedException
            End Select
        End While
        Game.Finish()
    End Sub

    Private Sub HandleConvolutingPlot(character As PlayerCharacter)
        character.CurrentPlot.Convolute()
        AnsiConsole.WriteLine()
        AnsiConsole.MarkupLine("[green]You convolute yer villainous plot![/]")
    End Sub

    Private Sub HandleHatchingPlot(character As PlayerCharacter)
        character.HatchPlot()
        AnsiConsole.WriteLine()
        AnsiConsole.MarkupLine("[green]You hatch a villainous plot![/]")
    End Sub
    Private Sub ShowStatistics(character As PlayerCharacter)
        AnsiConsole.WriteLine()
        AnsiConsole.MarkupLine("[teal]Statistics:[/]")
        For Each statisticType In AllStatisticTypes.Where(Function(x) character.HasStatistic(x))
            AnsiConsole.MarkupLine($"{statisticType.Name}: {character.GetStatistic(statisticType).Value}")
        Next
    End Sub

    Private Sub ShowCharacters(location As Location)
        AnsiConsole.MarkupLine($"Characters here: {String.Join(", ", location.Characters.Select(Function(x) x.Name))}")
    End Sub
End Module
