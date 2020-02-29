Imports System.IO
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class Form2
    Inherits Windows.Forms.Form


    'ADDED CODE
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

        Dim boxes() = {box1, box2, box3, box4, box5, box6, box7, box8, box9,
                Button4, Button7, Button8, Button9}
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

            Dim boxes() = {box1, box2, box3, box4, box5, box6, box7, box8, box9,
                Button4, Button7, Button8, Button9}
            For i As Integer = 0 To boxes.Length - 1
                boxes(i).Enabled = False
            Next

        Else
            TextBox2.Text = " Your answer is correct"
        End If

    End Sub


    Private Sub DrawFormGradient(ByVal TopColor As Color, ByVal BottomColor As Color)
        Dim objBrush As New Drawing2D.LinearGradientBrush(Me.DisplayRectangle, TopColor, BottomColor, Drawing2D.LinearGradientMode.Horizontal)
        Dim objGraphics As Graphics = Me.CreateGraphics
        objGraphics.FillRectangle(objBrush, Me.DisplayRectangle)
        objBrush.Dispose()
        objGraphics.Dispose()
    End Sub
    Private Sub Form1_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        DrawFormGradient(Color.Coral, Color.LightCoral)
    End Sub

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'ADDED CODE
        TextBox2.Text = ""
        randomChoices()



        For Each c As Control In Panel2.Controls
            If c.GetType() = GetType(Button) Then
                AddHandler c.Click, AddressOf btn_click
            End If
        Next
    End Sub

    Dim XorO As Integer = 0

    Private Sub btn_click(sender As Object, e As EventArgs)
        Dim btn As Button = sender

        If btn.Text.Equals("") Then

            If XorO Mod 2 = 0 Then
                btn.Text = "X"
                btn.ForeColor = Color.FromArgb(216, 115, 127)
                playerturn.Text = "O Turn"
                getWinner()
            Else
                btn.Text = "O"
                playerturn.Text = "X Turn"
                getWinner()
            End If
            XorO += 1

        End If
    End Sub

    Dim win As Boolean = False
    Private Sub getWinner()
        If Not box1.Text.Equals("") AndAlso box1.Text.Equals(box2.Text) AndAlso box1.Text.Equals(box3.Text) Then
            win = True
            winnotif(box1, box2, box3)
        End If

        If Not box4.Text.Equals("") AndAlso box4.Text.Equals(box5.Text) AndAlso box4.Text.Equals(box6.Text) Then
            win = True
            winnotif(box4, box5, box6)
        End If

        If Not box7.Text.Equals("") AndAlso box7.Text.Equals(box8.Text) AndAlso box7.Text.Equals(box9.Text) Then
            win = True
            winnotif(box7, box8, box9)
        End If

        If Not box1.Text.Equals("") AndAlso box1.Text.Equals(box4.Text) AndAlso box1.Text.Equals(box7.Text) Then
            win = True
            winnotif(box1, box4, box7)
        End If

        If Not box2.Text.Equals("") AndAlso box2.Text.Equals(box5.Text) AndAlso box2.Text.Equals(box8.Text) Then
            win = True
            winnotif(box2, box5, box8)
        End If

        If Not box3.Text.Equals("") AndAlso box3.Text.Equals(box6.Text) AndAlso box3.Text.Equals(box9.Text) Then
            win = True
            winnotif(box3, box6, box9)
        End If

        If Not box3.Text.Equals("") AndAlso box3.Text.Equals(box5.Text) AndAlso box3.Text.Equals(box7.Text) Then
            win = True
            winnotif(box3, box5, box7)
        End If

        If Not box1.Text.Equals("") AndAlso box1.Text.Equals(box5.Text) AndAlso box1.Text.Equals(box9.Text) Then
            win = True
            winnotif(box1, box5, box9)
        End If

        ' no one wins
        If allbuttonsTextLength() = 9 AndAlso win = False Then
            playerturn.Text = "DRAW"
        End If

    End Sub

    Function allbuttonsTextLength() As Integer
        Dim btnsTxtLength As Integer = 0

        For Each c As Control In Panel2.Controls
            If c.GetType() = GetType(Button) Then
                btnsTxtLength += c.Text.Length
            End If
        Next

        Return btnsTxtLength

    End Function

    Private Sub winnotif(ByVal b1 As Button, ByVal b2 As Button, ByVal b3 As Button)
        b1.BackColor = Color.FromArgb(104, 93, 121)
        b2.BackColor = Color.FromArgb(104, 93, 121)
        b3.BackColor = Color.FromArgb(104, 93, 121)

        b1.ForeColor = Color.White
        b2.ForeColor = Color.White
        b3.ForeColor = Color.White

        playerturn.Text = b1.Text + " wins!"
    End Sub

    Private Sub playbutton_Click(sender As Object, e As EventArgs) Handles playbutton.Click
        XorO = 0
        win = False
        playerturn.Text = ""

        For Each c As Control In Panel2.Controls
            If c.GetType() = GetType(Button) Then
                c.BackColor = Color.White
                c.Text = ""
            End If
        Next
    End Sub

    'ADDED CODE

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

        randomChoices()
    End Sub

    Private Sub box1_Click(sender As Object, e As EventArgs) Handles box1.Click

    End Sub
End Class