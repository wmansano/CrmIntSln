//$(document).ready(function () {
//    $('#StartingPrice').blur(checkStartPrice)
//})

function validateEmail(txtEmail) {
    var a = document.getElementById(txtEmail).value;
    var filter = /^[a-zA-Z0-9_.-]+@@[a-zA-Z0-9]+[a-zA-Z0-9.-]+[a-zA-Z0-9]+.[a-z]{0,4}$/;
    if (filter.test(a)) {
        return true;
    }
    else {
        return false;
    }
}​

//function validateEmail($email) {
//    var emailReg = /^([\w-\.]+@@([\w-]+\.)+[\w-]{2,4})?$/gi;
//    return emailReg.test($email);
//}

function ValidateEmail() {
    var email = document.getElementById("txtEmail").value;
    var lblError = document.getElementById("lblError");
    lblError.innerHTML = "";
    var expr = /^([\w-\.]+)@@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
    if (!expr.test(email)) {
        lblError.innerHTML = "Invalid email address.";
    }
}