﻿Imports Spectre.Console
Imports UGJ61.Game

Module DeathTrapMenu
    Private Const BeginConstructionText = "Begin construction"
    Private Const ConstructText = "Construct"
    Sub Run(character As PlayerCharacter)
        Dim done = False
        While Not done
            AnsiConsole.WriteLine()
            Dim prompt As New SelectionPrompt(Of String) With
                {.Title = "[olive]What now?[/]"}
            If Locations.Exists(LocationType.DeathTrap) Then
                Dim deathTrap As Location = Locations.GetDeathTrap()
                If deathTrap.UnderConstruction Then
                    AnsiConsole.MarkupLine("Status: Under construction")
                    prompt.AddChoice(ConstructText)
                Else
                    AnsiConsole.MarkupLine("Status: Ready for use")
                End If
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
                Case ConstructText
                    HandleConstruction(character)
                Case Else
                    Throw New NotImplementedException
            End Select
        End While

    End Sub
    Private Sub HandleConstruction(character As PlayerCharacter)
        Throw New NotImplementedException()
    End Sub
    Private Sub HandleBeginConstruction(character As PlayerCharacter)
        Locations.CreateDeathTrap()
        AnsiConsole.WriteLine()
        AnsiConsole.MarkupLine("You begin construction of a death trap.")
    End Sub
End Module
