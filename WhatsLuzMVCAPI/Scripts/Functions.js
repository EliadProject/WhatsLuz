function form_eventSport_send() {
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
}
function checkJoin() {
    var cookie = document.cookie;
    var username = cookie.split('=')[1];
}

function addEvent(sport_event) {
    $.ajax({
        type: "POST",
        url: "http://localhost:61733/api/Events/createEvent",
        data: sport_event,
        success: function () {
            alert('success');
        },
        error: function (XMLHttpReqest, textStatus, errorThrown) {
            alert(errorThrown);
        }
    });
}