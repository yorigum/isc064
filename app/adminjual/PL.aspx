<%@ Page Language="c#" AutoEventWireup="true" Inherits="ISC064.ADMINJUAL.PL" CodeFile="PL.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html >
<html lang="en">
<head>
    <title>Price List</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Report.css" type="text/css" rel="stylesheet">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Unit - Set Price List">
</head>
<body class="body-padding" onkeyup="if(document.getElementById('cancel')){if(event.keyCode==27)document.getElementById('cancel').click()}">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <div id="pilih" runat="server">
            <h1 class="title title-line">Price List</h1>
            <p>Halaman 1 dari 2</p>
            <br>
            <asp:DropDownList runat="server" ID="project" AutoPostBack="true" OnSelectedIndexChanged="project_SelectedIndexChanged" Width="150px"></asp:DropDownList>
            <br>
            <ul class="plike" style="margin-left: 25px; line-height: 1.2; width: 300px">
                <li>
                    <a href="?f=0" style="font-size: 16px;" id="f0" runat="server">Price List - Pending <label runat="server" id="pending"></label></a>
                    <br>
                    Price list untuk unit baru belum ditentukan.
						<br>
                    <br>
                </li>
                <li>
                    <a href="?f=1" style="font-size: 16px;" id="f1" runat="server">Price List - Approved <label runat="server" id="approve"></label></a>
                    <br>
                    Price list sudah ditentukan dan siap untuk dipasarkan.
						<br>
                    <br>
                </li>
                <li>
                    <a href="?f=2" style="font-size: 16px;" id="f2" runat="server">Price List - Edit Unit <label runat="server" id="edit"></label></a>
                    <br>
                    Price list sudah ditentukan tetapi unit di-edit.
                </li>
            </ul>
            <br>
            <p class="feed">
                <asp:Label ID="feed" runat="server"></asp:Label>
            </p>
        </div>
        <div id="frm" runat="server">
            <h1 class="title title-line">Price List</h1>
            <h2 id="judul" runat="server"></h2>
            <p>Halaman 2 dari 2</p>
            <br>
            <p style="font: 8pt; padding-left: 3px">
                Harga price list dan harga minimum marketing adalah dalam rupiah.
            </p>
            <table class="tb blue-skin" cellspacing="1">
                <tr valign="bottom" align="left">
                    <th width="100">No. Stock</th>
                    <th width="100">Unit</th>
                    <th width="50" align="right">Luas</th>
                    <th width="50" align="right">Luas Tanah</th>
                    <th width="50" align="right">Luas Lebih Tanah</th>
                    <th width="50" align="right">Luas Bangunan</th>
                    <th align="right">Minimum
                    </th>
                    <th align="right" colspan="2">Price List Rumah
                    </th>
                    <th align="right" colspan="2">Price List Kavling
                    </th>
                    <th align="right">Biaya BPHTP
                    </th>
                    <th align="right">Biaya Surat
                    </th>
                    <th align="right">Biaya Proses
                    </th>
                    <th align="right">Biaya Lain-Lain
                    </th>
                    <th align="right">Tgl History Price List
                    </th>
                    <th align="right">Tgl Price List
                    </th>
                    <th align="right" style="display: none;">Tambahan Harga Gimmick
                    </th>
                    <th align="right" style="display: none;">Tambahan Harga Lain - Lain
                    </th>
                </tr>
                <tr>
                    <td colspan="6" align="right">Set harga berdasarkan nilai tetap :
                    </td>
                    <td>
                        <input type="text" class="txt_num" size="15" onblur="CalcBlur(this);oksave();if(this.value!=''){hitnilai(this.value,'min');this.value=''}"
                            onfocus="tempnum=CalcFocus(this);nosave();" onkeyup="CalcType(this,tempnum);">
                    </td>
                    <td colspan="2">
                        <input type="text" class="txt_num" size="22" onblur="CalcBlur(this);oksave();if(this.value!=''){hitnilai(this.value,'pl');this.value=''}"
                            onfocus="tempnum=CalcFocus(this);nosave();" onkeyup="CalcType(this,tempnum);" />
                    </td>
                    <%--<td style="width: 20px">
                        <asp:CheckBox runat="server" ID="cbrumah" Text="&nbsp" Width="100px" OnCheckedChanged="cbrumah_CheckedChanged" AutoPostBack="true" />
                    </td>--%>
                    <td colspan="2">
                        <input type="text" class="txt_num" size="22" onblur="CalcBlur(this);oksave();if(this.value!=''){hitnilai(this.value,'plkav');this.value=''}"
                            onfocus="tempnum=CalcFocus(this);nosave();" onkeyup="CalcType(this,tempnum);">
                    </td>
                    <%--<td>
                        <asp:CheckBox runat="server" ID="cbkavling" Text="&nbsp" Width="100px" />
                    </td>--%>
                    <td>
                        <input type="text" class="txt_num" size="15" onblur="CalcBlur(this);oksave();if(this.value!=''){hitnilai(this.value,'bphtb');this.value=''}"
                            onfocus="tempnum=CalcFocus(this);nosave();" onkeyup="CalcType(this,tempnum);">
                    </td>
                    <td>
                        <input type="text" class="txt_num" size="15" onblur="CalcBlur(this);oksave();if(this.value!=''){hitnilai(this.value,'bsurat');this.value=''}"
                            onfocus="tempnum=CalcFocus(this);nosave();" onkeyup="CalcType(this,tempnum);">
                    </td>
                    <td>
                        <input type="text" class="txt_num" size="15" onblur="CalcBlur(this);oksave();if(this.value!=''){hitnilai(this.value,'bproses');this.value=''}"
                            onfocus="tempnum=CalcFocus(this);nosave();" onkeyup="CalcType(this,tempnum);">
                    </td>
                    <%--<td>
                        <input type="text" class="txt_num" size="15" onblur="CalcBlur(this);oksave();if(this.value!=''){hitnilai(this.value,'blain');this.value=''}"
                            onfocus="tempnum=CalcFocus(this);nosave();" onkeyup="CalcType(this,tempnum);">
                    </td>--%>
                    <td colspan="3"></td>
                    <td style="display: none;">
                        <input type="text" class="txt_num" size="15" onblur="CalcBlur(this);oksave();if(this.value!=''){hitnilai(this.value,'gimmick');this.value=''}"
                            onfocus="tempnum=CalcFocus(this);nosave();" onkeyup="CalcType(this,tempnum);">
                    </td>
                    <td style="display: none;">
                        <input type="text" class="txt_num" size="15" onblur="CalcBlur(this);oksave();if(this.value!=''){hitnilai(this.value,'lainlain');this.value=''}"
                            onfocus="tempnum=CalcFocus(this);nosave();" onkeyup="CalcType(this,tempnum);">
                    </td>
                </tr>
                <tr>
                    <td colspan="6" align="right">Set harga berdasarkan persentase :
                    </td>
                    <td>+
                        <input type="text" class="txt_center" size="4" onblur="if(this.value!=''){hitpersen(this.value,'min');this.value=''}">
                        persen
                    </td>
                    <td colspan="2">+
                        <input type="text" class="txt_center" size="4" onblur="if(this.value!=''){hitpersen(this.value,'pl');this.value=''}">
                        persen
                    </td>
                    <td colspan="2">+
                        <input type="text" class="txt_center" size="4" onblur="if(this.value!=''){hitpersen(this.value,'pl');this.value=''}">
                        persen
                    </td>
                    <td colspan="7"></td>
                </tr>
                <asp:PlaceHolder ID="list" runat="server"></asp:PlaceHolder>
            </table>
            <br>
            <table style="height: 50px">
                <tr>
                    <td>
                        <asp:LinkButton ID="save" runat="server" CssClass="btn btn-blue" Width="75" OnClick="save_Click"><i class="fa fa-share"></i> OK</asp:LinkButton>
                    </td>
                    <td>
                        <input type="button" onclick="location.href = '?'" class="btn btn-red" value="Cancel" style="width: 75px"
                            id="cancel" runat="server" name="cancel">
                    </td>
                </tr>
            </table>
        </div>
        <script type="text/javascript">
            function nosave() {
                document.getElementById('save').disabled = true;
            }
            function oksave() {
                document.getElementById('save').disabled = false;
            }
            function nonaktif(rumah, kavling) {
                if (rumah.checked) {
                    kavling.disabled = true;
                }
                else if (kavling.checked) {
                    rumah.disabled = true;
                }
                else {
                    kavling.disabled = false;
                    rumah.disabled = false;
                }
            }
            function hitnilai(nilai, tipe) {
                i = -1;
                fix = cvtnum(nilai) * 1;
                eof = false;
                while (!eof) {
                    i++;
                    foo = document.getElementById(tipe + '_' + i);
                    if (!foo) {
                        eof = true;
                        break;
                    } else {
                        foo.value = Math.round(100 * fix) / 100;
                        //CalcType(foo);
                        CalcType(document.getElementById(tipe + '_' + i));
                    }
                }
            }
            function hitpersen(nilai, tipe) {
                if (!isNaN(nilai)) {
                    i = -1;
                    persen = cvtnum(nilai) * 1;
                    while (true) {
                        i++;
                        foo = document.getElementById(tipe + '_' + i);
                        if (foo) {
                            cr = cvtnum(foo.value) * 1;
                            nett = cr + (cr * (persen / 100));
                            foo.value = Math.round(100 * nett) / 100;
                        } else { break; }
                    }
                }
            }
            function cvtnum(foo) {
                return foo.replace(/,/gi, "");
            }
        </script>
    </form>
</body>
</html>
