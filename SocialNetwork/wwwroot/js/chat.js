"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/ChatHub").build();

document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (message, account, currentTime,idMessage) {
    var div = document.createElement("div");
    div.classList.add("message");

    var info = document.createElement("div");
    info.classList.add("info");
    div.appendChild(info);

    var name = document.createElement("div");
    name.classList.add("name");
    name.textContent = account;
    info.appendChild(name);

    var time = document.createElement("div");
    time.classList.add("time");
    time.textContent = currentTime;
    info.appendChild(time);

    var text = document.createElement("div");
    text.classList.add("textMessage");
    text.textContent = message;
    div.appendChild(text);

    var menu = document.createElement("div");
    menu.classList.add("menuMessage");
    var deleteBtn = document.createElement("button");
    deleteBtn.textContent = "delete";
    deleteBtn.value = idMessage;
    menu.appendChild(deleteBtn);
    var updateBtn = document.createElement("button");
    updateBtn.textContent = "update";
    updateBtn.value = idMessage;
    menu.appendChild(updateBtn);
    div.appendChild(menu);

    document.getElementById("messagesList").appendChild(div);
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
    var idChat = document.getElementById("idChat").value;
    connection.invoke("AddToGroup", idChat).catch(function (err) {
        return console.error(err.toString());
    });
}).catch(function (err) {
    return console.error(err.toString());
});

connection.onclose(function (error) {
    var idChat = document.getElementById("idChat").value;
    connection.invoke("RemoveFromGroup", idChat).catch(function (err) {
        return console.error(err.toString());
    });
});

function sendMessage(idMessage) { 
    var message = document.getElementById("messageInput").value;
    var idAccount = document.getElementById("idAccount").value;
    var idChat = document.getElementById("idChat").value;
    connection.invoke("SendMessage", message, idAccount, idChat).catch(function (err) {
        return console.error(err.toString());
    });
}

function deleteMessage(id) {
    connection.invoke("DeleteMessage", id)
        .then(() => {
            var messageList = document.getElementById("messagesList");
            var messageItem = document.getElementById("message-" + id);
            if (messageItem) {
                messageList.removeChild(messageItem);
            }
        })
        .catch(error => {
            console.error("Error deleting message:", error.toString());
        });
}


function updateMessageBTN(idMessage) {
    var button = document.getElementById("sendButton");
    button.textContent = "Update";
    button.removeEventListener("click", function () { sendMessage(idMessage); }); 
    button.addEventListener("click", function () { updateMessage(idMessage); });
}

function updateMessage(idMessage) {
    var message = document.getElementById("messageInput").value;
    connection.invoke("UpdateMessage", message, idMessage).catch(function (err) {
        return console.error(err.toString());
    });
    var button = document.getElementById("sendButton");
    button.textContent = "Send";
    var messageText = document.getElementById("text-" + idMessage);
    messageText.textContent = message;
    button.removeEventListener("click", function () { updateMessage(idMessage); });
    button.addEventListener("click", function () { sendMessage(idMessage); });
}