<%@ Page Language="C#" AutoEventWireup="true" CodeFile="KomisiGenerate.aspx.cs" Inherits="ISC064.MARKETINGJUAL.KomisiGenerate" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Generate Komisi</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1" />
    <meta name="CODE_LANGUAGE" content="C#" />
    <meta name="vs_defaultClientScript" content="JavaScript" />
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5" />
    <link href="/Media/Style.css" type="text/css" rel="stylesheet" />
    <meta name="ctrl" content="1" />
    <meta name="sec" content="Komisi - Generate Komisi" />

    <script type="text/javascript">
    	function call(nokontrak)
			{
				document.getElementById('nokontrak').value = nokontrak;
				document.getElementById('next').click();
			}
    </script>

</head>
<body onkeyup="&#13;&#10;&#9;if(document.getElementById('backbtn')){if(event.keyCode==27)document.getElementById('backbtn').click()};&#13;&#10;&#9;if(document.getElementById('cancel')){if(event.keyCode==27)document.getElementById('cancel').click()}">
    <form id="Form1" method="post" runat="server" class="cnt">
    <uc1:Head ID="Head1" runat="server"></uc1:Head>
    <div id="pilih" runat="server" style="padding: 10px;">
        <h1>
            Generate Komisi</h1>
        <p>
            Halaman 1 dari 2</p>
        <br />
        <table style="border-right: #dcdcdc 1px solid; border-top: #dcdcdc 1px solid; border-left: #dcdcdc 1px solid;
            border-bottom: #dcdcdc 1px solid" cellspacing="5">
            <tr>
                <td>
                    No. Kontrak :
                </td>
                <td>
                    <asp:TextBox ID="nokontrak" runat="server" Width="100" CssClass="txt"></asp:TextBox>
                    <input type="button" value="..." class="btn" onclick="popDaftarKontrak('a')" id="btnpop"
                        runat="server" name="btnpop">
                </td>
                <td>
                    <asp:Button ID="next" runat="server" Text="Next..." CssClass="btn" OnClick="next_Click">
                    </asp:Button>
                </td>
            </tr>
        </table>
        <br />
        <p class="feed">
            <asp:Label ID="feed" runat="server"></asp:Label>
        </p>
        
    </div>
    <div id="frm" runat="server" visible="false" style="padding: 10px;">
        <h1>
            Generate Komisi</h1>
        <p>
            Halaman 2 dari 2</p>
        <br />
        <table>
            <tr>
                <td>
                    No. Kontrak
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:Label ID="no" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Customer
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:Label ID="customer" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    No. Unit
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:Label ID="unit" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Nilai DPP
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:Label ID="dpp" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <br />
        <br />
        <table class="tb" cellspacing="3" style="border:1px solid black;border-collapse:collapse;" border="1">
            <tr>
                <td width="100">
                    Jenis Komisi
                </td>
                <td width="100">
                    Penerima
                </td>
                <td width="120" align="right">
                    Nilai
                </td>
            </tr>
            <asp:PlaceHolder ID="list" runat="server"></asp:PlaceHolder>
        </table>
        <br />
        <asp:Button ID="save" runat="server" Text="Save" onclick="save_Click" />
        
    </div>
    </form>
</body>
</html>
