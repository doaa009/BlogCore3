"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/NotficationHub").build();

//Disable send button until connection is established
document.getElementById("btnSubmit").disabled = true;

connection.on("ReceiveMessage", function (x, y,xx) {

    //var msg = y.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg = x +y +" "+xx;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
     $('.toast').toast('show');
});

connection.start().then(function () {
    document.getElementById("btnSubmit").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});


document.getElementById("btnSubmit").addEventListener("click", function (event) {
    //var user = document.getElementById("userInput").value;
    //var message = document.getElementById("messageInput").value;

    //connection.invoke("SendMessage", user, message).catch(function (err) {
    //    return console.error(err.toString());
    //});
    event.preventDefault();
});