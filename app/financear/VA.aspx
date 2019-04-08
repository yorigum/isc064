<%@ Page Language="c#" Inherits="ISC064.FINANCEAR.VA" CodeFile="VA.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html >
<html>
<head>
	<title>Virtual Account</title>
	<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
	<meta name="CODE_LANGUAGE" content="C#">
	<meta name="vs_defaultClientScript" content="JavaScript">
	<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	<link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
	<meta name="ctrl" content="1">
	<meta name="sec" content="Virtual Account">
</head>
<body class="body-padding">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <input type="text" style="display: none">
        <h1 class="title title-line">Virtual Account</h1>
        <br />
        <table style="border: 1px solid #DCDCDC" cellspacing="5">
            <tr>
                <td>
                    <input type="button" class="btn btn-blue" value="Search" show-modal='#ModalPopUp' modal-title='Daftar Virtual Account' modal-url='DaftarVA.aspx' id="search" runat="server" name="search" accesskey="s">
                </td>
                <td>
                    <asp:DropDownList ID="bank" runat="server" CssClass="select-dropdown" Width="180">
                        <asp:ListItem Value="">Bank :</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="project" runat="server" CssClass="select-dropdown" Width="180" OnSelectedIndexChanged="project_SelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem Value="">Project :</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="display" runat="server" CssClass="btn btn-blue" Text="Display" OnClick="display_Click"></asp:Button>
                </td>
            </tr>
        </table>
        <br>
        <asp:GridView ID="tb" runat="server" SkinID="pager" OnPageIndexChanging="tb_PageIndexChanging">
            <Columns>
                <asp:BoundField HeaderText="No. VA" DataField="VA" />
                <asp:BoundField HeaderText="Bank " DataField="Bank" />
                <asp:BoundField HeaderText="No. Unit" DataField="Unit" />
                <asp:BoundField HeaderText="No. Kontrak" DataField="Kontrak" />
                <asp:BoundField HeaderText="Customer" DataField="Customer" />
                <asp:BoundField HeaderText="Project" DataField="Project" />
            </Columns>
        </asp:GridView>
        <script type="text/javascript">
            function call(nomor) {
                popEditVA(nomor);
            }
        </script>

	</form>
</body>
</html>
