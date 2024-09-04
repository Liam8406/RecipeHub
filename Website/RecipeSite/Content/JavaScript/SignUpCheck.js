function checkUsername(username) {
    if (username.length < 6) {
        return false;
    }
    if (!/^[a-zA-Z0-9]+$/.test(username)) {
        return false;
    }
    if (!/[a-zA-Z]/.test(username)) {
        return false;
    }
    return true;
}

function checkEmail(email) {
    if (!/^[a-zA-Z0-9._%+-]+@gmail\.com$/.test(email)) {
        return false;
    }
    return true;
}

function checkPassword(password) {
    if (password.length < 8) {
        return false;
    }
    if (!/[A-Z]/.test(password)) {
        return false;
    }
    return true;
}

document.getElementById("signUpButton").addEventListener("click", function () {
    let username = document.getElementById("username").value;
    let email = document.getElementById("email").value;
    let password = document.getElementById("password").value;

    let usernameValid = checkUsername(username);
    let emailValid = checkEmail(email);
    let passwordValid = checkPassword(password);

    let errorUserName = "";
    let errorEmail = "";
    let errorPassword = "";
    if (!usernameValid) {
        errorUserName += "Username has to contain at least one letter, have at least 6 characters and not contain a special character\n";
    }
    if (!emailValid) {
        errorEmail += "Please enter a valid Gmail address.\n";
    }
    if (!passwordValid) {
        errorPassword += "Please enter a valid password (at least 8 characters with at least one uppercase letter).\n";
    }

    if (usernameValid && emailValid && passwordValid) {
        document.getElementById("errorUserName").innerText = "";
        document.getElementById("errorEmail").innerText = "";
        document.getElementById("errorPassword").innerText = "";
        document.forms[0].submit();
    } else {
        document.getElementById("errorUserName").innerText = errorUserName;
        document.getElementById("errorEmail").innerText = errorEmail;
        document.getElementById("errorPassword").innerText = errorPassword;
        document.getElementById("errorUserName").style.display = "block";
        document.getElementById("errorEmail").style.display = "block";
        document.getElementById("errorPassword").style.display = "block";
    }
});