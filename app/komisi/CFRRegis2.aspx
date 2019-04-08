<%@ Page Language="c#" Inherits="ISC064.KOMISI.CFRRegis2" CodeFile="CFRRegis2.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Realisasi Pencairan Closing Fee</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Realisasi Pencairan Closing Fee - Registrasi (Step 2)">
</head>
<body class="body-padding">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <h1 class="title title-line">Realisasi Pencairan Closing Fee</h1>
        <br>
        <table class="tb blue-skin" cellspacing="1">
            <tr align="left" valign="bottom">
                <th></th>
                <th>Penerima</th>
                <th>No. Kontrak</th>
                <th>Unit</th>
                <th>Marketing</th>
                <th>Skema</th>
                <th class="right">Nilai Closing Fee</th>
                <th class="right">Nilai PPH</th>
            </tr>
            <tr>
                <td colspan="8">
                    <ul class="floatsm">
                        <li><a href="javascript:checkCtrl('cb','true')">Check&nbsp;&nbsp;&nbsp;&nbsp;</a></li>
                        <li><a href="javascript:checkCtrl('cb','false')">Uncheck</a></li>
                    </ul>
                    <br />
                </td>
            </tr>
            <asp:PlaceHolder ID="list" runat="server"></asp:PlaceHolder>
        </table>

        <br />
        <table cellspacing="5">
            <tr valign="top">
                <td width="20%">No. Pengajuan</td>
                <td width="1%">:</td>
                <td>
                    <asp:textbox id="kode" runat="server" width="250" maxlength="100" cssclass="txt" Enabled="false"></asp:textbox>
                </td>
            </tr>
            <tr valign="top">
                <td width="20%">Tgl. Realisasi</td>
                <td width="1%">:</td>
                <td>
                    <nobr>
						<asp:textbox id="tgl" runat="server" cssclass="txt_center" width="85"></asp:textbox>
                        <label for="tgl" class="btn btn-cal"><i class="fa fa-calendar"></i></label>                                            
					</nobr>
                    <asp:Label ID="tglc" runat="server" CssClass="err"></asp:Label>
                </td>
            </tr>
            <tr valign="top">
                <td width="20%">Keterangan</td>
                <td width="1%">:</td>
                <td>
                    <asp:TextBox ID="ket" runat="server" Width="350" Height="150" TextMode="MultiLine" CssClass="txt"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:LinkButton ID="save" runat="server" CssClass="btn btn-blue" Width="75" OnClick="save_Click"><i class="fa fa-share"></i> OK
                    </asp:LinkButton>
                </td>
            </tr>
        </table>
        <script type="text/javascript">
            function checkCtrl(foo, n) {
                var x = true; var i = 0;
                while (x) {
                    if (document.getElementById(foo + "_" + i)) {
                        if (!document.getElementById(foo + "_" + i).disabled) {
                            if (n == "true")
                                document.getElementById(foo + "_" + i).checked = true;
                            else
                                document.getElementById(foo + "_" + i).checked = false;
                        }
                        i++;
                    } else { x = false; }
                }
            }

            //function hitungaja(i) {
            //    var ad = parseFloat(document.getElementById('kodebr_' + i).value.replace(/,/g, ""));//$('#kodebr_').val().replace(/,/g, "") * 1; //

            //    if (!isNaN(parseFloat(ad))) {
            //        var harga = parseFloat(document.getElementById('hrgabr_' + i).value.replace(/,/g, ""));
            //        var qty = parseFloat(document.getElementById('qtybr_' + i).value.replace(/,/g, ""));

            //        Hasil = Math.round(harga * qty);

            //        foogt = document.getElementById('totalbr_' + i);
            //        eval("foogt.value = FinalFormat('" + Hasil + "')");

            //        hitungtotal();
            //    }
            //}

            //function hitungtotal() {
            //    total = 0;

            //    var i = 1;
            //    eof = false;
            //    while (!eof) {
            //        foo = document.getElementById('totalbr_' + i);

            //        if (!foo) {
            //            eof = true;
            //            break;
            //        }
            //        else {
            //            var tempel = foo.value.replace(/,/g, "");
            //            if (!isNaN(parseFloat(tempel)))
            //                total += parseFloat(tempel);
            //        }
            //        i++;
            //    }
            //    foogt = document.getElementById('total');
            //    eval("foogt.value = FinalFormat('" + total + "')");
            //}

        </script>
    </form>
</body>
</html>
