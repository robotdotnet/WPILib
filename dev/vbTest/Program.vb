Imports System
Imports Stereologue

Module Program
    Sub Main(args As String())
        Console.WriteLine("Hello World!")
    End Sub
End Module

Namespace vbTest
    <Stereologue.GenerateLog()> Partial Public Class LoggedClass
        <Stereologue.Log()> Dim x As String
        <Stereologue.Log()> Dim y As Integer
    End Class
End Namespace
