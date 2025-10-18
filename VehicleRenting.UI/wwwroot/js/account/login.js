$(document).ready(function () {
    $("#loginForm").submit(function (e) {
        var username = $("#Username").val().trim();
        var password = $("#Password").val().trim();

        if (username === "" || password === "") {
            e.preventDefault();
            alert("Lütfen kullanıcı adı ve şifre alanlarını doldurun!");
            return false;
        }
    });
});
