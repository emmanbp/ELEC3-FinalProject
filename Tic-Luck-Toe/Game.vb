Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports System.Net.Sockets

Partial Public Class Game
    Inherits Form

    Public Sub New(ByVal isHost As Boolean, ByVal Optional ip As String = Nothing)
        InitializeComponent()
        AddHandler messageReceiver.DoWork, AddressOf messageReceiver_DoWork
        'messageReceiver.DoWork += messageReceiver_DoWork()
        CheckForIllegalCrossThreadCalls = False

        If isHost Then
            PlayerChar = "X"
            OpponentChar = "O"
            server = New TcpListener(System.Net.IPAddress.Any, 5732)
            server.Start()
            sock = server.AcceptSocket()
        Else
            PlayerChar = "O"
            OpponentChar = "X"
            Try
                client = New TcpClient(ip, 5732)
                sock = client.Client
                messageReceiver.RunWorkerAsync()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
                Close()
            End Try
        End If
    End Sub

    Public Sub messageReceiver_DoWork(sender As Object, e As DoWorkEventArgs)
        If CheckState() Then Return
        DisableBoxes()
        playerTurnLabel.Text = "Opponent's Turn"
        ReceiveMove()
        playerTurnLabel.Text = "Your Turn"
        If Not CheckState() Then EnableBoxes()

    End Sub

    Private PlayerChar As Char
    Private OpponentChar As Char
    Private sock As Socket
    Private messageReceiver As BackgroundWorker = New BackgroundWorker()
    Private server As TcpListener = Nothing
    Private client As TcpClient

    Private Function CheckState() As Boolean
        'horizontal

        If box1.Text = box2.Text AndAlso box2.Text = box3.Text AndAlso box3.Text <> "" Then

            If box1.Text(0) = PlayerChar Then
                messageTxt.Text = "You Won!"
                MessageBox.Show("You Won!")
            Else
                messageTxt.Text = "You Lost!"
                MessageBox.Show("You Lost!")
            End If


        ElseIf box4.Text = box5.Text AndAlso box5.Text = box6.Text AndAlso box6.Text <> "" Then

            If box4.Text(0) = PlayerChar Then
                messageTxt.Text = "You Won!"
                MessageBox.Show("You Won!")
            Else
                messageTxt.Text = "You Lost!"
                MessageBox.Show("You Lost!")
            End If


        ElseIf box7.Text = box8.Text AndAlso box8.Text = box9.Text AndAlso box9.Text <> "" Then

            If box7.Text(0) = PlayerChar Then
                messageTxt.Text = "You Won!"
                MessageBox.Show("You Won!")
            Else
                messageTxt.Text = "You Lost!"
                MessageBox.Show("You Lost!")
            End If


            'vertical

        ElseIf box1.Text = box4.Text AndAlso box4.Text = box7.Text AndAlso box7.Text <> "" Then

            If box1.Text(0) = PlayerChar Then
                messageTxt.Text = "You Won!"
                MessageBox.Show("You Won!")
            Else
                messageTxt.Text = "You Lost!"
                MessageBox.Show("You Lost!")
            End If


        ElseIf box2.Text = box5.Text AndAlso box5.Text = box8.Text AndAlso box8.Text <> "" Then

            If box2.Text(0) = PlayerChar Then
                messageTxt.Text = "You Won!"
                MessageBox.Show("You Won!")
            Else
                messageTxt.Text = "You Lost!"
                MessageBox.Show("You Lost!")
            End If


        ElseIf box3.Text = box6.Text AndAlso box6.Text = box9.Text AndAlso box9.Text <> "" Then

            If box3.Text(0) = PlayerChar Then
                messageTxt.Text = "You Won!"
                MessageBox.Show("You Won!")
            Else
                messageTxt.Text = "You Lost!"
                MessageBox.Show("You Lost!")
            End If


            'diagonal

        ElseIf box1.Text = box5.Text AndAlso box5.Text = box9.Text AndAlso box9.Text <> "" Then

            If box1.Text(0) = PlayerChar Then
                messageTxt.Text = "You Won!"
                MessageBox.Show("You Won!")
            Else
                messageTxt.Text = "You Lost!"
                MessageBox.Show("You Lost!")
            End If


        ElseIf box3.Text = box5.Text AndAlso box5.Text = box7.Text AndAlso box7.Text <> "" Then

            If box3.Text(0) = PlayerChar Then
                messageTxt.Text = "You Won!"
                MessageBox.Show("You Won!")
            Else
                messageTxt.Text = "You Lost!"
                MessageBox.Show("You Lost!")
            End If


            'Draw

        ElseIf box1.Text <> "" AndAlso box2.Text <> "" AndAlso box3.Text <> "" AndAlso
                box4.Text <> "" AndAlso box5.Text <> "" AndAlso box6.Text <> "" AndAlso
                box7.Text <> "" AndAlso box8.Text <> "" AndAlso box9.Text <> "" Then

            messageTxt.Text = "It's a draw"
            MessageBox.Show("It's a draw")
            Return True

        End If
        Return False
    End Function

    Private Sub DisableBoxes()
        Dim boxes() As Button = {box1, box2, box3, box4, box5, box6, box7, box8, box9}

        For Each box As Button In boxes
            box.Enabled = False
        Next

    End Sub

    Private Sub EnableBoxes()
        Dim boxes() As Button = {box1, box2, box3, box4, box5, box6, box7, box8, box9}

        For Each box As Button In boxes
            If box.Text = "" Then
                box.Enabled = True
            End If
        Next

    End Sub

    Private Sub ReceiveMove()
        Dim buffer As Byte() = New Byte(0) {}
        Sock.Receive(buffer)

        Dim boxes() As Button = {box1, box2, box3, box4, box5, box6, box7, box8, box9}
        Dim num As Integer = 1

        For i = 0 To boxes.Length - 1
            If buffer(0) = num Then boxes(i).Text = OpponentChar.ToString()
            num += 1

            Console.WriteLine(num)
        Next
    End Sub

    Private Sub box1_Click(sender As Object, e As EventArgs) Handles box1.Click
        Dim num As Byte() = {1}
        sock.Send(num)
        box1.Text = PlayerChar.ToString()
        messageReceiver.RunWorkerAsync()

    End Sub

    Private Sub box2_Click(sender As Object, e As EventArgs) Handles box2.Click
        Dim num As Byte() = {2}
        sock.Send(num)
        box2.Text = PlayerChar.ToString()
        messageReceiver.RunWorkerAsync()
    End Sub

    Private Sub box3_Click(sender As Object, e As EventArgs) Handles box3.Click
        Dim num As Byte() = {3}
        sock.Send(num)
        box3.Text = PlayerChar.ToString()
        messageReceiver.RunWorkerAsync()
    End Sub

    Private Sub box4_Click(sender As Object, e As EventArgs) Handles box4.Click
        Dim num As Byte() = {4}
        sock.Send(num)
        box4.Text = PlayerChar.ToString()
        messageReceiver.RunWorkerAsync()
    End Sub

    Private Sub box5_Click(sender As Object, e As EventArgs) Handles box5.Click
        Dim num As Byte() = {5}
        sock.Send(num)
        box5.Text = PlayerChar.ToString()
        messageReceiver.RunWorkerAsync()
    End Sub

    Private Sub box6_Click(sender As Object, e As EventArgs) Handles box6.Click
        Dim num As Byte() = {6}
        sock.Send(num)
        box6.Text = PlayerChar.ToString()
        messageReceiver.RunWorkerAsync()
    End Sub

    Private Sub box7_Click(sender As Object, e As EventArgs) Handles box7.Click
        Dim num As Byte() = {7}
        sock.Send(num)
        box7.Text = PlayerChar.ToString()
        messageReceiver.RunWorkerAsync()
    End Sub

    Private Sub box8_Click(sender As Object, e As EventArgs) Handles box8.Click
        Dim num As Byte() = {8}
        sock.Send(num)
        box8.Text = PlayerChar.ToString()
        messageReceiver.RunWorkerAsync()
    End Sub

    Private Sub box9_Click(sender As Object, e As EventArgs) Handles box9.Click
        Dim num As Byte() = {9}
        sock.Send(num)
        box9.Text = PlayerChar.ToString()
        messageReceiver.RunWorkerAsync()
    End Sub

    Private Sub Game_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        messageReceiver.WorkerSupportsCancellation = True
        messageReceiver.CancelAsync()
        If server IsNot Nothing Then server.Stop()

    End Sub

End Class