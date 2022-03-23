Imports UGJ61.Data

Public Module Locations
    Public Function Exists(locationType As LocationType) As Boolean
        Return LocationData.ReadCountByLocationType(locationType) > 0
    End Function

    Public Sub CreateDeathTrap()
        Dim locationId = LocationData.Create(LocationType.DeathTrap)
        LocationStatisticData.Write(locationId, StatisticType.ConstructionNeeded, 20)
    End Sub
End Module
