function deleteHandler(id, url) {
    if (id && url) {
        Swal.fire({
            title: 'Bạn có chắc chắn muốn xóa?',
            text: "Hành động này không thể hoàn tác!",
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'xóa',
            cancelButtonText: 'Hủy',
            customClass: {
                title: 'h3 text-center text-dark', // gán class CSS tùy chỉnh
                confirmButton: 'btn btn-success', // Thêm class Bootstrap cho confirm button
                cancelButton: 'btn btn-danger'  // Thêm class Bootstrap cho cancel button
            }
        }).then((result) => {
            if (result.isConfirmed) {
                var antiForgeryToken = $('input[name="__RequestVerificationToken"]').val();

                $.ajax({
                    url: url,
                    method: "POST",
                    dataType: "json",
                    headers: {
                        __RequestVerificationToken: antiForgeryToken
                    },
                    data: { id: id },
                    success: function (respone) {
                        showToaster("Success", `${respone.message}`)
                        $(table).bootstrapTable("refresh");
                    },
                    error: function (xhr, status, error) {
                        var Response = JSON.parse(xhr.responseText)
                        showToaster("Error", `${status} ${Response.message}`)
                    }
                })
            }
        });
    }
}