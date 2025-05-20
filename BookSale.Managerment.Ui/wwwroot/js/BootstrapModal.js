function modalClose() {
    $("#bootstrap-modal").val("")
    $("input#bootstrap-modal-id").val("")
    $("input#bootstrap-modal-name").val("")
    $("textarea#bootstrap-modal-description").val("")
    $('#bootstrap-modal').modal('hide')
}

function showModal(title, id, name, description) {
    $("#bootstrap-modal-title").text(title)
    $("input#bootstrap-modal-id").val(id)
    $("input#bootstrap-modal-name").val(name)
    $("textarea#bootstrap-modal-description").val(description)
    $("#bootstrap-modal").modal("show");
}

$(function () {
    // config modal
    $('#bootstrap-modal').modal({
        backdrop: 'static'
    })

    // Nghe sự kiện submit của form modal
    $('#form-modal').on('submit', function (e) {
        e.preventDefault(); // Ngăn form gửi bình thường
        if ($(this).valid()) {
            $.ajax({
                type: 'POST',
                url: $(this).attr('action'), 
                data: $(this).serialize(), // Lấy toàn bộ dữ liệu trong form
                success: function (respone) {
                    showToaster("Success", `${respone.message}`)
                    $(table).bootstrapTable("refresh");
                    $('#bootstrap-modal').modal('hide');
                },
                error: function (xhr, status, error) {
                    var Response = JSON.parse(xhr.responseText)
                    showToaster("Error", `${status} ${Response.message}`)
                }
            });
        }
    });
})