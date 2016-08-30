Imports System.Data.OleDb

Class WinPrincipalL
    Public loggedIn As Boolean
    Public usuarios As ArrayList
    Private strPath = "..\..\dataBaseVisual.mdb"
    'Private strConexion As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & strPath
    Private strConexion As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & strPath
    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)




    End Sub

    Private Sub btn_aceptar_Click(sender As Object, e As RoutedEventArgs) Handles btn_aceptar.Click
        Using conexion As New OleDbConnection(strConexion)
            conexion.Open()

            Dim consulta As String = "SELECT Count(*) FROM usuarios WHERE Usuario = @usuario and Contraseña = @password"

            Dim cmd As New OleDbCommand(consulta, conexion)
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
                    Dim winAd As New winAdmi
                    winAd.Owner = Me
                    winAd.Show()
                    Me.Hide()
                Else
                    Dim winvendedor As New winFactura
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
