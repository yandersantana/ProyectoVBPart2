Imports System.Data.OleDb

Public Class winNewVendedor

    Private strPath = "..\..\dataBaseVisual.mdb"

    Private strConexion As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & strPath

    Private Sub btnCancel_Click(sender As Object, e As RoutedEventArgs) Handles btnCancel.Click
        'Dim winADM As New winAdministrador
        'winADM.Owner = Me
        'winADM.Show()
        'Me.Hide()
    End Sub

    Private Sub Window_Closing(sender As Object, e As ComponentModel.CancelEventArgs)
        'Dim winADM As New winAdministrador
        'winADM.Owner = Me
        'winADM.Show()
        'Me.Hide()

    End Sub

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)

    End Sub
    Private Sub btnRegistrar_Click(sender As Object, e As RoutedEventArgs) Handles btnRegistrar.Click
        Using conexion As New OleDbConnection(strConexion)
            conexion.Open()
            Dim Insertar As String
            Insertar = "INSERT INTO usuarios ([Id], [Nombre], [Apellido], [Edad], [Genero]) values ( txtId.Text, txtNombre.Text,
            txtApellido.Text,txtEdad.Text,txtGenero.Text)"
            Dim cmd As OleDbCommand = New OleDbCommand(Insertar, conexion)
            cmd.Parameters.Add(New OleDbParameter("Id", CType(txtId.Text, String)))
            cmd.Parameters.Add(New OleDbParameter("Nombre", CType(txtNombre.Text, String)))
            cmd.Parameters.Add(New OleDbParameter("Apellido", CType(txtApellido.Text, String)))
            cmd.Parameters.Add(New OleDbParameter("Edad", CType(txtEdad.Text, String)))
            cmd.Parameters.Add(New OleDbParameter("Genero", CType(txtGenero.Text, String)))
            cmd.ExecuteNonQuery()




        End Using

    End Sub

    Private Sub txtId_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtId.TextChanged

    End Sub
End Class
