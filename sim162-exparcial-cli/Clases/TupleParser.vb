
Imports System.Text

Public Class ArrayPrinter
    Const TOP_LEFT_JOINT As String = "┌"
    Const TOP_RIGHT_JOINT As String = "┐"
    Const BOTTOM_LEFT_JOINT As String = "└"
    Const BOTTOM_RIGHT_JOINT As String = "┘"
    Const TOP_JOINT As String = "┬"
    Const BOTTOM_JOINT As String = "┴"
    Const LEFT_JOINT As String = "├"
    Const JOINT As String = "┼"
    Const RIGHT_JOINT As String = "┤"
    Const HORIZONTAL_LINE As Char = "─"c
    Const PADDING As Char = " "c
    Const VERTICAL_LINE As String = "│"

    Private Shared Function GetMaxCellWidths(table As List(Of String())) As Integer()
        Dim maximumCells As Integer = 0
        For Each row As Array In table
            If row.Length > maximumCells Then
                maximumCells = row.Length
            End If
        Next

        Dim maximumCellWidths As Integer() = New Integer(maximumCells - 1) {}
        For i As Integer = 0 To maximumCellWidths.Length - 1
            maximumCellWidths(i) = 0
        Next

        For Each row As Array In table
            For i As Integer = 0 To row.Length - 1
                If row.GetValue(i).ToString().Length > maximumCellWidths(i) Then
                    maximumCellWidths(i) = row.GetValue(i).ToString().Length
                End If
            Next
        Next

        Return maximumCellWidths
    End Function

    Public Shared Function GetDataInTableFormat(table As List(Of String())) As String
        Dim formattedTable As New StringBuilder()
        Dim nextRow As Array = table.FirstOrDefault()
        Dim previousRow As Array = table.FirstOrDefault()

        If table Is Nothing OrElse nextRow Is Nothing Then
            Return [String].Empty
        End If

        ' FIRST LINE:
        Dim maximumCellWidths As Integer() = GetMaxCellWidths(table)
        For i As Integer = 0 To nextRow.Length - 1
            If i = 0 AndAlso i = nextRow.Length - 1 Then
                formattedTable.Append([String].Format("{0}{1}{2}", TOP_LEFT_JOINT, [String].Empty.PadLeft(maximumCellWidths(i), HORIZONTAL_LINE), TOP_RIGHT_JOINT))
            ElseIf i = 0 Then
                formattedTable.Append([String].Format("{0}{1}", TOP_LEFT_JOINT, [String].Empty.PadLeft(maximumCellWidths(i), HORIZONTAL_LINE)))
            ElseIf i = nextRow.Length - 1 Then
                formattedTable.AppendLine([String].Format("{0}{1}{2}", TOP_JOINT, [String].Empty.PadLeft(maximumCellWidths(i), HORIZONTAL_LINE), TOP_RIGHT_JOINT))
            Else
                formattedTable.Append([String].Format("{0}{1}", TOP_JOINT, [String].Empty.PadLeft(maximumCellWidths(i), HORIZONTAL_LINE)))
            End If
        Next

        Dim rowIndex As Integer = 0
        Dim lastRowIndex As Integer = table.Count - 1
        For Each thisRow As Array In table
            ' LINE WITH VALUES:
            Dim cellIndex As Integer = 0
            Dim lastCellIndex As Integer = thisRow.Length - 1
            For Each thisCell As Object In thisRow
                Dim thisValue As String = thisCell.ToString().PadLeft(maximumCellWidths(cellIndex), PADDING)

                If cellIndex = 0 AndAlso cellIndex = lastCellIndex Then
                    formattedTable.AppendLine([String].Format("{0}{1}{2}", VERTICAL_LINE, thisValue, VERTICAL_LINE))
                ElseIf cellIndex = 0 Then
                    formattedTable.Append([String].Format("{0}{1}", VERTICAL_LINE, thisValue))
                ElseIf cellIndex = lastCellIndex Then
                    formattedTable.AppendLine([String].Format("{0}{1}{2}", VERTICAL_LINE, thisValue, VERTICAL_LINE))
                Else
                    formattedTable.Append([String].Format("{0}{1}", VERTICAL_LINE, thisValue))
                End If

                cellIndex += 1
            Next

            previousRow = thisRow

            ' SEPARATING LINE:
            If rowIndex <> lastRowIndex Then
                nextRow = table(rowIndex + 1)

                Dim maximumCells As Integer = Math.Max(previousRow.Length, nextRow.Length)
                For i As Integer = 0 To maximumCells - 1
                    If i = 0 AndAlso i = maximumCells - 1 Then
                        formattedTable.Append([String].Format("{0}{1}{2}", LEFT_JOINT, [String].Empty.PadLeft(maximumCellWidths(i), HORIZONTAL_LINE), RIGHT_JOINT))
                    ElseIf i = 0 Then
                        formattedTable.Append([String].Format("{0}{1}", LEFT_JOINT, [String].Empty.PadLeft(maximumCellWidths(i), HORIZONTAL_LINE)))
                    ElseIf i = maximumCells - 1 Then
                        If i > previousRow.Length Then
                            formattedTable.AppendLine([String].Format("{0}{1}{2}", TOP_JOINT, [String].Empty.PadLeft(maximumCellWidths(i), HORIZONTAL_LINE), TOP_RIGHT_JOINT))
                        ElseIf i > nextRow.Length Then
                            formattedTable.AppendLine([String].Format("{0}{1}{2}", BOTTOM_JOINT, [String].Empty.PadLeft(maximumCellWidths(i), HORIZONTAL_LINE), BOTTOM_RIGHT_JOINT))
                        ElseIf i > previousRow.Length - 1 Then
                            formattedTable.AppendLine([String].Format("{0}{1}{2}", JOINT, [String].Empty.PadLeft(maximumCellWidths(i), HORIZONTAL_LINE), TOP_RIGHT_JOINT))
                        ElseIf i > nextRow.Length - 1 Then
                            formattedTable.AppendLine([String].Format("{0}{1}{2}", JOINT, [String].Empty.PadLeft(maximumCellWidths(i), HORIZONTAL_LINE), BOTTOM_RIGHT_JOINT))
                        Else
                            formattedTable.AppendLine([String].Format("{0}{1}{2}", JOINT, [String].Empty.PadLeft(maximumCellWidths(i), HORIZONTAL_LINE), RIGHT_JOINT))
                        End If
                    Else
                        If i > previousRow.Length Then
                            formattedTable.Append([String].Format("{0}{1}", TOP_JOINT, [String].Empty.PadLeft(maximumCellWidths(i), HORIZONTAL_LINE)))
                        ElseIf i > nextRow.Length Then
                            formattedTable.Append([String].Format("{0}{1}", BOTTOM_JOINT, [String].Empty.PadLeft(maximumCellWidths(i), HORIZONTAL_LINE)))
                        Else
                            formattedTable.Append([String].Format("{0}{1}", JOINT, [String].Empty.PadLeft(maximumCellWidths(i), HORIZONTAL_LINE)))
                        End If
                    End If
                Next
            End If

            rowIndex += 1
        Next

        ' LAST LINE:
        For i As Integer = 0 To previousRow.Length - 1
            If i = 0 Then
                formattedTable.Append([String].Format("{0}{1}", BOTTOM_LEFT_JOINT, [String].Empty.PadLeft(maximumCellWidths(i), HORIZONTAL_LINE)))
            ElseIf i = previousRow.Length - 1 Then
                formattedTable.AppendLine([String].Format("{0}{1}{2}", BOTTOM_JOINT, [String].Empty.PadLeft(maximumCellWidths(i), HORIZONTAL_LINE), BOTTOM_RIGHT_JOINT))
            Else
                formattedTable.Append([String].Format("{0}{1}", BOTTOM_JOINT, [String].Empty.PadLeft(maximumCellWidths(i), HORIZONTAL_LINE)))
            End If
        Next

        Return formattedTable.ToString()
    End Function
End Class

'=======================================================
'Service provided by Telerik (www.telerik.com)
'Conversion powered by NRefactory.
'Twitter: @telerik
'Facebook: facebook.com/telerik
'=======================================================
