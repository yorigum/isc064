<%@ Page Language="c#" Inherits="ISC064.ADMINJUAL.PetaDaftar" CodeFile="PetaDaftar.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Peta Floor Plan</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Registrasi Peta">
    <style>
        .listitem ul {
            list-style: square;
            font-size: 16px;
            margin-left: 3px !important;
        }

        .listitem a {
            text-decoration: none;
        }

        .listitem .link {
            font-weight: bold;
            color: #494949 !important;
        }
    </style>
</head>
<body class="default-content">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <h1>Peta Floor Plan</h1>
        <p class="feed">
            <asp:Label ID="feed" runat="server"></asp:Label>
        </p>
        <div class="listitem">
            <table cellspacing="5">
                <tr id="proj" runat="server" visible="false">
                    <td>Project</td>
                    <td>:</td>
                    <td>
                        <asp:DropDownList runat="server" ID="project">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td valign="top">Nama Peta</td>
                    <td valign="top">:</td>
                    <td>
                        <asp:TextBox ID="namapeta" runat="server" CssClass="txt" MaxLength="100" Width="300"></asp:TextBox>

                        <p colspan="3" style="font: 7pt; color: gray; font-style: italic;">
                            Field ini akan menjadi <u>nama peta floor plan</u> yang akan muncul di dalam 
						program
                        </p>
                    </td>
                </tr>
                <tr>
                    <td>Tipe</td>
                    <td>:</td>
                    <td>
                        <asp:RadioButtonList ID="tipe" runat="server" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="tipe_SelectedIndexChanged">
                            <asp:ListItem Selected="True">Parent</asp:ListItem>
                            <asp:ListItem>Child</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr id="trpetadasar" runat="server" visible="false">
                    <td valign="top">File Peta Dasar</td>
                    <td valign="top">:</td>
                    <td>
                        <asp:FileUpload ID="file1" runat="server" class="txt" Style="width: 500px" />
                        <asp:Label runat="server" ID="file1c" CssClass="err"></asp:Label>
                        <p colspan="3" style="font: 8pt; color: gray; font-style: italic;">
                            File gambar yang akan menjadi peta dasar.
						<br>
                            Formatnya adalah <u>JPG dengan mode warna RGB</u>.
						<br>
                            Dimensi yang dianjurkan adalah : 700 x 400
                        </p>
                    </td>
                </tr>
                <%--<tr id="trpetatransparent" runat="server" visible="false">
                    <td valign="top">File Peta Transparent</td>
                    <td valign="top">:</td>
                    <td>
                        <asp:FileUpload ID="file2" runat="server" class="txt" Style="width: 500px" />
                        <asp:Label runat="server" ID="file2c" CssClass="err"></asp:Label>
                        <p colspan="3" style="font: 8pt; color: gray; font-style: italic;">
                            File gambar yang akan menjadi peta dasar.
						<br>
                            Formatnya adalah <u>PNG </u>.
						<br>
                            Dimensi yang dianjurkan adalah : sama dengan gambar Peta Dasar
                        </p>
                    </td>
                </tr>--%>
                <tr>
                    <td></td>
                    <td></td>
                    <td>
                        <asp:Button ID="save" runat="server" Text="Save" Width="100" OnClick="save_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
