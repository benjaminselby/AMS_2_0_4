Public Class StudentReportDocument
    Public StudentOwner As Student
    Public ReportTitle As String
    Public DocumentSeqNumber As Integer

    Public Sub New(StudentOwner As Student,
            ReportTitle As String,
            DocumentSeqNumber As Integer)
        Me.StudentOwner = StudentOwner
        Me.ReportTitle = ReportTitle
        Me.DocumentSeqNumber = DocumentSeqNumber
    End Sub

    Public Overrides Function ToString() As String
        Return ReportTitle
    End Function
    
End Class
