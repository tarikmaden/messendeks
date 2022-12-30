am5.ready(function() {

  var root = am5.Root.new("chart-circle");

  const myTheme = am5.Theme.new(root);
  myTheme.rule("Label").set("fontSize", 7);
  myTheme.rule("Grid").set("strokeOpacity", 0.06);

  root.setThemes([
    am5themes_Animated.new(root),
    myTheme
  ]);

  // Data
  var colorsx = [
    '#1abc9c',
    '#2ecc71',
    '#e74c3c',
    '#2980b9',
    '#f39c12',
    '#8e44ad',
    '#2c3e50'
  ]
  var temperatures = {
    "PAZAR": [
      ["Yurtiçi Makroekonomik Performans", 33, 33],
      ["Yurtiçi Pazar Büyüklüğü2", 33, 33],
    ],
    "HUKUKİ ALTYAPI": [
      ["Genel Güvenlik Ortamı",  33, 33],
      ["Mevzuat",  33, 33]
    ],
    "FİNANSMAN VE İŞ YAPILABİLİRLİK": [
      ["Finansman Opsiyonlarının Erişilebilirliği/Bulunurluğu",  39, 39],
      ["Yatırımcılara Devlet Teşvikleri",  39, 39]
    ],
    "TEDARİK ZİNCİRİ": [
      ["Hammadde ve Diğer Girdilere Erişim",  44, 44]
    ],
    "FİZİKSEL VE DİJİTAL ALTYAPI": [
      ["Fiziksel Altyapının Kalitesi", 53, 53],
      ["Dijital Altyapının Kalitesi", 53, 53]
    ],
    "İŞ GÜCÜ": [
      ["İş Gücü Havuzunun Yetkinlik ve Kabiliyet Seviyesi", 46, 46],
      ["İş Gücü Maliyeti", 46, 46]
    ],
    "AR-GE İNOVASYON": [
      ["Teknoloji ve İnovasyon Kabiliyetleri", 48, 48],
      ["AR-GE Kabiliyetleri", 48, 48]
    ]
  }
  var nums = [];
  var numsx = Object.keys(temperatures).forEach(key => {
    temperatures[key].forEach(val => {
      nums.push(val.slice(-1)[0]);
    });
  });
  var avg = arr => arr.reduce((p, c) => p + c, 0) / arr.length;

  function hover_data(curr) {
    var keyx;
    var nums = [];
    var numsx = Object.keys(temperatures).forEach(key => {
      temperatures[key].forEach(val => {
        if (val[0] == curr) {
          keyx = key;
          temperatures[key].forEach(valx => {
            nums.push(valx.slice(-1)[0])
          });
        }
      });
    });
    return `[bold]${keyx}:[/] ${Math.round(avg(nums))}`;
  }

  // Modify defaults
  root.numberFormatter.set("numberFormat", "#|#|0");

  var startYear = 2022;
  var endYear = 2022;
  var currentYear = 2022;

  var div = document.getElementById("chart-circle");

  var colorSet = am5.ColorSet.new(root, {});


  // Create chart
  // https://www.amcharts.com/docs/v5/charts/radar-chart/
  var chart = root.container.children.push(am5radar.RadarChart.new(root, {
    panX: false,
    panY: false,
    wheelX: "panX",
    wheelY: "zoomX",
    innerRadius: am5.percent(40),
    radius: am5.percent(85),
    startAngle: 270 - 170,
    endAngle: 270 + 170
  }));

  // Add cursor
  // https://www.amcharts.com/docs/v5/charts/radar-chart/#Cursor
  var cursor = chart.set("cursor", am5radar.RadarCursor.new(root, {
    behavior: "zoomX",
    radius: am5.percent(40),
    innerRadius: -1
  }));
  cursor.lineY.set("visible", false);


  // Create axes and their renderers
  // https://www.amcharts.com/docs/v5/charts/radar-chart/#Adding_axes
  var xRenderer = am5radar.AxisRendererCircular.new(root, {
    minGridDistance: 10
  });

  xRenderer.labels.template.setAll({
    radius: -99999,
    textType: "radial",
    centerY: am5.p50
  });

  var yRenderer = am5radar.AxisRendererRadial.new(root, {
    axisAngle: 90
  });

  yRenderer.labels.template.setAll({
    centerX: am5.p50
  });

  var tooltipx = am5.Tooltip.new(root, {
    labelText: '{category}',
    fill: '#007AC3'
  });
  var categoryAxis = chart.xAxes.push(am5xy.CategoryAxis.new(root, {
    maxDeviation: 0,
    categoryField: "country",
    renderer: xRenderer,
    tooltip: tooltipx
  }));

  categoryAxis.get("tooltip").adapters.add("labelText", function(text, target) {
    if (target.dataItem) {
      // tooltipx.get("background").setAll({
      //   fill: colorsx[1]
      // });
      return hover_data(target.dataItem.dataContext.country);
    }
  });



  var valueAxis = chart.yAxes.push(am5xy.ValueAxis.new(root, {
    min: 0,
    max: 50,
    extraMax: 0.1,
    renderer: yRenderer
  }));

  // Create series
  // https://www.amcharts.com/docs/v5/charts/radar-chart/#Adding_series
  var series = chart.series.push(am5radar.RadarColumnSeries.new(root, {
    calculateAggregates: true,
    name: "Series",
    xAxis: categoryAxis,
    yAxis: valueAxis,
    valueYField: "value" + currentYear,
    categoryXField: "country",
    tooltip: am5.Tooltip.new(root, {
      labelText: "[bold]{categoryX}:[/] {valueY}"
    }),
    //fill: 'red'
  }));

  series.columns.template.set("strokeOpacity", 0);
  //series.columns.template.set("fill", 'blue');


  // Set up heat rules
  //https://www.amcharts.com/docs/v5/concepts/settings/heat-rules/
  series.set("heatRules", [{
    target: series.columns.template,
    key: "fill",
    min: am5.color(0x673AB7),
    max: am5.color(0xF44336),
    dataField: "valueY"
  }]);

  // Add scrollbars
  // https://www.amcharts.com/docs/v5/charts/xy-chart/scrollbars/
  // chart.set("scrollbarX", am5.Scrollbar.new(root, { orientation: "horizontal" }));
  // chart.set("scrollbarY", am5.Scrollbar.new(root, { orientation: "vertical" }));

  // Add year label
  var avg_init = Math.round(avg(nums));
  var y_label_opts = {
    fontSize: "3.5em",
    text: avg_init, // Ortadaki Yazı
    centerX: am5.p50,
    centerY: am5.p50,
    fill: am5.color(0x673AB7)
  }
  var yearLabel = chart.radarContainer.children.push(am5.Label.new(root, y_label_opts));


  // Generate and set data
  // https://www.amcharts.com/docs/v5/charts/radar-chart/#Setting_data
  var data = generateRadarData();
  series.data.setAll(data);
  categoryAxis.data.setAll(data);

  series.appear(1000);
  chart.appear(1000, 100);

  function generateRadarData() {
    var data = [];
    var i = 0;
    for (var continent in temperatures) {
      var continentData = temperatures[continent];

      continentData.forEach(function(country) {
        var rawDataItem = { "country": country[0] }

        for (var y = 2; y < country.length; y++) {
          rawDataItem["value" + (startYear + y - 2)] = country[y];
        }

        data.push(rawDataItem);
      });

      createRange(continent, continentData, i);
      i++;

    }
    return data;
  }


  function createRange(name, continentData, index) {
    var axisRange = categoryAxis.createAxisRange(categoryAxis.makeDataItem({above:true}));
    // categoryAxis.showTooltip();
    axisRange.get("label").setAll({ text: name });
    // first country
    axisRange.set("category", continentData[0][0]);
    var data_arrx = []
    continentData.forEach(val => {
      data_arrx.push(val.slice(-1)[0])
    })
    axisRange.set("valx", data_arrx);
    // last country
    axisRange.set("endCategory", continentData[continentData.length - 1][0]);

    // every 3rd color for a bigger contrast
    var fill = axisRange.get("axisFill");
    fill.setAll({
      toggleKey: "active",
      cursorOverStyle: "pointer",
      fill: colorsx[index],
      visible: true,
      innerRadius: -25,
      // hoverable: true,
      // hoverOnFocus: true
    });
    fill.events.on('pointerover', function (e) {
      //console.log(Math.round(avg(e.target.dataItem.get('valx'))));
      //chart.showTooltip();
      // $('#prv-tooltip').css({
      //   'left': e.point.x + 'px',
      //   'top': e.point.y + 'px'
      // });
      //console.log('in');
    });
    // fill.events.on('pointerout', function (e) {
    //   fill.events.hover();
    // });
    axisRange.get("grid").set("visible", false);

    var label = axisRange.get("label");
    label.setAll({
      fill: am5.color(0xffffff),
      textType: "circular",
      radius: -16
    });

    fill.events.on("click", function(event) {
      var dataItem = event.target.dataItem;
      yearLabel.set("text", Math.round(avg(dataItem.get("valx"))));

      if (event.target.get("active")) {
        categoryAxis.zoom(0, 1);
        yearLabel.set("text", avg_init);
      }
      else {
        categoryAxis.zoomToCategories(dataItem.get("category"), dataItem.get("endCategory"));
      }
    });


  }

  chart.zoomOutButton.events.on('click', function () {
    yearLabel.set("text", avg_init);
  });

  // categoryAxis.events.on("over", function(event) {
  //     var dataItem = event.target.dataItem;

  //     console.log(123123);
  //   });

  // chart.interactionsEnabled = true;
  // chart.events.on("pointerover", function (e) {
  //   console.log(123);
  // });


  // Create controls
  // var container = chart.children.push(am5.Container.new(root, {
  //   y: am5.percent(95),
  //   centerX: am5.p50,
  //   x: am5.p50,
  //   width: am5.percent(80),
  //   layout: root.horizontalLayout
  // }));

  // var playButton = container.children.push(am5.Button.new(root, {
  //   themeTags: ["play"],
  //   centerY: am5.p50,
  //   marginRight: 15,
  //   icon: am5.Graphics.new(root, {
  //     themeTags: ["icon"]
  //   })
  // }));

  // playButton.events.on("click", function () {
  //   if (playButton.get("active")) {
  //     slider.set("start", slider.get("start") + 0.0001);
  //   }
  //   else {
  //     slider.animate({
  //       key: "start",
  //       to: 1,
  //       duration: 15000 * (1 - slider.get("start"))
  //     });
  //   }
  // })

  // var slider = container.children.push(am5.Slider.new(root, {
  //   orientation: "horizontal",
  //   start: 0.5,
  //   centerY: am5.p50
  // }));

  // slider.on("start", function (start) {
  //   if (start === 1) {
  //     playButton.set("active", false);
  //   }
  // });

  // slider.events.on("rangechanged", function () {
  //   updateRadarData(startYear + Math.round(slider.get("start", 0) * (endYear - startYear)));
  // });

  // function updateRadarData(year) {
  //   if (currentYear != year) {
  //     currentYear = year;
  //     yearLabel.set("text", currentYear.toString());
  //     am5.array.each(series.dataItems, function (dataItem) {
  //       var newValue = dataItem.dataContext["value" + year];
  //       dataItem.set("valueY", newValue);
  //       dataItem.animate({ key: "valueYWorking", to: newValue, duration: 500 });
  //     });
  //   }
  // }

}); // end am5.ready()