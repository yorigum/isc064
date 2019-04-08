<%@ Page Language="c#" Inherits="ISC064.FINANCEAR.MemoCashback" CodeFile="MemoCashback.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Cashback Registrasi</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Kontrak - Memo Cashback">
</head>
<body class="body-padding" onkeyup="&#13;&#10;&#9;if(document.getElementById('backbtn')){if(event.keyCode==27)document.getElementById('backbtn').click()};&#13;&#10;&#9;if(document.getElementById('cancel')){if(event.keyCode==27)document.getElementById('cancel').click()}">
    <form id="Form1" method="post" runat="server" class="cnt">
    <uc1:Head ID="Head1" runat="server"></uc1:Head>
    <input type="text" style="display: none">
    <div style="display: none">
        <asp:CheckBox ID="dariReminder" runat="server"></asp:CheckBox>
    </div>
    <div id="pilih" runat="server">
        <h1 class="title title-line">
            Memo Cashback</h1>
        <p>
            Halaman 1 dari 2</p>
        <br>
        <table style="border-right: #dcdcdc 1px solid; border-top: #dcdcdc 1px solid; border-left: #dcdcdc 1px solid;
            border-bottom: #dcdcdc 1px solid" cellspacing="5">
            <tr>
                <td>
                    No. Kontrak :
                </td>
                <td>
                        <asp:TextBox ID="nokontrak" runat="server" Width="100" CssClass="txt"></asp:TextBox>
                        <input class="btn btn-orange" id="btnpop" show-modal='#ModalPopUp' modal-title='Daftar Kontrak' modal-url='/marketingjual/DaftarKontrak.aspx?status=a&amp;ppjb=1' type="button" value="&#xf002;" style="font-family: 'fontawesome'" name="btnpop" runat="server" />
                        </button>
                </td>
                <td>
                        <asp:LinkButton ID="next" runat="server" CssClass="btn btn-blue" OnClick="next_Click">Next <i class="fa fa-arrow-right"></i>
                        </asp:LinkButton>
                </td>
            </tr>
        </table>
        <p class="feed">
            <asp:Label ID="feed" runat="server"></asp:Label>
        </p>
        <input type="button" id="backbtn" runat="server" onclick="history.back(-1)" value="Cancel"
            class="btn" style="margin: 5px" name="backbtn">
    </div>
    <div id="frm" runat="server">
        <h1 class="title title-line">
            Memo Cashback</h1>
        <p>
            Halaman 2 dari 2</p>
        <br>
        <table cellspacing="5">
            <tr>
                <td>
                    No. Kontrak
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:Label ID="nokontrakl" runat="server" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Unit
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:Label ID="unit" runat="server" Font-Bold="True"></asp:Label>
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
                    <asp:Label ID="customer" runat="server" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Sales
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:Label ID="agent" runat="server" Font-Bold="True"></asp:Label>
                </td>
            </tr>
        </table>
        <br>
        <table cellspacing="5">
            
            <tr>
                <td>
                    Sisa Tagihan
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox ID="sisa" runat="server" CssClass="txt_num" Width="100" ReadOnly="true">0</asp:TextBox>
                    <asp:Label ID="sisac" runat="server" CssClass="err"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Lebih Bayar
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox ID="lb" runat="server" CssClass="txt_num" Width="100" ReadOnly="true">0</asp:TextBox>
                    <asp:Label ID="lbc" runat="server" CssClass="err"></asp:Label>
                </td>
            </tr>
            <tr>
                <td></td>
                <td></td>
                <td>
                    <asp:Button ID="btnClaim" runat="server" Text="Claim" CssClass="btn" Width="75" onclick="btnClaim_Click"/>
                    &nbsp;&nbsp;                   
                </td>
            </tr>
            <tr runat="server" id="rTglKembali">
                <td>
                    Tanggal Pengembalian
                </td>
                <td>
                    :
                </td>
                <td>
                    <nobr>
								<asp:textbox id="tgl" runat="server" cssclass="txt_center" width="85"></asp:textbox>
								<button type="button" value="..." class="btn" onclick="openCalendar('tgl')">
                                    <i class="fa fa-calendar"></i>
                    <asp:Label ID="tglc" runat="server" CssClass="err"></asp:Label>
                </td>
            </tr>
        </table>
        <table height="50" id="btnAct" runat="server">
            <tr>
                <td>
                    <asp:Button ID="save" runat="server" CssClass="btn btn-blue" Width="75" Text="OK" OnClick="save_Click">
                    </asp:Button>
                </td>
                <td>
                    <input type="button" onclick="location.href='?'" class="btn btn-red" value="Cancel" style="width: 75px"
                        id="cancel" runat="server">
                </td>
            </tr>
        </table>
        <br />
        <br />
    </div>

    <script language="javascript">
                    function tagihan(no, nilai, foo) {
                        if (foo.checked)
                            document.getElementById( no).value = nilai;
                        else
                            document.getElementById(no).value = "";

                        
                    }
			        function call(nokontrak) {
			            document.getElementById('nokontrak').value = nokontrak;
			            document.getElementById('next').click();
			        }
			        
    </script>

    </form>
</body>
</html>
