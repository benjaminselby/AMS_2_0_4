Public Class Student

    Public firstName As String
    Public surname As String
    Public id As Integer
    Public yearLevel As Integer
    ' The sequence number of a Student Services booklet stored in DocMan, if this student has one. 
    Public studentServicesDocumentSeq As Integer

    Public Sub New(
            firstName As String,
            surname As String,
            id As Integer,
            yearLevel As Integer,
            studentServicesDocumentSeq As Integer)

        Me.firstName = firstName
        Me.surname = surname
        Me.id = id
        Me.yearLevel = yearLevel
        Me.studentServicesDocumentSeq = studentServicesDocumentSeq

    End Sub

    Public Overrides Function ToString() As String
        Return Me.surname + ", " + Me.firstName
    End Function

End Class
