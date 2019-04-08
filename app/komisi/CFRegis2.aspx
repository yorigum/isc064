<%@ Page Language="c#" Inherits="ISC064.KOMISI.CFRegis2" CodeFile="CFRegis2.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Generate Closing Fee</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Closing Fee - Generate Closing Fee (Step 2)">
</head>
<body class="body-padding">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <h1 class="title title-line">Generate Closing Fee</h1>
        <br />

        <table>
            <tr style="vertical-align:top">
                <td style="width:20%"><b>Project</b></td>
                <td style="width:2%">:</td>
                <td>                                         
                    <asp:Label ID="project_txt" runat="server"></asp:Label>
                </td>
            </tr>
            <tr id="trtipe" runat="server" visible="false">
                <td>Tipe</td>
                <td>:</td>
                <td><asp:Label ID="tipesales_txt" runat="server"></asp:Label></td>
            </tr>
            <tr id="trsales" runat="server" visible="false">
                <td>Sales</td>
                <td>:</td>
                <td><asp:Label ID="sales_txt" runat="server"></asp:Label></td>
            </tr>
            <tr id="trskema" runat="server">
                <td>Skema</td>
                <td>:</td>
                <td><asp:Label ID="skema_txt" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>Periode</td>
                <td>:</td>
                <td><asp:Label ID="periode_txt" runat="server"></asp:Label></td>
            </tr>
        </table>

        <br />
        <table>
            <tr style="vertical-align:top">
                <td style="width:20%"><b>Tgl. Generate Closing Fee</b></td>
                <td style="width:2%">:</td>
                <td>
					<asp:textbox id="tgl" runat="server" cssclass="txt_center" width="85"></asp:textbox>
                    <label for="tgl" class="btn btn-cal"><i class="fa fa-calendar"></i></label>                                            
                    <asp:Label ID="tglc" runat="server" CssClass="err"></asp:Label>
                </td>
            </tr>
        </table>

        <br />
        <table class="tb blue-skin" cellspacing="1">
            <tr align="left" valign="bottom">
                <th>No. Kontrak</th>
                <th>Unit</th>
                <th>Customer</th>
                <th>Sales</th>
                <th class="right">Nilai CF</th>
                <th>Potong Komisi</th>
            </tr>
            <asp:PlaceHolder ID="list" runat="server"></asp:PlaceHolder>
            <tr>
                <td colspan="6">
                    <asp:LinkButton ID="save" runat="server" CssClass="btn btn-blue" Width="75" OnClick="save_Click"><i class="fa fa-share"></i> OK
                    </asp:LinkButton>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
