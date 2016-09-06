Imports System.Data
Imports System.Data.OleDb

Public Class winNewVendedor
    Private strPath = "..\..\dataBaseVisual.mdb"
    Private strConexion As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & strPath
    'Private strConexion As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & strPath

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
        If (txtId.Text = "" And txtNombre.Text = "" And txtApellido.Text = "" And txtUsuario.Text = "" And txtContraseña.Text = "") Then
            MessageBox.Show("Campos Vacios")
        Else
            Using conexion As New OleDbConnection(strConexion)
                conexion.Open()
                Dim Insertar As String
                Insertar = "INSERT INTO usuarios ([Id], [Nombre], [Apellido], [Edad], [Genero], [Email], [Telefono], [Cedula], [Usuario], [Contraseña], [FechaContrato], [Contacto], [administrador]) values ( txtId.Text, txtNombre.Text,
            txtApellido.Text,txtEdad.Text,txtGenero.Text,txtEmail.Text,txtTelefono.Text,txtCedula.Text,txtUsuario.Text,txtContraseña.Text,txtFC.Text,txtContacto.Text,False)"
                Dim cmd As OleDbCommand = New OleDbCommand(Insertar, conexion)
                cmd.Parameters.Add(New OleDbParameter("Id", CType(txtId.Text, String)))
                cmd.Parameters.Add(New OleDbParameter("Nombre", CType(txtNombre.Text, String)))
                cmd.Parameters.Add(New OleDbParameter("Apellido", CType(txtApellido.Text, String)))
                cmd.Parameters.Add(New OleDbParameter("Edad", CType(txtEdad.Text, String)))
                cmd.Parameters.Add(New OleDbParameter("Genero", CType(txtGenero.Text, String)))
                cmd.Parameters.Add(New OleDbParameter("Email", CType(txtEmail.Text, String)))
                cmd.Parameters.Add(New OleDbParameter("Telefono", CType(txtTelefono.Text, String)))
                cmd.Parameters.Add(New OleDbParameter("Cedula", CType(txtCedula.Text, String)))
                cmd.Parameters.Add(New OleDbParameter("Usuario", CType(txtUsuario.Text, String)))
                cmd.Parameters.Add(New OleDbParameter("Contraseña", CType(txtContraseña.Text, String)))
                cmd.Parameters.Add(New OleDbParameter("FechaContrato", CType(txtFC.Text, String)))
                cmd.Parameters.Add(New OleDbParameter("Contacto", CType(txtContacto.Text, String)))
                cmd.Parameters.Add(New OleDbParameter("administrador", False))
                cmd.ExecuteNonQuery()
            End Using
            MessageBox.Show("Se ha hecho el registro con exito .. !!")
            Me.Close()
        End If

    End Sub

    Private Sub btnActualizar_Click(sender As Object, e As RoutedEventArgs) Handles btnActualizar.Click
        If (txtNombre.Text = "" Or txtApellido.Text = "" Or txtEdad.Text = "" Or txtEmail.Text = "" Or
            txtTelefono.Text = "" Or txtGenero.Text = "" Or txtcedula.Text = "" Or txtUsuario.Text = "" Or txtContraseña.Text = "" Or txtFC.Text = "" Or txtContacto.Text = "") Then
            MessageBox.Show("Error .. !! Hay campos vacios")
        Else
            Using conexion As New OleDbConnection(strConexion)
                conexion.Open()
                Dim consultas As String = "Select * FROM usuarios"

                Dim adapter As New OleDbDataAdapter(New OleDbCommand(consultas, conexion))
                Dim vendedorCmBuilder As New OleDbCommandBuilder(adapter)
                Dim dsvendedor = New DataSet("Tienda")
                adapter.Fill(dsvendedor, "Usuarios")
                'Dim found = False
                'txtId.IsReadOnly = False
                For Each vended As DataRow In dsvendedor.Tables("usuarios").Rows
                    If vended("Id") = Me.txtId.Text Then
                        vended("Nombre") = Me.txtNombre.Text
                        vended("Apellido") = Me.txtApellido.Text
                        vended("Edad") = Me.txtEdad.Text
                        vended("Email") = Me.txtEmail.Text
                        vended("Telefono") = Me.txtTelefono.Text
                        vended("Genero") = Me.txtGenero.Text
                        vended("Cedula") = Me.txtcedula.Text
                        vended("Usuario") = Me.txtUsuario.Text
                        vended("Contraseña") = Me.txtContraseña.Text
                        vended("FechaContrato") = Me.txtFC.Text
                        vended("Contacto") = Me.txtContacto.Text
                        Exit For
                    End If
                Next

                Try
                    adapter.Update(dsvendedor.Tables("usuarios"))
                    MessageBox.Show("Se ha realizado la actualizacion con exito")
                Catch ex As Exception
                    MessageBox.Show("Error al guardar")
                End Try
            End Using

        End If

    End Sub
End Class
