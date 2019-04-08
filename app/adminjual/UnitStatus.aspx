<%@ Reference Page="~/Peta.aspx" %>

<%@ Page Language="c#" Inherits="ISC064.ADMINJUAL.UnitStatus" CodeFile="UnitStatus.aspx.cs" %>

<!DOCTYPE html >
<html lang="en">
<head>
    <title>Edit Unit (Status)</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Unit - Edit Unit (Status)">
    <meta http-equiv="pragma" content="no-cache">
    <base target="_self">
</head>
<body class="pop" onkeyup="if(event.keyCode==27)window.close()">
    <form id="Form1" method="post" runat="server">
        <table cellspacing="5">
            <tr>
                <td>
                    <asp:RadioButton ID="statusA" runat="server" Text="AVAILABLE" Font-Size="14" GroupName="stat"></asp:RadioButton>
                    <br>
                    <p style="font: 8pt">Unit bisa dijual oleh marketing.</p>
                    <br>
                    <asp:RadioButton ID="statusB" runat="server" Text="SOLD" Font-Size="14" GroupName="stat"></asp:RadioButton>
                    <p style="font: 8pt">
                        Unit tidak untuk dijual. Status ini tidak bisa dibalik jika transaksi marketing 
							sudah terjadi.
                    </p>
                    <br>
                    <asp:RadioButton ID="statusC" runat="server" Text="HOLD INTERNAL" Font-Size="14" GroupName="stat"></asp:RadioButton>
                    <p style="font: 8pt">
                        Unit di Hold.
                    </p>
                </td>
            </tr>
        </table>
        <table style="height: 50px;">
            <tr>
                <td>
                    <asp:Button ID="save" runat="server" CssClass="btn btn-blue" Text="OK" Width="75" OnClick="save_Click"></asp:Button>
                </td>
                <td>
                    <input id="cancel" type="button" class="btn btn-red" value="Cancel" style="width: 75px" onclick="window.close()">
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
