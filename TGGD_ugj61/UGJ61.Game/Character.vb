Imports UGJ61.Data

Public Class Character
    ReadOnly Property Id As Long
    Sub New(characterId As Long)
        Id = characterId
    End Sub
    Property Location As Location
        Get
            Return New Location(CharacterData.ReadLocationId(Id).Value)
        End Get
        Set(value As Location)
            CharacterData.WriteLocationId(Id, value.Id)
        End Set
    End Property
    ReadOnly Property CharacterType As CharacterType
        Get
            Return CType(CharacterData.ReadCharacterType(Id).Value, CharacterType)
        End Get
    End Property
    ReadOnly Property Name As String
        Get
            Select Case CharacterType
                Case CharacterType.Leftenant
                    Return "Yer trusty leftenant"
                Case CharacterType.Villain
                    Return "The Villain(you)"
                Case CharacterType.Minion
                    Return "Minion"
                Case CharacterType.Hero
                    Return "The Hero"
                Case CharacterType.LoveInterest
                    Return "Yer Love Interest"
                Case Else
                    Throw New NotImplementedException
            End Select
        End Get
    End Property

    ReadOnly Property CanWoo As Boolean
        Get
            Return CharacterType = CharacterType.LoveInterest
        End Get
    End Property

    ReadOnly Property UniqueName As String
        Get
            Return $"{Name}(#{Id})"
        End Get
    End Property
    ReadOnly Property CanInteract As Boolean
        Get
            Return Location.Characters.Any(Function(x) x.Id <> Id)
        End Get
    End Property
    Function HasStatistic(statisticType As StatisticType) As Boolean
        Return GetStatistic(statisticType).HasValue
    End Function
    Function GetStatistic(statisticType As StatisticType) As Long?
        Return CharacterStatisticData.Read(Id, statisticType)
    End Function
    Sub ChangeStatistic(statisticType As StatisticType, delta As Long)
        CharacterStatisticData.Write(Id, statisticType, GetStatistic(statisticType).Value + delta)
    End Sub
    ReadOnly Property CurrentPlot As Plot
        Get
            If CharacterPlotData.Read(Id).HasValue Then
                Return New Plot(Id)
            Else
                Return Nothing
            End If
        End Get
    End Property
    Sub HatchPlot()
        If CurrentPlot Is Nothing Then
            CharacterPlotData.Write(Id, 0)
        End If
    End Sub
    ReadOnly Property CanHireMinion As Boolean
        Get
            Return GetStatistic(StatisticType.Villainy).Value >= GetStatistic(StatisticType.MinionCost).Value
        End Get
    End Property

    ReadOnly Property CanChide As Boolean
        Get
            Return CharacterType = CharacterType.Leftenant OrElse CharacterType = CharacterType.Minion
        End Get
    End Property
    ReadOnly Property CanSlap As Boolean
        Get
            Return CharacterType = CharacterType.Leftenant OrElse CharacterType = CharacterType.Minion OrElse CharacterType = CharacterType.Hero
        End Get
    End Property
    ReadOnly Property CanPlaceInDeathTrap As Boolean
        Get
            Return CharacterType = CharacterType.LoveInterest OrElse CharacterType = CharacterType.Hero
        End Get
    End Property
    ReadOnly Property CanMonologueAt As Boolean
        Get
            Return CharacterType = CharacterType.LoveInterest OrElse CharacterType = CharacterType.Hero
        End Get
    End Property
    Sub HireMinion()
        If CanHireMinion Then
            ChangeStatistic(StatisticType.Villainy, -GetStatistic(StatisticType.MinionCost).Value)
            CharacterData.Create(CharacterType.Minion, Location.Id)
            ChangeStatistic(StatisticType.MinionCost, 1)
        End If
    End Sub
    Sub Destroy()
        CharacterData.Clear(Id)
    End Sub
End Class
