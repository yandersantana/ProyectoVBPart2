Imports System.Data
Imports System.Data.OleDb

Public Class WinAggProducto
    Private strPath = "..\..\dataBaseVisual.mdb"
    Private strConexion As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & strPath
    'Private strConexion As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & strPath

    Private Sub btnNuevo_Click(sender As Object, e As RoutedEventArgs) Handles btnNuevo.Click
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

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)

    End Sub

    Private Sub btn_actualizar_Click(sender As Object, e As RoutedEventArgs) Handles btn_actualizar.Click
        Using conexion As New OleDbConnection(strConexion)
            Dim consulta As String = "Select * FROM  producto"

            Dim adapter As New OleDbDataAdapter(New OleDbCommand(consulta, conexion))
            Dim productoCmBuilder As New OleDbCommandBuilder(adapter)
            Dim dsproducto = New DataSet("Tienda")
            adapter.Fill(dsproducto, "Producto")
            Dim found = False

            For Each prod As DataRow In dsproducto.Tables("producto").Rows
                If prod("id") = Me.txtCodigo.Text Then
                    prod("nombre") = Me.textNombreProducto.Text
                    prod("pUnitario") = Me.txtPrecioUnitario.Text
                    prod("iva") = Me.IvaSI.IsChecked
                    'If (IvaSI.IsChecked) Then
                    '    productoCmBuilder.Parameters.Add(New OleDbParameter("iva", CType("TRUE", String)))
                    'Else
                    '    productoCmBuilder.Parameters.Add(New OleDbParameter("iva", CType("FALSE", String)))
                    'End If
                    Exit For
                End If
            Next

            Try
                adapter.Update(dsproducto.Tables("producto"))
                MessageBox.Show("Se actualizo correctamente el producto")
            Catch ex As Exception
                MessageBox.Show("Error al guardar")
            End Try


        End Using
    End Sub
End Class
