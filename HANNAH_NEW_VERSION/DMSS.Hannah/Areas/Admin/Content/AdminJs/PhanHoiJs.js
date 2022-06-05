
$(document).ready(function () {
    ReloadData('/Admin/PhanHoiAdmin/_DanhSachPhanHoi');

});

function TraLoiFeedback(id, url) {
    var status = $('#status').text();
    var sendername = $('#name').val();
    var email = $('#email').val();
    var phonenumber = $('#phonenumber').val();
    var feedbackcontent = $('#feedbackcontent').val();
    var sentdate = $('#sentdate').val();
    var replyfeedback = $('#replyfeedback').val();

    if ($("#frmThemOrSua").valid()) {
        $('#ThemOrSuaModal').modal('hide');
        Swal.fire({
            position: 'absolute',
            icon: 'question',
            title: ' Reply Feedback ',
            text: 'Are you sure you want to Reply Feedback ?',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Send'
        }).then((result) => {
            if (result.isConfirmed) {
                showLoading();
                $.post('/Admin/PhanHoiAdmin/TraLoiPhanHoi' ,
                    {
                        MaPhanHoi: id,
                        TrangThai: status,
                        TenNguoiGui: sendername,
                        Email: email,
                        SDT: phonenumber,
                        NoidungPhanHoi: feedbackcontent,
                        NgayGui: sentdate,
                        NoiDungXuLy: replyfeedback
                    })
            .done(function (data) {
                if (data.status == "1") {
                    hideLoading();
                        $('#ThemOrSuaModal').modal('hide');
                        $.toast({
                            heading: 'Reply Feedback ',
                            text: data.message,
                            showhidetransition: 'fade',
                            icon: 'success'
                        });
                    ReloadData(url);

                    }
                    if (data.status == "0") {
                        $.toast({
                            heading: 'Error',
                            text: data.message,
                            showhidetransition: 'fade',
                            icon: 'error'
                        });
                    }
                })
            }
        })
    }


}

