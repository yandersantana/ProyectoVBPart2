﻿Public Class winAdministrador
    Private Sub btnListaProductos_Click(sender As Object, e As RoutedEventArgs) Handles btnListaProductos.Click
        Dim winListarProducto As New winListaProductos
        winListarProducto.Owner = Me
        winListarProducto.Show()
    End Sub

    Private Sub Window_Closing(sender As Object, e As ComponentModel.CancelEventArgs)

        Me.Owner.Show()

    End Sub

    Private Sub btnNewVendedor_Click(sender As Object, e As RoutedEventArgs) Handles btnNewVendedor.Click
        Dim winNewVend As New winNewVendedor
        winNewVend.Owner = Me
        winNewVend.Show()



    End Sub

    Private Sub btnNewProducto_Click(sender As Object, e As RoutedEventArgs) Handles btnNewProducto.Click
        Dim winNewPro As New WinAggProducto
        winNewPro.Owner = Me
        winNewPro.Show()
    End Sub
End Class
