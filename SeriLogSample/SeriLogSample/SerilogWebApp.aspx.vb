Imports Serilog
Imports Serilog.Context
Imports Serilog.Core

Public Class SerilogWebApp
    Inherits System.Web.UI.Page

    Dim globalLoggedProperties As Dictionary(Of String, String) = New Dictionary(Of String, String)


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'LogContext.PushProperty("BusinessUnit", Request.Item("businessunit")) 'This Logcontext.push property can be overwrriten if we have another value with the same name
        'LogContext.PushProperty("Command", Request.Item("command"))
        'LogContext.PushProperty("LookupType", Request.Item("lookuptype"))
        'LogContext.PushProperty("DefaultLoggingValue", globalLoggedProperties)

        'globalLoggedProperties.Add("Guuid", Guid.NewGuid().ToString("N"))
        globalLoggedProperties.Add("Environment", Request.Item("envname"))
        globalLoggedProperties.Add("BusinessUnit", Request.Item("businessunit"))
        globalLoggedProperties.Add("Command", Request.Item("command"))
        globalLoggedProperties.Add("LookupType", Request.Item("lookuptype"))

        Dim logger = Log.ForContext(Of SerilogWebApp)()
        logger.Information("Printed for Source Context")
        'Dim logger = Serilog.Log.ForContext("DefaultLoggingValue", globalLoggedProperties)

        'logger.Information("Before Adding some more properties")
        'globalLoggedProperties.Add("SomeExtraProp", "SomeExtraPropValue")
        'logger.Information("After Adding extraproperties")

        LogContext.PushProperty("LogContPushProp", "LogContPushPropVal")
        Logger.Information("After Pusing the properties")
        LogContext.Suspend() 'we can reset
        logger.Information("After suspending Log Context Pushed Properties")

        Using (LogContext.PushProperty("DefaultLoggingValue", globalLoggedProperties))
            Try
                'globalLoggedProperties.Add("CorrelationId", )

                'Using (LogContext.PushProperty("DefaultLoggingValue", globalLoggedProperties))
                Try
                    Dim debug As String = String.Empty
                    Dim isDebugEnabled As Boolean = False

                    If Request.Item("enabledebug") IsNot Nothing And Request.Item("enabledebug").Equals("Y") Then
                        isDebugEnabled = True
                    Else
                        isDebugEnabled = False
                    End If

                    logger.Warning("Warning logs goes here")

                    logger.Information("Some Info Logs Goes Here ")

                    If isDebugEnabled Then logger.Debug("Some Debug Logs Goes Here ")

                    Dim custcode As String = "SFEK001"
                    logger.Error("No Data found for the Customer {CustCode}", custcode)


                    'Threading.Thread.CurrentThread.Name = "SampleThreadName"
                    'logger.Information("{ProcessId} Process Id and Name {ProcessName}")
                    'logger.Information("{ThreadId} Thread Id and Thread Name {ThreadName}")

                Catch ex As Exception
                    logger.Error(ex, "Exception Occurred")
                    'logger.Error(Of SerilogWebApp)(ex, "Error occurred",)
                End Try
                'End Using

            Catch ex As Exception
                logger.Error(ex, "Exception Occurred")
            End Try

        End Using


    End Sub

End Class