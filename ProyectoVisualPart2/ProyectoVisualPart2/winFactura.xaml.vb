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
    Public porIva As Double

    Private Sub salir_Click(sender As Object, e As RoutedEventArgs) Handles salir.Click
        Me.Close()
        Me.Owner.Show()
    End Sub

    Private Sub codigo_DragEnter(sender As Object, e As DragEventArgs)

    End Sub

    Private Sub codigo_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles txtcodigoPro.MouseDoubleClick

    End Sub

    Private Sub codigo_KeyDown(sender As Object, e As KeyEventArgs) Handles txtcodigoPro.KeyDown
        If e.Key = Key.Enter Then

            Dim existe As Boolean = False
            result = Convert.ToDouble(txtcodigoPro.Text)
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
        ValordeIva()
        txtSubt.Text = Val(CDbl(txtpUnitario.Text) * CDbl(txtcantid.Text))
        txtsubtotalFinal.Text = Val(CDbl(txtSubt.Text) + CDbl(txtsubtotalFinal.Text))
        txtIva.Text = Val(CDbl(txtsubtotalFinal.Text) * porIva)
        txttotal.Text = Val(CDbl(txtsubtotalFinal.Text) + CDbl(txtIva.Text))

        Dim newfact As New winFactura
        Using conexion As New OleDbConnection(strConexion)
            conexion.Open()
            Dim Insertar As String
            Insertar = "INSERT INTO factura ( [Nfactura], [NombreCliente], [ApeCliente], [CodCliente], [CedCliente], [ForPago], [Vendedor], [Fecha], [codProd] ,[Descripcion] , [Punitario], [Cantidad], [Subtotal],[SubFinal], [Descuento], [Iva] ,[Total] ) 
values ( txtNfactura.Text,txtNombre.Text,txtApellido.Text,txtCodigo.Text,txtCedula.Text,txtPago.Text,txtVendedor.Text,txtFecha.Text,txtcodigoPro.Text,nombreProducto.Text,txtpUnitario.Text,txtcantid.Text,txtSubt.Text,txtsubtotalFinal.Text,txtdescuento.Text,txtIva.Text,txttotal.Text)"
            Dim cmd As OleDbCommand = New OleDbCommand(Insertar, conexion)
            cmd.Parameters.Add(New OleDbParameter("Nfactura", CType(txtNfactura.Text, String)))
            cmd.Parameters.Add(New OleDbParameter("NombreCliente", CType(txtNombre.Text, String)))
            cmd.Parameters.Add(New OleDbParameter("ApeCliente", CType(txtApellido.Text, String)))
            cmd.Parameters.Add(New OleDbParameter("CodCliente", CType(txtCodigo.Text, String)))
            cmd.Parameters.Add(New OleDbParameter("CedCliente", CType(txtCedula.Text, String)))
            cmd.Parameters.Add(New OleDbParameter("ForPago", CType(txtPago.Text, String)))
            cmd.Parameters.Add(New OleDbParameter("Vendedor", CType(txtVendedor.Text, String)))
            cmd.Parameters.Add(New OleDbParameter("Fecha", CType(txtFecha.Text, String)))
            cmd.Parameters.Add(New OleDbParameter("codProd", CType(txtcodigoPro.Text, String)))
            cmd.Parameters.Add(New OleDbParameter("Descripcion", CType(nombreProducto.Text, String)))
            cmd.Parameters.Add(New OleDbParameter("Punitario", CType(txtpUnitario.Text, String)))
            cmd.Parameters.Add(New OleDbParameter("Cantidad", CType(txtcantid.Text, String)))
            cmd.Parameters.Add(New OleDbParameter("Subtotal", CType(txtSubt.Text, String)))
            cmd.Parameters.Add(New OleDbParameter("SubFinal", CType(txtsubtotalFinal.Text, String)))
            cmd.Parameters.Add(New OleDbParameter("Descuento", CType(txtdescuento.Text, String)))
            cmd.Parameters.Add(New OleDbParameter("Iva", CType(txtIva.Text, String)))
            cmd.Parameters.Add(New OleDbParameter("Total", CType(txttotal.Text, String)))
            cmd.ExecuteNonQuery()
        End Using
        limpiar()

        Using conexion As New OleDbConnection(strConexion)
            Dim strQuery2 As String = "SELECT codProd , Descripcion , Punitario , Cantidad , Subtotal FROM factura WHERE Nfactura='" & txtNfactura.Text & "'"
            Dim dbAdapter2 As New OleDbDataAdapter(strQuery2, strConexion)
            Dim dsMaster2 As New DataSet("Datos")
            dbAdapter2.Fill(dsMaster2, "Factura")
            DGdetalle.DataContext = dsMaster2
            newfact.UpdateDataGrid()
        End Using


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
        txtNfactura.IsEnabled = "true"


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

    Public Sub UpdateDataGrid()
        Me.factura_Loaded(Nothing, Nothing)
    End Sub

    Private Sub limpiar()
        txtcodigoPro.Text = ""
        nombreProducto.Text = ""
        txtcantid.Text = ""
        txtpUnitario.Text = ""
        txtSubt.Text = ""
    End Sub

    Private Sub ValordeIva()

        If (txtProvincia.Text = "Esmeraldas" Or txtProvincia.Text = "Manabi") Then
            porIva = 0.12
        Else
            porIva = 0.14
        End If
    End Sub

    Private Sub porcDevolucion()
        If (txtPago.Text = "") Then

        End If
    End Sub
End Class
