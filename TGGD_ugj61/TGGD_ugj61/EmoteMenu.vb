Imports Spectre.Console
Imports UGJ61.Game

Module EmoteMenu
    Private Const TwirlMustacheText = "Twirl Mustache"
    Private Sub TwirlMustache()
        Dim character As New PlayerCharacter
        character.ChangeStatistic(StatisticType.Villainy, 1)
        AnsiConsole.MarkupLine(
"MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM
MMMMMMMMMMMMMMMMMNkc;'',:lkXMMMMXkl:,'';ckNMMMMMMMMMMMMMMMMM
MMMMMMMMMMMMMMMNk,         'codc'         ,xNMMMMMMMMMMMMMMM
MMMMMMMMMMMMMMKc.                          .cKWMMMMMMMMMMMMM
MMMMMMMMMMMMNx'                              'xNMMMMMMMMMMMM
NKXWMMMMMMNO:.                                .:ONMMMMMMWXKN
Wk::oxkxdl,.             ..,;;;;,..             .,ldxkxo::kW
MWO,                .;ldOKNWWMMWWNKOdl;.                ,OWM
MMMXo.        ..;lx0XWMMMMMMMMMMMMMMMMWX0xl;..        .oXMMM
MMMMWXxlcclodOKNWMMMMMMMMMMMMMMMMMMMMMMMMMMWNKOdolcclxXMMMMM")
    End Sub
    Sub Run()
        Dim done = False
        While Not done
            AnsiConsole.WriteLine()
            Dim prompt As New SelectionPrompt(Of String) With
            {
                .Title = "[olive]How would you like to villainously emote?[/]"
            }
            prompt.AddChoice(TwirlMustacheText)
            prompt.AddChoice(NeverMindText)
            Select Case AnsiConsole.Prompt(prompt)
                Case TwirlMustacheText
                    AnsiConsole.WriteLine()
                    TwirlMustache()
                    AnsiConsole.MarkupLine("[green]You twirl yer mustache sinisterly.[/]")
                Case NeverMindText
                    done = True
                Case Else
                    Throw New NotImplementedException
            End Select
        End While
    End Sub
End Module
