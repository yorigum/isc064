<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AgentTarget.aspx.cs" Inherits="ISC064.ADMINJUAL.AgentTarget" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html lang="en">
<head>
    <title>Sales Target</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Sales - Target">
</head>
<body class="body-padding" onkeyup="&#13;&#10;&#9;if(document.getElementById('backbtn')){if(event.keyCode==27)document.getElementById('backbtn').click()};&#13;&#10;&#9;if(document.getElementById('cancel')){if(event.keyCode==27)document.getElementById('cancel').click()}">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <input type="text" style="display: none">
        <div style="display: none">
            <asp:CheckBox ID="dariReminder" runat="server"></asp:CheckBox>
        </div>
        <div id="pilih" runat="server">
            <h1 class="title title-line">Target Sales</h1>
            <p>
                Halaman 1 dari 2
            </p>
            <br>
            <table style="border-right: #dcdcdc 1px solid; border-top: #dcdcdc 1px solid; border-left: #dcdcdc 1px solid; border-bottom: #dcdcdc 1px solid"
                cellspacing="5">
                <tr>
                    <td>Periode :
                    </td>
                    <td>
                        <asp:DropDownList ID="bln" runat="server" Width="150" Font-Bold="true" Font-Size="12" />
                        <asp:DropDownList ID="thn" runat="server" Font-Bold="true" Font-Size="12" />
                    </td>
                    <td>
                        <asp:LinkButton ID="next" runat="server" CssClass="btn btn-blue" OnClick="next_Click">Next <i class="fa fa-arrow-right"></i></asp:LinkButton>
                    </td>
                </tr>
            </table>
            <p class="feed">
                <asp:Label ID="feed" runat="server"></asp:Label>
            </p>
            <input type="button" id="backbtn" runat="server" onclick="history.back(-1)" value="Cancel"
                class="btn" style="margin: 5px" name="backbtn">
        </div>
        <div id="frm" runat="server">
            <h1 class="title title-line">Target Sales</h1>
            <p>
                Halaman 2 dari 2
            </p>
            <br>
            <p style="font: 8pt; padding-left: 3px">
                Target Sales adalah dalam rupiah.
            </p>
            <table class="tb blue-skin" cellspacing="1">
                <tr valign="bottom" align="left">
                    <th width="100px">No. Sales
                    </th>
                    <th width="200px">Nama Sales
                    </th>
                    <th width="50px" align="right">Target
                    </th>
                    <th></th>
                </tr>
                <asp:PlaceHolder ID="list" runat="server"></asp:PlaceHolder>
            </table>
            <br>
            <table height="50">
                <tr>
                    <td>
                        <asp:LinkButton ID="save" runat="server" CssClass="btn btn-blue" Width="75" OnClick="save_Click">
                            <i class="fa fa-share"></i> OK
                        </asp:LinkButton>
                    </td>
                    <td>
                        <input type="button" onclick="location.href = '?'" class="btn btn-red" value="Cancel" style="width: 75px"
                            id="cancel" runat="server" name="cancel">
                    </td>
                </tr>
            </table>
        </div>

        <script type="text/javascript">
            function call(nokontrak) {
                document.getElementById('nokontrak').value = nokontrak;
                document.getElementById('next').click();
            }
        </script>

    </form>
</body>
</html>
