Imports System.Data
Imports System.Data.OleDb
Imports System.IO
Imports System.Net.WebRequestMethods

Public Class winFactura
    Private strPath = "..\..\dataBaseVisual.mdb"
    'Private strConexion As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & strPath
    Private strConexion As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & strPath
    Public existe As Boolean = False
    Private listaProducto As New ArrayList
    Public result As Double

    Private Sub salir_Click(sender As Object, e As RoutedEventArgs) Handles salir.Click
        Me.Close()
        Me.Owner.Show()
    End Sub

    Private Sub codigo_DragEnter(sender As Object, e As DragEventArgs)

    End Sub

    Private Sub codigo_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles codigo.MouseDoubleClick

    End Sub

    Private Sub codigo_KeyDown(sender As Object, e As KeyEventArgs) Handles codigo.KeyDown
        If e.Key = Key.Enter Then

            Dim existe As Boolean = False
            result = Convert.ToDouble(codigo.Text)
            Using dbConexion As New OleDbConnection(strConexion) 'entrar y salir de la base
                Dim strQuery As String = "SELECT * FROM producto"
                Dim dbAdapter As New OleDbDataAdapter(strQuery, strConexion)
                Dim dsMaster As New DataSet("Datos")
                dbAdapter.Fill(dsMaster, "Productos")

                For Each em As DataRow In dsMaster.Tables("Productos").Rows
                    If (em(0) = result) Then
                        nombreProducto.Text = em(1)
                        pUnitario.Text = em(2)
                        registraIva.Text = em(3)
                        existe = True

                        Exit For
                    End If

                Next

            End Using
            If Not existe Then
                MessageBox.Show("Código Incorrecto")
            End If
        End If
    End Sub

    Private Sub agregar_Click(sender As Object, e As RoutedEventArgs) Handles agregar.Click
        Dim produc As New Producto()
        produc.NombreProducto = nombreProducto.Text
        produc.Codigo = Convert.ToString(result)

        produc.RegistraIva = registraIva.Text
        produc.PrecioUnitario = Convert.ToDouble(pUnitario.Text)
        produc.Cantidad = Convert.ToInt16(cantid.Text)
        produc.TotalPro = produc.Cantidad * produc.PrecioUnitario
        listaProducto.Add(produc)

    End Sub

    Private Sub factura_Loaded(sender As Object, e As RoutedEventArgs) Handles factura.Loaded
        txtNombre.IsEnabled = "False"
        txtCedula.IsEnabled = "False"
        txtTelefono.IsEnabled = "False"
        texDireccion.IsEnabled = "False"
        guardarCliente.IsEnabled = "False"
           txtApellido.IsEnabled = "false"


    End Sub

    Private Sub txtCodigo_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCodigo.KeyDown
        If e.Key = Key.Enter Then
            txtNombre.Text = ""
            txtCedula.Text = ""
            txtTelefono.Text = ""
            texDireccion.Text = ""
            txtApellido.Text = ""
            Me.validarExistencia()

            If Not existe Then
                MessageBox.Show("Cliente no Existe")
                txtNombre.Text = ""
                txtCedula.Text = ""
                txtTelefono.Text = ""
                texDireccion.Text = ""
                txtApellido.Text = ""


            End If
        End If
    End Sub

    Private Sub guardarCliente_Click(sender As Object, e As RoutedEventArgs) Handles guardarCliente.Click
        If (txtCodigo.Text = "" And txtNombre.Text = "" And txtApellido.Text = "") Then
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
                    cmd.Parameters.Add(New OleDbParameter("Id", CType(txtCodigo.Text, String)))
                    cmd.Parameters.Add(New OleDbParameter("Nombre", CType(txtNombre.Text, String)))
                    cmd.Parameters.Add(New OleDbParameter("Apellido", CType(txtApellido.Text, String)))
                    cmd.Parameters.Add(New OleDbParameter("Telefono", CType(txtTelefono.Text, String)))
                    cmd.Parameters.Add(New OleDbParameter("Cedula", CType(txtCedula.Text, String)))
                    cmd.Parameters.Add(New OleDbParameter("Contacto", CType(texDireccion.Text, String)))

                    cmd.ExecuteNonQuery()




                End Using
            End If





        End If

    End Sub
    Public Sub validarExistencia()

        result = Convert.ToDouble(txtCodigo.Text)
        Using dbConexion As New OleDbConnection(strConexion) 'entrar y salir de la base
            Dim strQuery As String = "SELECT * FROM cliente"
            Dim dbAdapter As New OleDbDataAdapter(strQuery, strConexion)
            Dim dsMaster As New DataSet("Datos")
            dbAdapter.Fill(dsMaster, "Cliente")

            For Each em As DataRow In dsMaster.Tables("Cliente").Rows
                If (em(0) = result) Then
                    txtNombre.Text = em(1)
                    txtApellido.Text = em(2)
                    txtCedula.Text = em(3)
                    txtTelefono.Text = em(4)
                    texDireccion.Text = em(5)
                    existe = True
                    txtNombre.IsEnabled = "False"
                    txtCedula.IsEnabled = "False"
                    txtTelefono.IsEnabled = "False"
                    texDireccion.IsEnabled = "False"
                    guardarCliente.IsEnabled = "False"
                    txtApellido.IsEnabled = "false"
                    Exit For
                Else
                    txtNombre.IsEnabled = "true"
                    txtApellido.IsEnabled = "true"
                    txtCedula.IsEnabled = "true"
                    txtTelefono.IsEnabled = "true"
                    texDireccion.IsEnabled = "true"
                    guardarCliente.IsEnabled = "true"
                End If

            Next

        End Using
    End Sub
End Class
