Public Module LocationStatisticData
    Friend Sub Initialize()
        LocationData.Initialize()
        ExecuteNonQuery(
            "CREATE TABLE IF NOT EXISTS [LocationStatistics]
            (
                [LocationId] INT NOT NULL,
                [StatisticType] INT NOT NULL,
                [StatisticValue] INT NOT NULL,
                UNIQUE([LocationId],[StatisticType]),
                FOREIGN KEY ([LocationId]) REFERENCES [Locations]([LocationId])
            );")
    End Sub
    Public Function Read(locationId As Long, statisticType As Long) As Long?
        Initialize()
        Return ExecuteScalar(Of Long)(
            "SELECT
                [StatisticValue]
            FROM
                [LocationStatistics]
            WHERE
                [LocationId]=@LocationId AND
                [StatisticType]=@StatisticType;",
            MakeParameter("@LocationId", locationId),
            MakeParameter("@StatisticType", statisticType))
    End Function
    Public Sub Write(locationId As Long, statisticType As Long, statisticValue As Long)
        Initialize()
        ExecuteNonQuery(
            "REPLACE INTO [LocationStatistics]
            (
                [LocationId],
                [StatisticType],
                [StatisticValue]
            ) 
            VALUES
            (
                @LocationId,
                @StatisticType,
                @StatisticValue
            );",
            MakeParameter("@LocationId", locationId),
            MakeParameter("@StatisticType", statisticType),
            MakeParameter("@StatisticValue", statisticValue))
    End Sub
End Module
