<%@ Page Language="c#" Inherits="ISC064.MARKETINGJUAL.KontrakApprovGU" CodeFile="KontrakApprovGU.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Approval Pindah Unit</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Kontrak - Approval Pindah Unit">
</head>
<body class="body-padding" onkeyup="if(document.getElementById('cancel')){if(event.keyCode==27)document.getElementById('cancel').click()}">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <input type="text" style="display: none" />
        <div id="frm" runat="server">
            <h1 class="title title-line">Approval Pindah Unit</h1>
            <br />
            <div>
                <p class="feed">
                    <asp:Label ID="feed" runat="server"></asp:Label>
                </p>
                <table style="border-right: #dcdcdc 1px solid; border-top: #dcdcdc 1px solid; border-left: #dcdcdc 1px solid; border-bottom: #dcdcdc 1px solid">
                    <tr>
                        <td><b>Tanggal Approval</b></td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="tglot" runat="server" CssClass="txt_center" Width="85"></asp:TextBox>
                            <input type="button" value="&#xf073;" style="font-family: 'fontawesome'" class="btn" onclick="openCalendar('tglot')" />
                            <asp:Label ID="tglotc" runat="server" CssClass="err"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <div style="border-right: #dcdcdc 1px solid; border-top: #dcdcdc 1px solid; border-left: #dcdcdc 1px solid; border-bottom: #dcdcdc 1px solid; padding: 5px">
                <table class="tb blue-skin" border="0">
                    <tr>
                        <th />
                        <th>No. Kontrak
                        </th>
                        <th>Unit
                        </th>
                        <th>Customer
                        </th>
                        <th>Sales
                        </th>
                        <th>Unit Lama
                        </th>
                        <th>Unit Baru
                        </th>
                        <th class="right">Biaya Administrasi
                        </th>
                    </tr>
                    <tr>
                        <td colspan="11">
                            <ul class="floatsm">
                                <li><a href="javascript:checkCtrl('nokontrak','true')">Check &nbsp;&nbsp;&nbsp;&nbsp;</a></li>
                                <li><a href="javascript:checkCtrl('nokontrak','false')">Uncheck</a></li>
                            </ul>
                            <br />
                        </td>
                    </tr>
                    <asp:PlaceHolder ID="list" runat="server" />
                </table>
                <asp:Button ID="save" runat="server" Text="Save" Width="75" OnClick="save_Click" CssClass="btn btn-blue"
                    AccessKey="s" />
            </div>
        </div>
        <script type="text/javascript">

            function checkCtrl(foo, n) {
                var x = true; var i = 0;
                while (x) {
                    if (document.getElementById(foo + "_" + i)) {
                        if (!document.getElementById(foo + "_" + i).disabled) {
                            if (n == "true")
                                document.getElementById(foo + "_" + i).checked = true;
                            else
                                document.getElementById(foo + "_" + i).checked = false;
                        }
                        i++;
                    } else { x = false; }
                }
            }
        </script>
    </form>
</body>
</html>
