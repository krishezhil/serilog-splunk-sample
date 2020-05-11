Imports System.Web.SessionState
Imports Serilog
Imports Serilog.Events

Public Class Global_asax
    Inherits System.Web.HttpApplication



    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the application is started

        'Logger creation for appsetting,json -- Need to know if we really need this, this should be handled via web.config in asp.net web applications

        'Dim configuration = New ConfigurationBuilder().AddJsonFile("appsettings.json").Build()
        'Dim logger = New LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger()

        'Logger Creation for Web.config
        Serilog.Log.Logger = New LoggerConfiguration() _
        .Enrich.FromLogContext() _
        .WriteTo.EventCollector("http://localhost:8088/services/collector", "322ffad6-4ab2-4937-8838-ab148530f14d") _
        .Enrich.WithCorrelationId _
        .ReadFrom.AppSettings().CreateLogger()

        '.Enrich.FromLogContext() - To add the custom property to log context. Serilog will automatically add the added property to the logs
        '.Enrich.WithCorrelationId - To append the correlation id into log
        '.ReadFrom.AppSettings() -- This will read all the logger related app setting from Web.config

        'Write to File is not possible as we have it in web.config
        '.WriteTo.File("log.txt", LogEventLevel.Debug, outputTemplate:"{Timestamp:HH:mm:ss} [{EventType:x8} {Level:u3}] {Message:lj}{NewLine}{Exception}") _

        '.Enrich.WithThreadId() _
        '.Enrich.WithThreadName() _

        '.Enrich.WithProcessId() _
        '.Enrich.WithProcessName() _

    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the session is started
    End Sub

    Sub Application_BeginRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires at the beginning of each request
    End Sub

    Sub Application_EndRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires at the beginning of each request
        'Serilog.Log.CloseAndFlush() 'Because log batches are sent asynchronously, it's particularly important to make sure that Logger.Dispose() or Log.CloseAndFlush() is called before your application exits. This will make sure queued events are sent to the server, and any outstanding network requests can complete.
    End Sub
    Sub Application_AuthenticateRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires upon attempting to authenticate the use
    End Sub

    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when an error occurs
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the session ends
    End Sub

    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the application ends
    End Sub

End Class