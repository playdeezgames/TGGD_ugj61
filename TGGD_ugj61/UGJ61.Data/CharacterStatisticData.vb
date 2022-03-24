Public Module CharacterStatisticData
    Friend Sub Initialize()
        CharacterData.Initialize()
        ExecuteNonQuery(
            "CREATE TABLE IF NOT EXISTS [CharacterStatistics]
            (
                [CharacterId] INT NOT NULL,
                [StatisticType] INT NOT NULL,
                [StatisticValue] INT NOT NULL,
                UNIQUE([CharacterId],[StatisticType]),
                FOREIGN KEY ([CharacterId]) REFERENCES [Characters]([CharacterId])
            );")
    End Sub
    Function Read(characterId As Long, statisticType As Long) As Long?
        Initialize()
        Read = ExecuteScalar(Of Long)(
            "SELECT [StatisticValue] FROM [CharacterStatistics] WHERE [CharacterId] = @CharacterId AND [StatisticType] = @StatisticType;",
            MakeParameter("@CharacterId", characterId),
            MakeParameter("@StatisticType", statisticType))
    End Function
    Sub Write(characterId As Long, statisticType As Long, statisticValue As Long)
        Initialize()
        ExecuteNonQuery(
            "REPLACE INTO [CharacterStatistics]([CharacterId],[StatisticType],[StatisticValue]) VALUES(@CharacterId,@StatisticType,@StatisticValue);",
            MakeParameter("@CharacterId", characterId),
            MakeParameter("@StatisticType", statisticType),
            MakeParameter("@StatisticValue", statisticValue))
    End Sub

    Friend Sub ClearForCharacterId(characterId As Long)
        Initialize()
        ExecuteNonQuery("DELETE FROM [CharacterStatistics] WHERE [CharacterId]=@CharacterId", MakeParameter("@CharacterId", characterId))
    End Sub
End Module
