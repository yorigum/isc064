<%--<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DashboardCollection.aspx.cs" Inherits="DashboardCollection" %>--%>

<%@ Page Language="c#" Inherits="ISC064.COLLECTION.DashboardCollection" CodeFile="DashboardCollection.aspx.cs" %>

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
                                <h4 class="title">Collection</h4>
                                <div class="item" style="text-align: right;">
                                    <asp:DropDownList runat="server" ID="project" AutoPostBack="true"></asp:DropDownList>
                                </div>
                                <p class="category"></p>
                            </div>
                            <div class="content">
                                <div id="chart30" class="ct-chart" style="height: 500px;"></div>
                                <%-- <div class="footer" style="text-align: left;">
                                        <div class="legend">
                                            <asp:Label ID="Label1" runat="server"></asp:Label>
                                        </div>
                                    </div>--%>
                            </div>
                        </div>
                        <div class="card">
                            <div class="header">
                                <h4 class="title">Collection</h4>
                            </div>
                            <div class="content">
                                <div id="chartPieColl" class="ct-chart ct-perfect-fourth"></div>
                                <div class="footer">
                                    <div class="legend">
                                        <i class="fa fa-circle text-info"></i>Piutang
                                        <i class="fa fa-circle text-danger"></i>Pelunasan
                                        <i class="fa fa-circle" style="color: #00e600"></i>Sisa
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
                var legend;


                // PIE CHART
                chart = new AmCharts.AmPieChart();
                chart.dataProvider = PieChartColl;
                chart.categoryField = "periode";
                chart.titleField = "title";
                chart.valueField = "value";
                chart.colorField = "color";
                chart.outlineColor = "#FFFFFF";
                chart.outlineAlpha = 0.8;
                chart.outlineThickness = 2;
                chart.balloonText = "[[title]]<br><span style='font-size:14px'><b>[[value]]</b> ([[percents]]%)</span>";
                // this makes the chart 3D
                chart.depth3D = 15;
                chart.angle = 30;
                chart.creditsPosition = "top-right";
                // WRITE
                chart.write("chartPieColl");
            });

        </script>
        <script type="text/javascript">
            AmCharts.ready(function () {
                var chart;
                // SERIAL CHART
                chart = new AmCharts.AmSerialChart();
                chart.dataProvider = chartz30;
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
                //categoryAxis.dashLength = 5;
                categoryAxis.gridPosition = "start";

                // value
                var valueAxis = new AmCharts.ValueAxis();
                valueAxis.title = "";
                valueAxis.dashLength = 5;
                chart.addValueAxis(valueAxis);

                // GRAPH
                var graph = new AmCharts.AmGraph();
                graph.title = "Piutang";
                graph.valueField = "piutang";
                graph.balloonText = "<span style='font-size:14px'>[[category]]: <b>[[value]]</b></span>";
                graph.type = "column";
                graph.lineAlpha = 0;
                graph.lineColor = "#0D8ECF";
                graph.fillAlphas = 1;
                chart.addGraph(graph);

                // GRAPH
                var graph = new AmCharts.AmGraph();
                graph.title = "Pelunasan";
                graph.valueField = "pelunasan";
                graph.balloonText = "<span style='font-size:14px'>[[category]]: <b>[[value]]</b></span>";
                graph.type = "column";
                graph.lineAlpha = 0;
                graph.lineColor = "#FF0F00";
                graph.fillAlphas = 1;
                chart.addGraph(graph);

                // GRAPH
                var graph = new AmCharts.AmGraph();
                graph.title = "Sisa";
                graph.valueField = "sisa";
                graph.balloonText = "<span style='font-size:14px'>[[category]]: <b>[[value]]</b></span>";
                graph.type = "column";
                graph.lineAlpha = 0;
                graph.lineColor = "#00e600";
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
                chart.write("chart30");
            });
        </script>

    </form>
</body>
</html>


