Public Class winListaFacturas
    Private Sub buscar_Click(sender As Object, e As RoutedEventArgs) Handles buscar.Click
        Dim winvendedor As New winFactura
        winvendedor.Owner = Me
        winvendedor.IsEnabled = False
        winvendedor.Show()
        Me.Hide()
    End Sub
End Class
