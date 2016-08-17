Imports System.Data.OleDb

Module Module1

    Sub Main()
        Dim dbPath = "C:\Users\ESTUDIANTE\Documents\visual\sample.mdb"
        Dim strConexion = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" & dbPath
        'Dim dbConexion As New OleDbConnection(strConexion)

        'dbConexion.Open()
        'Console.WriteLine("Conexion exitosa")
        'dbConexion.Close()

        Using dbConexion As New OleDbConnection(strConexion)
            Console.WriteLine("conexion exitosa")
            Dim strQuery As String = "SELECT * FROM tbl_master"
            Dim dbAdapter As New OleDbDataAdapter(strQuery, dbConexion)


            Dim dsMaster As New DataSet("Datos")
            dbAdapter.Fill(dsMaster, "Empleado")

            For Each empleado As DataRow In dsMaster.Tables("Empleado").Rows
                Console.WriteLine("Id: " & empleado("EmployeeId") & " - Nombre: " & empleado(1))
            Next

            Console.WriteLine("Hay " & dsMaster.Tables("Empleado").Rows.Count & " empleados")

        End Using

        Console.ReadLine()


    End Sub

End Module
