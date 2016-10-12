Imports System.Text
Imports CommandLine
Imports CommandLine.Text

Module Main

    Private Algoritmo As AlgoritmoAsignacionEnum
    Private TamañoMemoria As Integer
    Private ListaProcesos As List(Of Proceso)
    Private ListaParticiones As List(Of Particion)
    Private ParticionesLibres As List(Of Particion)
    Private ParticionesOcupadas As List(Of Particion)
    Private TiempoSimulacion As Integer

    Sub Main()


        Dim parser = New Parser(Sub(config) config.HelpWriter = Console.Out)

        Dim result = parser.ParseArguments(Of InstallOptions, RunOptions, ListOptions, InfoOptions, StartOptions, TestOptions)(My.Application.CommandLineArgs)

        result.WithParsed(Of InstallOptions)(AddressOf Install).
                WithParsed(Of RunOptions)(AddressOf Run).
                WithParsed(Of ListOptions)(AddressOf List).
                WithParsed(Of InfoOptions)(AddressOf Info).
                WithParsed(Of StartOptions)(AddressOf Start).
                WithParsed(Of TestOptions)(AddressOf Test).
                WithNotParsed(Function(errors) 1)

    End Sub

    Function Info(opts As InfoOptions)

        Dim sb As New StringBuilder()
        Dim arrlst As New ArrayList()
        Dim memSim As New MemSim()

        sb.AppendLine()
        sb.AppendLine(HeadingInfo.Default)
        sb.AppendLine()

        sb.AppendLine("---------------------------")
        sb.AppendLine(" Examen Parcial")
        sb.AppendLine(" Sistemas Operativos 2016-2")
        sb.AppendLine("---------------------------")
        sb.AppendLine()
        sb.AppendLine(" Dada la siguiente configuración de memoria y lista de trabajos: ")
        sb.AppendLine()
        sb.AppendLine("-------------------------------")
        sb.AppendFormat(" {0,-18} {1, -3} {2, -10} {3}", "T. Memoria", ":", memSim.TamañoMemoria & "kb", vbCrLf)
        sb.AppendFormat(" {0,-18} {1, -3} {2, -10} {3}", "E. Particion", ":", "P. Fija", vbCrLf)
        sb.AppendFormat(" {0,-18} {1, -3} {2, -10} {3}", "T. Simulacion", ":", memSim.TiempoSimulacion & " seg.", vbCrLf)
        sb.AppendLine("-------------------------------")
        sb.AppendLine()
        sb.AppendLine(memSim.GetPartitionListStrTbl())
        sb.AppendLine()
        sb.AppendLine(memSim.GetJobListStrTbl())


        sb.AppendLine("Realice la asignacion de memoria (comando 'start') y responda las siguientes preguntas para cada algoritmo de asignacion ('Primer Ajuste' y 'Mejor Ajuste').")
        sb.AppendLine()
        sb.AppendLine("  1. Numero de Trabajos Asignados.")
        sb.AppendLine("  2. Numero de Trabajos No Asignados.")
        sb.AppendLine("  3. Numero de Trabajos en Memoria Promedio.")
        sb.AppendLine("  4. Utilizacion de Memoria Promedio")
        sb.AppendLine("  5. Utilizacion de Memoria Maximo")
        sb.AppendLine("  6. Utilizacion de Memoria Minimo")
        sb.AppendLine("  7. Memoria No Utilizada Promedio")
        sb.AppendLine("  8. Memoria No Utilizada Maximo")
        sb.AppendLine("  9. Memoria No Utilizada Minimo")
        sb.AppendLine("  10. Utilizacion de Memoria Promedio por cada Particion")
        sb.AppendLine("  11. Fragmentacion Interna Promedio por cada Particion")
        sb.AppendLine("  12. Particion que registro mayor fragmentacion interna")
        sb.AppendLine("  13. Particion que registro menor fragmentacion interna")
        sb.AppendLine("  14. Particion Mas Utilizada")
        sb.AppendLine("  15. Particion Menos Utilizada")
        sb.AppendLine()
        sb.AppendLine("Por ultimo, indique cual de ambos algoritmos es el mas adecuado.")
        sb.AppendLine()
        sb.AppendLine("Las respuestas deberan ser enviadas en un documento en word con sus nombres y apellidos.")
        sb.AppendLine()
        sb.AppendLine("¡Mucha Suerte!")
        sb.AppendLine()
        Console.WriteLine(sb.ToString())
        Return 0


    End Function

    Function Start(opts As StartOptions)

        Dim memSim As New MemSim()
        Dim sb As New StringBuilder()
        sb.AppendLine(" ¿Algoritmo de asignacion a emplear? ")
        sb.AppendLine()
        sb.AppendLine("  (1) Primer Ajuste")
        sb.AppendLine("  (2) Mejor Ajuste")
        sb.AppendLine()
        sb.Append(" Ingrese una opcion: ")
        Console.Write(sb.ToString())

        Select Case Console.ReadLine()
            Case "1"
                memSim.AlgoritmoAsignacion = AlgoritmoAsignacionEnum.PrimerAjuste
            Case "2"
                memSim.AlgoritmoAsignacion = AlgoritmoAsignacionEnum.MejorAjuste
            Case Else
                Console.WriteLine(" Opcion no valida... :(")
                Return 0
        End Select
        Console.WriteLine()
        Console.Write(" Presione una tecla para iniciar la simulación... ")
        Console.ReadKey()

        Dim result As MemSimResult = memSim.ExecuteMemSim()
        Console.WriteLine(result.SimResultToString)

        Return 0

    End Function

    Function Install(opts As InstallOptions)
        Console.WriteLine("Install")
        Return 0
    End Function

    Function Run(opts As RunOptions)
        Console.WriteLine("Run")
        Return 0
    End Function

    Function List(opts As ListOptions)

        Dim sb = New StringBuilder()
        Dim stdLst = Util.Read()

        sb.AppendLine()
        sb.AppendLine(HeadingInfo.Default)
        sb.AppendLine()

        sb.AppendFormat("  {0,-8} {1, -40} {2, 10}", "Id", "Name", "Grade")
        sb.AppendLine()
        sb.AppendFormat("  {0,-8} {1, -40} {2, 10}", "--", "----", "-----")
        sb.AppendLine()

        For Each std As Student In stdLst
            sb.AppendFormat("  {0,-8} {1, -40} {2, 10}", "[" & CStr(std.Id) & "]", std.Name, CStr(std.Grade))
            sb.AppendLine()
        Next

        Console.WriteLine(sb.ToString())

        Return 0
    End Function

    Function Test(opts As TestOptions)
        Console.WriteLine("Test")
        Console.Write("Select an option: ")
        Dim input As String = Console.ReadLine()
        Console.WriteLine("Input: " & input)
        Return 0
    End Function

    Public Function ToStringTable(Of T)(values As IEnumerable(Of T), columnHeaders As String(), ParamArray valueSelectors As Func(Of T, Object)()) As String
        Return ToStringTable(values.ToArray(), columnHeaders, valueSelectors)
    End Function

    <System.Runtime.CompilerServices.Extension>
    Public Function ToStringTable(Of T)(values As T(), columnHeaders As String(), ParamArray valueSelectors As Func(Of T, Object)()) As String
        Debug.Assert(columnHeaders.Length = valueSelectors.Length)

        Dim arrValues = New String(values.Length, valueSelectors.Length - 1) {}

        ' Fill headers
        For colIndex As Integer = 0 To arrValues.GetLength(1) - 1
            arrValues(0, colIndex) = columnHeaders(colIndex)
        Next

        ' Fill table rows
        For rowIndex As Integer = 1 To arrValues.GetLength(0) - 1
            For colIndex As Integer = 0 To arrValues.GetLength(1) - 1
                arrValues(rowIndex, colIndex) = valueSelectors(colIndex).Invoke(values(rowIndex - 1)).ToString()
            Next
        Next

        Return ToStringTable(arrValues)
    End Function


    <System.Runtime.CompilerServices.Extension>
    Public Function Append(Of T)(source As IEnumerable(Of T), ParamArray item As T()) As IEnumerable(Of T)
        Return source.Concat(item)
    End Function

    <System.Runtime.CompilerServices.Extension>
    Public Function ToStringTable(arrValues As String(,)) As String
        Dim maxColumnsWidth As Integer() = GetMaxColumnsWidth(arrValues)
        Dim headerSpliter = New String("-"c, maxColumnsWidth.Sum(Function(i) i + 3) - 1)

        Dim sb = New StringBuilder()
        For rowIndex As Integer = 0 To arrValues.GetLength(0) - 1
            For colIndex As Integer = 0 To arrValues.GetLength(1) - 1
                ' Print cell
                Dim cell As String = arrValues(rowIndex, colIndex)
                cell = cell.PadRight(maxColumnsWidth(colIndex))
                sb.Append(" | ")
                sb.Append(cell)
            Next

            ' Print end of line
            sb.Append(" | ")
            sb.AppendLine()

            ' Print splitter
            If rowIndex = 0 Then
                sb.AppendFormat(" |{0}| ", headerSpliter)
                sb.AppendLine()
            End If
        Next
        sb.AppendFormat("  {0}  ", headerSpliter)
        sb.AppendLine()
        Return sb.ToString()
    End Function

    Private Function GetMaxColumnsWidth(arrValues As String(,)) As Integer()
        Dim maxColumnsWidth = New Integer(arrValues.GetLength(1) - 1) {}
        For colIndex As Integer = 0 To arrValues.GetLength(1) - 1
            For rowIndex As Integer = 0 To arrValues.GetLength(0) - 1
                Dim newLength As Integer = arrValues(rowIndex, colIndex).Length
                Dim oldLength As Integer = maxColumnsWidth(colIndex)

                If newLength > oldLength Then
                    maxColumnsWidth(colIndex) = newLength
                End If
            Next
        Next

        Return maxColumnsWidth
    End Function




End Module
