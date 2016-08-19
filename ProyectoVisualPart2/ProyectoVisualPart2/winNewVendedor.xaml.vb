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
            Dim insertar As String = "INSERT INTO usuarios (Id, Nombre , Apellido , Edad , Email , Telefono , Genero , Cedula , Usuario , Contraseña, FechaContrato , Contacto , administrador) VALUES (@id, @name, @alastname, @age, @mail, @phone, @genero, @cedula, @user, @contraseña, @fc, @contacto, @administrador)"
            '@name, @alastname, @age, @mail, @phone, @genero, @cedula, @user, @contraseña, @fc, @contacto, @administrador
            Dim cmd As New OleDbCommand(insertar, conexion)
            cmd.Parameters.Insert("@name", txtNombre.Text)
            cmd.Parameters.Insert("@name", txtNombre.Text)
            cmd.Parameters.Insert("@lastName", txtApellido)
            cmd.Parameters.Insert("@age", txtEdad.Text)
            cmd.Parameters.Insert("@mail", txtEmail.Text)
            cmd.Parameters.Insert("@phone", txtTelefono.Text)
            cmd.Parameters.Insert("@genero", txtGenero.Text)
            cmd.Parameters.Insert("@id", txtId.Text)
            cmd.Parameters.Insert("@cedula", txtCedula.Text)
            cmd.Parameters.Insert("@user", txtUsuario.Text)
            cmd.Parameters.Insert("@contraseña", txtContraseña.Text)
            cmd.Parameters.Insert("@fc", txtFC.Text)
            cmd.Parameters.Insert("@contacto", txtContacto.Text)
            cmd.Parameters.Insert("@administrador", False)

            ' cmd.Parameters.Insert("@name", txtNombre.Text)
            ' cmd.Parameters.AddWithValue("@name", txtNombre.Text)
            'cmd.Parameters.AddWithValue("@lastName", txtApellido)
            'cmd.Parameters.AddWithValue("@age", txtEdad.Text)
            'cmd.Parameters.AddWithValue("@mail", txtEmail.Text)
            'cmd.Parameters.AddWithValue("@phone", txtTelefono.Text)
            'cmd.Parameters.AddWithValue("@genero", txtGenero.Text)
            'cmd.Parameters.AddWithValue("@id", txtId.Text)
            'cmd.Parameters.AddWithValue("@cedula", txtCedula.Text)
            'cmd.Parameters.AddWithValue("@user", txtUsuario.Text)
            'cmd.Parameters.AddWithValue("@contraseña", txtContraseña.Text)
            'cmd.Parameters.AddWithValue("@fc", txtFC.Text)
            'cmd.Parameters.AddWithValue("@contacto", txtContacto.Text)
            'cmd.Parameters.AddWithValue("@administrador", False)
            'cmd.ExecuteNonQuery()
        End Using

    End Sub

    Private Sub txtId_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtId.TextChanged

    End Sub
End Class
