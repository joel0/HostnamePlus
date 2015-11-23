<%@ WebHandler Language="VB" Class="ipv4js" %>

Imports System
Imports System.Web

Public Class ipv4js : Implements IHttpHandler

    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        context.Response.ContentType = "application/x-javascript"
        context.Response.Write("document.getElementById(""IPv4Address"").innerHTML = ""<br /><span class='versionLabel'>IPv4: </span>" & context.Request.UserHostAddress & """;")
        Try
            context.Response.Write("document.getElementById(""IPv4Hostname"").innerHTML = ""<br /><span class='versionLabel'>IPv4: </span>" & Net.Dns.GetHostEntry(context.Request.UserHostAddress).HostName & """;")
        Catch ex As Exception
            ' Do nothing
        End Try
    End Sub

    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return True
        End Get
    End Property

End Class