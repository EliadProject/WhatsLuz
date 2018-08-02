
$(document).ready(function () {
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
                $('<option>', { text: item }).appendTo($('#categories'));
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
            sport_event.categories = $("#categories").val();
            sport_event.datetime = $("#datetime").val();
            sport_event.attendies = $("#attendies").val();
            sport_event.duration = $("#duration").val();
            sport_event.location = $("#location").val();
            sport_event.notes = $("#notes").val();



            //converting to json
            var json_sport_event = JSON.stringify(sport_event);

            addEvent(json_sport_event);
        }
    });
        
        

    

});


 /*
    function addEvent(sport_event) {
        $.ajax({
            type: "POST",
            url: "http://localhost:61733/api/Events",
            data: sport_event,
            success: function () {
                alert('success');
            },
            error: function (XMLHttpReqest, textStatus, errorThrown) {
                alert(errorThrown);
            }
        });
    }
    */
    /*
    $('#sport_event_join').click(function () {
        var check_disabled = $(this).attr("class");
        if (check_disabled.includes("disabled") == true) {

        }
    }
    */
 /*
    $('#form_eventSport_send').click(function () {
        var check_disabled = $(this).attr("class");
        if (check_disabled.includes("disabled") == true) {
            
        }
        else {
            var sport_event = new Object();
            //retrieve forms values 
            sport_event.title = $("#title").val();
            sport_event.categories = $("#categories").val();
            sport_event.datetime = $("#datetime").val();
            sport_event.attendies = $("#attendies").val();
            sport_event.duration = $("#duration").val();
            sport_event.location = $("#location").val();
            sport_event.notes = $("#notes").val();
            


            //converting to json
            var json_sport_event = JSON.stringify(sport_event);

            addEvent(json_sport_event);


           
        }




    });
    */