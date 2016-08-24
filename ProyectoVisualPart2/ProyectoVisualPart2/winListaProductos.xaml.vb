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

    Private Sub salir_Click(sender As Object, e As RoutedEventArgs) Handles salir.Click
        Me.Close()
    End Sub
End Class
