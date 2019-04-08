<%@ Page Language="c#" Inherits="ISC064.NUP.DaftarCustomer2" CodeFile="DaftarCustomer2.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Daftar Customer</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="(Pop-Up) Daftar Customer">
    <meta http-equiv="pragma" content="no-cache">
    <base target="_self">
</head>
<body class="pop" onkeyup="if(event.keyCode==27){window.close()}"
    onload="document.getElementById('keyword').select()">
    <form id="Form1" method="post" runat="server">
        <div class="pad">
            <table>
                <tr>
                    <td>
                        <asp:TextBox ID="keyword" runat="server" CssClass="txt" Width="400"></asp:TextBox>
                    </td>
                    <td>
                        <asp:DropDownList ID="metode" runat="server" CssClass="ddl">
                            <asp:ListItem>Semua</asp:ListItem>
                            <asp:ListItem>Customer Aktif</asp:ListItem>
                            <asp:ListItem>Customer Inaktif</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    	<td>
							<asp:dropdownlist id="project" runat="server" cssclass="ddl form-control" style='width: 100%'>
							</asp:dropdownlist>
						</td>
                    <td>
                        <asp:Button ID="search" runat="server" CssClass="btn" Text="Search" AccessKey="s" OnClick="search_Click"></asp:Button>
                    </td>
                </tr>
            </table>
        </div>
        <br>
        <div class="pad">
            <asp:GridView ID="tb" runat="server" SkinID="pager" OnPageIndexChanging="tb_PageIndexChanging">
                <Columns>
                    <asp:BoundField HeaderText="Nama" DataField="Nama" />
                    <asp:BoundField HeaderText="Alamat " DataField="Alamat" />
                </Columns>
            </asp:GridView>
        </div>
        <input type="text" style="display: none;">
        <script language="javascript">
            function call(no, nm, hp, telp, email, tgllahir, noktp, npwp, ktp1, ktp2, ktp3, ktp4, sifat, kor1, kor2, kor3, kor4, rekb, rekc, rekno, reknam) {                
               
                window.parent.call(no, nm, hp, telp, email, tgllahir, noktp, npwp, ktp1, ktp2, ktp3, ktp4, sifat, kor1, kor2, kor3, kor4, rekb, rekc, rekno, reknam);
                window.parent.globalclose();
            }
        </script>
    </form>
</body>
</html>
