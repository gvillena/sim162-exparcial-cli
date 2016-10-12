Imports CommandLine
Imports CommandLine.Text

Class Options

    <CommandLine.Option('r', "read", Required := true,
    HelpText:="Input files to be processed.")>
    Public Property InputFiles As IEnumerable(Of String)

    ' Omitting long name, default --verbose
    <CommandLine.Option(
    HelpText:="Prints all messages to standard output.")>
    Public Property Verbose As Boolean

    <CommandLine.Option(Default:="中文",
    HelpText:="Content language.")>
    Public Property Language As String

    <CommandLine.Value(0, MetaName:="offset",
    HelpText:="File offset.")>
    Public Property Offset As Long?
End Class

<CommandLine.Verb("info", HelpText:="TODO.")>
Class InfoOptions
End Class


<CommandLine.Verb("start", HelpText:="TODO.")>
Class StartOptions
End Class


<CommandLine.Verb("install", HelpText:="Install some app.")>
Class InstallOptions
End Class

<CommandLine.Verb("run", HelpText:="Install some app.")>
Class RunOptions
End Class

<CommandLine.Verb("list", HelpText:="List all students.")>
Class ListOptions
End Class

<CommandLine.Verb("test", HelpText:="My test command.")>
Class TestOptions
End Class

