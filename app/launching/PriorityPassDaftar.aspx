<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PriorityPassDaftar.aspx.cs"
    Inherits="ISC064.LAUNCHING.PriorityPassDaftar" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>NUP</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Priority Pass">
</head>
<body onkeyup="&#13;&#10;&#9;if(document.getElementById('backbtn')){if(event.keyCode==27)document.getElementById('backbtn').click()};&#13;&#10;&#9;if(document.getElementById('cancel')){if(event.keyCode==27)document.getElementById('cancel').click()}">
    <form id="Form1" method="post" runat="server" class="cnt">
    <uc1:Head ID="Head1" runat="server"></uc1:Head>
    <input type="text" style="display: none">
    <div style="display: none">
        <asp:CheckBox ID="dariReminder" runat="server"></asp:CheckBox>
    </div>
    <div id="pilih" runat="server">
        <h1>
            NUP</h1>
        <p>
            Halaman 1 dari 2</p>
        <br>
        <table style="border-right: #dcdcdc 1px solid; border-top: #dcdcdc 1px solid; border-left: #dcdcdc 1px solid;
            border-bottom: #dcdcdc 1px solid" cellspacing="5">
            <tr>
                <td>
                    No. NUP :
                </td>
                <td>
                    <asp:TextBox ID="nonup" runat="server" Width="200" CssClass="txt"></asp:TextBox>
                    <input type="button" value="..." class="btn" onclick="popDaftarPP('a')" id="btnpop"
                        runat="server" name="btnpop" visible="false" />
                </td>
                <td>
                    <asp:Button ID="next" runat="server" Text="Next..." CssClass="btn" OnClick="next_Click">
                    </asp:Button>
                </td>
            </tr>
        </table>
        <p class="feed">
            <asp:Label ID="feed" runat="server"></asp:Label>
        </p>
        <input type="button" id="backbtn" runat="server" onclick="window.location='TabelStok.htm';"  value="Cancel"
            class="btn" style="margin: 5px" name="backbtn">
    </div>

    <script language="javascript">
        function call(nonup) {
            document.getElementById('nonup').value = nopriority;
            document.getElementById('next').click();
        }
    </script>

    </form>
</body>
</html>
