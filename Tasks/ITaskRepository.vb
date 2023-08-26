Option Strict On
Option Explicit On
Option Infer On

Public Interface ITaskRepository
    Function GetAll() As List(Of Task)
    Function GetById(taskID As UInteger) As Task
    Function Add(task As Task) As Task
    Function Update(task As Task) As Task
    Function DeleteByID(taskID As UInteger) As Task
End Interface