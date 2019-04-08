Call AutomaticReminder()

Sub AutomaticReminder()
    OpenHTTP("http://192.168.2.39:8000/collection/AutoPJT.aspx")
End Sub

Sub OpenHTTP(URL)
    On Error Resume Next
    Dim objRequest
    Set objRequest = CreateObject("Microsoft.XMLHTTP")

    objRequest.Open "POST", URL , false
    objRequest.Send

    Set objRequest = Nothing
End Sub