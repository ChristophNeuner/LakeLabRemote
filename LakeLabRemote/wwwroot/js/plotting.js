$(document).ready(function () {
        var d3 = [[0, 12], [7, 12], null, [7, 2.5], [12, 2.5]];
        //$.plot($("#placeholder"), [ d3 ]);
        //$.plot("#placeholder", [[[0, 0], [1, 1]]])
        $.plot($("#placeholder"), [[[0, 0], [1, 1]]], { yaxis: { max: 1 } });
});