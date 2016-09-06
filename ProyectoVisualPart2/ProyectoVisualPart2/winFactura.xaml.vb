Imports System.Data
Imports System.Data.OleDb
Imports System.IO
Imports System.Net.WebRequestMethods

Public Class winFactura
    Private strPath = "..\..\dataBaseVisual.mdb"
    Private strConexion As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & strPath
    'Private strConexion As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & strPath
    Public existe As Boolean = False
    Private listaProducto As New ArrayList
    Public result As Double
    Dim dt As New DataTable
    Dim dr As DataRow
    Dim dcCodido As New DataColumn("Codigo", GetType(System.Int16))
    Dim dcnombre As New DataColumn("Nombre", GetType(System.String))

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
                        txtpUnitario.Text = em(2)

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
        txtSubt.Text = Val(CDbl(txtpUnitario.Text) * CDbl(txtcantid.Text))
        txtsubtotalFinal.Text = Val(CDbl(txtSubt.Text) + CDbl(txtsubtotalFinal.Text))
        txtIva.Text = Val(CDbl(txtsubtotalFinal.Text) * 0.14)
        txttotal.Text = Val(CDbl(txtsubtotalFinal.Text) + CDbl(txtIva.Text))

        'Dim produc As New Producto()
        'produc.NombreProducto = nombreProducto.Text
        'produc.Codigo = Convert.ToString(result)

        'produc.RegistraIva = registraIva.Text
        'produc.PrecioUnitario = Convert.ToDouble(pUnitario.Text)
        'produc.Cantidad = Convert.ToInt16(cantid.Text)
        'produc.TotalPro = produc.Cantidad * produc.PrecioUnitario
        'listaProducto.Add(produc)
        'Dim dt As New DataTable
        'Dim dr As DataRow
        'Dim dcCodido As New DataColumn("Codigo", GetType(System.Int16))
        'Dim dcnombre As New DataColumn("Nombre", GetType(System.String))
        'dt.Columns.Add(dcCodido)
        'dt.Columns.Add(dcnombre)
        'dr = dt.NewRow
        'dr("Codigo") = 24
        'dr("Nombre") = "gdfgdf"
        'dt.Rows.Add(dr)
        'Me.dataGrid.DataContext = dt
        'Dim row As DataGridViewRow = DataGridView1.Rows(0)
        'row.Cells(0).Value = TextBox4.Text
        'row.Cells(3).Value = TextBox3.Text
        'row.Cells(2).Value = TextBox2.Text
        'row.Cells(1).Value = TextBox1.Text

        'Me.detalle2.da
        'Dim dr As DataRow
        'Dim fila As DataRowView = sender.selectedItem
        'Dim data As New DataTable
        'data.Rows.Add("12", "lalala", "23", "45", "fdgfd", "45")
        ' Create new DataTable and DataSource objects.





    End Sub

    Private Sub factura_Loaded(sender As Object, e As RoutedEventArgs) Handles factura.Loaded
        txtVendedor.Text = "Yander"
        txtNombre.IsEnabled = "False"
        txtCedula.IsEnabled = "False"
        txtTelefono.IsEnabled = "False"
        texDireccion.IsEnabled = "False"
        txtApellido.IsEnabled = "false"
        txtsubtotalFinal.Text = 0.00
        txtIva.Text = 0.00
        txttotal.Text = 0.00

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



    'Private Sub guardarCliente_Click(sender As Object, e As RoutedEventArgs) Handles guardarCliente.Click
    '    If (txtCodigo.Text = "" And txtNombre.Text = "" And txtApellido.Text = "") Then
    '        MessageBox.Show("Campos Vacios")
    '    Else
    '        Me.validarExistencia()
    '        If (existe) Then
    '            MessageBox.Show("Cliente ya existe asigne otro código")
    '        Else
    '            Using conexion As New OleDbConnection(strConexion)
    '                conexion.Open()
    '                Dim Insertar As String
    '                Insertar = "INSERT INTO cliente ([Id],[Nombre], [Apellido], [Telefono], [Cedula], [Contacto]) values ( txtCodigo.Text,txtNombre.Text,
    '        txtApellido.Text,txtTelefono.Text,txtCedula.Text,texDireccion.Text)"
    '                Dim cmd As OleDbCommand = New OleDbCommand(Insertar, conexion)
    '                cmd.Parameters.Add(New OleDbParameter("Id", CType(txtCodigo.Text, String)))
    '                cmd.Parameters.Add(New OleDbParameter("Nombre", CType(txtNombre.Text, String)))
    '                cmd.Parameters.Add(New OleDbParameter("Apellido", CType(txtApellido.Text, String)))
    '                cmd.Parameters.Add(New OleDbParameter("Telefono", CType(txtTelefono.Text, String)))
    '                cmd.Parameters.Add(New OleDbParameter("Cedula", CType(txtCedula.Text, String)))
    '                cmd.Parameters.Add(New OleDbParameter("Contacto", CType(texDireccion.Text, String)))

    '                cmd.ExecuteNonQuery()




    '            End Using
    '        End If





    '    End If

    'End Sub

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
                    NuevoCliente.IsEnabled = "False"
                    txtApellido.IsEnabled = "false"
                    Exit For
                Else
                    txtNombre.IsEnabled = "true"
                    txtApellido.IsEnabled = "true"
                    txtCedula.IsEnabled = "true"
                    txtTelefono.IsEnabled = "true"
                    texDireccion.IsEnabled = "true"
                    NuevoCliente.IsEnabled = "true"
                End If

            Next

        End Using
    End Sub

    Private Sub NuevoCliente_Click(sender As Object, e As RoutedEventArgs) Handles NuevoCliente.Click
        Dim newcliente As New winNewCliente
        newcliente.Show()
        Me.Hide()
    End Sub
End Class
