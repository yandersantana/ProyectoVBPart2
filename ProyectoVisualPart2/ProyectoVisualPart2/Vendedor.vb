Imports System.Xml

Public Class Vendedor
    Inherits Persona
    Private _usuario As String
    Public Property Usuario() As String
        Get
            Return _usuario
        End Get
        Set(ByVal value As String)
            _usuario = value
        End Set
    End Property

    Private _contraseña As String
    Public Property Contraseña() As String
        Get
            Return _contraseña
        End Get
        Set(ByVal value As String)
            _contraseña = value
        End Set
    End Property
    Private _id As String

    Public Property Id As String
        Get
            Return _id
        End Get
        Set(value As String)
            _id = value
        End Set
    End Property
    Private _fechaDeContrato As String
    Public Property FechaDeContrato As String
        Get
            Return _fechaDeContrato
        End Get
        Set(value As String)
            _fechaDeContrato = value
        End Set
    End Property

    Private _contacto As String

    Public Property Contacto As String
        Get
            Return _contacto
        End Get
        Set(value As String)
            _contacto = value
        End Set
    End Property


    Public Sub New(nombre As String, apellido As String, edad As Integer, email As String, telefono As String, genero As String, cedula As String, usuario As String, contraseña As String, id As String, fechaContrato As String, contacto As String)
        MyBase.New(nombre, apellido, edad, email, telefono, genero, cedula)
        Me.Usuario = usuario
        Me.Contraseña = contraseña
        Me.Id = id
        Me.FechaDeContrato = fechaContrato
        Me.Contacto = contacto

    End Sub

    Public Sub New(usuario As String, contraseña As String)
        MyBase.New()
        Me.Usuario = usuario
        Me.Contraseña = contraseña
    End Sub

    Public Sub New(nombre As String)
        Me.Nombre = nombre
    End Sub


    Public Overrides Function toString() As String
        Return MyBase.toString() & "   Id:  " & Id & "    Fecha de contrato:     " & FechaDeContrato & "    Contacto:    " & Contacto
    End Function






End Class
