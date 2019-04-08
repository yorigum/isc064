<%@ Reference Page="~/Customer.aspx" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>

<%@ Page Language="c#" Inherits="ISC064.FINANCEAR.MemoRegistrasiMarketing" CodeFile="MemoRegistrasiMarketing.aspx.cs" %>

<!DOCTYPE html>
<html>
<head>
    <title>Registrasi Memo Pelunasan</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Memo Pelunasan - Registrasi Memo (Marketing)">
    <style type="text/css">
        #nilaitr TD {
            PADDING-TOP: 20px;
        }
    </style>
</head>
<body class="body-padding" onkeyup="keyup();">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <input type="text" style="display: none" />
        <h1 class="title title-line">Registrasi Memo Pelunasan</h1>
        <p><b><i>Halaman 2 dari 2</i></b></p>
        <br />
        <table cellspacing="5">
            <tr>
                <td>Tipe</td>
                <td>:</td>
                <td width="180">
                    <asp:Label ID="tipe" runat="server" Font-Bold="True"></asp:Label>
                </td>
                <td>Unit</td>
                <td>:</td>
                <td>
                    <asp:Label ID="unit" runat="server" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Ref.</td>
                <td>:</td>
                <td>
                    <asp:Label ID="referensi" runat="server" Font-Bold="True"></asp:Label>
                </td>
                <td>Customer</td>
                <td>:</td>
                <td>
                    <asp:Label ID="customer" runat="server" Font-Bold="True"></asp:Label>
                </td>
            </tr>
        </table>
        <hr size="1" noshade color="silver" />
        <br />
        <asp:RadioButtonList ID="tipememo" runat="server" RepeatColumns="2">
            <asp:ListItem class="radio" Value="PP">PP = Penghapusan Piutang</asp:ListItem>
            <asp:ListItem class="radio" Value="DN">DN = Diskon</asp:ListItem>
            <asp:ListItem class="radio" Value="TG">TG = Tukar Guling</asp:ListItem>
            <asp:ListItem class="radio" Value="SA">SA = Saldo Awal</asp:ListItem>
            <asp:ListItem class="radio" Value="PPA">PPA = Pengalihan Pembayaran</asp:ListItem>
            <asp:ListItem class="radio" Value="AL">AL = Alokasi Lebih Bayar</asp:ListItem>
        </asp:RadioButtonList>
        <br />
        <table>
            <tr>
                <td><b>Tanggal</b></td>
                <td>
                    <div class="input-group input-medium" style="margin-top: 0px; margin-left: 0px;">
                        <asp:TextBox ID="tglmemo" runat="server" type="text" CssClass="form-control" Style="width: 65%; height: 20px"></asp:TextBox>
                        <span class="input-group-btn" style="height: 34px; display: block">
                            <label for="tglmemo" class="btn-a default btn-cal"><i class="fa fa-calendar"></i></label>
                        </span>
                    </div>
                    <asp:Label ID="tglmemoc" runat="server" CssClass="err"></asp:Label>
                </td>
            </tr>
            <tr>
                <td><b>Keterangan</b></td>
                <td colspan="4">
                    <asp:TextBox ID="Ket" runat="server" Width="400px"></asp:TextBox>
                </td>
                <td></td>
                <td></td>
            </tr>
            <tr id="nilaitr" runat="server">
                <td><b>Nilai</b></td>
                <td>
                    <asp:TextBox ID="nilai" runat="server" Width="180" CssClass="txt_num"></asp:TextBox>
                </td>
                <td>
                    <asp:LinkButton ID="next" runat="server" CssClass="btn btn-blue" OnClick="next_Click">Next <i class="fa fa-arrow-right"></i></asp:LinkButton>
                </td>
                <td>
                    <input class="btn" id="cancel2" style="width: 75px" onclick="location.href='TTSRegistrasi.aspx'"
                        type="button" value="Cancel" name="cancel2" runat="server">
                </td>
            </tr>
        </table>
        <br />
        <div id="detildiv" runat="server">
            <table class="tb blue-skin">
                <tr>
                    <th width="100">No. Tagihan</th>
                    <th width="150">Tagihan</th>
                    <th>Tipe</th>
                    <th width="75">Jatuh Tempo</th>
                    <th align="right" width="120">Sisa Tagihan
                    </th>
                    <th align="right">Nilai Pembayaran</th>
                    <th>Tgl Pelunasan</th>
                    <th></th>
                </tr>
                <asp:PlaceHolder ID="list" runat="server"></asp:PlaceHolder>
                <tr>
                    <td colspan="5">
                        <b id="gtc" runat="server">Grand Total</b>
                    </td>
                    <td>
                        <asp:TextBox ID="gt" runat="server" CssClass="txt_num" Width="100"></asp:TextBox>
                    </td>
                    <td colspan="2"></td>
                </tr>
                <tr style="display: none">
                    <td colspan="4"></td>
                    <td>Administrasi Bank</td>
                    <td>
                        <asp:TextBox ID="admBank" runat="server" CssClass="txt_num" Width="100px">0</asp:TextBox></td>
                </tr>
            </table>
            <table style="height: 50px">
                <tr>
                    <td>
                        <asp:LinkButton ID="save" runat="server" Width="75" CssClass="btn btn-blue" Text="OK" OnClick="save_Click">
                            <i class="fa fa-share"></i> 
                            OK</asp:LinkButton>
                    </td>
                    <td>
                        <input class="btn btn-red" id="cancel" style="width: 75px" onclick="location.href='MemoRegistrasi.aspx'"
                            type="button" value="Cancel" name="cancel" runat="server">
                    </td>
                </tr>
            </table>
        </div>
        <script type="text/javascript">
			function keyup()
			{
				if(event.keyCode==27&&confirm('Apakah anda ingin membatalkan proses registrasi?'))
				{
					if(document.getElementById('cancel'))
						document.getElementById('cancel').click();
					else if(document.getElementById('cancel2'))
						document.getElementById('cancel2').click();
				}
			}
			function tagihan(no,nilai,foo)
			{
				if(foo.checked)
					document.getElementById('lunas_'+no).value = nilai;
				else
					document.getElementById('lunas_'+no).value = "";
					
				hitunggt();
			}
			function hitunggt()
			{
				foogt = document.getElementById('gt');
				grandtotal = 0*1;
				
				eof = false;
				i = 0*1;
				while(!eof) {
					foo = document.getElementById('lunas_'+i);
					if(!foo)
					{
						eof = true;
						break;
					}
					else
					{
						total = cvtnum(foo.value);
						if(!isNaN(total))
							grandtotal = grandtotal + (total*1);
						i++;
					}
				}
				
				finalnet = Math.round(100*grandtotal)/100;
				eval("foogt.value = FinalFormat('"+finalnet+"')");
			}
			function cvtnum(foo){
				return foo.replace(/,/gi ,"");
			}
			function call(nomor,tglcekgiro) {
				document.getElementById('nobg').value = nomor;
				document.getElementById('tglbg').value = tglcekgiro;
			}
        </script>
    </form>
</body>
</html>
