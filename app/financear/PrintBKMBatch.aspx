<%@ Reference Control="~/PrintBKMTemplate.ascx" %>

<%@ Page Language="c#" Inherits="ISC064.FINANCEAR.PrintBKMBatch" CodeFile="PrintBKMBatch.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Print Kuitansi</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Print.css" type="text/css" rel="stylesheet">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Print Kuitansi">

    <style type="text/css">
        table.datatb {
            border-collapse: collapse;
            border: solid 1px #CCC;
        }

            table.datatb tr.top {
                vertical-align: top;
            }

            table.datatb th {
                padding: 3px 5px 3px 5px;
                font-size: 8pt;
                font-weight: bold;
                background-color: #DCDCDC;
                vertical-align: bottom;
            }

            table.datatb td {
                padding: 3px 5px 3px 5px;
            }

                table.datatb td.kosong {
                    color: #999;
                    padding: 10px;
                }

        ul.floatsm li {
            float: left;
            padding-right: 5px;
            font-size: 8pt;
        }
    </style>
</head>
<body class="body-padding" onkeyup="if(event.keyCode==27&&confirm('Apakah anda ingin menutup layar print ini?')) window.close();">
    <form id="Form1" method="post" runat="server">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <input type="text" style="display: none">
        <div id="reprint" runat="server">
            <h1 class="title title-line">Print Kuitansi</h1>
            <p style="padding: 5px"><b>Tanggal :</b></p>
            <table>
                <tr>
                    <td style="text-align: right; vertical-align: middle">Dari</td>
                    <td>
                        <div class="input-group input-medium">
                            <asp:TextBox ID="dari" runat="server" type="text" CssClass="form-control" Style="width: 57%; height: 20px"></asp:TextBox>
                            <span class="input-group-btn" style="height: 34px; display: block">
                                <label for="dari" class="btn-a default btn-cal"><i class="fa fa-calendar"></i></label>
                            </span>
                        </div>
                    </td>
                    <td>&nbsp;</td>
                    <td>Sampai</td>
                    <td>
                        <div class="input-group input-medium">
                            <asp:TextBox ID="sampai" runat="server" type="text" CssClass="form-control" Style="width: 57%; height: 20px"></asp:TextBox>
                            <span class="input-group-btn" style="height: 34px; display: block">
                                <label for="sampai" class="btn-a default btn-cal"><i class="fa fa-calendar"></i></label>
                            </span>
                        </div>
                    </td>
                    <td>
                        <div class="input-group input-medium">
                            <asp:DropDownList ID="project" runat="server" Width="180" >                                
                            </asp:DropDownList>
                        </div>
                    </td>
                </tr>
               
                <tr>
                    <td colspan="3">
                        <asp:Label ID="daric" runat="server" CssClass="err"></asp:Label></td>
                    <td colspan="3">
                        <asp:Label ID="sampaic" runat="server" CssClass="err"></asp:Label></td>
                </tr>
            </table>
            <br>
            <div>
                <table style="width: 100%">
                    <tr>
                        <td class="ins">
                            <asp:Button ID="display" runat="server" CssClass="btn btn-blue" Text="Display" Width="75" AccessKey="d" OnClick="display_Click"></asp:Button>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div id="kui" runat="server">
            <table cellspacing="1" class="tb blue-skin">
                <tr>
                    <th />
                    <th>No. TTS
                    </th>
                    <th>Tgl. Kuitansi
                    </th>
                    <th>No. Kontrak
                    </th>
                    <th>Unit
                    </th>
                    <th>Customer
                    </th>
                    <th>Nilai
                    </th>
                </tr>
                <tr>
                    <td colspan="7">
                        <ul class="floatsm">
                            <li style="width: 50px"><a href="javascript:checkCtrl('notts','true')">Check</a></li>
                            <li><a href="javascript:checkCtrl('notts','false')">Uncheck</a></li>
                        </ul>
                        <br />
                    </td>
                </tr>
                <asp:PlaceHolder ID="list2" runat="server" />
                <tr>
                    <td colspan="7">
                        <asp:Button ID="print" runat="server" CssClass="btn btn-green" Text="Print" Width="75" AccessKey="p" OnClick="print_Click"></asp:Button>
                    </td>
                </tr>
            </table>
        </div>
        <asp:PlaceHolder ID="list" runat="server"></asp:PlaceHolder>

        <script type="text/javascript" type="text/javascript">
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
    </form>
</body>
</html>
