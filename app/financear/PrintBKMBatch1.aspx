<%@ Reference Control="~/PrintBKMTemplate.ascx" %>

<%@ Page Language="c#" Inherits="ISC064.FINANCEAR.PrintBKMBatch1" CodeFile="PrintBKMBatch1.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Print Bukti Kas Masuk</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Print.css" type="text/css" rel="stylesheet">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Print Bukti Kas Masuk (Batch)">

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
                <asp:PlaceHolder ID="list2" runat="server" />
        <asp:PlaceHolder ID="list" runat="server"></asp:PlaceHolder>

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
    </form>
</body>
</html>
