Imports System.Data
Imports System.Data.OleDb

Public Class winBuscarFactura
    Private strPath = "..\..\dataBaseVisual.mdb"
    Private strConexion As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & strPath
    Public winFac As winFactura
    Public num As Integer
    Private Sub salir_Click(sender As Object, e As RoutedEventArgs) Handles salir.Click
        Me.Close()
        Me.Owner.Show()
    End Sub

    Private Sub codFac_KeyDown(sender As Object, e As KeyEventArgs) Handles codFac.KeyDown
        If e.Key = Key.Enter Then
            Dim existe As Boolean = False
            Dim esOnoEsEnterio As Boolean
            esOnoEsEnterio = IsNumeric(codFac.Text)
            If (esOnoEsEnterio) Then
                Dim result = Convert.ToInt32(codFac.Text)
                Using dbConexion As New OleDbConnection(strConexion) 'entrar y salir de la base
                    Dim strQuery As String = "SELECT * FROM Facturas"
                    Dim dbAdapter As New OleDbDataAdapter(strQuery, strConexion)
                    Dim dsMaster As New DataSet("Datos")
                    dbAdapter.Fill(dsMaster, "Facturas")

                    For Each em As DataRow In dsMaster.Tables("Facturas").Rows
                        If (em(1) = result) Then
                            num = Convert.ToUInt32(em(0).ToString)
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


                            Me.llenarDataGridFactura()
                            winFac.Show()
                            Me.Hide()
                            existe = True
                            winFac.txtNfactura.Text = em(1)
                            winFac.comboBoxPago.SelectedItem = em(6)
                            winFac.comboProvincias.SelectedItem = em(7)
                            winFac.txtFecha.Text = em(9)
                            winFac.txttotal.Text = em(10)
                            winFac.txtdevolucion.Text = em(11)
                            Exit For
                        End If

                    Next

                End Using
                If Not existe Then
                    MessageBox.Show("Código Incorrecto")
                End If
            Else
                MessageBox.Show("ingrese un dato Válido")
            End If

        End If
    End Sub

    Public Sub llenarDataGridFactura()
        Using conexion As New OleDbConnection(strConexion)

            Dim consulta As String = "SELECT codProd , Descripcion , Punitario , Cantidad , Subtotal FROM Proxfactura WHERE Nfactura='" & num.ToString & "'"
            Dim adapter As New OleDbDataAdapter(New OleDbCommand(consulta, conexion))
            Dim personaCmdBuilder = New OleDbCommandBuilder(adapter)
            Dim dsProductos = New DataSet("Tienda")
            adapter.Fill(dsProductos, "Factura")

            winFac.DGdetalle.DataContext = dsProductos
        End Using
    End Sub
    Public Function validarDatosnumerico() As Integer
        Dim numero As Integer = 0
        Dim aux As Object = 0
        Dim bol As Boolean = True
        Do While True
            aux = Console.ReadLine()
            bol = IsNumeric(aux)
            If bol Then
                numero = aux
                Return numero
            Else
                Console.WriteLine("-----Error Vuelva a Ingresar-----")
            End If
        Loop
        numero = aux
        Return numero
    End Function

    Private Sub button_Click(sender As Object, e As RoutedEventArgs) Handles button.Click
        Dim winBusAvan As New winBuscarFacturaXmetAvan
        winBusAvan.Show()

    End Sub
End Class
