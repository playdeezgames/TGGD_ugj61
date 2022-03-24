Public Module LocationData
    Friend Sub Initialize()
        ExecuteNonQuery(
            "CREATE TABLE IF NOT EXISTS [Locations]
            (
                [LocationId] INTEGER PRIMARY KEY AUTOINCREMENT,
                [LocationType] INT NOT NULL
            );")
    End Sub
    Function Create(locationType As Long) As Long
        Initialize()
        ExecuteNonQuery(
            "INSERT INTO [Locations]
            (
                [LocationType]
            ) 
            VALUES
            (
                @LocationType
            );",
            MakeParameter("@LocationType", locationType))
        Return LastInsertRowId
    End Function
    Function ReadLocationType(locationId As Long) As Long?
        Initialize()
        Return ExecuteScalar(Of Long)(
            "SELECT
                [LocationType]
            FROM
                [Locations]
            WHERE
                [LocationId] = @LocationId;",
            MakeParameter("@LocationId", locationId))

    End Function
    Function ReadCountByLocationType(locationType As Long) As Long
        Initialize()
        Return ExecuteScalar(Of Long)(
            "SELECT
                COUNT([LocationId])
            FROM
                [Locations]
            WHERE
                [LocationType] = @LocationType;",
            MakeParameter("@LocationType", locationType)).Value
    End Function
    Function ReadForLocationType(locationType As Long) As List(Of Long)
        Initialize()
        Using command = CreateCommand(
            "SELECT [LocationId] FROM [Locations] WHERE [LocationType]=@LocationType;",
            MakeParameter("@LocationType", locationType))
            Using reader = command.ExecuteReader
                Dim result As New List(Of Long)
                While reader.Read
                    result.Add(CLng(reader("LocationId")))
                End While
                Return result
            End Using
        End Using
    End Function
End Module
