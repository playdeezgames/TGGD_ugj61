Imports Spectre.Console
Imports UGJ61.Game

Module DeathTrapMenu
    Private Const BeginConstructionText = "Begin construction"
    Private Const ConstructText = "Construct"
    Private Const TurnDeathTrapOnText = "Start deathtrap and leave, assuming that it will complete the job unattended"
    Sub Run(character As PlayerCharacter)
        Dim done = False
        While Not done
            AnsiConsole.WriteLine()
            Dim prompt As New SelectionPrompt(Of String) With
                {.Title = "[olive]What now?[/]"}
            If Locations.Exists(LocationType.DeathTrap) Then
                Dim deathTrap As Location = Locations.GetDeathTrap()
                If deathTrap.UnderConstruction Then
                    AnsiConsole.MarkupLine($"Construction remaining: {Locations.GetDeathTrap().ConstructionNeeded}")
                    If Characters.Exists(CharacterType.Minion) Then
                        prompt.AddChoice(ConstructText)
                    Else
                        AnsiConsole.MarkupLine("You need a minion to carry on construction. You have none.")
                    End If
                Else
                    AnsiConsole.MarkupLine("Status: Ready for use!")
                    ShowCharacters(deathTrap)
                    If deathTrap.HasCharacter Then
                        prompt.AddChoice(TurnDeathTrapOnText)
                    End If
                End If
            Else
                AnsiConsole.MarkupLine("You have no death trap!")
                If Characters.Exists(CharacterType.Minion) Then
                    prompt.AddChoice(BeginConstructionText)
                Else
                    AnsiConsole.MarkupLine("You need a minion to begin construction.")
                End If
            End If
            prompt.AddChoice(NeverMindText)
            Select Case AnsiConsole.Prompt(prompt)
                Case NeverMindText
                    done = True
                Case BeginConstructionText
                    HandleBeginConstruction(character)
                Case ConstructText
                    HandleConstruction(character)
                Case TurnDeathTrapOnText
                    HandleTurnDeathTrapOn()
                Case Else
                    Throw New NotImplementedException
            End Select
        End While

    End Sub
    Private Sub HandleTurnDeathTrapOn()
        AnsiConsole.WriteLine()
        AnsiConsole.MarkupLine("[red]The captive(s) manage(s) to escape just in the nick of time![/]")
        For Each character In GetDeathTrap().Characters
            character.Destroy()
        Next
        GetDeathTrap().Destroy()
    End Sub
    Private Sub HandleConstruction(character As PlayerCharacter)
        Dim minion = AllCharactersOfCharacterType(CharacterType.Minion).First
        Dim deathTrap = GetDeathTrap()
        Select Case deathTrap.Construct(minion)
            Case ConstructionResultType.LostMinion
                AnsiConsole.MarkupLine("[red]No progress. Minion lost.[/]")
            Case ConstructionResultType.Success
                AnsiConsole.MarkupLine("[green]Progress![/]")
            Case Else
                Throw New NotImplementedException
        End Select
    End Sub
    Private Sub HandleBeginConstruction(character As PlayerCharacter)
        Locations.CreateDeathTrap()
        AnsiConsole.WriteLine()
        AnsiConsole.MarkupLine("You begin construction of a death trap.")
    End Sub
End Module
