Imports Spectre.Console

Module EmoteMenu
    Private Const TwirlMustacheText = "Twirl Mustache"
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
                    AnsiConsole.MarkupLine("[green]You twirl yer mustache sinisterly.[/]")
                Case NeverMindText
                    done = True
                Case Else
                    Throw New NotImplementedException
            End Select
        End While
    End Sub
End Module
