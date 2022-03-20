Imports UGJ61.Game
Imports Spectre.Console

Module Program
    Private Sub Welcome()
        AnsiConsole.MarkupLine("[aqua]********************************************[/]")
        AnsiConsole.MarkupLine("[aqua]* A Game in VB.NET About Being the Villain *[/]")
        AnsiConsole.MarkupLine("[aqua]********************************************[/]")
        AnsiConsole.MarkupLine("[gray]A Production of TheGrumpyGameDev[/]")
        AnsiConsole.MarkupLine("[gray]...With ""help"" from his ""friends""[/]")
        AnsiConsole.WriteLine()
    End Sub
    Private Const EmbarkText = "Embark!"
    Private Const QuitText = "Quit"
    Private Const CreditsText = "Credits"
    Private Sub MainMenu()
        Dim done = False
        While Not done
            AnsiConsole.WriteLine()
            Dim prompt As New SelectionPrompt(Of String) With
            {
                .Title = "[olive]Main Menu:[/]"
            }
            prompt.AddChoice(EmbarkText)
            prompt.AddChoice(CreditsText)
            prompt.AddChoice(QuitText)
            Select Case AnsiConsole.Prompt(prompt)
                Case QuitText
                    done = ConfirmQuit()
                Case EmbarkText
                    Embark.Run()
                Case CreditsText
                    ShowCredits()
            End Select
        End While
    End Sub

    Private Sub ShowCredits()
        AnsiConsole.WriteLine()
        AnsiConsole.MarkupLine("Art Credits:")
        AnsiConsole.MarkupLine("https://game-icons.net/1x1/delapouite/mustache.html")
    End Sub

    Private Function ConfirmQuit() As Boolean
        Dim prompt = New ConfirmationPrompt("Are you sure you want to quit?")
        Return AnsiConsole.Prompt(prompt)
    End Function

    Sub Main(args As String())
        AddHandler Game.PlaySfx, AddressOf PlaySfx
        Console.Title = "A Game in VB.NET About Being the Villain"
        Welcome()
        MainMenu()
    End Sub
End Module
