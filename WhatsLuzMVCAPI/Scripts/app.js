

var module = angular.module('app', ['mwl.calendar', 'ui.bootstrap']);
var data = "";
module.controller('ctrl', function (moment, $scope, calendarConfig) {

    calendarConfig.dateFormatter = 'moment'; // use moment to format dates
    //calendarConfig.templates.calendarMonthCell = 'C:\Eliad\Project\groupedMonthEvents.html';


    moment.locale('he', {
        week: {
            dow: 0// Monday is the first day of the week
        }
    });

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
                //  alert(data);                

                //$scope.dan = data;
                var splitT, splitDash, year, month, day, splitDots, hour, min;

                for (i in data) {

                    
                    // data[i].startsAt = moment(data[i].startsAt).toDate();
                    //data[i].endsAt = moment(data[i].endsAt).toDate();

                    //data[i].startsAt = moment().startOf('week').add(3, 'days').toDate();
                    //data[i].endsAt =moment().startOf('week').add(3, 'days').toDate();

                    data[i].startsAt = new Date(parseInt(data[i].startsAt.replace("/Date(", "").replace(")/", ""), 10));

                    /*
                    //Retrieve Date data for convertion for startsAt
                    splitT = data[i].startsAt.split("T");
                    splitDash = splitT[0].split("-");
                    year = splitDash[0];
                    //month starts from 0
                    month = splitDash[1] - 1;
                    day = splitDash[2];
                    splitDots = splitT[1].split(":");
                    hour = splitDots[0];
                    min = splitDots[1];
                    data[i].startsAt = new Date(year, month, day, hour, min);
                    */
                    data[i].endsAt = new Date(parseInt(data[i].endsAt.replace("/Date(", "").replace(")/", ""), 10));

                    /*
                    //Retrieve Date data for convertion for endsAt
                    splitT = data[i].endsAt.split("T");
                    splitDash = splitT[0].split("-");
                    year = splitDash[0];
                    //month starts from 0
                    month = splitDash[1] - 1;
                    day = splitDash[2];
                    splitDots = splitT[1].split(":");
                    hour = splitDots[0];
                    min = splitDots[1];
                    data[i].endsAt = new Date(year, month, day, hour, min);
                    */
                    $scope.events.push(data[i]);

                }




            }
        });
    }
   

        
       
    
   
    //Gets All the Events
    /*
    $scope.init = function($scope, $http) {
        $http.get("http://localhost:61733/api/Events")
            .then(function (response) {
                $scope.events = response.data;
            })
    })
    */
    /*
    $http({
        method: 'GET',
        url: 'http://localhost:61733/api/Events'
    }).then(function successCallback(response) {
        $scope.events = response;
    });
        
    */

        $scope.calConfig = {
            calendarView: 'month',
            calendarDay: new Date()
        };

    

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
 
  $scope.viewDate = moment().startOf('month').toDate();
  //$scope.updateCloseEvents();
  

  $scope.eventClicked = function(event) {       
       //alert.show('Clicked', event);
       //$("#eventShow-modal").css("display", "block");

      $scope.title = event["title"];
      $scope.category = event["category"];
      $scope.owner = event["owner"];
      $scope.max_attendies = event["max_attendies"];
      $scope.location = event["location"];
      $scope.notes = event["notes"];
      $scope.date = moment(event["startsAt"]).format("DD/MM/YYYY");
      $scope.startsAt = moment(event["startsAt"]).format("HH:mm");
      $scope.endsAt = moment(event["endsAt"]).format("HH:mm");
         
         
                
      

          /*
       $scope.keys = Object.keys(event);
       //$scope.vals  = Object.values(event);
       $scope.vals = Object.keys(event).map(function(key){
		   return event[key];
	   });
       */
       
       //$scope.date_formated = moment($scope.vals[6]).format("HH:mm DD/MM/YYYY");
      /*
       $scope.timedate = moment($scope.vals[6]).format("DD/MM/YYYY");
       $scope.startHour = moment($scope.vals[6]).format("HH:mm");
       $scope.lastHour = $scope.startHour.addHours(va)
      */
       $('#eventShow-modal').modal({
          keyboard: true,
          show: true
        })
    }

  
    
  
  $scope.addEvent = function() {
      
      $scope.events.push ({

      //Event Categories
    title: 'Chuki Fluki 2',
    category : $("#category_select").val(),
    owner : "s6081260",
    max_attendies : $("#attendies_max").val(),
    location : $("#location").val(),
    notes : $("#notes").val(),
    
    //Must Categories
     // The title of the event
    startsAt:  moment($("#datetime").val(), "HH:mm DD/MM/YYYY").toDate(), // A javascript date object for when the event starts
    endsAt: moment($("#datetime").val(), "HH:mm DD/MM/YYYY").add('2', 'hours').toDate(), // Optional - a javascript date object for when the event end
    color: { // can also be calendarConfig.colorTypes.warning for shortcuts to the deprecated event types
      primary: '#e3bc08', // .css("display", "block");he primary event color (should be darker than secondary)
      secondary: '#fdf1ba' // the secondary event color (should be lighter than primary)
    },
    actions: [{ // an array of actions that will be displayed next to the event title
      label: '<i class=\'glyphicon glyphicon-pencil\'></i>', // the label of the action
      cssClass: 'edit-action', // a CSS class that will be added to the action element so you can implement custom styling
      onClick: function(args) { // the action that occurs when it is clicked. The first argument will be an object containing the parent event
        console.log('Edit event', args.calendarEvent);
      }
    }],
    draggable: true, //Allow an event to be dragged and dropped
    resizable: true, //Allow an event to be resizable
    incrementsBadgeTotal: true, //If set to false then will not count towards the badge total amount on the month and year view
    recursOn: 'year', // If set the event will recur on the given period. Valid values are year or month
    cssClass: 'a-css-class-name', //A CSS class (or more, just separate with spaces) that will be added to the event when it is displayed on each view. Useful for marking an event as selected / active etc
    allDay: false // set to true to display 





      })

      $scope.updateCloseEvents();
      
      

    }
/*
  $scope.modifyCell = function(calendarCell) {
      for (var i = 0; i < $scope.dates.length; i++) {
        if (moment(calendarCell.date).isSame(moment($scope.dates[i]), 'day')) calendarCell.isCustom = true;
      };
    }
*/

$scope.cellModifier = function(cell) {
      console.log(cell);
     // cell.cssClass = "background-color" : "coral" ;
      
      
    };
  


});

