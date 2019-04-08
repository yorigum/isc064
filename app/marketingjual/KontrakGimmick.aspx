<%@ Page Language="c#" AutoEventWireup="true" CodeFile="KontrakGimmick.aspx.cs" Inherits="ISC064.MARKETINGJUAL.KontrakGimmick" %>

<%@ Register TagPrefix="uc1" TagName="HeadKontrak" Src="HeadKontrak.ascx" %>
<%@ Register TagPrefix="uc1" TagName="NavKontrak" Src="NavKontrak.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Kontrak Gimmick</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Kontrak Gimmick">
</head>
<body onkeyup="if(event.keyCode==27)document.getElementById('cancel').click()">
    <form id="Form1" method="post" runat="server">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <div class="content-header">
            <uc1:NavKontrak ID="NavKontrak1" runat="server" Aktif="7"></uc1:NavKontrak>
        </div>
        <div class="tabdata">
            <div class="pad">
                <uc1:HeadKontrak ID="HeadKontrak1" runat="server"></uc1:HeadKontrak>
                <input type="text" style="display: none">
                <table>
                    <tr>
                        <td class="printhref">
                            Print : <a id="printGimmick" runat="server"><b>Tanda Terima Gimmick</b></a>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr style="vertical-align: top">
                        <td style="width: 550px">
                            <div>
                                <table>
                                    <tr>
                                        <td colspan="3">
                                            <h3>
                                                <span style="width: 30px">1.</span> Data Kontrak
                                            </h3>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 170px;">No. Kontrak</td>
                                        <td>:</td>
                                        <td>
                                            <asp:TextBox ID="nokon" runat="server" ReadOnly="true"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Tgl. Kontrak
                                        </td>
                                        <td>:</td>
                                        <td>
                                            <asp:Label ID="tglkontrak" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Customer
                                        </td>
                                        <td>:</td>
                                        <td>
                                            <asp:Label ID="cs" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Agent
                                        </td>
                                        <td>:</td>
                                        <td>
                                            <asp:Label ID="ag" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Cara Bayar
                                        </td>
                                        <td>:</td>
                                        <td>
                                            <asp:Label ID="carabayar" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                        <td style="width: 550px">
                            <div>
                                <table>
                                    <tr>
                                        <td colspan="3">
                                            <h3>
                                                <span style="width: 30px">2.</span> Nilai Kontrak
                                            </h3>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 170px;">Nilai DPP</td>
                                        <td>:</td>
                                        <td>
                                            <asp:Label ID="Ndpp" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Nilai PPN
                                        </td>
                                        <td>:</td>
                                        <td>
                                            <asp:Label ID="Nppn" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Nilai Kontrak
                                        </td>
                                        <td>:</td>
                                        <td>
                                            <asp:Label ID="nilaikon" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>

                <br />
                <table>
                    <tr>
                        <td style="width:170px;">Tgl. Diterima</td>
                        <td>:</td>
                        <td>
                            <asp:textbox id="tglditerima" runat="server" width="85" cssclass="txt_center"></asp:textbox>
							<label for="tglditerima" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                            <asp:Label ID="tglditerimac" runat="server" CssClass="err"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>Catatan</td>
                        <td>:</td>
                        <td>
                            <asp:Textbox Width="400px" Height="75px" TextMode="MultiLine" ID="catatan" runat="server"></asp:Textbox>
                        </td>
                    </tr>
                </table>

                <br />
                <table>
                    <tr>
                        <td colspan="3">
                            <table id="tb" class="datatb" border="1">
                                <tr>
                                    <th>No.</th>
                                    <th>Diterima
                                    </th>
                                    <th>Kode Barang
                                    </th>
                                    <th>Nama Barang
                                    </th>
                                    <th>Satuan Barang
                                    </th>
                                    <th>Harga Satuan Barang
                                    </th>
                                    <th>Qty Barang
                                    </th>
                                    <th>Total Harga
                                    </th>
                                    <th>&nbsp;
                                    </th>
                                </tr>
                                <asp:PlaceHolder ID="list" runat="server" />
                                <tr>
                                    <td colspan="2">Baru :</td>
                                    <td>
                                        <asp:TextBox ID="kodebaru" runat="server" Width="100" />
                                        <input type="button" value="..." show-modal='#ModalPopUp' modal-title='Daftar Gimmick' modal-url='DaftarGimmick.aspx' id="search" runat="server">
                                    </td>
                                    <td>
                                        <asp:TextBox ID="namabaru" runat="server" Width="200" ReadOnly="true" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="satuanbaru" runat="server" Width="70" ReadOnly="true" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="hargabaru" runat="server" Width="140" ReadOnly="true" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="qtybaru" runat="server" Width="50" onblur="hitung()" Text="0"/>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="totalbaru" runat="server" Width="140" ReadOnly="true" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="7"><b>Grand Total</b></td>
                                    <td align="right">
                                        <asp:TextBox ID="total" runat="server" ReadOnly="true" CssClass="right" Width="140" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="8">
                                        <br />
                                        <asp:Button ID="save" runat="server" Text="Save" Width="75" UseSubmitBehavior="false" AccessKey="s" OnClick="save_Click" />
                                        <asp:Label ID="feed" runat="server"></asp:Label> &nbsp; 
                                        <asp:Label ID="alertc" runat="server" CssClass="err"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <script type="text/javascript">
                    function callgimmick(ctrl1, ctrl2, ctrl4, ctrl5, ctrl7) {
                        foo1 = document.getElementById(ctrl1);
                        foo2 = document.getElementById(ctrl2);
                        foo4 = document.getElementById(ctrl4);
                        foo5 = document.getElementById(ctrl5);
                        foo7 = document.getElementById(ctrl7);

                        //PopupCenter("DaftarGimmick.aspx?ctrl=" + ctrl1 + "&ctrl2=" + ctrl2 + "&ctrl4=" + ctrl4 + "&ctrl5=" + ctrl5 + "&project=" + ctrl7
                        //    , "", 750, 580);

                        PopupCenter("DaftarGimmick.aspx?ctrl=" + ctrl1 + "&ctrl2=" + ctrl2 + "&ctrl4=" + ctrl4 + "&ctrl5=" + ctrl5 + "&ctrl6=" + ctrl6 + "&project=" + ctrl7
                    , "", 750, 580);
                    }
                    //function call2(ctrl1, ctrl2, ctrl4, ctrl5, value1, value2, value4, value5) {
                    //    foo1 = document.getElementById(ctrl1);
                    //    foo2 = document.getElementById(ctrl2);
                    //    foo4 = document.getElementById(ctrl4);
                    //    foo5 = document.getElementById(ctrl5);

                    //    if (foo1)
                    //        foo1.value = value1;
                    //    if (foo2)
                    //        foo2.value = value2;
                    //    if (foo4)
                    //        foo4.value = value4;
                    //    if (foo5)
                    //        foo5.value = value5;
                    //}

                    function call2(ctrl1, ctrl2, ctrl4, ctrl5, ctrl6, value1, value2, value4, value5, value6) {
                        foo1 = document.getElementById(ctrl1);
                        foo2 = document.getElementById(ctrl2);
                        foo4 = document.getElementById(ctrl4);
                        foo5 = document.getElementById(ctrl5);
                        foo6 = document.getElementById(ctrl6);

                        if (foo1)
                            foo1.value = value1;
                        if (foo2)
                            foo2.value = value2;
                        if (foo4)
                            foo4.value = value4;
                        if (foo5)
                            foo5.value = value5;
                        if (foo6)
                            foo6.value = value6;
                    }

                    function call3(no, no2, no4, no5) {
                        document.getElementById('kodebaru').value = no;
                        document.getElementById('namabaru').value = no2;
                        document.getElementById('satuanbaru').value = no4;
                        document.getElementById('hargabaru').value = no5;
                    }
                    function hitung() {
                        var ad = parseFloat(document.getElementById('kodebaru').value.replace(/,/g, ""));
                        if (!isNaN(parseFloat(ad))) {
                            var hargabaru = parseFloat(document.getElementById('hargabaru').value.replace(/,/g, ""));
                            var qtybaru = parseFloat(document.getElementById('qtybaru').value.replace(/,/g, ""));

                            totalbaru = Math.round(hargabaru * qtybaru);

                            foogtbaru = document.getElementById('totalbaru');
                            eval("foogtbaru.value = FinalFormat('" + totalbaru + "')");

                            hitungtotal();
                        }
                    }
                    function hitungaja(i)
                    {
                        var ad = parseFloat(document.getElementById('kodebr_' + i).value.replace(/,/g, ""));

                        if (!isNaN(parseFloat(ad))) {
                            var harga = parseFloat(document.getElementById('hrgabr_' + i).value.replace(/,/g, ""));
                            var qty = parseFloat(document.getElementById('qtybr_' + i).value.replace(/,/g, ""));

                            Hasil = Math.round(harga * qty);

                            foogt = document.getElementById('totalbr_' + i);
                            eval("foogt.value = FinalFormat('" + Hasil + "')");

                            hitungtotal();
                        }
                    }
                    function hitungtotal() {
                        total = 0;

                        var i = 0;
                        eof = false;
                        while (!eof) {
                            foo = document.getElementById('totalbr_' + i);

                            if (!foo) {
                                eof = true;
                                break;
                            }
                            else {
                                var tempel = foo.value.replace(/,/g, "");
                                if (!isNaN(parseFloat(tempel)))
                                    total += parseFloat(tempel);
                            }
                            i++;
                        }

                        foobaru = document.getElementById('totalbaru');
                        var tempelbaru = foobaru.value.replace(/,/g, "");

                        if (!isNaN(parseFloat(tempelbaru)))
                            total += parseFloat(tempelbaru);

                        foogt = document.getElementById('total');
                        eval("foogt.value = FinalFormat('" + total + "')");
                    }
                </script>
            </div>
        </div>
    </form>
</body>
</html>
