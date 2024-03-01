$(document).ready(function () {

    ShowHidePassword();

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