$(document).ready(function () {
    ShowHidePassword();
    ClickLoginButton();
    PressEnter();
});

function ShowHidePassword() {
    $("#password-addon").click(function () {
        if ($("#password-addon i").hasClass('mdi-eye-outline')) {
            $("#password-addon i").prop('class', 'mdi mdi-eye-off-outline');
            $("#password").prop("type", "text");
        }
        else if ($("#password-addon i").hasClass('mdi-eye-off-outline')) {
            $("#password-addon i").prop('class', 'mdi mdi-eye-outline');
            $("#password").prop("type", "password");
        }
    });
}

function Login(model) {
    return new Promise(async (resolve, reject) => {
        let url = `/login`;
        try {
            const res = await $.ajax({
                url: url,
                type: 'POST',
                data: model
            });

            resolve(res);
        }
        catch (err) {
            reject(err);
        }
    });
}

function ClickLoginButton() {
    $(document).delegate("#btnLogin", "click", (e) => {
        // prevent the default form submission when using AJAX submit form
        e.preventDefault();
        $('#username-error').text('');
        $('#password-error').text('');
        var username = $('#username').val();
        var password = $('#password').val();
        let model = {
            Username: username,
            Password: password
        };
        Login(model).then((res) => {
            if (res.success) {
                // Client-side redirection
                window.location.href = "/";
            }
        }).catch((err) => {
            if (err != undefined) {
                var res = JSON.parse(err.responseText);
                if (res.success == false) {
                    $('#usernameError').text('');
                    $('#username-error').text('');
                    $('#passwordError').text('');
                    $('#password-error').text('');
                    if (res.data.username != undefined) {
                        $('#usernameError').text(res.data.username);
                    }
                    if (res.data.password != undefined) {
                        $('#passwordError').text(res.data.password);
                    }
                }
            }
        });
    });
}

function PressEnter() {
    $(document).on("keydown", "#frmLogin", function (e) {
        // if key is Enter button
        if (e.keyCode === 13) {
            e.preventDefault();
            $("#btnLogin").trigger("click");
        }
    });
}