<%@ Page language="c#" Inherits="ISC064.FINANCEAR.MasterCB" CodeFile="MasterCB.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html >
<html>
<head>
    <title>Refund Lebih Bayar</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Refund Lebih Bayar">
</head>
<body class="body-padding">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <h1 class="title title-line">Refund Lebih Bayar</h1>
        <br />
        <input type="text" style="display: none">
        <table style="border: 1px solid #DCDCDC" cellspacing="5">
            <tr>
                <td><b>Search by</b></td>
                <td>
                    <asp:DropDownList ID="rek" runat="server" Width="180">
                        <asp:ListItem Value="">Rekening :</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td colspan="2">
                    <asp:DropDownList ID="project" runat="server" Width="180" AutoPostBack="true" OnSelectedIndexChanged="project_SelectedIndexChanged">
                        <asp:ListItem Value="">Project :</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <input type="button" class="btn btn-blue" value="Search" show-modal='#ModalPopUp' modal-title='Daftar Refund Lebih Bayar' modal-url='DaftarTB.aspx' id="search" runat="server"
                        name="search" accesskey="s">
						<%--<input type="button" class="btn btn-blue" value="Search" onclick="popDaftarTB();" id="search" runat="server"
							name="search" accesskey="s">--%>
                </td>
            </tr>
            <tr>
                <td><b>Dari</b></td>
                <td>
                    <asp:TextBox ID="dari" runat="server" CssClass="txt_center" Width="135"></asp:TextBox>
                    <label for="dari" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                    <asp:Label ID="daric" runat="server" CssClass="err"></asp:Label>
                </td>
                <td><b style="padding-left: 10px;">Sampai</b></td>
                <td>
                    <asp:TextBox ID="sampai" runat="server" CssClass="txt_center" Width="135"></asp:TextBox>
                    <label for="sampai" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                    <asp:Label ID="sampaic" runat="server" CssClass="err"></asp:Label>
                </td>
                <td>
                    <asp:Button ID="display" runat="server" CssClass="btn btn-blue" Text="Display" OnClick="display_Click"></asp:Button>
                </td>
            </tr>
            <tr style="display: none;" runat="server">
                <td></td>
                <td></td>
                <td colspan="4">
                    <b>Status </b>
                    <asp:RadioButton ID="statusA" runat="server" Text="SEMUA" Checked="True" GroupName="status"></asp:RadioButton>
                    <asp:RadioButton ID="statusB" runat="server" Text="BARU" GroupName="status"></asp:RadioButton>
                    <asp:RadioButton ID="statusID" runat="server" Text="IDENTIFIKASI" GroupName="status"></asp:RadioButton>
                    <asp:RadioButton ID="statusS" runat="server" Text="SOLVE" GroupName="status"></asp:RadioButton>
                </td>
            </tr>
        </table>
        <br>
        <p style="padding: 3px; font: 8pt">
        </p>
        <asp:GridView ID="tb" runat="server" SkinID="pager" OnPageIndexChanging="tb_PageIndexChanging">
            <Columns>
                <asp:BoundField HeaderText="No. CB" DataField="CB" />
                <asp:BoundField HeaderText="No. Kontrak " DataField="Kontrak" />
                <asp:BoundField HeaderText="Unit" DataField="Unit" />
                <asp:BoundField HeaderText="Customer" DataField="Customer" />
                <asp:BoundField HeaderText="Tgl Pengembalian" DataField="Tgl" />
                <asp:BoundField HeaderText="Sisa Tagihan" DataField="Sisa" ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField HeaderText="Lebih Bayar" DataField="Lebih" ItemStyle-HorizontalAlign="Right"  />
                <asp:BoundField HeaderText="Bank Keluar" DataField="Keluar" ItemStyle-HorizontalAlign="Right"  />
                <asp:BoundField HeaderText="Project" DataField="Project" />
            </Columns>
        </asp:GridView>
        <script type="text/javascript">
            function call(nomor) {
                popEditCB(nomor);
            }
        </script>
    </form>
</body>
</html>
