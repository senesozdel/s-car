$(document).ready(function () {
    const $sidebar = $('#sidebar');
    const $main = $('#main');
    const $toggleBtn = $('.sidebar-toggle');

    $toggleBtn.on('click', function () {
        $sidebar.toggleClass('closed');
        $toggleBtn.toggleClass('closed');
        $main.toggleClass('full');

    });
});
