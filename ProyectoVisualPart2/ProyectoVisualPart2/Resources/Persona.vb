Public Class Persona
    Private _nombre As String
    Public Property Nombre() As String
        Get
            Return _nombre
        End Get
        Set(ByVal value As String)
            _nombre = value
        End Set
    End Property

    Private _apellido As String
    Public Property Apellido() As String
        Get
            Return _apellido
        End Get
        Set(ByVal value As String)
            _apellido = value
        End Set
    End Property


    Private _edad As Integer
    Public Property Edad() As Integer
        Get
            Return _edad
        End Get
        Set(ByVal value As Integer)
            _edad = value
        End Set
    End Property


    Private _email As String
    Public Property Email As String
        Get
            Return _email
        End Get
        Set(value As String)
            _email = value
        End Set
    End Property
    Private _telefono As String
    Public Property Telefono As String
        Get
            Return _telefono
        End Get
        Set(value As String)
            _telefono = value
        End Set
    End Property
    Private _genero As String
    Public Property Genero As String
        Get
            Return _genero
        End Get
        Set(value As String)
            _genero = value
        End Set
    End Property
    Private _cedulaIdentidad As String

    Public Property CedulaIdentidad As String
        Get
            Return _cedulaIdentidad
        End Get
        Set(value As String)
            _cedulaIdentidad = value
        End Set
    End Property

    Sub New(nombre As String, apellido As String, edad As Integer, email As String, telefono As String, genero As String, cedula As String)
        Me.Nombre = nombre
        Me.Apellido = apellido
        Me.Edad = edad
        Me.Email = email
        Me.Telefono = telefono
        Me.Genero = genero
        Me.CedulaIdentidad = cedula
    End Sub

    Sub New()

    End Sub

    Public Overrides Function toString() As String
        Return "Nombre:  " & Nombre & "    Apellido:    " & Apellido & "    Edad:     " & Edad & "    Email:    " & Email & "    Telefono:    " & Telefono & "    Genero:   " & Genero &
            "    Cedula:    " & CedulaIdentidad
    End Function




End Class
