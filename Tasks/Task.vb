Option Strict On
Option Explicit On
Option Infer On

Imports System.ComponentModel.DataAnnotations.Schema

<Table("tasks")>
Public Class Task
    <Column("id")>
    Public Property ID As Integer
    <Column("description")>
    Public Property Description As String = ""
    <Column("deadline")>
    Public Property Deadline As DateTime
    <Column("completed")>
    Public Property Completed As Boolean
End Class
