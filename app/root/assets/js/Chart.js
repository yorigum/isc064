

Chart = {    
    
    initChartist: function(){    
    
    
        //var BarChartData1 = {
        //    labels: ['Jan', 'Feb', 'Mar', 'Apr', 'Mai', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
        //    series: [
        //      [542, 443, 320, 780, 553, 453, 326, 434, 568, 610, 756, 1095],
        //      [412, 243, 280, 580, 453, 353, 300, 364, 368, 410, 636, 695],
        //      [412, 243, 280, 580, 453, 353, 300, 364, 368, 410, 850, 750]
        //    ]
        //};
        
        //var options = {
        //   // seriesBarDistance: 12,
        //    axisX: {
        //        showGrid: true
        //    },
        //    height: "400px"
        //};
        
        var responsiveOptions = [
          ['screen and (max-width: 640px)', {
            seriesBarDistance: 10,
            axisX: {
              labelInterpolationFnc: function (value) {
                return value[0];
              }
            }
          }]
        ];
        Chartist.Bar('#chartActivity', BarChartData, options, responsiveOptions);
        var options2 = {
            seriesBarDistance: 12,
            axisX: {
                showGrid: true
            },
            axisY: {
                    type: Chartist.FixedScaleAxis,
                    ticks: [0, 10, 20, 30, 40, 50, 60, 70, 80, 90, 100],
                    low: 0,
                    high:100
                },
            height: "400px"
        };
        Chartist.Bar('#ChartPersentase', BarChartpersen, options2, responsiveOptions);
        
         Chartist.Pie('#chartPreferences', dataPreferences);
    }

    
}

