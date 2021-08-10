Public Class Welcome
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        OkBtn.Select()

    End Sub

    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OkBtn.Click
        Me.Hide()
        Main.Show()
    End Sub

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CancelBtn.Click
        Application.Exit()
    End Sub

End Class
