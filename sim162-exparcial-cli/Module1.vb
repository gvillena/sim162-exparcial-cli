Imports System.Text
Imports CommandLine
Imports CommandLine.Text

Module Module1

    Sub Main()
        '        // As you can see, it's a really simple:
        'var parser = New Parser(config >= config.HelpWriter = Console.Out);
        '        Dim parser = New Parser()
        ' As you can see, it's a really simple:
        Dim parser = New Parser(Sub(config) config.HelpWriter = Console.Out)
        Dim result = parser.ParseArguments(Of InstallOptions, RunOptions, ListOptions, TestOptions)(My.Application.CommandLineArgs)
        'Dim result = CommandLine.Parser.Default.ParseArguments(Of InstallOptions, RunOptions)(My.Application.CommandLineArgs)
        result.WithParsed(Of InstallOptions)(AddressOf Install).
        WithParsed(Of RunOptions)(AddressOf Run).
        WithParsed(Of ListOptions)(AddressOf List).
        WithParsed(Of TestOptions)(AddressOf Test).
        WithNotParsed(Function(errors) 1)

    End Sub

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
        Util.Test()
        Return 0
    End Function


End Module
