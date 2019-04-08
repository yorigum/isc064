<%@ Page Language="c#" Inherits="ISC064.FINANCEAR.FPPosting" CodeFile="FPPosting.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Posting Faktur Pajak</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Faktur Pajak - Posting Faktur Pajak">
</head>
<body class="body-padding" onkeyup="if(document.getElementById('cancel')){if(event.keyCode==27)document.getElementById('cancel').click()}">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <input type="text" style="display: none" />
        <h1 class="title title-line">Posting Faktur Pajak</h1>
        <div id="frm" runat="server">
            <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" EnablePartialRendering="true"
                runat="server" />
            <div>
                <p class="feed" style="display: none;">
                    <asp:Label ID="feed" runat="server"></asp:Label>
                </p>
            </div>
            <table>
                <tr>
                    <td>
                        <b>Dari</b>
                    </td>
                    <td>
                        <asp:TextBox ID="dari" runat="server" Width="150" CssClass="txt_center"></asp:TextBox>
                        <label for="dari" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                    </td>
                    <td rowspan="2">&nbsp;&nbsp;
                    </td>
                    <td>
                        <b>Sampai</b>
                    </td>
                    <td>
                        <asp:TextBox ID="sampai" runat="server" Width="150" CssClass="txt_center"></asp:TextBox>
                        <label for="sampai" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                        &nbsp;&nbsp;
                    </td>
                    <td>
                        <asp:DropDownList ID="project" runat="server" Width="180">
                            <asp:ListItem Value="SEMUA">Project :</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="display" CssClass="btn btn-blue" runat="server" Text="Display" OnClick="display_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:Label ID="daric" runat="server" CssClass="err"></asp:Label>
                    </td>
                    <td colspan="3">
                        <asp:Label ID="sampaic" runat="server" CssClass="err"></asp:Label>
                    </td>
                </tr>
            </table>
            <div class="peach">
                <b>Info</b>
                &nbsp;&nbsp;&nbsp;Data Kosong : Merah
            &nbsp;&nbsp;&nbsp;Data Duplikat : Biru
            </div>
            <div>
                <table cellspacing="1" style="" class="tb blue-skin">
                    <tr>
                        <th />
                        <th>No. TTS
                        </th>
                        <th>No. Faktur Pajak
                        </th>
                        <th>Tgl. Kwitansi
                        </th>
                        <th>No. Kontrak
                        </th>
                        <th>Unit
                        </th>
                        <th>Customer
                        </th>
                        <th>DPP
                        </th>
                        <th>PPN
                        </th>
                        <th>Total
                        </th>
                    </tr>
                    <tr>
                        <td colspan="10">
                            <ul class="floatsm">
                                <li><a href="javascript:checkCtrl('notts','true')">Check &nbsp;&nbsp;&nbsp;</a></li>
                                <li><a href="javascript:checkCtrl('notts','false')">Uncheck</a></li>
                            </ul>
                            <br />
                        </td>
                    </tr>
                    <asp:PlaceHolder ID="list" runat="server" />
                    <tr>
                        <td colspan="10">
                            <asp:LinkButton ID="save" runat="server" Width="75" class="btn btn-blue t-white" OnClick="save_Click"
                                AccessKey="s">
                                  <i class="fa fa-share"></i> OK  
                            </asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </div>
        </div>

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
            function call2(nomor, ctrl1, ctrl2) {
                document.getElementById(ctrl1).value = nomor;
                document.getElementById(ctrl2).checked = true;
            }

        </script>

    </form>
</body>
</html>
