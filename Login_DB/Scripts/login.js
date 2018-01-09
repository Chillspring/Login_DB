document.addEventListener("DOMContentLoaded", main);
function main() {
    document.getElementById("login").addEventListener("click", function () {
        var pw = document.getElementById("password").value;
        var hash = md5(pw);
        document.getElementById("passwordHidden").value = hash;
    });
}