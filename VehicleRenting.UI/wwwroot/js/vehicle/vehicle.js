$(document).ready(function () {

    $(".vehicleDeleteForm").on("submit", function (e) {
        e.preventDefault();

        const form = $(this);
        const vehicleId = form.data("id");
        const deleteUrl = form.data("delete-url");

        Swal.fire({
            title: 'Emin misiniz?',
            text: "Bu aracı silmek istediğinize emin misiniz?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Evet, sil!',
            cancelButtonText: 'Vazgeç'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    url: deleteUrl,
                    type: 'POST',
                    data: { id: vehicleId },
                    success: function () {
                        $("#row-" + vehicleId).fadeOut(300, function () {
                            $(this).remove();
                        });
                        Swal.fire({
                            icon: 'success',
                            title: 'Silindi!',
                            text: 'Araç başarıyla silindi.',
                            confirmButtonColor: '#3085d6',
                            timer: 1500
                        });
                    },
                    error: function () {
                        Swal.fire({
                            icon: 'error',
                            title: 'Hata!',
                            text: 'Araç silinirken bir hata oluştu.',
                            confirmButtonColor: '#d33'
                        });
                    }
                });
            }
        });
    });


    $("#editVehicleForm").on("submit", function (e) {
        const name = $("#Name").val().trim();
        const plate = $("#PlateNumber").val().trim();

        if (name === "" || plate === "") {
            e.preventDefault();
            Swal.fire({
                icon: 'warning',
                title: 'Eksik Bilgi!',
                text: 'Lütfen tüm alanları doldurun.',
                confirmButtonColor: '#3085d6'
            });
            return false;
        }
    });
});
