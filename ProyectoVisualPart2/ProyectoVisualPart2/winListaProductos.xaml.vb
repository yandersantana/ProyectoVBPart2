Imports System.Data
Imports System.Data.OleDb

Public Class winListaProductos
    Private dbPath = "ruta"
    Private strConexion = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" & dbPath
    Private dsProd As DataSet
    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        Using dbConexion As New OleDbConnection(strConexion)
            Dim consulta = "SELECT * FROM tbl_productos"
            Dim AdProducto As New OleDbDataAdapter(New OleDbCommand(consulta, strConexion))

            'Dim dsProducto As New winProducto
            'AdProducto.Fill(dsProducto, "Productos")
            'dtgListadoProductos.DataContext = dsProducto


        End Using

    End Sub

    Private Sub dtgListadoProductos_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles dtgListadoProductos.SelectionChanged
        Dim fila As DataRow = sender.selectedItem
        'Dim productos As New winProductos
        'productos.owner=Me 
        Dim unProducto As New Producto(fila(0), fila(1), fila(2), fila(3))
        'productos.DataContext()=unProducto
        'productos.show()
        Me.Hide()


    End Sub
End Class
