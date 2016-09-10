Imports System.Data
Imports System.Data.OleDb

Public Class winBuscarFactura
    Private strPath = "..\..\dataBaseVisual.mdb"
    Private strConexion As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & strPath
    'Private strConexion As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & strPath
    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        comboBoxtipoOlimp.Items.Add("codigo")
        comboBoxtipoOlimp.Items.Add("fecha")
        comboBoxtipoOlimp.Items.Add("cliente")
    End Sub

    Private Sub button_Click(sender As Object, e As RoutedEventArgs) Handles button.Click
        Dim factBusqueda As String
        Dim winNew As New winBuscarFactura
        If (comboBoxtipoOlimp.Text = "codigo") Then
            factBusqueda = "SELECT * FROM Facturas WHERE Nfactura='" + txtDato.Text + "'"
        ElseIf (comboBoxtipoOlimp.Text = "fecha") Then
            factBusqueda = "SELECT * FROM Facturas WHERE Fecha='" & txtDato.Text & "'"
        ElseIf (comboBoxtipoOlimp.Text = "cliente") Then
            factBusqueda = "SELECT * FROM Facturas WHERE CodCliente='" & txtDato.Text & "'"
        End If
        Using dbconexion As New OleDbConnection(strConexion)
            Dim strQuery As String = factBusqueda
            Dim dbAdapter As New OleDbDataAdapter(strQuery, strConexion)
            Dim dsMaster As New DataSet("Datos")
            dbAdapter.Fill(dsMaster, "Productos")
            dtResult.DataContext = dsMaster
            winNew.UpdateDataGrid()
        End Using

    End Sub

    Public Sub UpdateDataGrid()
        Me.Window_Loaded(Nothing, Nothing)
    End Sub
End Class
