<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FollowUpEdit.aspx.cs" Inherits="ISC064.COLLECTION.FollowUpEdit" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>

<!DOCTYPE html>

<html>
<head>
    <title>Edit Pemberitahuan Follow Up</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="P. Jatuh Tempo - Edit Pemberitahuan Follow Up(Marketing)">
</head>
<body class="body-padding" onkeyup="if(event.keyCode==27&&confirm('Apakah anda ingin membatalkan proses registrasi?')) document.getElementById('cancel').click();">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <input type="text" style="display: none">
        <h1 class="title title-line">Edit Follow Up </h1>
        <br />
        <table cellspacing="5">
            <tr>
                <td><b>No Follow Up</b></td>
                <td><b>:</b></td>
                <td>
                    <asp:Label ID="NoFolup" runat="server" Font-Bold="True"></asp:Label>
                </td>
                <td><b>Unit</b></td>
                <td><b>:</b></td>
                <td>
                    <asp:Label ID="unit" runat="server" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td><b>Ref.</b></td>
                <td><b>:</b></td>
                <td>
                    <asp:Label ID="referensi" runat="server" Font-Bold="True"></asp:Label>
                </td>
                <td><b>Customer</b></td>
                <td><b>:</b></td>
                <td>
                    <asp:Label ID="customer" runat="server" Font-Bold="True"></asp:Label>
                </td>
                <td style="padding-left:50%">
                    <label class="ibtn ibtn-remove">
                         <input type="button" class="btn btn-red btn-ico" value="Delete" id="btndel" runat="server" name="btndel"
                          accesskey="d">
                    </label>
                </td>
            </tr>
        </table>
        <hr size="1" noshade color="silver">
        <table cellspacing="5">
            <tr>
                <td><b>Tgl. Follow UP</b></td>
                <td><b>:</b></td>
                <td>
                    <asp:TextBox ID="tgl" runat="server" type="text" CssClass="txt_center" Width="150px" ReadOnly="true" Enabled="false"></asp:TextBox>
                    <button class="btn-a default" runat="server" onclick="openCalendar('tgl');" type="button" disabled="disabled">
                        <i class="fa fa-calendar"></i>
                    </button>
                    <asp:Label ID="tglc" runat="server" CssClass="err"></asp:Label>
                </td>
            </tr>
            <tr>
                <td><b>Grouping</b></td>
                <td><b>:</b></td>
                <td>
                    <asp:RadioButtonList ID="rblgrup" runat="server" RepeatDirection="Horizontal"></asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td><b>Keterangan</b></td>
                <td>:</td>
                <td><asp:TextBox ID="keterangan" runat="server" CssClass="input-text" Width="300px"></asp:TextBox></td>
            </tr>
            <tr>
                <td><b>No. Hp:</b></td>
                <td><b>:</b></td>
                <td>
                    <asp:TextBox ID="notelp" runat="server" CssClass="input-text" Width="300px" MaxLength="50"></asp:TextBox>
                </td>
            </tr>
            <tr valign="top">
                <td rowspan="3"><b>Alamat</b></td>
                <td rowspan="3"><b>:</b></td>
                <td>
                    <p>
                        <asp:TextBox ID="alamat1" runat="server" CssClass="input-text" Width="300px" MaxLength="50"></asp:TextBox></p>
                </td>
            </tr>
            <tr>
                <td>
                    <p>
                        <asp:TextBox ID="alamat2" runat="server" CssClass="input-text" Width="300px" MaxLength="50"></asp:TextBox></p>
                </td>
            </tr>
            <tr>
                <td>
                    <p>
                        <asp:TextBox ID="alamat3" runat="server" CssClass="input-text" Width="150px" MaxLength="50"></asp:TextBox></p>
                </td>
            </tr>
        </table>
        <br>
        <table class="tb blue-skin" cellspacing="1">
				<tr align="left" valign="bottom">
					<th>
						No.</th>
					<th>
						Tagihan</th>
					<th>
						Tipe</th>
					<th>
						Jatuh Tempo</th>
					<th align="right">
						Sisa Tagihan
					</th>
                    <th>Tgl Janji Bayar</th>
				</tr>
                <asp:placeholder id="list" runat="server"></asp:placeholder>
		</table>
        <table height="50">
            <tr>
                <td>
                    <asp:LinkButton ID="save" runat="server" Width="75" CssClass="btn btn-blue" OnClick="save_Click"><i class="fa fa-share"></i> OK</asp:LinkButton></td>
                <td>
                    <br />
                </td>
                <td>
                    <input class="btn btn-red" id="cancel" style="width: 75px" onclick="location.href = 'FollowUp.aspx'"
                        type="button" value="Cancel" name="cancel" runat="server">
                </td>
            </tr>
        </table>
        <script type="text/javascript">
            function tagihan(no, nilai, foo) {
                if (foo.checked)
                    document.getElementById('lunas_' + no).value = nilai;
                else
                    document.getElementById('lunas_' + no).value = "";

                hitunggt();
            }
            function hitunggt() {
                foogt = document.getElementById('gt');
                grandtotal = 0 * 1;

                eof = false;
                i = 0 * 1;
                while (!eof) {
                    foo = document.getElementById('lunas_' + i);
                    if (!foo) {
                        eof = true;
                        break;
                    }
                    else {
                        total = cvtnum(foo.value);
                        if (!isNaN(total))
                            grandtotal = grandtotal + (total * 1);
                        i++;
                    }
                }

                finalnet = Math.round(100 * grandtotal) / 100;
                eval("foogt.value = FinalFormat('" + finalnet + "')");
            }
            function cvtnum(foo) {
                return foo.replace(/,/gi, "");
            }
        </script>
    </form>
</body>
</html>
