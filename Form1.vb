Public Class Form1

    Dim numFilasColumnas = 4
    Dim movimientos = 0
    Dim cantidadCartasVolteadas = 0
    Dim cartasEnumeradas As List(Of String)
    Dim cartasRevueltas As List(Of String)
    Dim cartasSeleccinadasString As List(Of String)
    Dim pareja As Integer
    Dim resultado As List(Of String)
    Dim cartasSeleccionadas As ArrayList
    Dim cartaTemporal1 As PictureBox
    Dim cartaTemporal2 As PictureBox
    Dim cartaTemporalString1 As String
    Dim cartaTemporalString2 As String
    Dim cartaActual As Integer = 0

    Public Sub New()

        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        iniciarJuego()
    End Sub



    Public Sub iniciarJuego()
        pareja = 0
        Timer1.Enabled = False
        Timer1.Stop()
        lblRecord.Text = 0
        cantidadCartasVolteadas = 0
        movimientos = 0
        PanelJuego.Controls.Clear()
        cartasEnumeradas = New List(Of String)
        cartasRevueltas = New List(Of String)
        cartasSeleccinadasString = New List(Of String)
        resultado = New List(Of String)
        cartasSeleccionadas = New ArrayList
        For index = 1 To 8
            cartasEnumeradas.Add(index.ToString())
            cartasEnumeradas.Add(index.ToString())
            resultado.Add("1")
            resultado.Add("1")
        Next
        ' Desordenar cartasEnumeradas y guardarlo en Resultado
        Dim n = cartasEnumeradas.Count()
        Dim rnd = New Random()
        Dim i = n - 1
        While i >= 0
            Dim j = rnd.Next(0, i)
            resultado(i) = cartasEnumeradas(j)
            cartasEnumeradas.RemoveAt(j)
            i -= 1
        End While
        '' damos valor a cartasRevueltas desde resultado
        For Each valorCarta As String In resultado
            cartasRevueltas.Add(valorCarta)
        Next
        PanelJuego.Controls.Add(TableLayoutPanel1)
        For Each control In TableLayoutPanel1.Controls
            Dim cartasReverso = TryCast(control, PictureBox)
            cartasReverso.Image = My.Resources.reverso
        Next

    End Sub
    Private Sub ButtonReinicio_Click(sender As Object, e As EventArgs) Handles ButtonReinicio.Click
        iniciarJuego()
    End Sub

    Private Sub Carta_Click(sender As Object, e As EventArgs) Handles PictureBox9.Click, PictureBox8.Click, PictureBox7.Click, PictureBox6.Click, PictureBox5.Click, PictureBox4.Click, PictureBox3.Click, PictureBox2.Click, PictureBox15.Click, PictureBox14.Click, PictureBox13.Click, PictureBox12.Click, PictureBox11.Click, PictureBox10.Click, PictureBox1.Click, PictureBox0.Click
        Dim cartaSeleccionada As PictureBox
        Dim posicionString As String
        Dim posicionNum As Integer
        Dim nombreImagen As String
        If (cartasSeleccinadasString.Count < 2) Then
            movimientos += 1
            lblRecord.Text = movimientos.ToString
            cartaSeleccionada = DirectCast(sender, PictureBox)
            posicionString = cartaSeleccionada.Name.Substring(10)
            posicionNum = CInt(posicionString)
            cartaActual = cartasRevueltas(posicionNum)
            nombreImagen = "img" & cartaActual
            Dim cartaImagen = My.Resources.ResourceManager.GetObject(nombreImagen)
            cartaSeleccionada.Image = cartaImagen
            cartasSeleccionadas.Add(cartaSeleccionada)
            cartasSeleccinadasString.Add(cartaActual)
        End If
        ''
        If (cartasSeleccinadasString.Count = 2) Then
            cartaTemporal1 = cartasSeleccionadas(0)
            cartaTemporal2 = cartasSeleccionadas(1)
            cartaTemporalString1 = cartasSeleccinadasString(0)
            cartaTemporalString2 = cartasSeleccinadasString(1)
            If Equals(cartaTemporalString1, cartaTemporalString2) Then
                cartaTemporalString1 = ""
                cartaTemporalString2 = ""
                cartasSeleccinadasString.Clear()
                cartasSeleccionadas.Clear()
                pareja += 1
                'ganar 
                CheckWin()
            Else
                Timer1.Enabled = True
                Timer1.Start()

            End If
        End If

    End Sub

    Private Sub CheckWin()
        If pareja > 2 Then
            Dim mensaje = "You matched all the icons !  you needed " & movimientos & " tries to win"
            MessageBox.Show(mensaje, "Winner")
            Close()
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim Voltear = True
        If Voltear Then
            cartaTemporal1.Image = My.Resources.reverso
            cartaTemporal2.Image = My.Resources.reverso
            cartasSeleccionadas.Clear()
            cartasSeleccinadasString.Clear()
            Voltear = False
            Timer1.Stop()
        End If
    End Sub

End Class
