Public Class winAdmi
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

    Private Sub winAdmi1_Closing(sender As Object, e As ComponentModel.CancelEventArgs) Handles MyBase1.Closing, MyBase1.Closing
        Me.Owner.Show()
    End Sub
End Class
