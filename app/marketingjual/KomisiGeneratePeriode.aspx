<%@ Page Language="C#" AutoEventWireup="true" CodeFile="KomisiGeneratePeriode.aspx.cs"
    Inherits="ISC064.MARKETINGJUAL.KomisiGeneratePeriode" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Generate Komisi</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Kontrak - Generate Komisi Periode">

    <script type="text/javascript" src="/Js/Pop.js"></script>

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
        <h1 class="title title-line">Generate Komisi</h1>
        <p>
            Halaman 1 dari 2</p>
        <br />
        <table style="border-right: #dcdcdc 1px solid; border-top: #dcdcdc 1px solid; border-left: #dcdcdc 1px solid;
            border-bottom: #dcdcdc 1px solid" cellspacing="5">
            <tr>
					<td>No. Sales</td>
					<td colspan="4">:
						<asp:textbox id="noagent" runat="server" cssclass="txt" width="100"></asp:textbox>
                        <button id="btnpop" class="btn btn-orange" show-modal='#ModalPopUp' modal-title='Daftar Agent' modal-url='DaftarAgent.aspx?status=a' type="button" name="btnpop" runat="server"><i class="fa fa-search"></i></button>
					</td>
				</tr>
            <tr>
                <td>Skema</td>
                <td colspan="10">:
                    <asp:DropDownList ID="komisi" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Periode Kontrak
                </td>
                <td>:</td>
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
                    &nbsp;  &nbsp;
                </td>
                <td>
                    Sampai
                </td>
                <td>
                    <asp:TextBox ID="sampai" runat="server" Width="85" CssClass="txt_center"></asp:TextBox>
                    <label for="sampai" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                </td>
                <td>
                    <asp:LinkButton ID="next" runat="server" CssClass="btn btn-blue" OnClick="next_Click">
                        Generate <i class="fa fa-arrow-right"></i>
                    </asp:LinkButton>
                </td>
            <tr>
            <tr>
                <td colspan="3"><asp:Label ID="daric" runat="server" /></td>
                <td colspan="3"><asp:Label ID="sampaic" runat="server" /></td>
            </tr>
                
            </tr>
                <td>
                    <asp:LinkButton ID="genfalse" runat="server" CssClass="btn btn-gray" OnClick="genfalse_Click">
                        Clear False Komisi <i class="fa fa-arrow-right"></i>
                    </asp:LinkButton> &nbsp;
                    <asp:Label ID="errclear" runat="server" />
                </td>

            </tr>
        </table>
        <br />
        <p class="feed">
            <asp:Label ID="feed" runat="server"></asp:Label>
        </p>
    </div>
    <div id="frm" runat="server" visible="false" style="padding: 10px;">
        <h1 class="title title-line">
            Generate Komisi</h1>
        <p>
            Halaman 2 dari 2</p>
        <br />
        <br />
        <table class="tb blue-skin" cellspacing="1">
            <asp:PlaceHolder ID="TopList" runat="server"></asp:PlaceHolder>
            <asp:PlaceHolder ID="MiddleList" runat="server"></asp:PlaceHolder>
            <asp:PlaceHolder ID="list" runat="server"></asp:PlaceHolder>
        </table>
        <br />
        <asp:Button ID="save" runat="server" Text="Save" OnClick="save_Click" cssclass="btn btn-blue" />
    </div>
        <script language="javascript">
            function call(noagent) {
                document.getElementById('noagent').value = noagent;
            }
			</script>
    </form>
</body>
</html>
