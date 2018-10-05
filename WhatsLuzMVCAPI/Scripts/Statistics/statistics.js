
    var CatContent = [];
    var PlacesContent = [];

    $.ajax({
        type: "GET",
        url: 'http://localhost:61733/SportEvents/getCategoriesStatistics',
        dataType: 'json',
        async: false,
        accepts: "application/json",
        success: function (data) {
            for (i in data) {
                CatContent.push(
               {
                   label: data[i].label,
                   value: data[i].value
               });
                console.log("I'm Here!")
            }
        }
    });
   
    var pie = new d3pie("CatPie", {
        header: {
            title: {
                text: "Events Pie by Category"
            }
        },
        data: {
            content: CatContent
        },

        //Here further operations/animations can be added like click event, cut out the clicked pie section.
        callbacks: {
            onMouseoverSegment: function (info) {
                console.log("mouse in", info);
            },
            onMouseoutSegment: function (info) {
                console.log("mouseout:", info);
            }
        }
    });
/*
    $.ajax({
        type: "GET",
        url: 'http://localhost:61733/SportEvents/getTopTenPlacesStatistics',
        dataType: 'json',
        async: false,
        accepts: "application/json",
        success: function (data) {
            for (i in data) {
                PlacesContent.push(
                    {
                        label: data[i].label,
                        value: data[i].value
                    });
                console.log("I'm Here!")
            }
        }
    });

    var pie = new d3pie("PlacesPie", {
        header: {
            title: {
                text: "Events Pie by Top 10 Places"
            }
        },
        data: {
            content: PlacesContent
        },

        //Here further operations/animations can be added like click event, cut out the clicked pie section.
        callbacks: {
            onMouseoverSegment: function (info) {
                console.log("mouse in", info);
            },
            onMouseoutSegment: function (info) {
                console.log("mouseout:", info);
            }
        }
    });


    */