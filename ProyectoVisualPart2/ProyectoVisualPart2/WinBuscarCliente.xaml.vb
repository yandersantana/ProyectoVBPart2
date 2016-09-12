Imports System.Data
Imports System.Data.OleDb

Public Class WinBuscarCliente
    Private strPath = "..\..\dataBaseVisual.mdb"
    Private strConexion As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & strPath
    'Private strConexion As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & strPath
    Public inf As String

    Private Sub btnBusc_Click(sender As Object, e As RoutedEventArgs) Handles btnBusc.Click
        If (comboBoxBuscarPor.SelectedItem = "Cedula") Then
            inf = "SELECT * FROM cliente WHERE Cedula='" & txtInf.Text & "'"
        ElseIf (comboBoxBuscarPor.SelectedItem = "Codigo") Then
            inf = "SELECT * FROM cliente WHERE Id='" & txtInf.Text & "'"
        End If

        Using dbConexion As New OleDbConnection(strConexion) 'entrar y salir de la base
            Dim strQuery As String = inf
            Dim dbAdapter As New OleDbDataAdapter(strQuery, strConexion)
            Dim dsMaster As New DataSet("Datos")
            dbAdapter.Fill(dsMaster, "Cliente")
            dtgInf.DataContext = dsMaster

        End Using
    End Sub

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        comboBoxBuscarPor.Items.Add("Cedula")
        comboBoxBuscarPor.Items.Add("Codigo")
        comboBoxBuscarPor.SelectedItem = "Codigo"
    End Sub


End Class
