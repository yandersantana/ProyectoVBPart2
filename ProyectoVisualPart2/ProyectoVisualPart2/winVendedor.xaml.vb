Public Class winVendedor
    Private Sub newFac_Click(sender As Object, e As RoutedEventArgs) Handles newFac.Click
        Dim winFac As New winFactura
        winFac.Owner = Me
        winFac.Show()
        Me.Hide()
    End Sub

    Private Sub MenuItem_Click(sender As Object, e As RoutedEventArgs)
        Me.Close()
        Me.Owner.Show()
    End Sub
End Class
