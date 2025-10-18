(() => {
    'use strict';
    const form = document.querySelector('#registerForm');

    form.addEventListener('submit', function (event) {
        if (!form.checkValidity()) {
            event.preventDefault();
            event.stopPropagation();
        }

        const password = document.getElementById("password").value;
        const confirmPassword = document.getElementById("confirmPassword").value;

        if (password !== confirmPassword) {
            event.preventDefault();
            event.stopPropagation();
            document.getElementById("confirmPassword").classList.add("is-invalid");
        } else {
            document.getElementById("confirmPassword").classList.remove("is-invalid");
        }

        form.classList.add('was-validated');
    }, false);
})();

// Şifre göster/gizle butonu
document.addEventListener("DOMContentLoaded", () => {
    const togglePassword = document.getElementById("togglePassword");
    const passwordInput = document.getElementById("password");

    togglePassword.addEventListener("click", () => {
        const type = passwordInput.getAttribute("type") === "password" ? "text" : "password";
        passwordInput.setAttribute("type", type);
        togglePassword.innerHTML = type === "password"
            ? '<i class="bi bi-eye"></i>'
            : '<i class="bi bi-eye-slash"></i>';
    });
});
