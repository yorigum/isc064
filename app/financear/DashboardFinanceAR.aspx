<%@ Page Language="c#" Inherits="ISC064.FINANCEAR.DashboardFinanceAR" CodeFile="DashboardFinanceAR.aspx.cs" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <title>Chart</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Dashboard">
    <link href="/assets/css/bootstrap.min.css" rel="stylesheet" />
    <link href="/assets/css/StyleSheet.css" rel="stylesheet" />
    <link href="/assets/css/light-bootstrap-dashboard.css" rel="stylesheet" />
    <script type="text/javascript">
        var forecolor = ["1DC7EA", "FB404B", "FFA534", "9368E9", "87CB16", "1F77D0", "5e5e5e", "dd4b39", "35465c", "e52d27", "55acee", "cc2127", "1769ff", "6188e2", "a748ca", "", "", "", "", "", "", "", "", "", "", ""];
    </script>
    <link href="http://maxcdn.bootstrapcdn.com/font-awesome/4.2.0/css/font-awesome.min.css" rel="stylesheet">
    <script src="/assets/js/amcharts.js" type="text/javascript"></script>
    <script src="/assets/js/serial.js" type="text/javascript"></script>
    <script src="/assets/js/pie.js" type="text/javascript"></script>
    <script src="/assets/js/export.min.js" type="text/javascript"></script>
    <script src="/assets/js/light.js" type="text/javascript"></script>
    <link href="/assets/css/export.css" rel="stylesheet" type="text/css" media="all" />
    <style>
        .shadow-hand-lightfury {
            -webkit-box-shadow: 0 1px 5px rgba(0, 0, 0, 1);
            -moz-box-shadow: 0 1px 5px rgba(0, 0, 0, 1);
            -ms-box-shadow: 0 1px 5px rgba(0, 0, 0, 1);
            box-shadow: 0 1px 5px rgba(0, 0, 0, 1);
        }

        .shadow {
            -webkit-box-shadow: 0 1px 5px rgba(0, 0, 0, 0.3);
            -moz-box-shadow: 0 1px 5px rgba(0, 0, 0, 0.3);
            -ms-box-shadow: 0 1px 5px rgba(0, 0, 0, 0.3);
            box-shadow: 0 1px 5px rgba(0, 0, 0, 0.3);
        }

        .amcharts-graph-label {
            font-size: 18px;
        }
    </style>
</head>
<body class="body-padding">
    <form id="Form1" method="post" runat="server" class="cnt">

        <div class="content">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-12">
                        <div class="card " runat="server">
                            <div class="header">
                                <h4 class="title">Pelunasan Piutang
                                    <label style="font-size: 25pt" class="title" id="labelthn3" runat="server" />
                                </h4>
                                <p class="category"></p>
                                <div class="item" style="text-align: right;">
                                    <asp:DropDownList ID="tahun3" runat="server" AutoPostBack="true" Width="110"></asp:DropDownList>
                                    <asp:DropDownList ID="project" runat="server" AutoPostBack="true" Width="110"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="content">
                                <div id="chartBarFin" class="ct-chart" style="height: 500px;"></div>
                                <div class="footer" style="text-align: left;">
                                    <div class="legend">
                                        <i class="fa fa-square text-danger"></i>Memo :
                                        <label id="memolegend" runat="server"></label>
                                        <i class="fa fa-square text-info"></i>Kuitansi :
                                        <label id="ttslegend" runat="server"></label>
                                        <i class="fa fa-square" style="color: #FFD700"></i>Total Pelunasan :
                                        <label id="totallegend" runat="server"></label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>

        <script src="/assets/js/jquery-1.10.2.js" type="text/javascript"></script>
        <script src="/assets/js/bootstrap.min.js" type="text/javascript"></script>
        <script src="/assets/js/chartist.min.js"></script>
        <script src="/assets/js/Chart.js">
        </script>


        <script type="text/javascript">
            AmCharts.ready(function () {
                var chart;
                // SERIAL CHART
                chart = new AmCharts.AmSerialChart();
                chart.dataProvider = chartBarFin;
                chart.categoryField = "periode";
                // the following two lines makes chart 3D
                chart.depth3D = 20;
                chart.angle = 30;

                var legend = new AmCharts.AmLegend();
                legend = new AmCharts.AmLegend();
                legend.position = "bottom";
                legend.align = "left";
                legend.markerType = "square";
                legend.valueText = "";
                chart.addLegend(legend);

                // AXES
                // category
                var categoryAxis = chart.categoryAxis;
                categoryAxis.labelRotation = 0;
                categoryAxis.fillColor = "#0D8ECF";
                categoryAxis.dashLength = 10;
                categoryAxis.gridPosition = "start";

                // value
                var valueAxis = new AmCharts.ValueAxis();
                valueAxis.title = "";
                valueAxis.dashLength = 5;
                chart.addValueAxis(valueAxis);

                // GRAPH
                var graph = new AmCharts.AmGraph();
                graph.title = "Memo";
                graph.valueField = "memo";
                graph.balloonText = "<span style='font-size:14px'>[[category]]: <b>[[value]]</b></span>";
                graph.type = "column";
                graph.lineAlpha = 0;
                graph.lineColor = "#FB404B";
                graph.fillAlphas = 1;
                chart.addGraph(graph);

                // GRAPH
                var graph = new AmCharts.AmGraph();
                graph.title = "Kuitansi";
                graph.valueField = "kuitansi";
                graph.balloonText = "<span style='font-size:14px'>[[category]]: <b>[[value]]</b></span>";
                graph.type = "column";
                graph.lineAlpha = 0;
                graph.lineColor = "#0D8ECF";
                graph.fillAlphas = 1;
                chart.addGraph(graph);

                // GRAPH
                var graph = new AmCharts.AmGraph();
                graph.title = "Total Pelunasan";
                graph.valueField = "total";
                graph.balloonText = "<span style='font-size:14px'>[[category]]: <b>[[value]]</b></span>";
                graph.type = "column";
                graph.lineAlpha = 0;
                graph.lineColor = "#FFD700";
                graph.fillAlphas = 1;
                chart.addGraph(graph);

                // CURSOR
                var chartCursor = new AmCharts.ChartCursor();
                chartCursor.cursorAlpha = 0;
                chartCursor.zoomable = false;
                chartCursor.categoryBalloonEnabled = false;
                chart.addChartCursor(chartCursor);
                chart.creditsPosition = "top-right";
                // WRITE
                chart.write("chartBarFin");
            });
        </script>
    </form>
</body>
</html>


