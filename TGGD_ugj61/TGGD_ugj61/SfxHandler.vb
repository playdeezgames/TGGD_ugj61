Imports UGJ61.Game

Module SfxHandler
    Sub PlaySfx(sfx As Sfx)
#Disable Warning CA1416 ' Validate platform compatibility
        Console.Beep(100, 100)
#Enable Warning CA1416 ' Validate platform compatibility
    End Sub
End Module
