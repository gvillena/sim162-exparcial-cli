Public Class Student

    Private _Id As Integer
    Private _Name As String
    Private _Grade As Double

    Public Sub New()
        _Id = 0
        _Name = String.Empty
        _Grade = 0
    End Sub

    Public Property Id() As Integer
        Get
            Return _Id
        End Get
        Set(ByVal value As Integer)
            _Id = value
        End Set
    End Property

    Public Property Name() As String
        Get
            Return _Name
        End Get
        Set(ByVal value As String)
            _Name = value
        End Set
    End Property

    Public Property Grade() As Double
        Get
            Return _Grade
        End Get
        Set(ByVal value As Double)
            _Grade = value
        End Set
    End Property



End Class
