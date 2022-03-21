﻿Imports Spectre.Console
Imports UGJ61.Game

Module InteractCharacterMenu
    Private Const ChideText = "Chide"
    Private Const SlapText = "Slap"
    Sub Run(character As PlayerCharacter, otherCharacter As Character)
        Dim done = False
        While Not done
            AnsiConsole.WriteLine()
            Dim prompt As New SelectionPrompt(Of String) With
                {
                    .Title = "[olive]How to interact?[/]"
                }
            prompt.AddChoice(ChideText)
            prompt.AddChoice(SlapText)
            prompt.AddChoice(NeverMindText)
            Select Case AnsiConsole.Prompt(prompt)
                Case ChideText
                    HandleChide(character, otherCharacter)
                Case SlapText
                    HandleSlap(character, otherCharacter)
                Case NeverMindText
                    done = True
                Case Else
                    Throw New NotImplementedException
            End Select
        End While
    End Sub

    Private Sub HandleSlap(character As Character, otherCharacter As Character)
        AnsiConsole.WriteLine()
        AnsiConsole.MarkupLine($"[green]You slap {otherCharacter.Name}.[/]")
        Game.Play(Sfx.Slap)
        character.ChangeStatistic(StatisticType.Villainy, 1)
    End Sub

    Private Sub HandleChide(character As Character, otherCharacter As Character)
        AnsiConsole.WriteLine()
        AnsiConsole.MarkupLine($"[green]You chide {otherCharacter.Name}.[/]")
        Game.Play(Sfx.Chide)
        character.ChangeStatistic(StatisticType.Villainy, 1)
    End Sub
End Module
