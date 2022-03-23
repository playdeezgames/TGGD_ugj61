Imports Spectre.Console
Imports UGJ61.Game

Module DeathTrapMenu
    Private Const BeginConstructionText = "Begin construction"
    Sub Run(character As PlayerCharacter)
        Dim done = False
        While Not done
            AnsiConsole.WriteLine()
            Dim prompt As New SelectionPrompt(Of String) With
                {.Title = "[olive]What now?[/]"}
            If Locations.Exists(LocationType.DeathTrap) Then
                AnsiConsole.MarkupLine("TODO: status of deathtrap")
            Else
                AnsiConsole.MarkupLine("You have no death trap!")
                prompt.AddChoice(BeginConstructionText)
            End If
            prompt.AddChoice(NeverMindText)
            Select Case AnsiConsole.Prompt(prompt)
                Case NeverMindText
                    done = True
                Case BeginConstructionText
                    HandleBeginConstruction(character)
                Case Else
                    Throw New NotImplementedException
            End Select
        End While

    End Sub
    Private Sub HandleBeginConstruction(character As PlayerCharacter)
        Locations.CreateDeathTrap()
        AnsiConsole.WriteLine()
        AnsiConsole.MarkupLine("You begin construction of a death trap.")
    End Sub
End Module
