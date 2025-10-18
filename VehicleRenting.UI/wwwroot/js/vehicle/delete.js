$(document).ready(function () {

    $("#vehicleDeleteForm").on("submit", function (e) {
        var vehicleId = $(this).data("id");

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
            if (result.isConfirmed) 
                const deleteUrl = $("#vehicleDeleteForm").data("delete-url");

                $.ajax({
                    url: deleteUrl, 
                    type: 'POST',
                    data: { id: vehicleId },
                    success: function () {
                        $("#row-" + vehicleId).fadeOut();

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


});