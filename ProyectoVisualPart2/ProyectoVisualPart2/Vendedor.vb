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


    Private _fechaContrato As String
    Public Property FechaContrato() As String
        Get
            Return _fechaContrato
        End Get
        Set(value As String)
            _fechaContrato = value
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


    Public Sub New(id As String, nombre As String, apellido As String, edad As String, email As String, telefono As String, genero As String, cedula As String, usuario As String, contraseña As String, fechaContrato As String, contacto As String)
        MyBase.New(id, nombre, apellido, edad, email, telefono, genero, cedula)
        Me.Usuario = usuario
        Me.Contraseña = contraseña
        Me.FechaContrato = fechaContrato
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
        Return MyBase.toString() & "   Id:  " & Id & "    Fecha de contrato:     " & FechaContrato & "    Contacto:    " & Contacto
    End Function






End Class
