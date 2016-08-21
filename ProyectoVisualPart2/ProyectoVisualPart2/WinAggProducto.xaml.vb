Imports System.Data.OleDb

Public Class WinAggProducto
    Private strPath = "..\..\dataBaseVisual.mdb"
    Private strConexion As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & strPath
    Private Sub aceptar_Click(sender As Object, e As RoutedEventArgs) Handles aceptar.Click
        If (txtPrecioUnitario.Text = "" And textNombreProducto.Text = "") Then
            MessageBox.Show("Campos Vacios")
        Else
            Using conexion As New OleDbConnection(strConexion)
                conexion.Open()
                Dim Insertar As String
                Insertar = "INSERT INTO producto ( [nombre], [Punitario], [iva]) values ( textNombreProducto.Text,txtPrecioUnitario.Text,'False')"
                Dim cmd As OleDbCommand = New OleDbCommand(Insertar, conexion)
                cmd.Parameters.Add(New OleDbParameter("nombre", CType(textNombreProducto.Text, String)))
                cmd.Parameters.Add(New OleDbParameter("pUnitario", CType(txtPrecioUnitario.Text, String)))
                If (IvaSI.IsChecked) Then
                    cmd.Parameters.Add(New OleDbParameter("iva", CType("TRUE", String)))
                Else
                    cmd.Parameters.Add(New OleDbParameter("iva", CType("FALSE", String)))
                End If


                cmd.ExecuteNonQuery()




            End Using
            Me.Close()
        End If

    End Sub

    Private Sub cancelar_Click(sender As Object, e As RoutedEventArgs) Handles cancelar.Click
        Me.Close()
    End Sub
End Class
