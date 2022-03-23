Imports Spectre.Console
Imports UGJ61.Game

Module InteractCharacterMenu
    Private Const ChideText = "Chide"
    Private Const SlapText = "Slap"
    Private Const WooText = "Woo"
    Private Const PlaceInDeathTrap = "Place in death trap"
    Private Const MonologueAt = "Monologue at"
    Sub Run(character As PlayerCharacter, otherCharacter As Character)
        Dim done = False
        While Not done
            AnsiConsole.WriteLine()
            Dim prompt As New SelectionPrompt(Of String) With
                {
                    .Title = "[olive]How to interact?[/]"
                }
            If otherCharacter.CanChide Then
                prompt.AddChoice(ChideText)
            End If
            If otherCharacter.CanSlap Then
                prompt.AddChoice(SlapText)
            End If
            If otherCharacter.CanWoo Then
                prompt.AddChoice(WooText)
            End If
            If otherCharacter.CanPlaceInDeathTrap Then
                prompt.AddChoice(PlaceInDeathTrap)
            End If
            If otherCharacter.CanMonologueAt Then
                prompt.AddChoice(MonologueAt)
            End If
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
