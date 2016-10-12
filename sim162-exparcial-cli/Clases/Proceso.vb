Public Class Proceso

#Region " Constructors "

    Public Sub New()
    End Sub

    Public Sub New(nombre As String, tamaño As Integer, estado As EstadoProceso, tiempo As Integer)

        Me.Nombre = nombre
        Me.Tamaño = tamaño
        Me.Estado = estado
        Me.Duracion = tiempo
        Me.TimeLeft = New TimeSpan(tiempo)

    End Sub

#End Region

#Region " Properties "

    Private _Nombre As String
    Private _Tamaño As Integer
    Private _Estado As EstadoProceso
    Private _Duracion As Integer
    Private _HoraInicio As DateTime
    Private _HoraFin As DateTime
    Private _TiempoEspera As Integer
    Private _DireccionMemoria As Integer
    Private _TimeLeft As TimeSpan

    Public Property TimeLeft() As TimeSpan
        Get
            Return _TimeLeft
        End Get
        Set(ByVal value As TimeSpan)
            _TimeLeft = value
        End Set
    End Property

    Public Property Nombre() As String
        Get
            Return _Nombre
        End Get
        Set(ByVal value As String)
            _Nombre = value
        End Set
    End Property

    Public Property Tamaño() As Integer
        Get
            Return _Tamaño
        End Get
        Set(ByVal value As Integer)
            _Tamaño = value
        End Set
    End Property

    Public Property Estado() As EstadoProceso
        Get
            Return _Estado
        End Get
        Set(ByVal value As EstadoProceso)
            _Estado = value
        End Set
    End Property

    Public Property Duracion() As Integer
        Get
            Return _Duracion
        End Get
        Set(ByVal value As Integer)
            _Duracion = value
        End Set
    End Property

    Public Property TiempoEspera() As Integer
        Get
            Return _TiempoEspera
        End Get
        Set(ByVal value As Integer)
            _TiempoEspera = value
        End Set
    End Property

    Public Property HoraFin() As DateTime
        Get
            Return _HoraFin
        End Get
        Set(ByVal value As DateTime)
            _HoraFin = value
        End Set
    End Property

    Public Property HoraInicio() As DateTime
        Get
            Return _HoraInicio
        End Get
        Set(ByVal value As DateTime)
            _HoraInicio = value
        End Set
    End Property

    Public Property DireccionMemoria() As Integer
        Get
            Return _DireccionMemoria
        End Get
        Set(ByVal value As Integer)
            _DireccionMemoria = value
        End Set
    End Property

#End Region

End Class
