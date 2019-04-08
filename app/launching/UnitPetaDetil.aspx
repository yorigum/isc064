<%@ Page Language="c#" Inherits="ISC064.LAUNCHING.UnitPetaDetil" CodeFile="UnitPetaDetil.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Peta Site Plan</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Unit - Peta Floor Plan Detil">
    <meta http-equiv="pragma" content="no-cache">
    <script type="text/javascript" language="javascript">
        function DisableBackButton() {
            window.history.forward()
        }
        DisableBackButton();
        window.onload = DisableBackButton;
        window.onpageshow = function (evt) { if (evt.persisted) DisableBackButton() }
        window.onunload = function () { void (0) }
    </script>
</head>
<body class="body-padding" onkeyup="if(event.keyCode==27) history.back(-1)">
    <form id="Form1" method="post" runat="server">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <div style="z-index: 1; left: 50px; width: 100%; position: absolute; top: 100px;">
        </div>
        <div style="top: 50px; left: 50px;">
        </div>
        <asp:PlaceHolder ID="list" runat="server"></asp:PlaceHolder>

    </form>
</body>
</html>
