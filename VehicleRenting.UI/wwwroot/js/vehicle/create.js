$(document).ready(function () {

    $("#vehicleForm").on("submit", function (e) {
        e.preventDefault();

        const vehicleData = {
            Name: $("#Name").val(),
            PlateNumber: $("#PlateNumber").val()
        };
        const createUrl = $("#vehicleForm").data("create-url");
        $.ajax({
            url: createUrl,
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(vehicleData),
            success: function (response) {
                if (response.success) {
                    Swal.fire({
                        icon: 'success',
                        title: 'Başarılı!',
                        text: response.message,
                        confirmButtonColor: '#3085d6'
                    });
                    $("#vehicleForm")[0].reset();
                } else {
                    Swal.fire({
                        icon: 'error',
                        title: 'Hata!',
                        text: response.message,
                        confirmButtonColor: '#d33'
                    });
                }
            },
            error: function (xhr, status, error) {
                Swal.fire({
                    icon: 'error',
                    title: 'Sunucu Hatası!',
                    text: 'Bir hata oluştu: ' + error,
                    confirmButtonColor: '#d33'
                });
            }
        });
    });

});