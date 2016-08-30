Imports System.Data
Imports System.Data.OleDb

Public Class winListaVendedores
    Private dbPath = "..\..\dataBaseVisual.mdb"
    Private strConexion = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & dbPath
    Private dsVendedores As DataSet
    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        Using dbConexion As New OleDbConnection(strConexion)
            Dim consulta As String = "SELECT * FROM usuarios"
            Dim AdVendedores As New OleDbDataAdapter(New OleDbCommand(consulta, dbConexion))

            Dim dsLisVendedores = New DataSet("Vendedores")
            AdVendedores.Fill(dsLisVendedores, "usuarios")
            dtgListadoVendedores.DataContext = dsLisVendedores

        End Using

    End Sub

    Private Sub dtgListadoVendedores_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles dtgListadoVendedores.SelectionChanged
        ' Dim fila As DataRow = sender.selectedItem
        Dim newvendedor As New winNewVendedor
        newvendedor.Owner = Me
        'Dim unVendedor As New Vendedor(fila(0), fila(1), fila(2), fila(3), fila(4), fila(5), fila(6), fila(7), fila(8), fila(9), fila(10), fila(11))
        'newvendedor.DataContext = unVendedor
        newvendedor.IsEnabled = False

        newvendedor.Show()
        Me.Hide()


    End Sub

    Private Sub salir_Click(sender As Object, e As RoutedEventArgs) Handles salir.Click
        Me.Close()
    End Sub


End Class
