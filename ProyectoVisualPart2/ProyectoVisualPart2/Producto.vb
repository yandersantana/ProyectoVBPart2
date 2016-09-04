Public Class Producto
    Private _cantidad As Integer
    Private _totalPro As Double
    Private _codigo As String
    Public Property Codigo() As String
        Get
            Return _codigo
        End Get
        Set(ByVal value As String)
            _codigo = value
        End Set
    End Property

    Private _nombreProducto As String
    Public Property NombreProducto() As String
        Get
            Return _nombreProducto
        End Get
        Set(ByVal value As String)
            _nombreProducto = value
        End Set
    End Property

    Private _precioUnitario As Double
    Public Property PrecioUnitario() As Double
        Get
            Return _precioUnitario
        End Get
        Set(ByVal value As Double)
            _precioUnitario = value
        End Set
    End Property
    Private _registraIva As String





    Public Property TotalPro As Double
        Get
            Return _totalPro
        End Get
        Set(value As Double)
            _totalPro = value
        End Set
    End Property

    Public Property Cantidad As Integer
        Get
            Return _cantidad
        End Get
        Set(value As Integer)
            _cantidad = value
        End Set
    End Property

    Public Property RegistraIva As String
        Get
            Return _registraIva
        End Get
        Set(value As String)
            _registraIva = value
        End Set
    End Property

    Public Sub New()

    End Sub


    Public Sub New(codigo As String, nombreProducto As String, precioUnitario As Double, registraIva As Boolean)
        Me.Codigo = codigo
        Me.NombreProducto = nombreProducto
        Me.PrecioUnitario = precioUnitario
        Me.RegistraIva = registraIva

    End Sub

End Class
