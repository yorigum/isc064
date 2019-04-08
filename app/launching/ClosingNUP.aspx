<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ClosingNUP.aspx.cs" Inherits="ISC064.LAUNCHING.ClosingNUP" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Batavianet Business Application :: Launching</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Closing NUP">
</head>
<style>
    .nav, .navsub {
        border: 0px;
        background-color: #EEEEEE;
        font: 8pt Trebuchet MS;
        padding-left: 7;
        text-align: left;
        width: 190;
        height: 18px;
    }

    .nav2 {
        border: 0px;
        background-color: #EEEEEE;
        font: 14pt Trebuchet MS;
        padding-left: 7;
        text-align: left;
        width: 200;
        height: 30px;
    }
</style>
<body class="body-padding" onkeyup="&#13;&#10;&#9;if(document.getElementById('backbtn')){if(event.keyCode==27)document.getElementById('backbtn').click()};&#13;&#10;&#9;if(document.getElementById('cancel')){if(event.keyCode==27)document.getElementById('cancel').click()}">
    <form id="Form1" method="post" runat="server" class="cnt">
        <div style="float: left; width: 40%;">
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="width: 20px">
                        <a href="Index.aspx" style="text-align: center; vertical-align: middle; font-size: x-large;">
                            <img src="/Media/icon_prev_c.png" style="width: 80px; height: 80px;"></a>
                    </td>
                </tr>
            </table>
        </div>
        <div style="float: right; width: 10%;">
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="width: 20px">
                        <a href="/Gateway.aspx" style="text-align: center; vertical-align: middle; font-size: x-large;">
                            <img src="/Media/icon_gateway2.png" style="width: 80px; height: 80px;"></a>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20px">
                        <a href="/SignOut.aspx" style="text-align: center; vertical-align: middle; font-size: x-large;">
                            <img src="/Media/icon_out.png" style="width: 80px; height: 80px;"></a>
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <br />
        <br />
        <br />
        <br />
        <div id="pilih" runat="server">
            <h1>NUP</h1>
            <p>
                Halaman 1 dari 2
            </p>
            <br>
            <table style="border-right: #dcdcdc 1px solid; border-top: #dcdcdc 1px solid; border-left: #dcdcdc 1px solid; border-bottom: #dcdcdc 1px solid"
                cellspacing="5">
                <tr>
                    <td>No. NUP :
                    </td>
                    <td>
                        <asp:TextBox ID="nonup" runat="server" Width="200" CssClass="txt"></asp:TextBox>
                        <input type="button" value="..." class="btn" onclick="popDaftarPP('a')" id="btnpop"
                            runat="server" name="btnpop" visible="false" />
                    </td>
                    <td>
                        <asp:Button ID="next" runat="server" Text="Next..." CssClass="btn btn-blue" OnClick="next_Click"></asp:Button>
                    </td>
                    <td>
                        <asp:DropDownList ID="project" runat="server" AutoPostBack="true" OnSelectedIndexChanged="project_SelectedIndexChanged"></asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList ID="jenis" runat="server"></asp:DropDownList>
                    </td>
                </tr>
            </table>
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
