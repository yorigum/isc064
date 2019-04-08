<%@ Page Language="c#" Inherits="ISC064.MARKETINGJUAL.Laporan.MasterCustomer" CodeFile="MasterCustomer.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Laporan Master Customer</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link href="/Media/Report.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Laporan Master Customer">
</head>
<body onkeyup="if(event.keyCode==27&&confirm('Apakah anda ingin menutup laporan ini?'))window.close()">
    <form id="Form1" method="post" runat="server">
        <div style="display: none">
            <uc1:Head ID="Head1" runat="server"></uc1:Head>
        </div>
        <table id="param" runat="server" width="100%" cellspacing="3">
            <tr>
                <td>
                    <p class="comp" id="comp" runat="server"></p>
                    <h1 id="judul" class="title title-line" runat="server">Laporan Master Customer
                    </h1>
                    <table style="width:100%">
                        <tr style="vertical-align:top">
                            <td style="width:25%">
                                <p class="pparam">
                                    <b>Project :</b>
                                    <br>
                                    <asp:dropdownlist ID="project" runat="server" CssClass="ddl" Width="200" Rows="13">
                                        <asp:ListItem>SEMUA</asp:ListItem>
                                    </asp:dropdownlist>
                                </p>
                                <p class="pparam">
                                    <b>Bulan Lahir :</b>
                                    <br>
                                    <asp:ListBox ID="lahir" runat="server" CssClass="ddl" Width="200" Rows="13">
                                        <asp:ListItem>SEMUA</asp:ListItem>
                                    </asp:ListBox>
                                </p>
                            </td>
                            <td style="width:5%"></td>
                            <td style="width:25%">
                                <br />
                                <p class="pparam">
                                    <asp:CheckBox ID="namaCheck" runat="server" Text="<b>Nama :</b>" AutoPostBack="True" Checked="True" OnCheckedChanged="namaCheck_CheckedChanged"></asp:CheckBox>
                                    <asp:Label ID="namac" runat="server" CssClass="err"></asp:Label>
                                </p>
                                <p>
                                    <asp:CheckBoxList ID="nama" runat="server">
                                        <asp:ListItem Selected="True" Value="ABCD">A B C D</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="EFGH">E F G H</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="IJKL">I J K L</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="MNOP">M N O P</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="QRST">Q R S T</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="UVWX">U V W X</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="YZ09">Y Z 0..9</asp:ListItem>
                                    </asp:CheckBoxList>
                                </p>
                                <p class="pparam">
                                    <b>Periode Input :</b>
                                    <br>
                                    <asp:ListBox ID="input" runat="server" CssClass="ddl" Width="200" Rows="10">
                                        <asp:ListItem>SEMUA</asp:ListItem>
                                    </asp:ListBox>
                                </p>
                            </td>
                            <td style="width:5%"></td>
                            <td>
                                <p class="pparam">
                                    <b>Status</b><b>:</b>
                                    <asp:RadioButton ID="statusS" Text="SEMUA" runat="server" GroupName="status" style="padding-right:20px"></asp:RadioButton>
                                    <asp:RadioButton ID="statusA" Text="AKTIF" runat="server" GroupName="status" style="padding-right:20px" Checked="True"></asp:RadioButton>
                                    <asp:RadioButton ID="statusI" Text="INAKTIF" runat="server" GroupName="status" style="padding-right:20px"></asp:RadioButton>
                                </p>
                                <p class="pparam">
                                    <b>Sifat</b><b style="padding-left:10px">:</b>
                                    <asp:RadioButton ID="sifatALL" Text="SEMUA" runat="server" GroupName="sifat" Checked="True" style="padding-right:20px"></asp:RadioButton>
                                    <asp:RadioButton ID="sifatSUDAH" Text="SUDAH BELI" runat="server" GroupName="sifat" style="padding-right:20px"></asp:RadioButton>
                                    <asp:RadioButton ID="sifatBELUM" Text="BELUM BELI" runat="server" GroupName="sifat" style="padding-right:20px"></asp:RadioButton>
                                </p>
                                <table cellpadding="0" cellspacing="0">
                                    <tr valign="top">
                                        <td>
                                            <p class="pparam">
                                                <b>Sales Account :</b>
                                                <br>
                                                <asp:ListBox ID="agentinput" runat="server" CssClass="ddl" Width="200" Rows="15">
                                                    <asp:ListItem>SEMUA</asp:ListItem>
                                                </asp:ListBox>
                                            </p>
                                        </td>
                                        <td style="display:none" width="40"></td>
                                        <td style="display:none">
                                            <p class="pparam">
                                                <asp:CheckBox ID="agamaCheck" runat="server" Text="<b>Agama :</b>" AutoPostBack="True" Checked="True" OnCheckedChanged="agamaCheck_CheckedChanged"></asp:CheckBox>
                                                <asp:Label ID="agamac" runat="server" CssClass="err"></asp:Label>
                                            </p>
                                            <asp:CheckBoxList ID="agama" runat="server">
                                                <asp:ListItem Selected="True">ISLAM</asp:ListItem>
                                                <asp:ListItem Selected="True">KRISTEN</asp:ListItem>
                                                <asp:ListItem Selected="True">KATOLIK</asp:ListItem>
                                                <asp:ListItem Selected="True">BUDHA</asp:ListItem>
                                                <asp:ListItem Selected="True">HINDU</asp:ListItem>
                                                <asp:ListItem Selected="True">KONGHUCU</asp:ListItem>
                                                <asp:ListItem Selected="True">LAINNYA</asp:ListItem>
                                            </asp:CheckBoxList>
                                        </td>
                                        <td style="display:none" width="40"></td>
                                        <td style="display:none">
                                            <p class="pparam">
                                                <asp:CheckBox ID="sumberdataCheck" runat="server" Text="<b>Sumber Data :</b>" AutoPostBack="True"
                                                    Checked="True" OnCheckedChanged="sumberdataCheck_CheckedChanged"></asp:CheckBox>
                                                <asp:Label ID="sumberdatac" runat="server" CssClass="err"></asp:Label>
                                            </p>
                                            <asp:CheckBoxList ID="sumberdata" runat="server">
                                                <asp:ListItem Selected="True">WALK IN</asp:ListItem>
                                                <asp:ListItem Selected="True">CALL IN</asp:ListItem>
                                                <asp:ListItem Selected="True">CANVAS</asp:ListItem>
                                                <asp:ListItem Selected="True">IKLAN</asp:ListItem>
                                                <asp:ListItem Selected="True">BUYER GET BUYER</asp:ListItem>
                                                <asp:ListItem Selected="True">REFERENSI</asp:ListItem>
                                                <asp:ListItem Selected="True">PEMBELI LAMA</asp:ListItem>
                                                <asp:ListItem Selected="True">PAMERAN</asp:ListItem>
                                                <asp:ListItem Selected="True">LAINNYA</asp:ListItem>
                                                <asp:ListItem Selected="True"></asp:ListItem>
                                            </asp:CheckBoxList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <br>
                    <div class="ins">
                        <table>
                            <tr>
                                <td>
                                    <asp:LinkButton ID="scr" runat="server" CssClass="btn btn-blue" AccessKey="s" OnClick="scr_Click">
											<i class="fa fa-search"></i> Screen Preview
                                    </asp:LinkButton>
                                    <asp:Button ID="xls" runat="server" Text="Download Excel" CssClass="btn btn-green" AccessKey="e" OnClick="xls_Click"></asp:Button>
                                    <asp:Button ID="pdf" runat="server" Text="Download PDF" CssClass="btn btn-orange" AccessKey="e" OnClick="pdf_Click"></asp:Button>

                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
        <div id="headReport" runat="server"></div>
        <br />
        <asp:Table ID="rpt" runat="server" CssClass="tb blue-skin" CellSpacing="1">
            <asp:TableRow VerticalAlign="Bottom">
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">No. Urut</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">Nama</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">Tipe</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">Sumber</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">No. Unit</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">Sales<br>Account</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">Nama<br>Bisnis</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">Jenis<br>Bisnis</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">Merek<br>Bisnis</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">Agama</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">Tgl.<br>Lahir</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">No.<br>Telepon</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">No.<br>HP</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">No.<br>Kantor</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">No.<br>Fax</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">Alamat<br>Email</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">No.<br>KTP / Identitas</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">Alamat<br>Surat Menyurat</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">Alamat<br>KTP / Identitas</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">NPWP</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">Alamat<br>NPWP</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">Sifat</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">No. Unit</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" BackColor="#1E90FF" ForeColor="White">Luas SG</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" BackColor="#1E90FF" ForeColor="White">Harga<br>Terakhir</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">Cara<br>Bayar</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">Tgl.<br>SP</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">Tgl.<br>Input</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">Transaksi<br>Terakhir</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">Project</asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>
    </form>
</body>
</html>
