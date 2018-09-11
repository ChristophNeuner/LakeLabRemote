$(document).ready(function () {

    $("#numberOfDaysUserLogsInput").on('input', function () {
        printLog();
    });
    printLog();
});

function printLog() {
    $("#logs").empty();
    var days = $(this).find(".days-input").val();
    $.ajax({
        //url: "http://localhost:50992/api/GetUserLogsForLastNDaysAsync",
        url: "https://212.227.175.108/api/GetUserLogsForLastNDaysAsync",
        data: {days : days },
        type: 'GET',
        dataType: "json",
        success: function (data) {
            data.items.forEach(function (item) {
                $("#logs").append("<tr><td>" + item.timestamp + "</td><td>" + item.username + "</td></tr>");
            });
        }
    });
}