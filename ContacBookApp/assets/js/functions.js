

let OnBegin = (response) => {

}

function OnSuccess(response) {
    let type = response.Type.split("-");
    $.each(type, function (index, value) {
        console.log(value);
        switch (value) {
            case "M":
                Message(response.Message, response.Status);
                break;
            case "R":
                window.location = response.RedirectURL;
                break;
            case "DL":
                setTimeout(function () { window.location = response.RedirectURL; }, 3000);
                break;
            case "D":
                $("#" + response.FieldID).html(response.Data);
                break;
            case "RL":
                window.location.reload();
                break;
            case "DRL":
                setTimeout(function () { window.location.reload(); }, 3000);

        }
    })
}


let OnFailure = (response) => {

}

let OnComplete = (response) => {

}

function Message(message,type) {
    alertify.set('notifier', 'position', 'top-center');
    if (type) alertify.success(message);
    else alertify.error(message);
}