Imports Spectre.Console
Imports UGJ61.Data

Module GameMenu
    Private Const AbandonGameText = "Abandon Game"
    Friend Const NeverMindText = "Never Mind"
    Private Const SaveGameText = "Save Game"
    Private Function ConfirmAbandon() As Boolean
        Dim prompt = New ConfirmationPrompt("Are you sure you want to abandon the game?")
        Return AnsiConsole.Prompt(prompt)
    End Function
    Function Run() As Boolean
        Dim prompt As New SelectionPrompt(Of String) With
            {
            .Title = "[olive]Game Menu:[/]"
            }
        prompt.AddChoice(NeverMindText)
        prompt.AddChoice(SaveGameText)
        prompt.AddChoice(AbandonGameText)
        Select Case AnsiConsole.Prompt(prompt)
            Case AbandonGameText
                Return ConfirmAbandon()
            Case SaveGameText
                HandleSaveGame()
                Return False
            Case NeverMindText
                Return False
            Case Else
                Throw New NotImplementedException
        End Select
    End Function

    Private Sub HandleSaveGame()
        AnsiConsole.WriteLine()
        Dim fileName = AnsiConsole.Ask(Of String)("Filename:")
        Store.Save(fileName)
    End Sub
End Module
