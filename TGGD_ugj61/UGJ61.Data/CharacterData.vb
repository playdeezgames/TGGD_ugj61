Public Module CharacterData
    Friend Sub Initialize()
        LocationData.Initialize()
        ExecuteNonQuery(
            "CREATE TABLE IF NOT EXISTS [Characters]
            (
                [CharacterId] INTEGER PRIMARY KEY AUTOINCREMENT,
                [CharacterType] INT NOT NULL,
                [LocationId] INT NOT NULL,
                FOREIGN KEY([LocationId]) REFERENCES [Locations]([LocationId])
            );")
    End Sub
    Function Create(characterType As Long, locationId As Long) As Long
        Initialize()
        ExecuteNonQuery(
            "INSERT INTO [Characters]
            (
                [CharacterType],
                [LocationId]
            ) 
            VALUES
            (
                @CharacterType,
                @LocationId
            );",
            MakeParameter("@CharacterType", characterType),
            MakeParameter("@LocationId", locationId))
        Return LastInsertRowId
    End Function
    Function ReadLocationId(characterId As Long) As Long?
        Initialize()
        Return ExecuteScalar(Of Long)(
            "SELECT
                [LocationId]
            FROM
                [Characters]
            WHERE
                [CharacterId]=@CharacterId;",
            MakeParameter("@CharacterId", characterId))
    End Function
    Function ReadForLocationId(locationId As Long) As List(Of Long)
        Initialize()
        Using command = CreateCommand(
            "SELECT [CharacterId] FROM [Characters] WHERE [LocationId]=@LocationId;",
            MakeParameter("@LocationId", locationId))
            Using reader = command.ExecuteReader
                ReadForLocationId = New List(Of Long)
                While reader.Read
                    ReadForLocationId.Add(CLng(reader("CharacterId")))
                End While
            End Using
        End Using
    End Function
    Function ReadCharacterType(characterId As Long) As Long?
        Initialize()
        Return ExecuteScalar(Of Long)(
            "SELECT
                [CharacterType]
            FROM
                [Characters]
            WHERE
                [CharacterId]=@CharacterId;",
            MakeParameter("@CharacterId", characterId))
    End Function
    Function ReadCountByCharacterType(characterType As Long) As Long
        Initialize()
        Return ExecuteScalar(Of Long)(
            "SELECT
                COUNT([CharacterId])
            FROM
                [Characters]
            WHERE
                [CharacterType] = @CharacterType;",
            MakeParameter("@CharacterType", characterType)).Value
    End Function
    Function ReadForCharacterType(characterType As Long) As List(Of Long)
        Initialize()
        Using command = CreateCommand(
            "SELECT [CharacterId] FROM [Characters] WHERE [CharacterType]=@CharacterType;",
            MakeParameter("@CharacterType", characterType))
            Using reader = command.ExecuteReader
                Dim result As New List(Of Long)
                While reader.Read
                    result.Add(CLng(reader("CharacterId")))
                End While
                Return result
            End Using
        End Using
    End Function
    Sub Clear(characterId As Long)
        Initialize()
        CharacterPlotData.Clear(characterId)
        CharacterStatisticData.ClearForCharacterId(characterId)
        ExecuteNonQuery("DELETE FROM [Characters] WHERE [CharacterId]=@CharacterId;", MakeParameter("@CharacterId", characterId))
    End Sub
End Module
