
$(document).ready(function () {
    //get all events
    data = "";
    angular.element(document.getElementById('ctrlid')).scope().getevents(data);


    $('.firefilter').click(function () {

        var filter_obj= new Object();
        filter_obj.place = $("#places_filter").val();
        filter_obj.date = $("#date_filter").val();
     
        var filter_data = JSON.stringify(filter_obj);

    
        angular.element(document.getElementById('ctrlid')).scope().getevents(filter_data);
        console.log(filter_data);
    })

    /*
    $.get(
        "http://localhost:61733/Cookie"
    );

        

    $('#sport_event_join').click(function () {
        var cookie = document.cookie;
        var username = cookie.split('=')[1];
        var owner_id = $("#owner_id").text().replace(/\s/g, '');

        if (username == owner_id)
            alert("לא ניתן להצטרף, אתה הוא בעל האירוע");

    }); 
    */
    


    //Retrieving cateogies from Database
    $.getJSON('http://localhost:61733/api/Category')
        .done(function (data) {
            $.each(data, function (key, item) {
                /*
                $(".categories").each(function () {    //loop over each list item
                    
                });
                */
                $('<option>', { text: item }).appendTo($('.categories'));
            });
        });


    //customizing datetimepicker
    $('#datetimepicker1').datetimepicker(
        {
            locale: 'en',
            format: "HH:mm DD/MM/YYYY"

        });
   

    $('#form_eventSport_send').click(function () {
        var check_disabled = $(this).attr("class");
        if (check_disabled.includes("disabled") == true) {
            //
        }
        else {
            var sport_event = new Object();
            //retrieve forms values 
            sport_event.title = $("#title").val();
            sport_event.category = $("#categories_create").val();
            sport_event.datetime = $("#datetime").val();
            sport_event.max_attendies = $("#attendies").val();
            sport_event.duration = $("#duration").val();
            sport_event.location = $("#location").val();
            sport_event.notes = $("#notes").val();



            //converting to json
            var json_sport_event = JSON.stringify(sport_event);

            addEvent(json_sport_event);
        }
    });

    
        
        

    

});


 