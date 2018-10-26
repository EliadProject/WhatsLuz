function getTemperature(cord,divId)
{
    var weatherContent = [];

    $.ajax({
        type: "GET",
        url: 'http://api.openweathermap.org/data/2.5/weather?lat=' + cord.lat + '&lon=' + cord.lng +'&appid=973c170e0e5b7c560beeca7f7566aece',
        dataType: 'json',
        async: false,
        accepts: "application/json",
        success: function (data) {
            weatherContent.push(
            {
                main: data["weather"][0].main,
                description: data["weather"][0].description
                });
            document.getElementById(divId + "_weatherImg").src = "http://openweathermap.org/img/w/" + data["weather"][0].icon +".png";
            //console.log(weatherContent[0]["description"]);
            document.getElementById(divId + "_weather").innerHTML =
                weatherContent[0]["main"] + ", " + weatherContent[0]["description"];
        }
    });
}