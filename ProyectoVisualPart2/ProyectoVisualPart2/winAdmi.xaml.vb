Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Drawing.Imaging

Public Class winAdmi
    Private strPath = "..\..\dataBaseVisual.mdb"
    'Private strConexion As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & strPath
    Private strConexion As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & strPath
    Public persona As Persona



    Private Sub registrar_vendedor_Click(sender As Object, e As RoutedEventArgs) Handles registrar_vendedor.Click
        Dim winNewVend As New winNewVendedor
        winNewVend.Owner = Me
        winNewVend.Show()
    End Sub

    Private Sub añadir_Producto_Click(sender As Object, e As RoutedEventArgs) Handles añadir_Producto.Click
        Dim winNewPro As New WinAggProducto
        winNewPro.Owner = Me
        winNewPro.Show()
    End Sub

    Private Sub listar_Vendedores_Click(sender As Object, e As RoutedEventArgs) Handles listar_Vendedores.Click
        Dim winNewlv As New winListaVendedores
        winNewlv.Owner = Me
        winNewlv.Show()
    End Sub

    Private Sub listar_Producto_Click(sender As Object, e As RoutedEventArgs) Handles listar_Producto.Click
        Dim winListarProducto As New winListaProductos
        winListarProducto.Owner = Me
        winListarProducto.Show()
    End Sub

    Private Sub buscar_Facturas_Click(sender As Object, e As RoutedEventArgs) Handles buscar_Facturas.Click
        Dim winNewl As New winListaFacturas
        winNewl.Owner = Me
        winNewl.Show()
    End Sub

    Private Sub cerrar_Click(sender As Object, e As RoutedEventArgs) Handles cerrar.Click
        Me.Close()
        Me.Owner.Show()
    End Sub



    Private Sub winAdmi_Loaded(sender As Object, e As RoutedEventArgs) Handles MyBase.Loaded, MyBase.Loaded

        Using dbConexion As New OleDbConnection(strConexion) 'entrar y salir de la base
            Dim strQuery As String = "SELECT * FROM usuarios"
            Dim dbAdapter As New OleDbDataAdapter(strQuery, strConexion)
            Dim dsMaster As New DataSet("Datos")
            dbAdapter.Fill(dsMaster, "Empleado")

            For Each em As DataRow In dsMaster.Tables("Empleado").Rows
                If (em(8) = persona.Nombre) Then
                    txtNombre.Text = em(1)
                    txtApellido.Text = em(2)
                    txtEdad.Text = em(3)
                    txtCorreo.Text = em(4)


                End If



            Next
        End Using

    End Sub
End Class
