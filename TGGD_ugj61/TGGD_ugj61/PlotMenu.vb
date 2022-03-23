Imports Spectre.Console
Imports UGJ61.Game

Module PlotMenu
    Private Const HatchPlotText = "Hatch a Villainous Plot"
    Private Const ExecutePlotText = "Execute Villainous Plot"
    Private Const CancelPlotText = "Cancel Villainous Plot"
    Private Const ConvolutePlotText = "Convolute Villanous Plot"
    Sub Run(character As PlayerCharacter)
        Dim done = False
        While Not done
            AnsiConsole.WriteLine()
            If character.CurrentPlot IsNot Nothing Then
                AnsiConsole.MarkupLine($"You have a villainous plot with a convolutedness of {character.CurrentPlot.Convolutedness}.")
            Else
                AnsiConsole.MarkupLine("You currently have no villainous plot.")
            End If
            Dim prompt As New SelectionPrompt(Of String) With {
                .Title = "[olive]Now what?[/]"
            }
            If character.CurrentPlot Is Nothing Then
                prompt.AddChoice(HatchPlotText)
            Else
                prompt.AddChoice(ExecutePlotText)
                prompt.AddChoice(ConvolutePlotText)
                prompt.AddChoice(CancelPlotText)
            End If
            prompt.AddChoice(NeverMindText)
            Select Case AnsiConsole.Prompt(prompt)
                Case HatchPlotText
                    HandleHatchingPlot(character)
                Case ConvolutePlotText
                    HandleConvolutingPlot(character)
                Case ExecutePlotText
                    HandleExecutingPlot(character)
                Case CancelPlotText
                    HandleCancelingPlot(character)
                Case NeverMindText
                    done = True
                Case Else
                    Throw New NotImplementedException
            End Select
        End While
    End Sub
    Private Sub HandleCancelingPlot(character As PlayerCharacter)
        character.CurrentPlot.Cancel()
        AnsiConsole.WriteLine()
        AnsiConsole.MarkupLine("[red]You cancel yer villainous plot![/]")
    End Sub

    Private Sub HandleExecutingPlot(character As PlayerCharacter)
        Dim results = character.CurrentPlot.Execute()
        AnsiConsole.WriteLine()
        For Each result In results
            Select Case result
                Case PlotResult.Success
                    AnsiConsole.MarkupLine("[green]You succeed![/]")
                Case PlotResult.Failure
                    AnsiConsole.MarkupLine("[red]You fail![/]")
                Case PlotResult.CaptureHero
                    AnsiConsole.MarkupLine("[green]You capture the hero![/]")
                Case PlotResult.CaptureLoveInterest
                    AnsiConsole.MarkupLine("[green]You capture yer love interest![/]")
                Case PlotResult.GainMinions
                    AnsiConsole.MarkupLine("[green]You gain minions![/]")
                Case PlotResult.GainVillainy
                    AnsiConsole.MarkupLine("[green]You gain villainy![/]")
                Case PlotResult.LoseMinions
                    AnsiConsole.MarkupLine("[red]You lose minions![/]")
                Case Else
                    Throw New NotImplementedException
            End Select
        Next
    End Sub

    Private Sub HandleConvolutingPlot(character As PlayerCharacter)
        character.CurrentPlot.Convolute()
        AnsiConsole.WriteLine()
        AnsiConsole.MarkupLine("[green]You convolute yer villainous plot![/]")
    End Sub

    Private Sub HandleHatchingPlot(character As PlayerCharacter)
        character.HatchPlot()
        AnsiConsole.WriteLine()
        AnsiConsole.MarkupLine("[green]You hatch a villainous plot![/]")
    End Sub
End Module
