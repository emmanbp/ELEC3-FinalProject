Public Class MainMenu
    Private Sub playbutton_Click(sender As Object, e As EventArgs) Handles playbutton.Click
        ConnectForm.Show()
        Me.Hide()
    End Sub
End Class
