Call AutomaticReminder()

Sub AutomaticReminder()
    OpenHTTP("http://westpoint.softwareproperti.com/AutoDenda.aspx")
End Sub

Sub OpenHTTP(URL)
    On Error Resume Next
    Dim objRequest
    Set objRequest = CreateObject("Microsoft.XMLHTTP")

    objRequest.Open "POST", URL , false
    objRequest.Send

    Set objRequest = Nothing
End Sub