Imports System
Imports Spectre.Console
Imports UGJ61.Game

Module Program
    Sub Main(args As String())
        Console.Title = "A Game in VB.NET About Being the Villain"
        AddHandler Game.PlaySfx, AddressOf SfxHandler.PlaySfx
        AnsiConsole.MarkupLine("[teal]A Game in VB.NET About Being the Villain[/]")
    End Sub
End Module
