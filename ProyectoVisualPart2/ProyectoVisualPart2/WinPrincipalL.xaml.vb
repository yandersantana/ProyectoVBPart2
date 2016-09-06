Imports System.Data.OleDb

Class WinPrincipalL
    Private user As String
    Public loggedIn As Boolean
    Public usuarios As ArrayList
    Private strPath = "..\..\dataBaseVisual.mdb"
    Private strConexion As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & strPath
    'Private strConexion As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & strPath
    Public persona As New Persona

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)



    End Sub

    Private Sub btn_aceptar_Click(sender As Object, e As RoutedEventArgs) Handles btn_aceptar.Click
        Using conexion As New OleDbConnection(strConexion)
            conexion.Open()

            Dim consulta As String = "SELECT Count(*) FROM usuarios WHERE Usuario = @usuario and Contraseña = @password"
            Dim consulta2 As String = "SELECT Nombre FROM usuarios WHERE Usuario=txtUsuario.Text"
            Dim cmd As New OleDbCommand(consulta, conexion)
            Dim cmd2 As New OleDbCommand(consulta, conexion)
            cmd.Parameters.AddWithValue("@usuario", txtUsuario.Text)
            cmd.Parameters.AddWithValue("@password", passwordBox.Password)

            Dim i As Integer = CInt(cmd.ExecuteScalar())

            If i = 0 Then
                MessageBox.Show("No paso la autenticacion")

            Else
                'MessageBox.Show("autenticacion correcta")


                cmd.CommandText = "SELECT administrador FROM usuarios WHERE Usuario=txtUsuario.Text"
                Dim risul As Boolean = cmd.ExecuteScalar

                If (risul) Then

                    persona.Nombre = txtUsuario.Text

                    Dim winAd As New winAdmi()
                    winAd.persona.Nombre = persona.Nombre
                    winAd.Owner = Me
                    winAd.Show()
                    Me.Hide()
                Else
                    persona.Nombre = txtUsuario.Text
                    Dim winvendedor As New winVendedor
                    winvendedor.persona2.Nombre = persona.Nombre
                    winvendedor.Owner = Me
                    winvendedor.Show()
                    Me.Hide()
                End If

            End If

        End Using

    End Sub

    Private Sub btn_cancelar_Click(sender As Object, e As RoutedEventArgs) Handles btn_cancelar.Click
        Me.Close()
    End Sub
End Class
