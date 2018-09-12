$(document).ready(function () {

    $("#numberOfDaysUserLogsInput").on('input', function () {
        printLog();
    });
    printLog();
});

function printLog() {
    $("#logs").empty();
    var daysLogs = $("#numberOfDaysUserLogsInput").val();
    $.ajax({
        //url: "http://localhost:50992/api/GetUserLogsForLastNDaysAsync",
        url: "https://212.227.175.108/api/GetUserLogsForLastNDaysAsync",
        data: { days: daysLogs },
        type: 'GET',
        dataType: "json",
        success: function (data) {
            data.items.forEach(function (item) {
                $("#logs").append("<tr><td>" + item.timestamp + "</td><td>" + item.username + "</td></tr>");
            });
        }
    });
}