Public Class Alternative

    Private _Id As Integer
    Public Property Id() As Integer
        Get
            Return _Id
        End Get
        Set(ByVal value As Integer)
            _Id = value
        End Set
    End Property

    Private _Name As String
    Public Property Name() As String
        Get
            Return _Name
        End Get
        Set(ByVal value As String)
            _Name = value
        End Set
    End Property

    Private _IsCorrect As Boolean
    Public Property IsCorrect() As Boolean
        Get
            Return _IsCorrect
        End Get
        Set(ByVal value As Boolean)
            _IsCorrect = value
        End Set
    End Property

    Private _Score As Double
    Public Property Score() As Double
        Get
            Return _Score
        End Get
        Set(ByVal value As Double)
            _Score = value
        End Set
    End Property

    Public Sub New()
        _Id = 0
        _Name = String.Empty
        _IsCorrect = False
        _Score = 0
    End Sub



End Class
