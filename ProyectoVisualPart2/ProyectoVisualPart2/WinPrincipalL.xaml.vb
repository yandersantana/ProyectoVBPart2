Class WinPrincipalL
    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)

    End Sub

    Private Sub btn_aceptar_Click(sender As Object, e As RoutedEventArgs) Handles btn_aceptar.Click
        Dim winAd As New winAdministrador
        winAd.Owner = Me
        winAd.Show()
        Me.Hide()
    End Sub
End Class
