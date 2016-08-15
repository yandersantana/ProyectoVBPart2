Public Class Cliente
    Inherits Persona
    Private _tipo As String
    Public Property Tipo() As String
        Get
            Return _tipo
        End Get
        Set(ByVal value As String)
            _tipo = value
        End Set
    End Property

    Public Sub New()

    End Sub
    Public Sub New(nombre As String, apellido As String)
        Me.Nombre = nombre
        Me.Apellido = apellido
    End Sub

    Public Sub New(nombre As String, apellido As String, edad As Short, email As String, telefono As String, genero As String, cedula As String, tipo As String)
        MyBase.New(nombre, apellido, edad, email, telefono, genero, cedula)
        Me.Tipo = tipo
    End Sub

End Class
