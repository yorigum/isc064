<%@ Page Language="c#" Inherits="ISC064.COLLECTION.Laporan.LapTunggakan" CodeFile="LapTunggakan.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Laporan Tunggakan</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Report.css" type="text/css" rel="stylesheet">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Laporan Tunggakan">
</head>
<body class="body-padding" onkeyup="if(event.keyCode==27&amp;&amp;confirm('Apakah anda ingin menutup laporan ini?'))window.close()">
    <form id="Form1" method="post" runat="server">
        <div style="display: none">
            <uc1:Head ID="Head1" runat="server"></uc1:Head>
        </div>
        <table id="param" runat="server" width="100%" cellspacing="3">
            <tr>
                <td>
                    <p class="comp" id="comp" runat="server"></p>
                    <h1 id="judul" runat="server" class="title title-line">Laporan Tunggakan
                    </h1>
                    <div class="form-model">
                        <div class="form-inline col">
                            <div class="pparam">
                                <asp:Label ID="Label2" runat="server"><b>Perusahaan</b><b style="padding-left:20px;font-size:12px">:</b></asp:Label>
                            </div>
                            <table>
                                <tbody>
                                    <tr>
                                        <td>
                                            <br />
                                            <asp:DropDownList ID="pers" runat="server" Width="200" AutoPostBack="true" OnSelectedIndexChanged="pers_SelectedIndexChanged">
                                                <asp:ListItem>SEMUA</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div class="form-inline col">
                            <div class="pparam">
                                <asp:Label ID="Label1" runat="server"><b>Project</b><b style="padding-left:58px;font-size:12px">:</b></asp:Label>
                            </div>
                            <table>
                                <tbody>
                                    <tr>
                                        <td>
                                            <br />
                                            <asp:DropDownList ID="project" runat="server" Width="200" AutoPostBack="true" OnSelectedIndexChanged="project_SelectedIndexChanged">
                                                <asp:ListItem>SEMUA</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div class="form-inline col">
                            <div class="pparam">
                                <p class="lbl" style="padding-right:10px">Lokasi </p>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <b style="padding-right:10px;vertical-align:top">:</b>                                            
                                                <asp:ListBox ID="lokasi" runat="server" Width="200" CssClass="ddl" Rows="10">                                            
                                                     <asp:ListItem>SEMUA</asp:ListItem>
                                                </asp:ListBox>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <div class="form-inline col">
                            <div class="pparam">
                                <p class="lbl" style="padding-right:10px">Status</p>                                
                                <table>
                                    <tbody>                                        
                                        <tr>    
                                            <td>
                                                <b style="padding-right:10px">:</b>                                                                                                                            
                                                <asp:RadioButton ID="statusS" Text="SEMUA" runat="server" GroupName="status" style="padding-right:50px"></asp:RadioButton>
                                                <asp:RadioButton ID="statusA" Text="AKTIF" runat="server" GroupName="status" style="padding-right:50px" Checked="True"></asp:RadioButton>
                                                <asp:RadioButton ID="statusB" Text="BATAL" runat="server" GroupName="status" style="padding-right:50px"></asp:RadioButton>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <div class="form-inline col">
                            <div class="pparam">   
                                <p class="lbl" style="padding-right:10px">As of </p>
                                <table>
                                    <tbody>                                        
                                        <tr>    
                                            <td>
                                            <b style="padding-right:10px">:</b>
                                            <asp:TextBox ID="dari" runat="server" type="text" CssClass="txt_center"></asp:TextBox>
                                            <label for="dari" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                                            <asp:Label ID="daric" runat="server" CssClass="err"></asp:Label></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <div class="form-inline col">
                            <div class="pparam">
                                <p class="lbl" style="padding-right:10px">Status KPR</p>
                                <table>
                                    <tbody>                                        
                                        <tr>
                                            <td>
                                                <b style="padding-right:10px">:</b>
                                                <asp:RadioButton ID="kpa1" runat="server" Text="INCLUDE TAGIHAN KPR" GroupName="kpa" style="padding-right:30px"></asp:RadioButton>
                                                <asp:RadioButton ID="kpa2" runat="server" Text="EXCLUDE TAGIHAN KPR" GroupName="kpa" Checked="True"></asp:RadioButton>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <div class="form-inline col">
                            <div class="pparam">
                                <asp:CheckBox ID="carabayarCheck" runat="server" Text="<b>Cara Bayar:</b>"
                                    Checked="True" AutoPostBack="True"
                                    OnCheckedChanged="carabayarCheck_CheckedChanged"></asp:CheckBox>
                                <asp:Label ID="carabayarc" runat="server" CssClass="err"></asp:Label>
                            </div>
                            <table>
                                    <tbody>                                        
                                        <tr>
                                            <td>
                                                <br />
                            <asp:CheckBoxList ID="carabayar" runat="server" RepeatColumns="1">
                                <asp:ListItem Selected="True" Value="KPR">KPR</asp:ListItem>
                                <asp:ListItem Selected="True" Value="CASH BERTAHAP">CASH BERTAHAP</asp:ListItem>
                                <asp:ListItem Selected="True" Value="CASH KERAS">CASH KERAS</asp:ListItem>
                            </asp:CheckBoxList>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>    
                        </div>
                        <div class="form-inline col pparam sub">
                            <div class="ins">
                            <table>
                                <tr>
                                    <td style="min-width: auto; padding-right: 10px">
                                        <asp:LinkButton ID="scr" AccessKey="s" runat="server" CssClass="btn btn-blue" OnClick="scr_Click"><i class="fa fa-search"></i> Screen Preview</asp:LinkButton>
                                    </td>
                                    <td>
                                        <asp:Button ID="xls" AccessKey="e" runat="server" Text="Download Excel" CssClass="btn btn-green" OnClick="xls_Click"></asp:Button>
                                    </td>
                                    <td>
                                        <asp:Button ID="pdf" runat="server" Text="Download PDF" CssClass="btn btn-orange" AccessKey="e" OnClick="pdf_Click"></asp:Button>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    </div>
                </td>
            </tr>
        </table>
        <div id="headReport" runat="server">
        </div>
        <br />
        <asp:Table ID="rpt" runat="server" CssClass="tb blue-skin" CellSpacing="1">
            <asp:TableRow VerticalAlign="Bottom">
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" BackColor="#1E90FF" ForeColor="White">No.</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" BackColor="#1E90FF" ForeColor="White">No. Tagihan</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" BackColor="#1E90FF" ForeColor="White">Customer</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" BackColor="#1E90FF" ForeColor="White">No. Telp</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" BackColor="#1E90FF" ForeColor="White">No. HP</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" BackColor="#1E90FF" ForeColor="White">No. Unit</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" BackColor="#1E90FF" ForeColor="White">Nama Tagihan</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" BackColor="#1E90FF" ForeColor="White">Tgl. Jatuh Tempo</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" Wrap="false" BackColor="#1E90FF" ForeColor="White">Telat (hari)</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" Wrap="false" BackColor="#1E90FF" ForeColor="White">Nilai Tagihan</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" Wrap="false" BackColor="#1E90FF" ForeColor="White">Pelunasan</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" Wrap="false" BackColor="#1E90FF" ForeColor="White">Outstanding</asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>
    </form>
</body>
</html>
