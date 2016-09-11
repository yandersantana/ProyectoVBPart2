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
    Public result As String
    Public result2 As String
    Public porIva As Double
    Dim vfecha As Date = Now
    Public valor As Double
    Public valor2 As Double



    Private Sub salir_Click(sender As Object, e As RoutedEventArgs) Handles salir.Click
        Me.Close()
        Me.Owner.Show()
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
                        txtcantid.Text = 1
                        agregar.IsEnabled = True
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

    Public Sub agregar_Click(sender As Object, e As RoutedEventArgs) Handles agregar.Click
        ValordeIva()
        txtSubt.Text = Val(CDbl(txtpUnitario.Text) * txtcantid.Text)
        txtsubtotalFinal.Text = Val(txtSubt.Text + CDbl(txtsubtotalFinal.Text))
        calcular()

        Dim newfact As New winFactura
        Using conexion As New OleDbConnection(strConexion)
            conexion.Open()
            Dim Insertar As String
            Insertar = "INSERT INTO Proxfactura ( [Nfactura], [NombreCliente], [ApeCliente], [CodCliente], [CedCliente], [ForPago], [Vendedor], [Fecha], [codProd] ,[Descripcion] , [Punitario], [Cantidad], [Subtotal],[SubFinal], [Descuento], [Iva] ,[Total] ) 
values ( txtNfactura.Text,txtNombre.Text,txtApellido.Text,txtCodigo.Text,txtCedula.Text,comboBoxPago.SelectedValue.ToString,txtVendedor.Text,txtFecha.Text,txtcodigoPro.Text,nombreProducto.Text,txtpUnitario.Text,txtcantid.Text,txtSubt.Text,txtsubtotalFinal.Text,txtdescuento.Text,txtIva.Text,txttotal.Text)"
            Dim cmd As OleDbCommand = New OleDbCommand(Insertar, conexion)
            cmd.Parameters.Add(New OleDbParameter("Nfactura", CType(txtNfactura.Text, String)))
            cmd.Parameters.Add(New OleDbParameter("NombreCliente", CType(txtNombre.Text, String)))
            cmd.Parameters.Add(New OleDbParameter("ApeCliente", CType(txtApellido.Text, String)))
            cmd.Parameters.Add(New OleDbParameter("CodCliente", CType(txtCodigo.Text, String)))
            cmd.Parameters.Add(New OleDbParameter("CedCliente", CType(txtCedula.Text, String)))
            cmd.Parameters.Add(New OleDbParameter("ForPago", CType(comboBoxPago.SelectedValue.ToString, String)))
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
            Dim strQuery2 As String = "SELECT codProd , Descripcion , Punitario , Cantidad , Subtotal FROM Proxfactura WHERE Nfactura='" & txtNfactura.Text & "'"
            Dim dbAdapter2 As New OleDbDataAdapter(strQuery2, strConexion)
            Dim dsMaster2 As New DataSet("Datos")
            dbAdapter2.Fill(dsMaster2, "Factura")
            DGdetalle.DataContext = dsMaster2
            btnEliminar.IsEnabled = True
            btnGuardarFactura.IsEnabled = True
            newfact.UpdateDataGrid()
        End Using
        porcDevolucion()


    End Sub

    Private Sub factura_Loaded(sender As Object, e As RoutedEventArgs) Handles factura.Loaded
        Dim winFac As New winVendedor
        txtVendedor.Text = winFac.nomVend
        txtNombre.IsEnabled = "False"
        txtCedula.IsEnabled = "true"
        txtTelefono.IsEnabled = "False"
        texDireccion.IsEnabled = "False"
        txtApellido.IsEnabled = "false"
        txtFecha.Text = Mid(vfecha, 1, 10)
        txtFecha.IsReadOnly = True
        txtsubtotalFinal.Text = 0.00
        txtdescuento.Text = 0.00
        txtIva.Text = 0.00
        txttotal.Text = 0.00
        txtNfactura.IsEnabled = "true"
        comboBoxPago.Items.Add("Efectivo")
        comboBoxPago.Items.Add("Tarjeta de credito")
        comboBoxPago.Items.Add("Dinero Electronico")
        comboProvincias.Items.Add("Azuay")
        comboProvincias.Items.Add("Bolivar")
        comboProvincias.Items.Add("Cañar")
        comboProvincias.Items.Add("Carchi")
        comboProvincias.Items.Add("Chimborazo")
        comboProvincias.Items.Add("Cotopaxi")
        comboProvincias.Items.Add("El Oro")
        comboProvincias.Items.Add("Esmeraldas")
        comboProvincias.Items.Add("Galapagos")
        comboProvincias.Items.Add("Guayas")
        comboProvincias.Items.Add("Imbabura")
        comboProvincias.Items.Add("Loja")
        comboProvincias.Items.Add("Los Rios")
        comboProvincias.Items.Add("Manabi")
        comboProvincias.Items.Add("Morona santiago")
        comboProvincias.Items.Add("Napo")
        comboProvincias.Items.Add("Orellana")
        comboProvincias.Items.Add("Pastaza")
        comboProvincias.Items.Add("Pichincha")
        comboProvincias.Items.Add("Santa Elena")
        comboProvincias.Items.Add("Santo Domingo de los Tsachilas")
        comboProvincias.Items.Add("Sucumbios")
        comboProvincias.Items.Add("Tungurahua")
        comboProvincias.Items.Add("Zamora Chinchipe")
        comboProvincias.SelectedItem = "Guayas"
        comboBoxPago.SelectedItem = "Efectivo"
        txtcantid.Text = 1
        btnEliminar.IsEnabled = False
        btnGuardarFactura.IsEnabled = False
        agregar.IsEnabled = False
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
            End If
        End If
    End Sub


    Public Sub validarExistencia()

        result = txtCodigo.Text
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
                End If

            Next

        End Using
    End Sub

    Private Sub NuevoCliente_Click(sender As Object, e As RoutedEventArgs) Handles NuevoCliente.Click
        Dim newcliente As New winNewCliente
        newcliente.Owner = Me
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
        If (comboProvincias.SelectedItem = "Esmeraldas" Or comboProvincias.SelectedItem = "Manabi") Then
            porIva = 0.12
        Else
            porIva = 0.14
        End If
    End Sub

    Private Sub porcDevolucion()
        If (comboBoxPago.SelectedValue.ToString = "Dinero Electronico") Then
            txtdevolucion.Text = Val(txttotal.Text * 0.04)
        ElseIf (comboBoxPago.SelectedValue.ToString = "Tarjeta de Credito") Then
            txtdevolucion.Text = Val(CDbl(txttotal.Text) * 0.02)
        Else
            txtdevolucion.Text = 0.00
        End If
    End Sub

    Private Sub txtCedula_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCedula.KeyDown
        If e.Key = Key.Enter Then
            txtNombre.Text = ""
            txtTelefono.Text = ""
            texDireccion.Text = ""
            txtApellido.Text = ""
            Me.validarExistenciaPorCedula()

            If Not existe Then
                MessageBox.Show("Cliente no Existe")
            End If
        End If
    End Sub

    Public Sub validarExistenciaPorCedula()
        result2 = txtCedula.Text
        Using dbConexion As New OleDbConnection(strConexion) 'entrar y salir de la base
            Dim strQuery As String = "SELECT * FROM cliente"
            Dim dbAdapter As New OleDbDataAdapter(strQuery, strConexion)
            Dim dsMaster As New DataSet("Datos")
            dbAdapter.Fill(dsMaster, "Cliente")

            For Each em As DataRow In dsMaster.Tables("Cliente").Rows
                If (em(3) = result2) Then
                    txtCodigo.Text = em(0)
                    txtNombre.Text = em(1)
                    txtApellido.Text = em(2)
                    txtCedula.Text = em(3)
                    txtTelefono.Text = em(4)
                    texDireccion.Text = em(5)
                    existe = True
                    txtNombre.IsEnabled = "False"
                    txtTelefono.IsEnabled = "False"
                    texDireccion.IsEnabled = "False"
                    NuevoCliente.IsEnabled = "False"
                    txtApellido.IsEnabled = "false"
                    Exit For
                End If
            Next
        End Using
    End Sub

    Private Sub btnEliminar_Click(sender As Object, e As RoutedEventArgs) Handles btnEliminar.Click
        Dim newfact As New winFactura
        Using dbconexion As New OleDbConnection(strConexion)
            Dim consulta As String = "SELECT * From Proxfactura"
            Dim dbadapter As New OleDbDataAdapter(consulta, strConexion)
            Dim dsMaster2 As New DataSet("Datos")
            dbadapter.Fill(dsMaster2, "Factura")

            Dim fila As DataRowView = DGdetalle.SelectedItem
            For Each em As DataRow In dsMaster2.Tables("Factura").Rows
                If ((em(10) = fila(1)) And (txtNfactura.Text = em(1))) Then
                    valor = em(13)
                    Dim filaElim As String = "DELETE * From Proxfactura WHERE Nfactura='" & txtNfactura.Text & "' AND Descripcion='" & fila(1) & "'"
                    Dim dbAdapter3 As New OleDbDataAdapter(filaElim, strConexion)
                    Dim dsMaster3 As New DataSet("Datos")
                    dbAdapter3.Fill(dsMaster3, "Factura")
                    'MessageBox.Show("se encontro" & valor)
                    Exit For
                End If
            Next
            newfact.UpdateDataGrid()
        End Using
        txtsubtotalFinal.Text = Val(CDbl(txtsubtotalFinal.Text) - CDbl(valor))
        calcular()

    End Sub

    Public Sub calcular()
        txtIva.Text = Val(CDbl(txtsubtotalFinal.Text) * porIva)
        txttotal.Text = Val(CDbl(txtsubtotalFinal.Text) + CDbl(txtIva.Text))

    End Sub

    Private Sub btnGuardarFactura_Click(sender As Object, e As RoutedEventArgs) Handles btnGuardarFactura.Click

        Using conexion As New OleDbConnection(strConexion)
            conexion.Open()
            Dim Insertar As String
            Insertar = "INSERT INTO Facturas ( [Nfactura], [NombreCliente], [ApeCliente], [CodCliente], [CedCliente],[Provincia], [ForPago], [Vendedor], [Fecha],[Total],[DevolucionIva] ) 
values ( txtNfactura.Text,txtNombre.Text,txtApellido.Text,txtCodigo.Text,txtCedula.Text,comboProvincias.SelectedValue.ToString,comboBoxPago.SelectedValue.ToString,txtVendedor.Text,txtFecha.Text,txttotal.Text,txtdevolucion.Text)"
            Dim cmd As OleDbCommand = New OleDbCommand(Insertar, conexion)
            cmd.Parameters.Add(New OleDbParameter("Nfactura", CType(txtNfactura.Text, String)))
            cmd.Parameters.Add(New OleDbParameter("NombreCliente", CType(txtNombre.Text, String)))
            cmd.Parameters.Add(New OleDbParameter("ApeCliente", CType(txtApellido.Text, String)))
            cmd.Parameters.Add(New OleDbParameter("CodCliente", CType(txtCodigo.Text, String)))
            cmd.Parameters.Add(New OleDbParameter("CedCliente", CType(txtCedula.Text, String)))
            cmd.Parameters.Add(New OleDbParameter("Provincia", CType(comboProvincias.SelectedValue.ToString, String)))
            cmd.Parameters.Add(New OleDbParameter("ForPago", CType(comboBoxPago.SelectedValue.ToString, String)))
            cmd.Parameters.Add(New OleDbParameter("Vendedor", CType(txtVendedor.Text, String)))
            cmd.Parameters.Add(New OleDbParameter("Fecha", CType(txtFecha.Text, String)))
            cmd.Parameters.Add(New OleDbParameter("Total", CType(txttotal.Text, Double)))
            cmd.Parameters.Add(New OleDbParameter("DevolucionIva", CType(txtdevolucion.Text, Double)))
            cmd.ExecuteNonQuery()

            Try
                MessageBox.Show("Se guardo la factura con exito..!!")
                CleanAll()

            Catch ex As Exception
                MessageBox.Show("Error al guardar")
            End Try
        End Using
    End Sub

    Private Sub CleanAll()
        limpiar()
        txtNombre.Text = ""
        txtCedula.Text = ""
        txtTelefono.Text = ""
        texDireccion.Text = ""
        txtApellido.Text = ""
        txtCodigo.Text = ""
        txtNfactura.Text = ""
        comboProvincias.SelectedItem = "Guayas"
        comboBoxPago.SelectedItem = "Efectivo"
        txtsubtotalFinal.Text = 0.00
        txtdescuento.Text = 0.00
        txtIva.Text = 0.00
        txttotal.Text = 0.00
        txtdevolucion.Text = 0.00
        DGdetalle.DataContext = ""
        txtcantid.Text = 1
    End Sub

End Class
