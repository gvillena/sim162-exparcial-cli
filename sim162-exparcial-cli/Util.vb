Imports System.Text

Public Class Util



    Public Shared Function Read() As ArrayList

        Dim lst As ArrayList = New ArrayList()
        Dim std As Student = Nothing
        Dim path As String = IO.Path.Combine(My.Application.Info.DirectoryPath, "test.txt")

        Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(path)

            MyReader.TextFieldType = FileIO.FieldType.Delimited
            MyReader.SetDelimiters(vbTab)

            Dim currentRow As String()
            While Not MyReader.EndOfData
                Try
                    currentRow = MyReader.ReadFields()
                    std = New Student()
                    With std
                        .Id = currentRow.ElementAt(0)
                        .Name = currentRow.ElementAt(1)
                        .Grade = 0
                    End With
                    lst.Add(std)
                Catch ex As Microsoft.VisualBasic.
                            FileIO.MalformedLineException
                    Console.WriteLine("Line " & ex.Message &
                    "is not valid and will be skipped.")
                End Try
            End While
        End Using

        'For Each s As Student In lst
        '    Console.WriteLine("{0} {1}", CStr(s.Id), s.Name)
        'Next
        Return lst
    End Function

    Public Shared Function Test()
        Dim q As New Question()
        q.Alt01 = New Alternative() With {.Id = 1, .Name = "Pregunta 01", .IsCorrect = False, .Score = 0}
        q.Alt02 = New Alternative() With {.Id = 2, .Name = "Pregunta 02", .IsCorrect = True, .Score = 1}
        q.Alt03 = New Alternative() With {.Id = 3, .Name = "Pregunta 03", .IsCorrect = False, .Score = 0}
        q.Alt04 = New Alternative() With {.Id = 4, .Name = "Pregunta 04", .IsCorrect = True, .Score = 1}

        Dim scr As Double = q.calcScore("0101")
        Console.WriteLine("Score: " & scr)
        Return 0
    End Function

End Class
