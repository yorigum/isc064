<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FormatWarnaUnit.aspx.cs" Inherits="ISC064.SETTINGS.FormatWarnaUnit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Format Warna Unit</title>
    <link href="/Media/Style.css" type="text/css" rel="stylesheet"/>
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css"/>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Format Warna Unit">
    <script src="/Js/JQuery.min.js"></script>
    <script src="/Js/spectrum.js"></script>
    <link rel="stylesheet" type="text/css" href="/Media/spectrum.css" />
</head>
<body class="body-padding">
    <form id="Form1" method="post" runat="server" class="cnt">
        <%--<uc1:Head ID="Head1" runat="server"></uc1:Head>--%>
        <div>
            <h1 class="title title-line">Format Warna Unit</h1>
            <br>
            <asp:DropDownList runat="server" ID="project" AutoPostBack="true" OnSelectedIndexChanged="project_SelectedIndexChanged"></asp:DropDownList>
            <table>
                <tr>
                    <td>Sold :
                    </td>
                    <td>
                        <input type="text" class="basic" id="jual" runat="server"/>
                    </td>
                    <td style="padding-left:50px">Reserved :</td>
                    <td>
                        <input type="text" class="basic2" id="booked" runat="server"/>
                    </td>
                    <td style="padding-left:50px">Available :</td>
                    <td>
                        <input type="text" class="basic3" id="cancel" runat="server"/>
                    </td>
                    <td style="padding-left:50px">Hold Internal :</td>
                    <td>
                        <input type="text" class="basic4" id="hold" runat="server"/>
                    </td>
                </tr>
                <tr style="height:10px">
                    <td/>
                </tr>
                <tr>
                    <td colspan="3" style="padding-top:10px">
                            <asp:LinkButton id="ok" runat="server" cssclass="btn btn-blue" width="75" onclick="save_Click"><i class="fa fa-share"></i> OK</asp:LinkButton>
                    </td>
                </tr>
            </table>
            <p class="feed">
                <asp:Label ID="feed" runat="server"></asp:Label>
            </p>
        </div>
        <script type="text/javascript">
           $(".basic").spectrum({
               //color: "#fff",
               change: function (color) {
                   $("#jual").val(color.toHexString());
               }
           });
        </script>
        <script type="text/javascript">
           $(".basic2").spectrum({
               change: function (color) {
                   $("#booked").val(color.toHexString());
               }
           });
        </script>
        <script type="text/javascript">
           $(".basic3").spectrum({
               change: function (color) {
                   $("#cancel").val(color.toHexString());
               }
           });
        </script>
        <script type="text/javascript">
           $(".basic4").spectrum({
               change: function (color) {
                   $("#hold").val(color.toHexString());
               }
           });
        </script>

    </form>
</body>
</html>

