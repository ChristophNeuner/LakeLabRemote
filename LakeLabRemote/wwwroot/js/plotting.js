$(document).ready(function () {
    var d3 = [[0, 12], [7, 12], null, [7, 2.5], [12, 2.5]];
    //$.plot($("#placeholder"), [ d3 ]);
    //$.plot("#placeholder", [[[0, 0], [1, 1]]])
    $.plot($("#placeholder"), [[[0, 0], [1, 1]]], { yaxis: { max: 1 } });


    //TODO: loop through all device wrappers and draw the graphs
    var wrappers = getAllDeviceWrappers();
    console.log(wrappers);
    console.log(wrappers[0]);
    getSensorDataForDeviceForTheLastNDays(wrappers[0].deviceId, 1, 7);
});


//gets all elements where class="device-wrapper" and maps them into a list of DeviceWrapper objects
function getAllDeviceWrappers()
{
    var deviceWrappers = new Array();
    $(".device-wrapper").each(function () {
        var id = $(this).attr("data-device-id");
        var name = $(this).attr("data-device-name");
        ///later this should be read from html, but at the moment the device class does not have a list with its sensor types
        var sensorTypes = [0, 1];
        var days = $(this).find(".days-input").val();
        var start;
        var end;
        deviceWrappers.push(new DeviceWrapper(id, name, sensorTypes, days, start, end));
    });
    
    return deviceWrappers;
}


//calls the webApp's api to get sensor data
//sensorType: integer value, 0 = dissolved oxygen, 1 = temperature
//days: int value
function getSensorDataForDeviceForTheLastNDays(deviceId, sensorType, days)
{
    $.ajax({
        url: "http://localhost:50992/api/GetValuesFromLastNDaysAsJsonAsync",
        data: { deviceId: deviceId, sensorType: sensorType, days: days },
        dataType: "json",
        success: function (data) {
            console.log(data);
            //TODO: call a function that draws the graph with the data
        }
    });
}


function drawGraph()
{
    //TODO
}


class DeviceWrapper
{
    constructor(deviceId, deviceName, sensorTypes, days = 7, StartDate, EndDate)
    {
        this.deviceId = deviceId;
        this.deviceName = deviceName;
        this.sensorTypes = sensorTypes;
        this.days = days;
        this.StartDate = StartDate;
        this.EndDate = EndDate;
    }
}