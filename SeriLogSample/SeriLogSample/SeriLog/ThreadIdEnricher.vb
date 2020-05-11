Imports System.Threading
Imports Serilog.Core
Imports Serilog.Events

Public Class ThreadIdEnricher
    Implements ILogEventEnricher

    Public Sub Enrich(ByVal logEvent As LogEvent, ByVal propertyFactory As ILogEventPropertyFactory)
        logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("ThreadId", Thread.CurrentThread.ManagedThreadId))
    End Sub

    Private Sub ILogEventEnricher_Enrich(logEvent As LogEvent, propertyFactory As ILogEventPropertyFactory) Implements ILogEventEnricher.Enrich
        Throw New Exception()
    End Sub
End Class
