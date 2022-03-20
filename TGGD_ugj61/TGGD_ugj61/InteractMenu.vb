Imports Spectre.Console
Imports UGJ61.Game

Module InteractMenu
    Sub Run(character As PlayerCharacter)
        Dim done = False
        While Not done
            AnsiConsole.WriteLine()
            Dim prompt As New SelectionPrompt(Of String) With
                {
                    .Title = ""
                }
        End While
    End Sub
End Module
