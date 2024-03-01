$(document).ready(function () {
    ShowHidePassword();
    ClickLoginButton();
    PressEnter();
    LoginFormValidation();
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
        let validForm = $("#frmLogin").valid();
        if (validForm) {
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
        }
    });
}

function PressEnter() {
    $(document).on("keydown", "#frmLogin", function (e) {
        // if key is Enter button
        if (e.keyCode === 13) {
            let validForm = $("#frmLogin").valid();
            if (validForm) {
                e.preventDefault();
                $("#btnLogin").trigger("click");
            }
        }
    });
}

function LoginFormValidation() {
    $("#frmLogin").validate({
        errorClass: 'red',
        ignore: [],        
        rules: {
            username: {
                required: true,
                rangelength: [4, 50],     
                noWhitespaceAtBeginEnd: true,
                startWithLetter: true,
                englishUnderscoresChars: true
            },
            password: {
                required: true,
                rangelength: [6, 30],      
                noWhitespaceAtBeginEnd: true,
                startWithLetter: true,
                atLeastOneNumber: true,
                atLeastOneSpecialChar: true,
                atLeastOneUpperLetterAndLowerLetter: true
            }
        },
        messages: {
            username: {
                required: "Please enter the username.",
                rangelength: "Please enter a value between 4 and 50 characters long.",               
            },
            password: {
                required: "Please enter the password.",
                rangelength: "Please enter a value between 6 and 30 characters long.",
            }
        },        
    });
}