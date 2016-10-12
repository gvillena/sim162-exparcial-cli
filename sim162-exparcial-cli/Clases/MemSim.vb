

Imports System.Text

Public Class MemSim

#Region " Fields "

    Private _AlgoritmoAsignacion As AlgoritmoAsignacionEnum
    Private _TamañoMemoria As Integer
    Private _TiempoSimulacion As Integer

    Private ListaProcesos As List(Of Proceso)
    Private ListaParticiones As List(Of Particion)
    Private ParticionesLibres As List(Of Particion)
    Private ParticionesOcupadas As List(Of Particion)

#End Region

#Region " Properties "

    Public Property TiempoSimulacion() As Integer
        Get
            Return _TiempoSimulacion
        End Get
        Set(ByVal value As Integer)
            _TiempoSimulacion = value
        End Set
    End Property

    Public Property TamañoMemoria() As Integer
        Get
            Return _TamañoMemoria
        End Get
        Set(ByVal value As Integer)
            _TamañoMemoria = value
        End Set
    End Property

    Public Property AlgoritmoAsignacion() As AlgoritmoAsignacionEnum
        Get
            Return _AlgoritmoAsignacion
        End Get
        Set(ByVal value As AlgoritmoAsignacionEnum)
            _AlgoritmoAsignacion = value
        End Set

    End Property




#End Region

    Public Sub New()

        ' Inicializando Parametros 
        Me.AlgoritmoAsignacion = AlgoritmoAsignacionEnum.PrimerAjuste
        Me.TamañoMemoria = 250
        Me.TiempoSimulacion = 10

        ' Inicializando Particiones
        ListaParticiones = New List(Of Particion)
        ListaParticiones.Add(New Particion(1, 0, 30, EstadoParticion.Libre))  ' P1 ~ 50kb
        ListaParticiones.Add(New Particion(2, 30, 115, EstadoParticion.Libre)) ' P2 ~ 20kb
        ListaParticiones.Add(New Particion(3, 145, 20, EstadoParticion.Libre)) ' P3 ~ 10kb
        ListaParticiones.Add(New Particion(4, 165, 85, EstadoParticion.Libre)) ' P4 ~ 15kb

        ParticionesLibres = New List(Of Particion)(ListaParticiones.ToArray)
        ParticionesOcupadas = New List(Of Particion)

        ' Inicializando Procesos de Prueba
        ListaProcesos = New List(Of Proceso)
        ListaProcesos.Add(New Proceso("J1", 35, EstadoProceso.Espera, 2)) ' J1 ~ 10kb
        ListaProcesos.Add(New Proceso("J2", 10, EstadoProceso.Espera, 3)) ' J2 ~ 20kb
        ListaProcesos.Add(New Proceso("J3", 90, EstadoProceso.Espera, 1)) ' J1 ~ 10kb
        ListaProcesos.Add(New Proceso("J4", 70, EstadoProceso.Espera, 2)) ' J2 ~ 20kb
        ListaProcesos.Add(New Proceso("J5", 95, EstadoProceso.Espera, 3)) ' J1 ~ 10kb
        ListaProcesos.Add(New Proceso("J6", 42, EstadoProceso.Espera, 1)) ' J1 ~ 10kb
        ListaProcesos.Add(New Proceso("J7", 80, EstadoProceso.Espera, 2)) ' J1 ~ 10kb
        ListaProcesos.Add(New Proceso("J8", 110, EstadoProceso.Espera, 3)) ' J1 ~ 10kb
        ListaProcesos.Add(New Proceso("J9", 35, EstadoProceso.Espera, 2)) ' J1 ~ 10kb
        ListaProcesos.Add(New Proceso("J10", 26, EstadoProceso.Espera, 2)) ' J1 ~ 10kb
        ListaProcesos.Add(New Proceso("J11", 44, EstadoProceso.Espera, 2)) ' J1 ~ 10kb
        ListaProcesos.Add(New Proceso("J12", 75, EstadoProceso.Espera, 2)) ' J1 ~ 10kb
        ListaProcesos.Add(New Proceso("J13", 40, EstadoProceso.Espera, 2)) ' J1 ~ 10kb
        ListaProcesos.Add(New Proceso("J14", 110, EstadoProceso.Espera, 2)) ' J1 ~ 10kb
        ListaProcesos.Add(New Proceso("J15", 16, EstadoProceso.Espera, 3)) ' J1 ~ 10kb

    End Sub

    Public Function GetPartitionListStrTbl() As String

        Dim sb As New StringBuilder()
        Dim arrlst As New ArrayList()

        For Each p As Particion In ListaParticiones
            arrlst.Add(Tuple.Create(String.Format("P{0}", p.Numero), String.Format("{0:00000}", p.Direccion), String.Format("{0}kb", p.Tamaño)))
        Next

        sb.AppendLine(arrlst.ToArray().ToStringTable({"N", "Direccion", "Tamaño"}, Function(a) a.Item1, Function(a) a.Item2, Function(a) a.Item3))
        Return sb.ToString()

    End Function

    Public Function GetJobListStrTbl() As String

        Dim sb As New StringBuilder()
        Dim arrlst As New ArrayList()

        For Each p As Proceso In ListaProcesos
            arrlst.Add(Tuple.Create(p.Nombre, String.Format("{0}kb", p.Tamaño), String.Format("{0} seg.", p.Duracion)))
        Next

        sb.AppendLine(arrlst.ToArray().ToStringTable({"N", "Tamaño", "Duracion"}, Function(a) a.Item1, Function(a) a.Item2, Function(a) a.Item3))
        Return sb.ToString()

    End Function

    Function ExecuteMemSim() As MemSimResult

        Dim result As MemSimResult = Nothing
        Dim resultItem As DataTable = Nothing

        Dim arrlst As New ArrayList()



        result = New MemSimResult()

        For index = 1 To TiempoSimulacion

            resultItem = New DataTable()
            resultItem.Columns.Add("N")
            resultItem.Columns.Add("D")
            resultItem.Columns.Add("T")
            resultItem.Columns.Add("P")
            resultItem.Columns.Add("TP")
            resultItem.Columns.Add("E")

            ' Actualizando tiempos 
            For Each p As Proceso In ListaProcesos

                If p.Estado = EstadoProceso.Asignado Then
                    If p.TimeLeft.Ticks > 0 Then
                        p.TimeLeft = p.TimeLeft.Subtract(New TimeSpan(1))
                    Else
                        TerminarProceso(p.Nombre)
                    End If
                End If
            Next

            AsignarEspacio()

            Dim prt As List(Of Particion) = New List(Of Particion)
            prt.AddRange(ParticionesLibres.ToArray)
            prt.AddRange(ParticionesOcupadas.ToArray)
            prt.Sort(Function(x, y) x.Direccion.CompareTo(y.Direccion))

            Dim NumPrt As Integer = 0
            Dim DirPrt As Integer = 0
            Dim TamPrt As Integer = 0
            Dim NomPrc As String = ""
            Dim TamPrc As Integer = 0
            Dim EstPrt As String = ""

            For Each p As Particion In prt

                NumPrt = p.Numero
                DirPrt = p.Direccion
                TamPrt = p.Tamaño

                If p.Proceso IsNot Nothing Then
                    NomPrc = p.Proceso.Nombre
                    TamPrc = p.Proceso.Tamaño
                Else
                    NomPrc = "Ninguno"
                    TamPrc = 0
                End If
                EstPrt = p.Estado.ToString()
                resultItem.Rows.Add(New Object() {NumPrt, DirPrt, TamPrt, NomPrc, TamPrc, EstPrt})

            Next

            result.Data.Add(resultItem)
        Next

        Return result

    End Function

    Private Sub AsignarEspacio()

        ' Verificar si existen procesos en la lista de procesos
        If ListaProcesos.Count = 0 Then Exit Sub

        ' Declarando Variables
        Dim index As Integer = -1
        Dim msj As String = String.Empty

        ' Recorrer cada proceso
        For Each proceso As Proceso In ListaProcesos

            If Not proceso.Estado = EstadoProceso.Espera Then Continue For

            index = BuscarParticionLibre(proceso.Tamaño)
            If index = -1 Then Continue For

            Dim ptOcupada As New Particion
            With ptOcupada
                .Numero = ParticionesLibres.Item(index).Numero
                .Direccion = ParticionesLibres.Item(index).Direccion
                .Tamaño = ParticionesLibres.Item(index).Tamaño
                .Proceso = proceso
                .Estado = EstadoParticion.Ocupada
            End With

            ParticionesOcupadas.Add(ptOcupada)
            ParticionesLibres.RemoveAt(index)
            ListaProcesos.Item(ListaProcesos.IndexOf(proceso)).Estado = EstadoProceso.Asignado
            ListaProcesos.Item(ListaProcesos.IndexOf(proceso)).DireccionMemoria = ptOcupada.Direccion

        Next

    End Sub

    Private Sub TerminarProceso(nomProcSel As String)

        ' Declarando variables
        Dim ptOcupada As Particion = Nothing
        Dim idxProcSel As Integer = -1

        ' Buscando particion donde fue asignado el proceso a terminar
        For Each p As Particion In ParticionesOcupadas

            ' Verificando coincidencia para el nombre del proceso 
            If p.Proceso.Nombre = nomProcSel Then

                ' Asignando particion 
                ptOcupada = p

                ' Asignando indice proceso seleccionado
                idxProcSel = ListaProcesos.IndexOf(p.Proceso)

                ' Actualizando lista de procesos 
                ListaProcesos.Item(idxProcSel).Estado = EstadoProceso.Terminado

                ' Estableciendo Hora Fin
                ListaProcesos.Item(idxProcSel).HoraFin = Date.Now()

                ' Saliendo de bucle for
                Exit For

            End If
        Next

        ' Creando nueva particion libre
        Dim ptLibre As New Particion
        With ptLibre
            .Numero = ptOcupada.Numero
            .Direccion = ptOcupada.Direccion
            .Tamaño = ptOcupada.Tamaño
            .Proceso = Nothing
            .Estado = EstadoParticion.Libre
        End With

        ' Eliminando particion de lista de particiones ocupadas
        ParticionesOcupadas.Remove(ptOcupada)

        ''''''''''''''''''''''''''''''''''
        '' Combinando particiones libres '
        ''''''''''''''''''''''''''''''''''

        '' Declarando Variables
        'Dim ptLibreAnterior As Particion = Nothing
        'Dim ptLibrePosterior As Particion = Nothing

        '' Buscando particion libre anterior
        'For Each p As Particion In ParticionesLibres

        '    ' Verificando si la particion es adyacente
        '    If (p.Direccion + p.Tamaño) = ptLibre.Direccion Then

        '        ' Asignando particion 
        '        ptLibreAnterior = p

        '        ' Saliendo de bucle for
        '        Exit For

        '    End If
        'Next

        '' Verificando si se encontro particion libre anterior 
        'If ptLibreAnterior IsNot Nothing Then

        '    ' Redimensionando particion libre 
        '    ptLibre.Direccion = ptLibreAnterior.Direccion
        '    ptLibre.Tamaño = ptLibre.Tamaño + ptLibreAnterior.Tamaño

        '    ' Eliminando particion libre anterior
        '    ParticionesLibres.Remove(ptLibreAnterior)

        'End If

        '' Buscando particion libre posterior
        'For Each p As Particion In ParticionesLibres

        '    ' Verificando si la particion es adyacente
        '    If (ptLibre.Direccion + ptLibre.Tamaño) = p.Direccion Then

        '        ' Asignando particion 
        '        ptLibrePosterior = p

        '        ' Saliendo de bucle for
        '        Exit For

        '    End If
        'Next

        '' Verificando si se encontro particion libre posterior
        'If ptLibrePosterior IsNot Nothing Then

        '    ' Redimensionando particion libre 
        '    ptLibre.Tamaño = ptLibre.Tamaño + ptLibrePosterior.Tamaño

        '    ' Eliminando particion libre posterior
        '    ParticionesLibres.Remove(ptLibrePosterior)

        'End If

        ' Agregando particion a lista de particiones libres
        ParticionesLibres.Add(ptLibre)

        ' Asignar Espacio
        'AsignarEspacio()

    End Sub

    Private Function BuscarParticionLibre(tamañoProceso As Integer) As Integer

        ' Declarando variable index 
        Dim index As Integer = -1

        ' Realizando ordenamiento algoritmo primer ajuste
        If AlgoritmoAsignacion = AlgoritmoAsignacionEnum.PrimerAjuste Then
            ParticionesLibres.Sort(Function(x, y) x.Direccion.CompareTo(y.Direccion))
        End If

        ' Realizando ordenamiento algoritmo mejor ajuste
        If AlgoritmoAsignacion = AlgoritmoAsignacionEnum.MejorAjuste Then
            ParticionesLibres.Sort(Function(x, y) x.Tamaño.CompareTo(y.Tamaño))
        End If

        ' Recorriendo cada particion 
        For Each particion As Particion In ParticionesLibres

            ' Si el tamaño del proceso es mayor al de la particion
            If tamañoProceso > particion.Tamaño Then Continue For

            ' Devolviendo el indice de la particion libre encontrada 
            index = ParticionesLibres.IndexOf(particion)
            Exit For

        Next

        'If index = -1 Then Return index


        '' Obteniendo tamaño particion encontrada
        'Dim tamañoParticion As Integer = ParticionesLibres.Item(index).Tamaño

        '' Verificando si se requiere redimensionar y crear particiones
        'If tamañoParticion > tamañoProceso Then

        '    ' Obteniendo direccion inicial particion encontrada
        '    Dim direccionParticion As Integer = ParticionesLibres.Item(index).Direccion

        '    ' Creando nueva particion
        '    Dim nuevaParticion As Particion = New Particion()
        '    With nuevaParticion
        '        .Direccion = direccionParticion + tamañoProceso
        '        .Tamaño = tamañoParticion - tamañoProceso
        '    End With

        '    ' Redimensionando particion encontrada
        '    ParticionesLibres.Item(index).Tamaño = tamañoProceso

        '    ' Insertando nueva particion despues de la particion encontrada
        '    ParticionesLibres.Insert(index + 1, nuevaParticion)

        'End If

        ' Devolviendo indice encontrado
        Return index
    End Function


End Class
