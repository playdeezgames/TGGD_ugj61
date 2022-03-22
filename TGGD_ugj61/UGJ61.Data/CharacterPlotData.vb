Public Module CharacterPlotData
    Friend Sub Initialize()
        CharacterData.Initialize()
        ExecuteNonQuery(
            "CREATE TABLE IF NOT EXISTS [CharacterPlots]
            (
                [CharacterId] INT NOT NULL UNIQUE,
                [Convolutedness] INT NOT NULL,
                FOREIGN KEY ([CharacterId]) REFERENCES [Characters]([CharacterId])
            );")
    End Sub
    Function Read(characterId As Long) As Long?
        Initialize()
        Read = ExecuteScalar(Of Long)("SELECT [Convolutedness] FROM [CharacterPlots] WHERE [CharacterId]=@CharacterId;", MakeParameter("@CharacterId", characterId))
    End Function
    Sub Write(characterId As Long, convolutedness As Long)
        Initialize()
        ExecuteNonQuery(
            "REPLACE INTO [CharacterPlots]([CharacterId],[Convolutedness]) VALUES(@CharacterId,@Convolutedness);",
            MakeParameter("@CharacterId", characterId),
            MakeParameter("@Convolutedness", convolutedness))
    End Sub
    Sub Clear(characterId As Long)
        Initialize()
        ExecuteNonQuery(
            "DELETE FROM [CharacterPlots] WHERE [CharacterId]=@CharacterId;",
            MakeParameter("@CharacterId", characterId))
    End Sub
End Module
