Imports System.Runtime.CompilerServices

Public Enum StatisticType
    Villainy
End Enum
Public Module StatisticTypeExtensions
    <Extension()>
    Function Name(statisticType As StatisticType) As String
        Select Case statisticType
            Case StatisticType.Villainy
                Name = "Villainy"
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
    Public ReadOnly AllStatisticTypes As New List(Of StatisticType) From
        {
            StatisticType.Villainy
        }
End Module
