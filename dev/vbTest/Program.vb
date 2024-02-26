Imports System
Imports Stereologue
Imports WPIMath.Geometry

Module Program
    Sub Main(args As String())
        Console.WriteLine("Hello World!")
    End Sub
End Module

Namespace vbTest
    <Stereologue.GenerateLog()> Partial Public Class ExtraLogged
        <Log()>
        Public Function Variable() as Integer
            Dim ret as Integer
            Return ret
        End Function
    End Class

    <Stereologue.GenerateLog()> Partial Public Class LoggedClass
        <Stereologue.Log()> Dim x As String
        <Stereologue.Log()> Dim y As Integer
        <Stereologue.Log()> Dim z As ReadOnlyMemory(Of Long)

        <Stereologue.Log()> Dim rot As Rotation2d
        <Stereologue.Log()> Dim rotArray As Rotation2d()

        <Stereologue.Log()> Dim cls As ExtraLogged
        <Stereologue.Log()> Dim classArray As ExtraLogged()

        <Stereologue.Log()> Function GetMemory() As ReadOnlyMemory(Of Long)
            Return z
        End Function
    End Class
End Namespace
