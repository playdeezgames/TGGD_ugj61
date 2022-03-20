Imports Spectre.Console
Imports UGJ61.Game

Module InteractMenu
    Sub Run(character As PlayerCharacter)
        Dim done = False
        While Not done
            AnsiConsole.WriteLine()
            Dim prompt As New SelectionPrompt(Of String) With
                {
                    .Title = "[olive]Interact with...[/]"
                }
            For Each otherCharacter In character.Location.Characters.Where(Function(c) c.Id <> character.Id)
                prompt.AddChoice(otherCharacter.UniqueName)
            Next
            prompt.AddChoice(NeverMindText)
            Select Case AnsiConsole.Prompt(prompt)
                Case NeverMindText
                    done = True
                Case Else
                    'TODO: parse unique name, send to submenu
            End Select
        End While
    End Sub
End Module
