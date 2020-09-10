function initialData() {
    var userName;
    try {
        $.ajax({
            url: "api/values/GetUsername",
            method: "get",
            data: [
                "id" = 10
            ],
            dataType: "json",
            success: (name) => {
                userName = name
            },
            error: (msg) => {
                window.console.error(msg);
            }
        })
    } catch (e) {
        console.error(e);
        userName = "default";
    }

    var protocol = location.protocol === "https:" ? "wss:" : "ws:";
    var wsUri = protocol + "//" + window.location.host + ':' + window.location.port + "/ws";
    var socket = new WebSocket(wsUri);
    socket.onopen = e => {
        console.log("socket opened", e);
    };
    socket.onclose = function (e) {
        console.log("socket closed", e);
    };
    //function to receive from server.
    socket.onmessage = function (e) {
        console.log("Message:" + e.data);
        $('#msgs').append(e.data + '<br />');
    };
    socket.onerror = function (e) {
        console.error(e.data);
    };
    $("#submit").click(function () {
        var message = userName + ": " + $('#MessageField').val();
        socket.send(message);
        $('#MessageField').val('');
    });
}