

var module = angular.module('app', ['mwl.calendar', 'ui.bootstrap']);
module.controller('ctrl', function ($scope, calendarConfig) {

    
    //more calender settings 
    $scope.calConfig = {
        calendarView: 'month',
        calendarDay: new Date()
    };

    //receives filter data and get the events by the filter 
    //on init page this function called without filter
    $scope.getevents = function (filter_data) {
        $scope.events = [];
        $.ajax({
            type: "POST",
            url: "http://localhost:61733/SportEvents/getEvents",
            data: filter_data,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            accepts: "application/json",
            async: false,
            success: function (data, status, xhr) {
                                    
                var splitT, splitDash, year, month, day, splitDots, hour, min;

                for (i in data) {

                    //parsing date from json
                    data[i].startsAt = new Date(parseInt(data[i].startsAt.replace("/Date(", "").replace(")/", ""), 10));
                    data[i].endsAt = new Date(parseInt(data[i].endsAt.replace("/Date(", "").replace(")/", ""), 10));
                    $scope.events.push(data[i]);

                }




            }
        });
    }
   
 
  $scope.viewDate = moment().startOf('month').toDate();
  //$scope.updateCloseEvents();
  

  $scope.eventClicked = function(event) {       
      $scope.eventID = event["eventID"];
      $scope.title = event["title"];
      $scope.category = event["category"];
      $scope.owner = event["owner"];
      $scope.max_attendies = event["max_attendies"];
      $scope.location = event["location"];
      $scope.notes = event["notes"];
      $scope.date = moment(event["startsAt"]).format("DD/MM/YYYY");
      $scope.startsAt = moment(event["startsAt"]).format("HH:mm");
      $scope.endsAt = moment(event["endsAt"]).format("HH:mm");
         
         
         //make the eventShow-modal appear       
            $('#eventShow-modal').modal({
         // keyboard: true,
         // show: true
        })
    }


    //This example below gives an example for event that can be viewed in this angular calender
    /*
    $scope.events = [
    
    {
  
      //Event Categories
      title: 'Chuki Fluki 1 ',
      category : "לגרודכ",
      owner : "s6081260",
      max_attendies : "5",
      location : "םש להוא",
      notes : "רחאל אל",
      
      //Must Categories
       // The title of the event
      startsAt:  moment().startOf('week').add(3, 'days').toDate(), // A javascript date object for when the event starts
      endsAt: moment().startOf('week').add(3, 'days').toDate(), // Optional - a javascript date object for when the event ends
     
      color: { // can also be calendarConfig.colorTypes.warning for shortcuts to the deprecated event types
          primary: '#ff0000', // .css("display", "block");he primary event color (should be darker than secondary)
        secondary: '#fdf1ba' // the secondary event color (should be lighter than primary)
      },
      
      draggable: true, //Allow an event to be dragged and dropped
      cssClass: 'a-css-class-name', //A CSS class (or more, just separate with spaces) that will be added to the event when it is displayed on each view. Useful for marking an event as selected / active etc
      allDay: false // set to true to display the event as an all day event on the day view
    },{
     //Event Categories
      title: 'Chuki Fluki 2',
      category : "לגרודכ",
      owner : "s6081260",
      max_attendies : "8",
      location : "םש דן",
      notes : "רחאל אל",
      
      //Must Categories
       // The title of the event
      startsAt:  moment().startOf('week').add(2, 'days').toDate(), // A javascript date object for when the event starts
      endsAt: moment().startOf('week').add(2, 'days').toDate(), // Optional - a javascript date object for when the event ends
     
      color: { // can also be calendarConfig.colorTypes.warning for shortcuts to the deprecated event types
          primary: '#ff0000', // .css("display", "block");he primary event color (should be darker than secondary)
          secondary: '#ff0000' // the secondary event color (should be lighter than primary)
      },
      actions: [{ // an array of actions that will be displayed next to the event title
        label: '<i class=\'glyphicon glyphicon-pencil\'></i>', // the label of the action
        cssClass: 'edit-action', // a CSS class that will be added to the action element so you can implement custom styling
        onClick: function(args) { // the action that occurs when it is clicked. The first argument will be an object containing the parent event
          console.log('Edit event', args.calendarEvent);
        }
      }],
      draggable: true, //Allow an event to be dragged and dropped
      cssClass: 'a-css-class-name', //A CSS class (or more, just separate with spaces) that will be added to the event when it is displayed on each view. Useful for marking an event as selected / active etc
      allDay: false // set to true to display the event as an all day event on the day view
    }
  
  
  ];
   */
    

    //Update close Events, not relavent for us
    /*
       $scope.updateCloseEvents = function () {

           var temp = $scope.events.slice(0);

           $scope.close_events_arr = [];
           var max_date_diff = 0;
           var max_date_index = 0;
           var cur_date = moment();
           for (var j = 0; j < 2; j++) {
               max_date_diff = 0;
               for (var i = 0; i < temp.length; i++) {
                   var diff = cur_date.diff(temp[i].startsAt)
                   if (diff < max_date_diff) {
                       max_date_diff = diff;
                       max_date_index = i;
                   }
               }
               if (max_date_diff != 0) {

                   $scope.close_events_arr.push(temp[max_date_index]);

                   // var length_close_arr =  $scope.close_events_arr.length
                   // $scope.close_events_arr[length_close_arr-1].startsAt = moment($scope.close_events_arr[length_close_arr-1].startsAt).format("HH:mm DD/MM/YYYY");

                   temp.splice(max_date_index, 1);

               }


           }
       }
       */
  


  


});

