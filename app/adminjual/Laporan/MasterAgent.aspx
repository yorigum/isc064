<%@ Page Language="c#" Inherits="ISC064.ADMINJUAL.Laporan.MasterAgent" CodeFile="MasterAgent.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>
<!DOCTYPE html>
<html lang="en">
<head>
    <title>Master Marketing</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Report.css" type="text/css" rel="stylesheet">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Laporan Master Sales">
</head>
<body class="body-padding" onkeyup="if(event.keyCode==27&&confirm('Apakah anda ingin menutup laporan ini?'))window.close()">
    <form id="Form1" method="post" runat="server">
        <div style="display: none">
            <uc1:Head ID="Head1" runat="server"></uc1:Head>
        </div>
        <table id="param" runat="server" width="100%" cellspacing="3">
            <tr>
                <td>
                    <div class="underline">
                        <p class="comp" id="comp" runat="server"></p>
                        <h1 id="judul" runat="server" class="title">Master Marketing
                        </h1>
                    </div>
                    <div class="form-model">
                        <div class="form-inline col">
                            <div class="pparam">
                                <p class="lbl">Project :</p>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <asp:DropDownList runat="server" ID="project" Width="200px" AutoPostBack="true" OnSelectedIndexChanged="project_SelectedIndexChanged">
                                                    <asp:ListItem>SEMUA</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <br />
                                <p class="lbl">Status :</p>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <asp:RadioButton ID="statusS" Text="SEMUA" runat="server" GroupName="status"></asp:RadioButton>
                                            </td>
                                            <td>
                                                <asp:RadioButton ID="statusA" Text="AKTIF" runat="server" GroupName="status" Checked="True"></asp:RadioButton>
                                            </td>
                                            <td>
                                                <asp:RadioButton ID="statusI" Text="INAKTIF" runat="server" GroupName="status"></asp:RadioButton>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <div class="form-inline col">
                            <div class="pparam">
                                <p class="lbl">Nama :</p>
                                <div class="item">
                                    <asp:Label ID="namac" runat="server" CssClass="err"></asp:Label>
                                    <asp:CheckBox ID="namaCheck" runat="server" Text="Semua" AutoPostBack="True" Checked="True" OnCheckedChanged="namaCheck_CheckedChanged"></asp:CheckBox>
                                </div>
                            </div>
                            <asp:CheckBoxList ID="nama" runat="server" RepeatColumns="7">
                                <asp:ListItem Selected="True" Value="ABCD">A B C D</asp:ListItem>
                                <asp:ListItem Selected="True" Value="EFGH">E F G H</asp:ListItem>
                                <asp:ListItem Selected="True" Value="IJKL">I J K L</asp:ListItem>
                                <asp:ListItem Selected="True" Value="MNOP">M N O P</asp:ListItem>
                                <asp:ListItem Selected="True" Value="QRST">Q R S T</asp:ListItem>
                                <asp:ListItem Selected="True" Value="UVWX">U V W X</asp:ListItem>
                                <asp:ListItem Selected="True" Value="YZ09">Y Z 0..9</asp:ListItem>
                            </asp:CheckBoxList>
                        </div>
                        <div>
                            <p class="pparam">
                                <b>Periode Input :</b>
                                <br>
                                <asp:ListBox ID="input" runat="server" CssClass="ddl" Width="200" Rows="10">
                                    <asp:ListItem>SEMUA</asp:ListItem>
                                </asp:ListBox>
                            </p>
                            <p class="pparam">
                                <b>Jabatan :</b>
                                <br>
                                <asp:ListBox ID="principal" runat="server" Width="200" CssClass="ddl" Rows="10">
                                    <asp:ListItem>SEMUA</asp:ListItem>
                                </asp:ListBox>
                            </p>
                            <p class="pparam">
                                <b>Tipe Marketing :</b>
                                <br>
                                <asp:ListBox ID="tipe" runat="server" Width="200" CssClass="ddl" Rows="10">
                                    <asp:ListItem>SEMUA</asp:ListItem>
                                </asp:ListBox>
                            </p>
                        </div>
                        <br>
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                    <div class="ins">
                        <table>
                            <tr>
                                <td>
                                    <asp:LinkButton ID="scr" runat="server" CssClass="btn btn-blue" AccessKey="s" OnClick="scr_Click">
											<i class="fa fa-search"></i> Screen Preview
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="xls" runat="server" CssClass="btn btn-green" AccessKey="e" OnClick="xls_Click">
											Download Excel
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="pdf" runat="server" CssClass="btn btn-orange" AccessKey="e" OnClick="pdf_Click">
											Download PDF
                                    </asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
        <div id="headReport" runat="server">
        </div>
        <br />
        <asp:Table ID="rpt" runat="server" CssClass="tb blue-skin" CellSpacing="1">
            <asp:TableRow VerticalAlign="Bottom">
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">No.</asp:TableHeaderCell>
                <%--<asp:TableHeaderCell HorizontalAlign="Left">Tgl. Input</asp:TableHeaderCell>--%>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">Tipe</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">Level</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">Atasan</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">Kode Marketing</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">Nama Marketing</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">Status</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">Tgl Inaktif</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">Alamat</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">Email</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">Telepon</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">Handphone</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">Whatsapp</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">NPWP</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">Rekening</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">Bank</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Style="width: 200px" BackColor="#1E90FF" ForeColor="White">Atas Nama Rekening</asp:TableHeaderCell>
                <%--<asp:tableheadercell horizontalalign="Left">Skema Komisi</asp:tableheadercell>--%>
            </asp:TableRow>
        </asp:Table>
    </form>
</body>
</html>
