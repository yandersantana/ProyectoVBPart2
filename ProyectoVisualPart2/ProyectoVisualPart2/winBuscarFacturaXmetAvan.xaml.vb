Imports System.Data
Imports System.Data.OleDb

Public Class winBuscarFacturaXmetAvan
    Private strPath = "..\..\dataBaseVisual.mdb"
    Private strConexion As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & strPath
    'Private strConexion As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & strPath
    Public inf As String
    Public winFac As New winFactura
    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        comboBoxBuscarPor.Items.Add("Fecha")
        comboBoxBuscarPor.Items.Add("Provincia")
        comboBoxBuscarPor.Items.Add("Codigo Cliente")
        comboBoxBuscarPor.SelectedItem = "Fecha"
    End Sub

    Private Sub btnBusc_Click(sender As Object, e As RoutedEventArgs) Handles btnBusc.Click
        If (comboBoxBuscarPor.SelectedItem = "Fecha") Then
            inf = "SELECT * FROM Facturas WHERE Fecha='" & txtInf.Text & "'"
        ElseIf (comboBoxBuscarPor.SelectedItem = "Provincia") Then
            inf = "SELECT * FROM Facturas WHERE Provincia='" & txtInf.Text & "'"
        ElseIf (comboBoxBuscarPor.SelectedItem = "Codigo Cliente") Then
            inf = "SELECT * FROM Facturas WHERE CodCliente='" & txtInf.Text & "'"
        End If

        Using dbConexion As New OleDbConnection(strConexion) 'entrar y salir de la base
            Dim strQuery As String = inf
            Dim dbAdapter As New OleDbDataAdapter(strQuery, strConexion)
            Dim dsMaster As New DataSet("Datos")
            dbAdapter.Fill(dsMaster, "Cliente")
            Try
                dtgDet.DataContext = dsMaster
            Catch ex As Exception
                MessageBox.Show("No existe informacion ")
            End Try

        End Using
    End Sub

    Private Sub btnSalirBusqueda_Click(sender As Object, e As RoutedEventArgs) Handles btnSalirBusqueda.Click
        Me.Close()
        Dim winBus As New winBuscarFactura
        winBus.Show()
    End Sub

    Private Sub dtgDet_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles dtgDet.SelectionChanged
        winFac.Owner = Me
        Using dbConexion As New OleDbConnection(strConexion) 'entrar y salir de la base
            Dim strQuery As String = "SELECT * FROM Facturas"
            Dim dbAdapter As New OleDbDataAdapter(strQuery, strConexion)
            Dim dsMaster As New DataSet("Datos")
            dbAdapter.Fill(dsMaster, "Facturas")
            Dim fila As DataRowView = dtgDet.SelectedItem
            For Each em As DataRow In dsMaster.Tables("Facturas").Rows
                If (em(1) = fila(1)) Then
                    winFac = New winFactura
                    winFac.agregar.IsEnabled = False
                    winFac.btnEliminar.IsEnabled = False
                    winFac.btnGuardarFactura.IsEnabled = False
                    winFac.txtCodigo.IsEnabled = False
                    winFac.txtcodigoPro.IsEnabled = False
                    winFac.NuevoCliente.IsEnabled = False
                    winFac.txtcantid.IsEnabled = False
                    winFac.comboBoxPago.IsEnabled = False
                    winFac.comboProvincias.IsEnabled = False
                    winFac.txtCedula.IsEnabled = False
                    winFac.txtSubt.IsEnabled = False
                    winFac.txtNfactura.IsEnabled = False
                    winFac.txtFecha.IsEnabled = False
                    winFac.Owner = Me
                    winFac.salir.IsEnabled = True
                    winFac.txtNombre.Text = em(2)
                    winFac.txtApellido.Text = em(3)
                    winFac.txtCedula.Text = em(5)
                    winFac.Show()
                    Me.Hide()
                    winFac.txtNfactura.Text = em(1)
                    winFac.comboBoxPago.SelectedItem = em(6)
                    winFac.comboProvincias.SelectedItem = em(7)
                    winFac.txtFecha.Text = em(9)
                    winFac.txttotal.Text = em(10)
                    winFac.txtdevolucion.Text = em(11)
                    Me.llenarDataGridFactura()
                    Exit For
                End If
            Next
        End Using





    End Sub

    Public Sub llenarDataGridFactura()
        Using conexion As New OleDbConnection(strConexion)

            Dim consulta As String = "SELECT codProd , Descripcion , Punitario , Cantidad , Subtotal FROM Proxfactura WHERE Nfactura='" & winFac.txtNfactura.Text & "'"
            Dim adapter As New OleDbDataAdapter(New OleDbCommand(consulta, conexion))
            Dim personaCmdBuilder = New OleDbCommandBuilder(adapter)
            Dim dsProductos = New DataSet("Tienda")
            adapter.Fill(dsProductos, "Factura")

            winFac.DGdetalle.DataContext = dsProductos
        End Using
    End Sub
End Class
