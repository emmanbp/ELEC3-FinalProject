Partial Public Class ConnectForm
    Inherits Form

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub connectBtn_Click(sender As Object, e As EventArgs) Handles connectBtn.Click
        Dim newGame As Game = New Game(False, TextBox1.Text)
        Visible = False
        If Not newGame.IsDisposed Then newGame.ShowDialog()
        Visible = True

    End Sub

    Private Sub hostGameBtn_Click(sender As Object, e As EventArgs) Handles hostGameBtn.Click
        Dim newGame As Game = New Game(True, TextBox1.Text)
        Visible = False
        If Not newGame.IsDisposed Then newGame.ShowDialog()
        Visible = True
    End Sub


End Class