Imports System.Data
Imports System.Data.OleDb

Public Class winNewCliente
    Private strPath = "..\..\dataBaseVisual.mdb"
    Private strConexion As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & strPath
    'Private strConexion As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & strPath
    Public existe As Boolean = False
    Public result As Double

    Private Sub btnRegistrar_Click(sender As Object, e As RoutedEventArgs) Handles btnRegistrar.Click
        If (txtcodigo.Text = "" And txtNombre.Text = "" And txtApellido.Text = "") Then
            MessageBox.Show("Campos Vacios")
        Else
            Me.validarExistencia()
            If (existe) Then
                MessageBox.Show("Cliente ya existe asigne otro código")
            Else
                Using conexion As New OleDbConnection(strConexion)
                    conexion.Open()
                    Dim Insertar As String
                    Insertar = "INSERT INTO cliente ([Id],[Nombre], [Apellido], [Telefono], [Cedula], [Contacto]) values ( txtCodigo.Text,txtNombre.Text,
            txtApellido.Text,txtTelefono.Text,txtCedula.Text,texDireccion.Text)"
                    Dim cmd As OleDbCommand = New OleDbCommand(Insertar, conexion)
                    cmd.Parameters.Add(New OleDbParameter("Id", CType(txtcodigo.Text, String)))
                    cmd.Parameters.Add(New OleDbParameter("Nombre", CType(txtNombre.Text, String)))
                    cmd.Parameters.Add(New OleDbParameter("Apellido", CType(txtApellido.Text, String)))
                    cmd.Parameters.Add(New OleDbParameter("Telefono", CType(txtTelefono.Text, String)))
                    cmd.Parameters.Add(New OleDbParameter("Cedula", CType(txtcedula.Text, String)))
                    cmd.Parameters.Add(New OleDbParameter("Contacto", CType(txtDireccion.Text, String)))

                    cmd.ExecuteNonQuery()


                End Using
            End If
            Try
                MessageBox.Show("Se ha realizado el registro con exito.. !!")
            Catch ex As Exception
                MessageBox.Show("Error.. !! No se pudo completar la accion")
            End Try
        End If
    End Sub

    Public Sub validarExistencia()

        result = Convert.ToDouble(txtcodigo.Text)
        Using dbConexion As New OleDbConnection(strConexion) 'entrar y salir de la base
            Dim strQuery As String = "SELECT * FROM cliente"
            Dim dbAdapter As New OleDbDataAdapter(strQuery, strConexion)
            Dim dsMaster As New DataSet("Datos")
            dbAdapter.Fill(dsMaster, "Cliente")

            For Each em As DataRow In dsMaster.Tables("Cliente").Rows
                If (em(0) = result) Then
                    txtNombre.Text = em(1)
                    txtApellido.Text = em(2)
                    txtcedula.Text = em(3)
                    txtTelefono.Text = em(4)
                    txtDireccion.Text = em(5)
                    existe = True
                    txtNombre.IsEnabled = "False"
                    txtcedula.IsEnabled = "False"
                    txtTelefono.IsEnabled = "False"
                    txtDireccion.IsEnabled = "False"
                    btnRegistrar.IsEnabled = "False"
                    txtApellido.IsEnabled = "false"
                    Exit For
                Else
                    txtNombre.IsEnabled = "true"
                    txtApellido.IsEnabled = "true"
                    txtcedula.IsEnabled = "true"
                    txtTelefono.IsEnabled = "true"
                    txtDireccion.IsEnabled = "true"
                    btnRegistrar.IsEnabled = "true"
                End If

            Next

        End Using
    End Sub
End Class
