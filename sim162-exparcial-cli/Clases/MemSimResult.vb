Imports System.Text

Public Class MemSimResult

    Private _Data As ArrayList

    Public Property Data() As ArrayList
        Get
            Return _Data
        End Get
        Set(ByVal value As ArrayList)
            _Data = value
        End Set
    End Property


    Public Sub New()

        _Data = New ArrayList()

    End Sub



    Public Function SimResultToString() As String

        Dim sb As New StringBuilder()
        Dim arrlst As New ArrayList()
        Dim count As Integer = 1

        Dim tbl As ArrayList = New ArrayList()



        Dim NumPrt As String = ""
        Dim DirPrt As String = ""
        Dim TamPrt As String = ""
        Dim NomPrc As String = ""
        Dim TamPrc As String = ""
        Dim EstPrt As String = ""

        sb.AppendLine()

        For Each d As DataTable In _Data

            sb.AppendLine("  -----------------------------------------------------")
            sb.AppendLine(" | T: 00:" & String.Format("{0:00}", count))
            sb.AppendLine(" |----------------------")

            For Each row As DataRow In d.Rows

                NumPrt = String.Format("P{0}", row.Item(0))
                DirPrt = String.Format("{0:000000}", row.Item(1))
                TamPrc = String.Format("{0}kb", row.Item(2))
                NomPrc = row.Item(3)
                TamPrc = String.Format("{0}kb", row.Item(4))
                EstPrt = row.Item(5)
                tbl.Add(Tuple.Create(NumPrt, DirPrt, TamPrc, NomPrc, TamPrc, EstPrt))

            Next
            sb.AppendLine(tbl.ToArray().ToStringTable({"N", "Direccion", "Tamaño", "N. Proc.", "T. Proc.", "Estado"}, Function(a) a.Item1, Function(a) a.Item2, Function(a) a.Item3, Function(a) a.Item4, Function(a) a.Item5, Function(a) a.Item6))
            sb.AppendLine()
            tbl.Clear()

            count += 1

        Next

        Return sb.ToString()


    End Function



End Class
