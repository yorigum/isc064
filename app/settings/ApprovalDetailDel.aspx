<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApprovalDetailDel.aspx.cs" Inherits="ISC064.SETTINGS.ApprovalDetailDel" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Delete Approval</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Approval - Delete Approval">
    <script language="javascript">
        function confirmation(tipe, lvl) {
            if (confirm('Approval tidak dapat dihapus.\\n\\nKemungkinan terjadi karena : \\n1. Masih terdapat kontrak yang harus di approve')) {
                location.href = 'ApprovalDetailEdit.aspx?tipe=' + tipe + '&lvl=' + lvl;
            }
        }
    </script>
</head>
<body onkeyup="if(event.keyCode==27) history.back(-1)">
    <form id="Form1" method="post" runat="server">
    </form>
</body>
</html>
