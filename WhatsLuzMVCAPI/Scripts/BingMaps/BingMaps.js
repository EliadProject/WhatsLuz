//get all places info from the DB
var Places = [];

$.ajax({
    type: "GET",
    url: 'http://localhost:61733/Places/getAllPlaces',
    dataType: 'json',
    async: false,
    accepts: "application/json",
    success: function (data) {
        Places = data;
    }
});

function getPlaceCord(PlaceName)
{   
    for (i = 0; i < Places.length; i++) { 
        if(Places[i].Name == PlaceName)
        return Places[i];
    }
}