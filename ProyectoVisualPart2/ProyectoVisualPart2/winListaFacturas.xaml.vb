Imports System.Data
Imports System.Data.OleDb

Public Class winListaFacturas
    Private strPath = "..\..\dataBaseVisual.mdb"
    Private strConexion As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & strPath
    Private Sub buscar_Click(sender As Object, e As RoutedEventArgs) Handles buscar.Click

        Dim existe As Boolean = False
        Dim result = Convert.ToInt32(codigoBuscar.Text)
        Using dbConexion As New OleDbConnection(strConexion) 'entrar y salir de la base
            Dim strQuery As String = "SELECT * FROM Facturas"
            Dim dbAdapter As New OleDbDataAdapter(strQuery, strConexion)
            Dim dsMaster As New DataSet("Datos")
            dbAdapter.Fill(dsMaster, "Facturas")

            For Each em As DataRow In dsMaster.Tables("Facturas").Rows
                If (em(0) = result) Then
                    Dim winFac As New winFactura
                    winFac.IsEnabled = False
                    winFac.Owner = Me
                    winFac.txtNfactura.Text = em(1)
                    winFac.txtNombre.Text = em(2)
                    winFac.txtApellido.Text = em(3)
                    winFac.txtCedula.Text = em(5)
                    winFac.txtVendedor.Text = em(7)
                    winFac.txtFecha.Text = em(8)
                    winFac.Show()
                    Me.Hide()
                    existe = True

                    Exit For
                End If

            Next

        End Using
        If Not existe Then
            MessageBox.Show("Código Incorrecto")
        End If
    End Sub
End Class
