

$(document).ready(function () {

    $('#sport_event_join').click(function () {

        /*
        $('#eventShow-modal').modal({
            keyboard: false,
            show: false
        });
        */
    });

    //on init page - retrieve all events with no filter
    data = "";
    angular.element(document.getElementById('ctrlid')).scope().getevents(data);

    //retrieve all events with filter inserted
    $('.firefilter').click(function () {

        var filter_obj = new Object();

        filter_obj.category = $("#categories_filter").val();
        filter_obj.place = $("#places_filter").val();
        filter_obj.date = $("#date_filter").val();

        var filter_data = JSON.stringify(filter_obj);


        angular.element(document.getElementById('ctrlid')).scope().getevents(filter_data);


    });

    /*
    $.get(
        "http://localhost:61733/Cookie"
    );

        

        //join event function
        //checks if the user is one of attendies/owner of the event

    $('#sport_event_join').click(function () {
        var cookie = document.cookie;
        var username = cookie.split('=')[1];
        var owner_id = $("#owner_id").text().replace(/\s/g, '');

        if (username == owner_id)
            alert("לא ניתן להצטרף, אתה הוא בעל האירוע");

    }); 
    */
   


   

    //Retrieving cateogies from Database
    $.getJSON('http://localhost:61733/Category/getCategoriesName')
        .done(function (data) {
            $.each(data, function (key, item) { 
                $('<option>', { text: item }).appendTo($('.categories'));
            });
        });

    //Retrieving places from Database
    $.getJSON('http://localhost:61733/places/getplacesname')
        .done(function (data) {
            $.each(data, function (key, item) {
                $('<option>', { text: item }).appendTo($('.places'));
            });
        });

    //customizing datetimepicker format
    $('#datetimepicker1').datetimepicker(
        {
            locale: 'en',
            format: "HH:mm DD/MM/YYYY"

        });
   

    //create event function
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

    //add event ajax funtion
    function addEvent(sport_event) {
        $.ajax({
            type: "POST",
            url: "http://localhost:61733/SportEvents/createEvent",
            data: sport_event,
            contentType: "application/json; charset=utf-8",
            success: function () {
                location.reload();
            },
            error: function (XMLHttpReqest, textStatus, errorThrown) {
                alert(errorThrown.textStatus);
            }

        });
    }

    
        
        

    

});


 