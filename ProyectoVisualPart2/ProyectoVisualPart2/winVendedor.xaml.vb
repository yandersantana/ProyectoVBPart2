Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Drawing.Imaging

Public Class winVendedor
    Private strPath = "..\..\dataBaseVisual.mdb"
    Private strConexion As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & strPath
    'Private strConexion As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & strPath
    Public persona2 As New Persona
    Public Shared nomVend

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

    Private Sub winVendedor_Loaded(sender As Object, e As RoutedEventArgs) Handles MyBase.Loaded, MyBase.Loaded
        Using dbconexion As New OleDbConnection(strConexion)
            Dim consulta As String = "Select * FROM usuarios"
            Dim adapter As New OleDbDataAdapter(consulta, strConexion)
            Dim dsUsuarios As New DataSet("Datos2")
            adapter.Fill(dsUsuarios, "Empleado2")

            For Each user As DataRow In dsUsuarios.Tables("Empleado2").Rows
                If (user(8).ToString = persona2.Nombre) Then
                    txtNombre.Text = user(1)
                    txtApellido.Text = user(2)
                    txtEdad.Text = user(3)

                End If

            Next
        End Using
        nomVend = txtNombre.Text
    End Sub

    Private Sub MenuItem_Click_1(sender As Object, e As RoutedEventArgs)
        Dim searchFact As New winBuscarFactura
        searchFact.Show()
    End Sub
End Class
