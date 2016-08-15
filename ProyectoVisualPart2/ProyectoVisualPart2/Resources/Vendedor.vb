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

    Sub New(raiz As XmlNode)
        Dim arrVendedores As New ArrayList
        Dim nombre1, apellido1, email1, telefono1, genero1, cedula1, id1, fechaContrato1, contacto1, usuario1, contraseña1 As String
        Dim edad1 As Integer
        Dim ven As Vendedor
        Dim i As Integer = 1

        For Each nodo As XmlNode In raiz.ChildNodes
            If nodo.Name = "Vendedor" Then
                For Each atributo As XmlNode In nodo.ChildNodes
                    Select Case atributo.Name
                        Case "Nombre"
                            nombre1 = atributo.InnerText
                        Case "Apellido"
                            apellido1 = atributo.InnerText
                        Case "Edad"
                            edad1 = atributo.InnerText
                        Case "Email"
                            email1 = atributo.InnerText
                        Case "Telefono"
                            telefono1 = atributo.InnerText
                        Case "Genero"
                            genero1 = atributo.InnerText
                        Case "Cedula"
                            cedula1 = atributo.InnerText
                        Case "Usuario"
                            usuario1 = atributo.InnerText
                        Case "Contraseña"
                            contraseña1 = atributo.InnerText
                        Case "Id"
                            id1 = atributo.InnerText
                        Case "FechaContrato"
                            fechaContrato1 = atributo.InnerText
                        Case "Contacto"
                            contacto1 = atributo.InnerText
                    End Select
                Next
                ven = New Vendedor(nombre1, apellido1, edad1, email1, telefono1, genero1, cedula1, usuario1, contraseña1, id1, fechaContrato1, contacto1)
                arrVendedores.Add(ven)
            End If


        Next

        For Each vend As Vendedor In arrVendedores
            Console.WriteLine(vend)
            Console.WriteLine(" ")
        Next
    End Sub


    Public Function agregarVendedor(xmldoc As XmlDocument)
        Dim vendedor As XmlElement = xmldoc.CreateElement("Vendedor")
        Dim nombre As XmlElement = xmldoc.CreateElement("Nombre")
        Dim apellido As XmlElement = xmldoc.CreateElement("Apellido")
        Dim edad As XmlElement = xmldoc.CreateElement("Edad")
        Dim email As XmlElement = xmldoc.CreateElement("Email")
        Dim telefono As XmlElement = xmldoc.CreateElement("Telefono")
        Dim genero As XmlElement = xmldoc.CreateElement("Genero")
        Dim cedula As XmlElement = xmldoc.CreateElement("Cedula")
        Dim usuario As XmlElement = xmldoc.CreateElement("Usuario")
        Dim contraseña As XmlElement = xmldoc.CreateElement("Contraseña")
        Dim id As XmlElement = xmldoc.CreateElement("Id")
        Dim fechaContrato As XmlElement = xmldoc.CreateElement("FechaContrato")
        Dim contacto As XmlElement = xmldoc.CreateElement("Contacto")

        nombre.InnerText = MyBase.Nombre
        apellido.InnerText = MyBase.Apellido
        edad.InnerText = MyBase.Edad
        email.InnerText = MyBase.Email
        telefono.InnerText = Me.Telefono
        genero.InnerText = Me.Genero
        cedula.InnerText = MyBase.CedulaIdentidad
        usuario.InnerText = Me.Usuario
        contraseña.InnerText = Me.Contraseña
        id.InnerText = Me.Id
        fechaContrato.InnerText = Me.FechaDeContrato
        contacto.InnerText = Me.Contacto

        vendedor.AppendChild(nombre)
        vendedor.AppendChild(apellido)
        vendedor.AppendChild(edad)
        vendedor.AppendChild(email)
        vendedor.AppendChild(telefono)
        vendedor.AppendChild(genero)
        vendedor.AppendChild(cedula)
        vendedor.AppendChild(usuario)
        vendedor.AppendChild(contraseña)
        vendedor.AppendChild(id)
        vendedor.AppendChild(fechaContrato)
        vendedor.AppendChild(contacto)
        Return vendedor
    End Function


End Class
