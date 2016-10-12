Public Class Question


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

    Private _Description As String
    Public Property Description() As String
        Get
            Return _Description
        End Get
        Set(ByVal value As String)
            _Description = value
        End Set
    End Property

    Private _Alt01 As Alternative

    Public Property Alt01() As Alternative
        Get
            Return _Alt01
        End Get
        Set(ByVal value As Alternative)
            _Alt01 = value
        End Set
    End Property

    Private _Alt02 As Alternative

    Public Property Alt02() As Alternative
        Get
            Return _Alt02
        End Get
        Set(ByVal value As Alternative)
            _Alt02 = value
        End Set
    End Property

    Private _Alt03 As Alternative

    Public Property Alt03() As Alternative
        Get
            Return _Alt03
        End Get
        Set(ByVal value As Alternative)
            _Alt03 = value
        End Set
    End Property

    Private _Alt04 As Alternative

    Public Property Alt04() As Alternative
        Get
            Return _Alt04
        End Get
        Set(ByVal value As Alternative)
            _Alt04 = value
        End Set
    End Property

    Public Function calcScore(regex As String) As Double

        Dim scr As Double = 0
        scr += IIf(regex.Substring(0, 1) = "1", Alt01.Score, 0)
        scr += IIf(regex.Substring(1, 1) = "1", Alt02.Score, 0)
        scr += IIf(regex.Substring(2, 1) = "1", Alt03.Score, 0)
        scr += IIf(regex.Substring(3, 1) = "1", Alt04.Score, 0)

        Return scr

    End Function
End Class
