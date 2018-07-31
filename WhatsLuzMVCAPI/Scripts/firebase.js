﻿
  // Initialize Firebase
  var config = {
    apiKey: "AIzaSyAtNkWrROVN5t19GqcYTEZuJr0knq7Rqjc",
    authDomain: "api-project-116228982858.firebaseapp.com",
    databaseURL: "https://api-project-116228982858.firebaseio.com",
    projectId: "api-project-116228982858",
    storageBucket: "api-project-116228982858.appspot.com",
    messagingSenderId: "116228982858"
  };
  firebase.initializeApp(config);
  
  var provider = new firebase.auth.FacebookAuthProvider();
console.log(provider)

firebase.auth().signInWithPopup(provider).then(function(result) {
  // This gives you a Facebook Access Token. You can use it to access the Facebook API.
  var token = result.credential.accessToken;
  // The signed-in user info.
  var user = result.user;
  //Redirect to main project page
  window.location = "http://localhost:61733/Home/Index";

}).catch(function(error) {
  // Handle Errors here.
    // window.location = "/Views/Shared/Error.cshtml";
    console.log("error");
  var errorCode = error.code;
  var errorMessage = error.message;
  // The email of the user's account used.
  var email = error.email;
  // The firebase.auth.AuthCredential type that was used.
  var credential = error.credential;
  // ...
    }
)
   ;
 
