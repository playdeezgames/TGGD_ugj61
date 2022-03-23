Imports Spectre.Console
Imports UGJ61.Game

Module Embark
    Private Const EmoteText = "Emote"
    Private Const GameMenuText = "Game Menu"
    Private Const InteractText = "Interact..."
    Private Const StatisticsText = "Statistics"
    Private Const HireMinionText = "Hire Minion"
    Private Const VillainousPlotText = "Villainous Plot..."
    Private Const DeathTrapText = "Death Trap..."

    Sub Run()
        Game.Start()
        AnsiConsole.WriteLine()
        AnsiConsole.MarkupLine("So, you embarked. Good for you!")
        Dim done = False
        While Not done
            AnsiConsole.WriteLine()
            Dim character As New PlayerCharacter
            AnsiConsole.MarkupLine($"Location: {character.Location.Name}")
            ShowCharacters(character.Location)
            Dim prompt As New SelectionPrompt(Of String) With {
                .Title = "[olive]Now what?[/]"
            }
            If character.CanInteract Then
                prompt.AddChoice(InteractText)
            End If
            prompt.AddChoice(VillainousPlotText)
            If character.CanHireMinion Then
                prompt.AddChoice(HireMinionText)
            End If
            prompt.AddChoice(DeathTrapText)
            prompt.AddChoice(EmoteText)
            prompt.AddChoice(StatisticsText)
            prompt.AddChoice(GameMenuText)
            Select Case AnsiConsole.Prompt(prompt)
                Case VillainousPlotText
                    PlotMenu.Run(character)
                Case HireMinionText
                    HandleHiringMinion(character)
                Case StatisticsText
                    ShowStatistics(character)
                Case InteractText
                    InteractMenu.Run(character)
                Case GameMenuText
                    done = GameMenu.Run()
                Case EmoteText
                    EmoteMenu.Run()
                Case DeathTrapText
                    DeathTrapMenu.Run(character)
                Case Else
                    Throw New NotImplementedException
            End Select
        End While
        Game.Finish()
    End Sub
    Private Sub HandleHiringMinion(character As PlayerCharacter)
        character.HireMinion()
        AnsiConsole.WriteLine()
        AnsiConsole.MarkupLine("[green]You hire a minion![/]")
    End Sub
    Private Sub ShowStatistics(character As PlayerCharacter)
        AnsiConsole.WriteLine()
        AnsiConsole.MarkupLine("[teal]Statistics:[/]")
        For Each statisticType In AllStatisticTypes.Where(Function(x) character.HasStatistic(x))
            AnsiConsole.MarkupLine($"{statisticType.Name}: {character.GetStatistic(statisticType).Value}")
        Next
    End Sub

    Private Sub ShowCharacters(location As Location)
        Dim stackedCharacters = location.StackedCharacters.Select(Function(entry)
                                                                      If entry.Value.Count > 1 Then
                                                                          Return $"{entry.Value.First.Name}(x{entry.Value.Count})"
                                                                      Else
                                                                          Return entry.Value.First.Name
                                                                      End If
                                                                  End Function)
        AnsiConsole.MarkupLine($"Characters here: {String.Join(", ", stackedCharacters)}")
    End Sub
End Module
