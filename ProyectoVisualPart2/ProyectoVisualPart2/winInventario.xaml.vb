Imports System.Data
Imports System.Data.OleDb

Public Class winInventario
    Private strPath = "..\..\dataBaseVisual.mdb"
    Private strConexion As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & strPath
    Private Sub salir_Click(sender As Object, e As RoutedEventArgs) Handles salir.Click
        Me.Close()
        Me.Owner.Show()
    End Sub

    Private Sub winInventar_Loaded(sender As Object, e As RoutedEventArgs) Handles winInventar.Loaded
        Dim total As Double = 0
        Using dbConexion As New OleDbConnection(strConexion) 'entrar y salir de la base
            Dim strQuery As String = "SELECT * FROM Facturas"
            Dim dbAdapter As New OleDbDataAdapter(strQuery, strConexion)
            Dim dsMaster As New DataSet("Datos")
            dbAdapter.Fill(dsMaster, "Facturas")

            For Each em As DataRow In dsMaster.Tables("Facturas").Rows
                total = Convert.ToDouble(em(10)) + total
                textTotalVentas.Text = total


            Next

        End Using
    End Sub
End Class
