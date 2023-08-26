Option Strict On
Option Explicit On
Option Infer On

Public Class TasksContext
    Private ReadOnly repo As ITaskRepository

    Public Function AddTask(description As string , deadline As DateTime) As Task
        Dim newTask = New Task() With {
            .Description=description,
            .Deadline=deadline,
            .Completed=False
        }

        Dim result = repo.Add(newTask)
        Return result
    End Function

    Public Function GetAll() As List(Of Task)
        Return repo.GetAll()
    End Function

    Public Function GetByID(id As UInteger) As Task
        Dim result = repo.GetById(id)
        Return result
    End Function

    Public Function Update(task As Task) As Task
        Dim result = repo.Update(task)
        Return result
    End Function

    Public Function DeleteByID(id As UInteger) As Task
        Return repo.DeleteByID(id)
    End Function

    Public Sub New(repo As ITaskRepository)
        Me.repo = repo
    End Sub
End Class