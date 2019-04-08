<%@ Page Language="C#" AutoEventWireup="true" CodeFile="KomisiPengajuan.aspx.cs"
    Inherits="ISC064.MARKETINGJUAL.KomisiPengajuan" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Pengajuan Komisi</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Komisi - Pengajuan Komisi" />

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
    </script>

</head>
<body onkeyup="&#13;&#10;&#9;if(document.getElementById('backbtn')){if(event.keyCode==27)document.getElementById('backbtn').click()};&#13;&#10;&#9;if(document.getElementById('cancel')){if(event.keyCode==27)document.getElementById('cancel').click()}">
    <form id="Form1" method="post" runat="server" class="cnt">
    <uc1:Head ID="Head1" runat="server"></uc1:Head>
    <div>
        <div id="pilih" runat="server" style="padding: 10px;">
            <h1 class="title title-line">Pengajuan Komisi</h1>
            <br />
            <br />
            <table style="border-right: #dcdcdc 1px solid; border-top: #dcdcdc 1px solid; border-left: #dcdcdc 1px solid;
                border-bottom: #dcdcdc 1px solid" cellspacing="5">
                <tr>
                    <td colspan="5">
                        Periode Kontrak :
                    </td>
                    <td>
                    </td>
                    <td>
                        Customer / Unit / No. Kontrak :
                    </td>
                </tr>
                <tr>
                    <td>
                        Dari
                    </td>
                    <td>
                        &nbsp;<asp:TextBox ID="dari" runat="server" Width="85" CssClass="txt_center"></asp:TextBox>
                        <label for="dari" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                    </td>
                    <td>
                        &nbsp;&nbsp;
                    </td>
                    <td>
                        Sampai
                    </td>
                    <td>
                        <asp:TextBox ID="sampai" runat="server" Width="85" CssClass="txt_center"></asp:TextBox>
                        <label for="sampai" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                    </td>
                    <td>
                    </td>
                    <td>
                        <asp:TextBox ID="keyword" runat="server" Width="100%"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="display" runat="server" Text="Display" CssClass="btn btn-blue" OnClick="display_Click">
                        </asp:Button>
                    </td>
                </tr>
            </table>
            <br />
            <p class="feed">
                <asp:Label ID="feed" runat="server"></asp:Label>
            </p>
        </div>
        <div>
            <p>
                *Checkbox nonaktif menunjukkan komisi belum memenuhi term cair</p>
            <table class="blue-skin" width="100%" cellspacing="1">
                <tr>
                    <th>
                    </th>
                    <th>
                        No.Kontrak
                    </th>
                    <th>
                        Customer
                    </th>
                    <th>
                        Unit
                    </th>
                    <th>
                        Nilai DPP
                    </th>
                    <th>
                        Persentase Pelunasan (%)
                    </th>
                    <th>
                        Komisi
                    </th>
                    <th>
                        Penerima
                    </th>
                    <th>
                        Term Cair (%)
                    </th>
                    <th>
                        Nilai Komisi
                    </th>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td colspan="9">
                        <ul class="floatsm">
                            <li><a href="javascript:checkCtrl('cb','true')"> Check &nbsp; &nbsp; &nbsp; &nbsp;</a></li>
                            <li><a href="javascript:checkCtrl('cb','false')">Uncheck</a></li>
                        </ul>
                        <br />
                    </td>
                </tr>
                <tr id="notfound" runat="server" visible="false">
                    <td>
                    </td>
                    <td colspan="10">
                        Data tidak ditemukan
                    </td>
                </tr>
                <asp:PlaceHolder ID="list" runat="server"></asp:PlaceHolder>
                <tr>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td colspan="10" align="right">
                        <asp:Button ID="save" Text="Save" runat="server" Width="100" OnClick="save_Click" cssclass="btn btn-blue" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
