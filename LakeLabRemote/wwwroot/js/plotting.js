$(document).ready(function () {
    var d3 = [[0, 12], [7, 12], null, [7, 2.5], [12, 2.5]];
    //$.plot($("#placeholder"), [ d3 ]);
    //$.plot("#placeholder", [[[0, 0], [1, 1]]])
    $.plot($("#placeholder"), [[[0, 0], [1, 1]]], { yaxis: { max: 1 } });


    var deviceWrappers = new Array();
    $(".device-wrapper").each(function () {
        deviceWrappers.push(new DeviceWrapper(//TODO));
    });
    console.log();


    deviceWrappers.forEach(getSensorDataForDeviceForTheLastNDays());
});

///calls the webApp's api to get sensor data
///sensorType: integer value, 0 = dissolved oxygen, 1 = temperature
///days: int value 
function getSensorDataForDeviceForTheLastNDays(deviceId, sensorType, days = 14)
{

}



class DeviceWrapper
{
    constructor(deviceId, deviceName, sensorTypes, days = 7, FromDate, ToDate)
    {
        this.deviceId = deviceId;
        this.deviceName = deviceName;
        this.sensorTypes = sensorTypes;
        this.days = days;
        this.FromDate = FromDate;
        this.ToDate;
    }
}