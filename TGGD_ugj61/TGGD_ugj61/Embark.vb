Imports Spectre.Console
Imports UGJ61.Game

Module Embark
    Private Const EmoteText = "Emote"
    Private Const GameMenuText = "Game Menu"
    Private Const InteractText = "Interact..."
    Private Const StatisticsText = "Statistics"
    Private Const HatchPlotText = "Hatch a Villainous Plot"
    Private Const ExecutePlotText = "Execute Villainous Plot"
    Private Const CancelPlotText = "Cancel Villainous Plot"
    Private Const ConvolutePlotText = "Convolute Villanous Plot"
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
                prompt.AddChoice(HatchPlotText)
            Else
                prompt.AddChoice(ExecutePlotText)
                prompt.AddChoice(ConvolutePlotText)
                prompt.AddChoice(CancelPlotText)
            End If
            prompt.AddChoice(EmoteText)
            prompt.AddChoice(StatisticsText)
            prompt.AddChoice(GameMenuText)
            Select Case AnsiConsole.Prompt(prompt)
                Case HatchPlotText
                    HandleHatchingPlot(character)
                Case ConvolutePlotText
                    HandleConvolutingPlot(character)
                Case ExecutePlotText
                    HandleExecutingPlot(character)
                Case CancelPlotText
                    HandleCancelingPlot(character)
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

    Private Sub HandleCancelingPlot(character As PlayerCharacter)
        character.CurrentPlot.Cancel()
        AnsiConsole.WriteLine()
        AnsiConsole.MarkupLine("[red]You cancel yer villainous plot![/]")
    End Sub

    Private Sub HandleExecutingPlot(character As PlayerCharacter)
        Dim results = character.CurrentPlot.Execute()
        AnsiConsole.WriteLine()
        For Each result In results
            Select Case result
                Case PlotResult.Success
                    AnsiConsole.MarkupLine("[green]You succeed![/]")
                Case PlotResult.Failure
                    AnsiConsole.MarkupLine("[red]You fail![/]")
                Case PlotResult.CaptureHero
                    AnsiConsole.MarkupLine("[green]You capture the hero![/]")
                Case PlotResult.CaptureLoveInterest
                    AnsiConsole.MarkupLine("[green]You capture yer love interest![/]")
                Case PlotResult.GainMinions
                    AnsiConsole.MarkupLine("[green]You gain minions![/]")
                Case PlotResult.GainVillainy
                    AnsiConsole.MarkupLine("[green]You gain villainy![/]")
                Case PlotResult.LoseMinions
                    AnsiConsole.MarkupLine("[red]You lose minions![/]")
                Case Else
                    Throw New NotImplementedException
            End Select
        Next
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
