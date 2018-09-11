$(document).ready(function () {
    //var d3 = [[0, 12], [7, 12], null, [7, 2.5], [12, 2.5]];
    //$.plot($("#placeholder"), [ d3 ]);
    //$.plot("#placeholder", [[[0, 0], [1, 1]]]);
    //$.plot($("#placeholder"), [[[0, 0], [1, 1]]], { yaxis: { max: 1 } });
    //$("#index-refresh").click(function () {
    //    main();
    //});
    $("#numberOfDaysInput").on('input', function () {
        main();
    });
    main();
});


function main() {
    var wrappers = getAllDeviceWrappers();
    wrappers.forEach(function (elem) {
        getSensorDataForDeviceForTheLastNDaysAndDrawGraphs(elem);
    });
}

//gets all elements where class="device-wrapper" and maps them into a list of DeviceWrapper objects
function getAllDeviceWrappers()
{
    var deviceWrappers = new Array();
    $(".device-wrapper").each(function () {
        var htmlElementRef = $(this);
        var id = $(this).attr("data-device-id");
        var name = $(this).attr("data-device-name");

        ///later this should be read from html, but at the moment the device class does not have a list with its sensor types
        var sensorTypes = [0, 1];

        var days = $(this).find(".days-input").val();
        var start;
        var end;
        deviceWrappers.push(new DeviceWrapper(htmlElementRef, id, name, sensorTypes, days, start, end));
    });
    
    return deviceWrappers;
}


//calls the webApp's api to get sensor data
//sensorType: integer value, 0 = dissolved oxygen, 1 = temperature
//days: int value
function getSensorDataForDeviceForTheLastNDaysAndDrawGraphs(deviceWrapper)
{
    deviceWrapper.sensorTypes.forEach(function (sensorType) {
        switch (sensorType) {
            case 0:
                $.ajax({
                    //url: "http://localhost:50992/api/GetValuesFromLastNDaysAsJsonAsync",
                    url: "https://212.227.175.108/api/GetValuesFromLastNDaysAsJsonAsync",
                    data: { deviceId: deviceWrapper.deviceId, sensorType: sensorType, days: deviceWrapper.days },
                    type: 'GET',
                    dataType: "json",
                    success: function (data) {
                        var values = [];
                        var n = 1;
                        data.items.forEach(function (item) {
                            valuePair = [item.timestamp, item.data];
                            values.push(valuePair);
                            n++;
                        });
                        //$.plot($(deviceWrapper.htmlElementRef).find(".graph-do"), [values], { xaxis: { mode: "time", timeformat: "%d.%m.%y %H:%M" } });
                        $.plot($(deviceWrapper.htmlElementRef).find(".graph-do"), [values], { xaxis: { mode: "time", timeformat: "%d.%m.\n%H:%M" } });
                    }
                });
                break;
            case 1:
                $.ajax({
                    //url: "http://localhost:50992/api/GetValuesFromLastNDaysAsJsonAsync",
                    url: "https://212.227.175.108/api/GetValuesFromLastNDaysAsJsonAsync",
                    data: { deviceId: deviceWrapper.deviceId, sensorType: sensorType, days: deviceWrapper.days },
                    type: 'GET',
                    dataType: "json",
                    success: function (data) {
                        var values = [];
                        var n = 1;
                        data.items.forEach(function (item) {
                            valuePair = [item.timestamp, item.data];
                            values.push(valuePair);
                            n++;
                        });
                        //$.plot($(deviceWrapper.htmlElementRef).find(".graph-temp"), [values], { xaxis: { mode: "time", timeformat: "%d.%m.%y %H:%M" } });
                        $.plot($(deviceWrapper.htmlElementRef).find(".graph-temp"), [values], { xaxis: { mode: "time", timeformat: "%d.%m.\n%H:%M" } });
                    }
                });
                break;
            default:
                console.log("default reached in drawGraph");
                break;
        }
    });  
}


//sensorTypes: dictionary with sensorType as the key and the sensor data as values
////sensorType: integer value, 0 = dissolved oxygen, 1 = temperature
class DeviceWrapper
{
    constructor(htmlElementRef, deviceId, deviceName, sensorTypes, days = 7, StartDate, EndDate)
    {
        this.htmlElementRef = htmlElementRef;
        this.deviceId = deviceId;
        this.deviceName = deviceName;
        this.sensorTypes = sensorTypes;
        this.days = days;
        this.StartDate = StartDate;
        this.EndDate = EndDate;
    }
}