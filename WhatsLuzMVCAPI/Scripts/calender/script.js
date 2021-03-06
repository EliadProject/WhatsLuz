﻿

$(document).ready(function () {

   

    //on init page - retrieve all events with no filter
    data = "";
    angular.element(document.getElementById('ctrlid')).scope().getevents(data);

    //retrieve all events with filter inserted
    $('.firefilter').click(function () {

        var filter_obj = new Object();

        filter_obj.category = $("#categories_filter").val();
        filter_obj.place = $("#places_filter").val();
        filter_obj.maxAttendies = $("#maxAttendies_filter").val();

        var filter_data = JSON.stringify(filter_obj);


        angular.element(document.getElementById('ctrlid')).scope().getevents(filter_data);


    });
   
    //join event function
   //checks if the user is one of attendies/owner of the event
    $('#sport_event_join').click(function (e) {
        event.preventDefault();
        var event_obj = new Object();
        var eventID = angular.element(document.getElementById('ctrlid')).scope().eventID;
        event_obj.eventID = eventID;
        var event_json = JSON.stringify(event_obj);
       
        $.ajax({
            type: "POST",
            url: "http://localhost:61733/SportEvents/Join",
            data: event_json,
                contentType: "application/json; charset=utf-8",
                dataType: "json",               
                async: false,
                accepts: "application/json",
                success: function (data, status, xhr) {
                    if (data == "Success") {
                        //dismiss modal 
                        $('#eventShow-modal').modal('hide');
                    }
                    else
                    {
                        $("#join_status_id").text(data);
                    }
                }
            });
         

    });

    $('#sport_event_cancel').click(function (e) {
        event.preventDefault();
        var event_obj = new Object();
        var eventID = angular.element(document.getElementById('ctrlid')).scope().eventID;
        event_obj.eventID = eventID;
        var event_json = JSON.stringify(event_obj);

        $.ajax({
            type: "POST",
            url: "http://localhost:61733/SportEvents/cancelEvent",
            data: event_json,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            accepts: "application/json",
            success: function (data, status, xhr) {
                if (data == true) {
                    //dismiss modal 
                    $('#eventShow-modal').modal('hide');
                }
                else {
                    $("#join_status_id").text("User is not among the attendies");
                }
            }
        });


    });
   
    $('#sport_event_delete').click(function (e) {
        event.preventDefault();
        var event_obj = new Object();
        var eventID = angular.element(document.getElementById('ctrlid')).scope().eventID;
        event_obj.eventID = eventID;
        var event_json = JSON.stringify(event_obj);

        $.ajax({
            type: "POST",
            url: "http://localhost:61733/SportEvents/deleteEvent",
            data: event_json,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            accepts: "application/json",
            success: function (data, status, xhr) {
                if (data == true) {
                    //update events on board
                    location.reload();

                    //dismiss modal 
                    $('#eventShow-modal').modal('hide');
                }
                else {

                    $("#join_status_id").text("User is not the owner");
                }
            }
        });


    });


    //Retrieving cateogies from Database
    $.getJSON('http://localhost:61733/SportEvents/getCategoriesName')
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

    $('#eventShow-modal').on('hidden.bs.modal', function () {
        //init status text of join div
        $("#join_status_id").text("");

        

    });
        
        

    

});


 