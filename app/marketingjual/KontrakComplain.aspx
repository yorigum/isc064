<%@ Page Language="C#" AutoEventWireup="true" CodeFile="KontrakComplain.aspx.cs" Inherits="ISC064.MARKETINGJUAL.KontrakComplain" %>


<%@ Register TagPrefix="uc1" TagName="HeadKontrak" Src="HeadKontrak.ascx" %>
<%@ Register TagPrefix="uc1" TagName="NavKontrak" Src="NavKontrak.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml">

<head>
    <title>Jurnal Complain</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="5">
    <meta name="sec" content="Kontrak - Proses PPJB">
</head>
<body onkeyup="if(event.keyCode==27) document.getElementById('cancel').click()">
    <form id="Form1" method="post" runat="server">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <div class="content-header">
            <uc1:NavKontrak ID="NavKontrak1" runat="server" Aktif="7"></uc1:NavKontrak>
        </div>
        <div class="tabdata">
            <uc1:HeadKontrak ID="Head2" runat="server"></uc1:HeadKontrak>
            <table width="100%">
                <tr>
                    <td width="20%" align="right">Complain
                    </td>
                    <td width="1px">:
                    </td>
                    <td>
                        <asp:DropDownList ID="ListComplain" runat="server"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td valign="top" align="right">Detil Complain
                    </td>
                    <td valign="top">:
                    </td>
                    <td>
                        <asp:TextBox Wrap="true" runat="server" Width="500" Height="70" TextMode="MultiLine" ID="det"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">Tgl Complain Diterima</td>
                    <td>:</td>
                    <td>
                        <asp:TextBox ID="tglcomplain" runat="server" Width="85" CssClass="txt_center"></asp:TextBox>
                        <label for="tglcomplain" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td colspan="2">
                        <asp:Button ID="save" CssClass="btn btn-blue" runat="server" Text="Save" OnClick="save_Click" />
                        <asp:Label ID="feed" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            <br />
            <table id="tb" width="100%" class="blue-skin tb" cellspacing="1">
                <tr>
                    <th>No.</th>
                    <th>Tgl Input </th>
                    <th>Tgl Complain </th>
                    <th>Jenis Complain</th>
                    <th>Detil</th>
                    <th>Solusi</th>
                    <th>Tgl Solved</th>
                    <th width="500">Status</th>
                    <th>Delete</th>
                </tr>
                <asp:PlaceHolder ID="list" runat="server"></asp:PlaceHolder>
            </table>
            <asp:Button ID="Update" runat="server" CssClass="btn btn-orange" Text="Update" OnClick="Update_Click" /><asp:Label ID="feed2" runat="server"></asp:Label>
        </div>
    </form>

    <script type="text/javascript">
        function call(nova) {
            document.getElementById('nova').value = nova;
        }
    </script>

</body>
</html>
