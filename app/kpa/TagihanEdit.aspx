<%@ Page Language="c#" Inherits="ISC064.KPA.TagihanEdit" CodeFile="TagihanEdit.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Edit Jadwal Tagihan KPR</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Tagihan - Edit Jadwal Tagihan KPR">
</head>
<body onkeyup="if(event.keyCode==27&amp;&amp;confirm('Kembali ke halaman jadwal tagihan KPR?')) document.getElementById('cancel').click()">

    <script language="javascript" src="/Js/Common.js"></script>

    <script language="javascript" src="/Js/NumberFormat.js"></script>

    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <h1 style="padding-right: 5px; padding-left: 5px; padding-bottom: 5px; padding-top: 5px">Edit Jadwal Tagihan KPR
        </h1>
        <table cellpadding="0" cellspacing="0">
            <tr valign="top">
                <td width="400">
                    <table cellspacing="5">
                        <tr>
                            <td>No. Kontrak
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:Label ID="nokontrak" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Unit
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:Label ID="unit" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Customer
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:Label ID="nama" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Sales
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:Label ID="agent" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <p class="feed" style="padding-left: 5px">
                        <asp:Label ID="feed" runat="server"></asp:Label>
                    </p>
                </td>
                <td>
                    <table cellspacing="5" style="border-right: 1px inset; border-top: 1px inset; border-left: 1px inset; border-bottom: 1px inset">
                        <tr>
                            <td colspan="3">Skema :
                            <asp:Label ID="skema" runat="server" Font-Underline="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td width="150">Nilai Kontrak KPR
                            </td>
                            <td>:
                            </td>
                            <td width="150" align="right">
                                <asp:Label ID="nilai" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td width="150">Nilai Kelebihan Pencairan KPR
                            </td>
                            <td valign="top">:
                            </td>
                            <td width="150" align="right" valign="top">
                                <asp:Label ID="nilailebih" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Tagihan KPR
                            </td>
                            <td>:
                            </td>
                            <td align="right">
                                <asp:Label ID="totaltagihan" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr id="outtr" runat="server">
                            <td align="right" colspan="3">
                                <asp:Label ID="outofbalance" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br>
        <table cellspacing="4" cellpadding="4" class="tb">
            <tr align="left">
                <th>No.
                </th>
                <th>Tipe
                </th>
                <th>Nama
                </th>
                <th>Tgl
                </th>
                <th style="display:none">Tipe Perhitungan
                </th>
                <th>Persen
                </th>
                <th>Lumpsum
                </th>
            </tr>
            <asp:PlaceHolder ID="list" runat="server"></asp:PlaceHolder>
            <tr>
                <td>Baru:
                </td>
                <td>
                    <asp:DropDownList ID="barutipe" runat="server" CssClass="baru">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:TextBox ID="barunama" runat="server" CssClass="txt baru" Width="140" MaxLength="50"
                        Font-Size="10"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="barutgl" runat="server" CssClass="txt_center tgl baru" Width="75" Font-Size="10"></asp:TextBox>
                    <label for="barutgl" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                </td>
                <td style="display:none">
                    <asp:RadioButtonList RepeatDirection="Horizontal" ID="barutarif" runat="server" CssClass="baru">
                        <asp:ListItem Value="Persen">Persen</asp:ListItem>
                        <asp:ListItem Value="Nilai">Nilai</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td>
                    <asp:TextBox ID="barunilaipersen" runat="server" CssClass="txt_num barupersen" Width="90" Font-Size="10"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="barunilailumpsum" runat="server" CssClass="txt_num barulumpsum" Width="90" Font-Size="10"></asp:TextBox>
                </td>
                <td>
                    <asp:CheckBox ID="pelunasancb" runat="server" OnClick="hitungklik()"/></td>
                <td>&nbsp;</td>
                <td>
                    <asp:Label ID="baruc" runat="server" CssClass="err"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td colspan="3">
                    <asp:LinkButton ID="ok" runat="server" CssClass="btn btn-blue" Width="75" OnClick="ok_Click">
                        <i class="fa fa-share"></i> OK
                    </asp:LinkButton>
                    <asp:LinkButton ID="save" runat="server" CssClass="btn btn-orange" Width="75" AccessKey="a" OnClick="save_Click">
                        <i class="fa fa-check"></i>Apply
                    </asp:LinkButton>
                    <input id="cancel" type="button" onclick="window.close()" class="btn btn-red" value="Cancel" style="width: 100px">
                </td>
                <td>
                    <asp:TextBox ID="totpersen" runat="server" CssClass="txt_num gtpersen" Width="90" Font-Size="10">0</asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="totlumpsum" runat="server" CssClass="txt_num gtpersen" Width="90" Font-Size="10">0</asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="totpersenc" runat="server" CssClass="err"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="5" style="padding-left: 10px; font-weight: normal; font-size: 10pt; line-height: normal; font-style: normal; font-variant: normal">
                    <asp:Label ID="noedit" runat="server" CssClass="err"></asp:Label>
                </td>
            </tr>
        </table>

        <script type="text/javascript">
            function hapus(nokontrak, nourut) {
                if (confirm('Apakah anda ingin menghapus tagihan KPR : ' + nokontrak + '.' + nourut + '?\nPerhatian bahwa data akan dihapus secara PERMANEN.')) {
                    location.href = 'TagihanDel.aspx?NoKontrak=' + nokontrak + '&NoUrut=' + nourut;
                }
            }

            function hitungklik() 
            {
                //menghitung nilai klik pelunasan
                var nilaikpr = $('#nilai').text().replace(/,/g, "") * 1;
                foo = document.getElementById('pelunasancb');
                var foo;
                if (foo.checked)
                {
                    totallumpsum = 0;
                    totalpersen = 0;

                    var i = 0;
                    eof = false;
                    while (!eof)
                    {
                        lumpsum = document.getElementById('nilailumpsum_' + i);
                        persen = document.getElementById('nilaipersen_' + i);

                        if (!lumpsum){
                            eof = true;
                            break;
                        }
                        else
                        {
                            //hitung total lumpsum
                            var tempellumpsum = lumpsum.value.replace(/,/g, "");
                            if (!isNaN(parseFloat(tempellumpsum)))
                            {
                                totallumpsum += parseFloat(tempellumpsum);
                            }

                            //hitung total persen
                            var tempelpersen = persen.value.replace(/,/g, "");
                            if (!isNaN(parseFloat(tempelpersen))) {
                                totalpersen += parseFloat(tempelpersen);
                            }
                        }
                        
                        i++;
                    }

                    //lumpsum
                    totlumpsum = nilaikpr - totallumpsum;

                    foogt2 = document.getElementById('barunilailumpsum');
                    eval("foogt2.value = FinalFormat('" + totlumpsum + "')");

                    lumpsumgrandtotal = document.getElementById('totlumpsum');
                    eval("lumpsumgrandtotal.value = FinalFormat('" + nilaikpr + "')");

                    //persen
                    totpersen = 100 - totalpersen;

                    foogt3 = document.getElementById('barunilaipersen');
                    eval("foogt3.value = FinalFormat('" + totpersen + "')");

                    persengrandtotal = document.getElementById('totpersen');
                    eval("persengrandtotal.value = FinalFormat('100')");
                }
                else
                {
                    document.getElementById('barunilailumpsum').value = "";
                    document.getElementById('barunilaipersen').value = "";

                    hitungtotal();
                }
            }

            //untuk di kolom persen
            $('.persen').on('change', function () {
                var bariske = $(this).attr('data-row');
                var nilaikpa = $('#nilai').text().replace(/,/g, "") * 1;
                var foogt = $('#nilaipersen_' + bariske).val().replace(/,/g, "") * 1;
                
                Hasil = Math.round(nilaikpa * foogt / 100);

                foogt = document.getElementById('nilailumpsum_' + bariske);
                eval("foogt.value = FinalFormat('" + Hasil + "')");

                $("input[name=tipetarif_" + bariske + "][value='Persen']").prop('checked', true);
            });


            //untuk di kolom lumpsum
            $('.nilai').on('change', function () {
                var bariske = $(this).attr('data-row');
                var nilaikpa = $('#nilai').text().replace(/,/g, "") * 1;
                var foogt = $('#nilailumpsum_' + bariske).val().replace(/,/g, "") * 1;

                Hasil = Math.round(foogt / nilaikpa * 100);

                foogt = document.getElementById('nilailumpsum_' + bariske);

                eval("foogt.value = FinalFormat('" + Hasil + "')");

                $("input[name=tipetarif_" + bariske + "][value='Nilai']").prop('checked', true);
            });


            //untuk di kolom baru persen
            $('.barupersen').on('change', function () {
                var nilaikpa = $('#nilai').text().replace(/,/g, "") * 1;
                var foogt = $('#barunilaipersen').val().replace(/,/g, "") * 1;

                Hasil = Math.round(nilaikpa * foogt / 100);

                foogt = document.getElementById('barunilailumpsum');
                eval("foogt.value = FinalFormat('" + Hasil + "')");

                $("input[name=barutarif][value='Persen']").prop('checked', true);

                hitungtotal();
            });


            //untuk di kolom baru lumpsum
            $('.barulumpsum').on('change', function () {
                var nilaikpa = $('#nilai').text().replace(/,/g, "") * 1;
                var foogt = $('#barunilailumpsum').val().replace(/,/g, "") * 1;

                Hasil = Math.round(foogt / nilaikpa * 100);

                foogt = document.getElementById('barunilaipersen');
                eval("foogt.value = FinalFormat('" + Hasil + "')");

                $("input[name=barutarif][value='Nilai']").prop('checked', true);

                hitungtotal();
            });

            //untuk menghitung grand total
            function hitungtotal() {
                totallumpsum = 0;
                totalpersen = 0;

                var i = 0;
                eof = false;
                while (!eof)
                {
                    fooPersen = document.getElementById('nilaipersen_' + i);
                    fooLumpsum = document.getElementById('nilailumpsum_' + i);
                    if (!fooLumpsum)
                    {
                        eof = true;
                        break;
                    }
                    else
                    {
                        //persen
                        var tempelpersen = fooPersen.value.replace(/,/g, "");
                        if (!isNaN(parseFloat(tempelpersen)))
                            totalpersen += parseFloat(tempelpersen);

                        //lumpsum
                        var tempellumpsum = fooLumpsum.value.replace(/,/g, "");
                        if (!isNaN(parseFloat(tempellumpsum)))
                            totallumpsum += parseFloat(tempellumpsum);
                    }
                    i++;
                }

                //persen
                foobarupersen = document.getElementById('barunilaipersen');
                var tempelbarupersen = foobarupersen.value.replace(/,/g, "");

                if (!isNaN(parseFloat(tempelbarupersen)))
                {
                    totalpersen += parseFloat(tempelbarupersen);
                }

                foogtpersen = document.getElementById('totpersen');
                eval("foogtpersen.value = FinalFormat('" + totalpersen + "')");
                
                //lumpsum
                foobarulumpsum = document.getElementById('barunilailumpsum');
                var tempelbarulumpsum = foobarulumpsum.value.replace(/,/g, "");

                if (!isNaN(parseFloat(tempelbarulumpsum)))
                {
                    totallumpsum += parseFloat(tempelbarulumpsum);
                }

                foogtlumpsum = document.getElementById('totlumpsum');
                eval("foogtlumpsum.value = FinalFormat('" + totallumpsum + "')");
            }
        </script>

    </form>
</body>
</html>
