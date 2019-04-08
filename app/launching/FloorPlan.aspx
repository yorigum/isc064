<%@ Page Language="c#" Inherits="ISC064.LAUNCHING.FloorPlan" CodeFile="FloorPlan.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Peta Floor Plan</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta http-equiv="Refresh" content="3">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Unit - Peta Floor Plan Detil">
    <meta http-equiv="pragma" content="no-cache">
    <%--<script type="text/javascript">
        $(document).click(function () {
            $('#tooltip').hide();
        });
        $('[tooltip-url]').mouseout(function () {
            $('#tooltip').hide();
        });
        $('[tooltip-url]').mouseenter(function () {
            var top = $(this).offset().top;
            var left = $(this).offset().left;
            var oleft = $(window).scrollLeft();

            if (left > (498 + oleft)) {
                $('#tooltip').css({
                    top: (top + $(this).height() + -190),
                    left: (left + $(this).height() + -225),
                    position: 'absolute'
                });
            }
            else {
                $('#tooltip').css({
                    top: (top + $(this).height() + -190),
                    //left: (left + $(this).height() + -125),
                    left: (left + $(this).height()),
                    position: 'absolute'
                });
            }

            var url = $(this).attr('tooltip-url');

            $.ajax({
                url: url,
                type: 'GET',
                success: function (result) {
                    tooltip = result;
                    $('#tooltip').html(result);
                }
            });
            $('#tooltip').show();
        });
    </script>--%>
</head>
<body class="body-padding" onkeyup="if(event.keyCode==27) history.back(-1)">
    <form id="Form1" method="post" runat="server">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>

        <asp:PlaceHolder ID="list" runat="server"></asp:PlaceHolder>
<%--        <div id="tooltip">
        </div>--%>
        <div style="padding: 200px 20px 10px 10px;">
            <img src="Image/SitePlan.jpg" width="400px" />
        </div>
    </form>
</body>
</html>
