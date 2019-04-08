<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ClosingLangsungGimmick.aspx.cs" Inherits="ISC064.MARKETINGJUAL.ClosingLangsungGimmick" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Pendaftaran Gimmick Closing Langsung</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Closing Langsung - Input Gimmick (Hal. 2)">
    <style type="text/css">
        .style1 {
            height: 26px;
        }
    </style>
</head>
<body class="body-padding" onkeyup="if(document.getElementById('cancel')){if(event.keyCode==27)document.getElementById('cancel').click()}">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <h1 class="title title-line">Closing Langsung Gimmick</h1>
        <table>
            <tr style="vertical-align: top">
                <td style="width: 550px">
                    <div>
                        <p><b><i>Halaman 3 dari 4</i></b></p>
                        <br />
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
                            <tr>
                                <td>Sumber
                                </td>
                                <td>:</td>
                                <td>
                                    <asp:Label ID="sum" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
                <%--<td style="padding-right: 10px; padding-left: 10px; padding-top: 30px">
                    <img src="/Media/line_vert.gif">
                </td>--%>
                <td style="width: 550px">
                    <div>
                        <p>&nbsp;</p>
                        <br />
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
            <tr>
                <td colspan="3">&nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <p>
                        <%--<asp:LinkButton ID="add" runat="server" OnClick="add_Click" AccessKey="t">+ Tambah Baris</asp:LinkButton>--%>
                        <asp:Button ID="add" runat="server" OnClick="add_Click" AccessKey="t" Text=" + Tambah Baris" />
                    </p>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <table id="tb" class="datatb" border="1">
                        <tr>
                            <th>No.</th>
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
                        </tr>
                        <asp:PlaceHolder ID="list" runat="server" />
                        <tr>
                            <td colspan="6"><b>Grand Total</b></td>
                            <td align="right">
                                <asp:TextBox ID="total" runat="server" ReadOnly="true" CssClass="right" Width="140" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="vertical-align:top">Catatan :
                    <asp:Textbox Width="500px" Height="75px" TextMode="MultiLine" ID="catatan" runat="server"></asp:Textbox>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:Button ID="save" runat="server" Text="Save" Width="75" UseSubmitBehavior="false" AccessKey="s" OnClick="save_Click" />
                    <asp:Button ID="skip" runat="server" Text="Skip" Width="75" OnClick="skip_Click" UseSubmitBehavior="false" AccessKey="k" />
                </td>
            </tr>
        </table>
        <script type="text/javascript">
            function callgimmick(ctrl1, ctrl2, ctrl4, ctrl5, ctrl6, ctrl7) {
                foo1 = document.getElementById(ctrl1);
                foo2 = document.getElementById(ctrl2);
                foo4 = document.getElementById(ctrl4);
                foo5 = document.getElementById(ctrl5);
                foo6 = document.getElementById(ctrl6);
                foo7 = document.getElementById(ctrl7);

                PopupCenter("DaftarGimmick.aspx?ctrl=" + ctrl1 + "&ctrl2=" + ctrl2 + "&ctrl4=" + ctrl4 + "&ctrl5=" + ctrl5 + "&ctrl6=" + ctrl6 + "&project=" + ctrl7
                    , "", 750, 580);
            }
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
            function hitungaja(i)
            {
                var ad = parseFloat(document.getElementById('kodebr_' + i).value.replace(/,/g, ""));//$('#kodebr_').val().replace(/,/g, "") * 1; //
                
                if (!isNaN(parseFloat(ad)))
                {
                    var harga = parseFloat(document.getElementById('hrgabr_' + i).value.replace(/,/g, ""));
                    var qty = parseFloat(document.getElementById('qtybr_' + i).value.replace(/,/g, ""));

                    Hasil = Math.round(harga * qty);

                    foogt = document.getElementById('totalbr_' + i);
                    eval("foogt.value = FinalFormat('" + Hasil + "')");

                    hitungtotal();
                }                
            }
            function hitungtotal()
            {
                total = 0;

                var i = 1;
                eof = false;
                while (!eof)
                {
                    foo = document.getElementById('totalbr_' + i);
                    
                    if (!foo)
                    {
                        eof = true;
                        break;
                    }
                    else
                    {
                        var tempel = foo.value.replace(/,/g, "");
                        if (!isNaN(parseFloat(tempel)))
                            total += parseFloat(tempel);
                    }
                    i++;
                }
                foogt = document.getElementById('total');
                eval("foogt.value = FinalFormat('" + total + "')");
            }
        </script>
    </form>
</body>
</html>
