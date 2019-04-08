<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>

<%@ Page Language="c#" Inherits="ISC064.MARKETINGJUAL.Laporan.LaporanComplain" CodeFile="LaporanComplain.aspx.cs" %>

<!DOCTYPE html>
<html>
<head>
    <title>Laporan Complain</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Report.css" type="text/css" rel="stylesheet">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Laporan Complain">
</head>
<body onkeyup="if(event.keyCode==27&amp;&amp;confirm('Apakah anda ingin menutup laporan ini?'))window.close()">
    <form id="Form1" method="post" runat="server">
        <div style="display: none">
            <uc1:Head ID="Head1" runat="server"></uc1:Head>
        </div>
        <table id="param" cellspacing="3" width="100%" runat="server">
            <tr>
                <td colspan="3">
                    <p class="comp" id="comp" runat="server"></p>
                    <h1 id="judul" runat="server" class="title">Laporan Complain</h1>
                    <p class="pparam"><b>Tanggal Complain :</b></p>
                    <table style="margin-top: 10px">
                        <tr>
                            <td>dari</td>
                            <td>
                                <asp:TextBox ID="dari" runat="server" CssClass="txt_center" Width="85"></asp:TextBox>
                                <label for="dari" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                            </td>
                            <td rowspan="2">&nbsp;&nbsp;</td>
                            <td>sampai</td>
                            <td>
                                <asp:TextBox ID="sampai" runat="server" CssClass="txt_center" Width="85">

                                </asp:TextBox>
                                <label for="sampai" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="daric" runat="server" CssClass="err"></asp:Label></td>
                            <td>
                                <asp:Label ID="sampaic" runat="server" CssClass="err"></asp:Label></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <p class="pparam"><b>Project</b><b style="padding-left: 63px">:</b></p>
                    <asp:DropDownList ID="project" runat="server" Width="200px" Style="margin-top: 15px" AutoPostBack="true" OnSelectedIndexChanged="project_SelectedIndexChanged">
                        <asp:ListItem>SEMUA</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <p class="pparam"><b>Perusahaan</b><b style="padding-left: 35px">:</b></p>
                    <asp:DropDownList ID="pers" runat="server" Width="200px" Style="margin-top: 15px">
                        <asp:ListItem>SEMUA</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td></td>
            </tr>
            <tr>
                <td colspan="3">
                    <p class="pparam"><b>Customer</b><b style="padding-left: 50px">:</b></p>
                    <br />
                    <asp:TextBox ID="customer" runat="server" CssClass="txt_center" Width="85"></asp:TextBox>
                    <input type="button" value="&#xf002;" style="font-family: 'fontawesome'" class="btn btn-orange" onclick="popDaftarCustomer('a');"
                        id="btnpop" runat="server" name="btnpop" />
                </td>
            </tr>
            <tr>
                <td></td>
            </tr>
            <tr>
                <td style="width: 100px">
                    <p class="pparam"><b>Complain</b><b style="padding-left: 50px">:</b></p>
                    <asp:DropDownList ID="List" runat="server" Width="100px" Style="margin-top: 15px">
                        <asp:ListItem Value="0">SEMUA</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td></td>
            </tr>
            <tr>
                <td>
                    <p class="pparam"><b>Status</b><b style="padding-left: 69px">:</b></p>
                    <asp:DropDownList ID="status" runat="server" Width="100px" Style="margin-top: 15px">
                        <asp:ListItem Value="2">SEMUA</asp:ListItem>
                        <asp:ListItem Value="0">Unsolved</asp:ListItem>
                        <asp:ListItem Value="1">Solved</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 100%">
                    <%-- <h5>Jenis Complain</h5>
			<asp:DropDownList ID="jc" runat="server">
			    <asp:ListItem Value="0">SEMUA</asp:ListItem>
						            <asp:ListItem Value="1">1</asp:ListItem>
						            <asp:ListItem Value="2">2</asp:ListItem>
						        </asp:DropDownList>--%>
                    <div class="ins">
                        <table>
                            <tr>
                                <td>
                                    <asp:LinkButton ID="scr" AccessKey="s" runat="server" CssClass="btn btn-blue" OnClick="scr_Click">
											<i class="fa fa-search"></i> Screen Preview
                                    </asp:LinkButton>
                                    <asp:Button ID="xls" AccessKey="e" runat="server" Text="Download Excel" CssClass="btn btn-green" OnClick="xls_Click"></asp:Button>
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
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">No. Urut</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">Tgl. Complain</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">Jenis Complain</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">PIC</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">No. Kontrak</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">Customer</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">Unit</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">Keterangan</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">Solusi</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">Tgl. Solved</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">Status</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">Project</asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>
    </form>
</body>
<script type="text/javascript">
    function call(nomor) {
        document.getElementById('customer').value = nomor;
    }
</script>
</html>
