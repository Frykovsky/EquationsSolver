function draw(data) {
    "use strict";

    var margin = 50, width = 700, height = 300;

    d3.select("#picture")
        .append("svg")
        .attr("width", width)
        .attr("height", height)
        .selectAll()
        .data(data)
        .enter();

    var x_extent = d3.extent(data, function (d) { return d.x; });
    var x_scale = d3.scaleLinear().range([margin, width - margin]).domain(x_extent);
    var y_extent = d3.extent(data, function (d) { return d.val; });
    var y_scale = d3.scaleLinear().range([height - margin, margin]).domain(y_extent);

    d3.selectAll()
        .attr("cx", function (d) { return x_scale(d.x); })
        .attr("cy", function (d) { return y_scale(d.val); });

    var x_axis = d3.axisBottom().scale(x_scale);

    d3.select("svg")
        .append("g")
        .attr("class", "x_axis")
        .attr("transform", "translate(0, " + (height - margin) + ")")
        .call(x_axis);

    var y_axis = d3.axisLeft().scale(y_scale);

    d3.select("svg")
        .append("g")
        .attr("class", "y_axis")
        .attr("transform", "translate(" + margin + ", 0)")
        .call(y_axis);

    d3.select(".x.axis")
        .append("text")
        .text("value of x")
        .attr("x", width / 2 - margin)
        .attr("y", margin / 1.5);

    d3.select(".y.axis")
        .append("text")
        .text("value of y")
        .attr("transform", "rotate (-90, -43, 0) translate(-280)");

    var line = d3.line()
        .x(function (d) { return x_scale(d.x); })
        .y(function (d) { return y_scale(d.val); });

    var initial = d3.select("svg")
        .append("path");
    var result = initial;
    for (var i = 0; i < 100; i++) {
        result = result
            .attr("d", line(data.filter(function (d) { return d.t === i; })))
            .transition();
    }

    d3.select("#restart").on("click", function () {
        var replay = initial;
        for (var i = 0; i < 100; i++) {
            replay = replay
                .attr("d", line(data.filter(function (d) { return d.t === i; })))
                .transition();
        }
    });


}

function stabwarn(item) {
    if (item.checked) { document.getElementById("StabilityWarning").style.display = "none"; }
    else { document.getElementById("StabilityWarning").style.display = "block"; }

}