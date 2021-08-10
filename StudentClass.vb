Public Class StudentClass
    Public ClassCode As String
    Public ClassDescription As String
    ' Collection of all students for the current class. 
    Public Students As List(Of Student)

    Public Sub New(ClassCode As String, ClassDescription As String)
        Me.ClassCode = ClassCode
        Me.ClassDescription = ClassDescription
        Me.Students = New List(Of Student)
    End Sub

    Public Overrides Function ToString() As String
        Return Me.ClassCode + " - " + Me.ClassDescription
    End Function

End Class
