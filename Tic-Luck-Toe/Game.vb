Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports System.Net.Sockets

Imports System.IO
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Partial Public Class Game
    Inherits Form

    ' !-Generate Question

    Private jsonString As String = File.ReadAllText("questions.json")
    Private jsonObject As JObject = JObject.Parse(jsonString)
    Private resQuestion As Integer
    Private questionArray As JArray = JArray.Parse(jsonObject.SelectToken("listQuestions").ToString)
    Private choiceArray As JArray
    Private correct As Byte
    Private selectedQuestion As Integer

    Private Sub randomChoices()
        Dim rndQuestion As New Random
        selectedQuestion = rndQuestion.Next(0, questionArray.Count)

        choiceArray = JArray.Parse(questionArray(selectedQuestion).SelectToken("choices").ToString)

        Dim rndChoice As New Random
        Dim usedChoices As New List(Of Integer)
        Dim buttons() = {Button4, Button7, Button8, Button9}

        For i As Integer = 0 To buttons.Length - 1
            Dim uniqueChoices As Boolean = False

            Do
                Dim resChoice = rndChoice.Next(0, choiceArray.Count)
                Dim strChoice As String = choiceArray(resChoice).ToString

                If usedChoices.Contains(resChoice) Then
                    uniqueChoices = False
                Else
                    uniqueChoices = True
                    usedChoices.Add(resChoice)
                    buttons(usedChoices(i)).Text = strChoice

                End If
            Loop Until uniqueChoices

        Next

        Dim boxes() = {Button4, Button7, Button8, Button9}
        For i As Integer = 0 To boxes.Length - 1
            boxes(i).Enabled = True
        Next


        TextBox1.Text = questionArray(selectedQuestion).SelectToken("question").ToString

    End Sub

    Private Sub checkAnswer(ByVal btnStr As String)
        choiceArray = JArray.Parse(questionArray(selectedQuestion).SelectToken("choices").ToString)
        correct = Byte.Parse(questionArray(selectedQuestion).SelectToken("correct"))

        If choiceArray(correct) <> btnStr Then
            TextBox2.Text = "Oops! Your answer is incorrect"
            DisableBoxes()

            Dim boxes() = {Button4, Button7, Button8, Button9}
            For i As Integer = 0 To boxes.Length - 1
                boxes(i).Enabled = False

            Next

        Else
            TextBox2.Text = " Your answer is correct"

            If Not CheckState() Then EnableBoxes()


            Dim boxes() = {Button4, Button7, Button8, Button9}
            For i As Integer = 0 To boxes.Length - 1
                boxes(i).Enabled = False

            Next


        End If

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        checkAnswer(Button4.Text)
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        checkAnswer(Button7.Text)
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        checkAnswer(Button8.Text)
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        checkAnswer(Button9.Text)
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        TextBox2.Text = ""
        DisableBoxes()
        randomChoices()
    End Sub

    ' Generate Question-!

    ' !-Game Code

    Private isClickRestart As Boolean = False

    Public Sub New(ByVal isHost As Boolean, ByVal Optional ip As String = Nothing)
        InitializeComponent()
        AddHandler messageReceiver.DoWork, AddressOf messageReceiver_DoWork
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
        'DisableBoxes()

        ReceiveMove()

        If Not CheckState() Then EnableBoxes()

        If isClickRestart = True Then
            MessageBox.Show("Opponent Player has left. Restart the game")
        End If

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
        sock.Receive(buffer)

        Dim boxes() As Button = {box1, box2, box3, box4, box5, box6, box7, box8, box9}
        Dim num As Integer = 1

        For i = 0 To boxes.Length - 1
            If buffer(0) = num Then boxes(i).Text = OpponentChar.ToString()
            num += 1


        Next

    End Sub

    ' Form components
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox2.Text = ""
        DisableBoxes()
        randomChoices()
    End Sub

    Private Sub box1_Click(sender As Object, e As EventArgs) Handles box1.Click


        Dim num As Byte() = {1}
        sock.Send(num)
        box1.Text = PlayerChar.ToString()
        messageReceiver.RunWorkerAsync()
        TextBox2.Text = ""
        randomChoices()
        DisableBoxes()



    End Sub

    Private Sub box2_Click(sender As Object, e As EventArgs) Handles box2.Click
        Dim num As Byte() = {2}
        sock.Send(num)
        box2.Text = PlayerChar.ToString()
        messageReceiver.RunWorkerAsync()
        TextBox2.Text = ""
        randomChoices()
        DisableBoxes()

    End Sub

    Private Sub box3_Click(sender As Object, e As EventArgs) Handles box3.Click
        Dim num As Byte() = {3}
        sock.Send(num)
        box3.Text = PlayerChar.ToString()
        messageReceiver.RunWorkerAsync()
        TextBox2.Text = ""
        randomChoices()
        DisableBoxes()


    End Sub

    Private Sub box4_Click(sender As Object, e As EventArgs) Handles box4.Click
        Dim num As Byte() = {4}
        sock.Send(num)
        box4.Text = PlayerChar.ToString()
        messageReceiver.RunWorkerAsync()
        TextBox2.Text = ""
        randomChoices()
        DisableBoxes()


    End Sub

    Private Sub box5_Click(sender As Object, e As EventArgs) Handles box5.Click
        Dim num As Byte() = {5}
        sock.Send(num)
        box5.Text = PlayerChar.ToString()
        messageReceiver.RunWorkerAsync()
        TextBox2.Text = ""
        randomChoices()
        DisableBoxes()


    End Sub

    Private Sub box6_Click(sender As Object, e As EventArgs) Handles box6.Click
        Dim num As Byte() = {6}
        sock.Send(num)
        box6.Text = PlayerChar.ToString()
        messageReceiver.RunWorkerAsync()
        TextBox2.Text = ""
        randomChoices()
        DisableBoxes()


    End Sub

    Private Sub box7_Click(sender As Object, e As EventArgs) Handles box7.Click
        Dim num As Byte() = {7}
        sock.Send(num)
        box7.Text = PlayerChar.ToString()
        messageReceiver.RunWorkerAsync()
        TextBox2.Text = ""
        randomChoices()
        DisableBoxes()


    End Sub

    Private Sub box8_Click(sender As Object, e As EventArgs) Handles box8.Click
        Dim num As Byte() = {8}
        sock.Send(num)
        box8.Text = PlayerChar.ToString()
        messageReceiver.RunWorkerAsync()
        TextBox2.Text = ""
        randomChoices()
        DisableBoxes()


    End Sub

    Private Sub box9_Click(sender As Object, e As EventArgs) Handles box9.Click
        Dim num As Byte() = {9}
        sock.Send(num)
        box9.Text = PlayerChar.ToString()
        messageReceiver.RunWorkerAsync()
        TextBox2.Text = ""
        randomChoices()
        DisableBoxes()


    End Sub

    Private Sub Game_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        messageReceiver.WorkerSupportsCancellation = True
        messageReceiver.CancelAsync()
        If server IsNot Nothing Then server.Stop()

    End Sub

    Private Sub restartBtn_Click(sender As Object, e As EventArgs) Handles restartBtn.Click
        isClickRestart = True
        Me.Close()
        ConnectForm.Show()
    End Sub

    ' Game Code-!
End Class