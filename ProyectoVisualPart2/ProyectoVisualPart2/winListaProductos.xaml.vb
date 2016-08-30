Imports System.Data
Imports System.Data.OleDb

Public Class winListaProductos
    Private strPath = "..\..\dataBaseVisual.mdb"
    Private strConexion = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & strPath
    Private dsProd As DataSet
    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        Using dbConexion As New OleDbConnection(strConexion) 'entrar y salir de la base
            Console.WriteLine("Conexion Exitosa")
            Dim strQuery As String = "SELECT * FROM producto"
            Dim dbAdapter As New OleDbDataAdapter(strQuery, dbConexion)
            Dim dsMaster As New DataSet("Productos")
            dbAdapter.Fill(dsMaster, "producto")
            dtgProducto.DataContext = dsMaster

        End Using
        Console.ReadLine()

    End Sub
    Private Sub dtgProducto_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles dtgProducto.SelectionChanged
        ' Dim fila As DataRow = sender.selectedItem
        Dim pro As New WinAggProducto
        pro.Owner = Me
        'Dim unVendedor As New Vendedor(fila(0), fila(1), fila(2), fila(3), fila(4), fila(5), fila(6), fila(7), fila(8), fila(9), fila(10), fila(11))
        'newvendedor.DataContext = unVendedor
        pro.IsEnabled = False

        pro.Show()
        Me.Hide()


    End Sub

    Private Sub salir_Click(sender As Object, e As RoutedEventArgs) Handles salir.Click
        Me.Close()
    End Sub
End Class
