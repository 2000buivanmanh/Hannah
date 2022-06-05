$(document).ready(function () {
    ReloadData('/Admin/Slide/_DanhSachSlide');
});


function ThemOrSuaTheLoai(id, url) {
    var image = $('#img-preview').attr('src') == '' ? $('#save-img').val() : $('#img-preview').attr('src');
    var slidename = $('#name').val();
    var title = $('#title').val();
    var trangthai = $('#trangthai').text();
    var content = $('#content').val();
    var description = $('#description').val();
    var displaytitle = $('#displaytitle').val();
    var displaycontent = $('#displaycontent').val();
    var displaydescription = $('#displaydescription').val();
    $("#frmThemOrSua").validate({
        errorClass: "error fail-alert",
        validClass: "valid success-alert",
        rules: {
            slidename
                : {
                required: true,
            },
            title: {
                required: true,
            },
            displaytitle: {
                required: true,
            },
            displaycontent: {
                required: true,
            },
            displaydescription: {
                required: true,
            },
        },
        messages: {
            slidename: {
                required: "Please enter your Slide Name",
            },
            title: {
                required: "Please enter your Title",
            },
            displaytitle: {
                required: "Please enter your  Display Title",
            },
            icon: {
                required: "Please enter your Image Icon",
            },
            displaycontent: {
                required: "Please enter your Display Content",
            },
            displaydescription: {
                required: "Please enter your Display Description",
            },
        },
    })

    if ($("#frmThemOrSua").valid()) {
        Swal.fire({
            position: 'absolute',
            icon: 'question',
            title: id == 0 || id == null ? 'Insert' : 'Update',
            text: id == 0 || id == null ? 'Are you sure you want to insert?' : 'Are you sure you want to Update?',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    type: "POST",
                    dataType: 'json',
                    async: false,
                    url: '/Admin/Slide/ThemHoacSuaSlide' + "/" + id,
                    data: {
                        MaSlide: id,
                        TieuDe: title,
                        TenSlide: slidename,
                        TrangThai: trangthai,
                        NoiDung: content,
                        AnhSlide: image,
                        MoTa: description,
                        TieuDeHienThi: displaytitle,
                        NoiDungHienThi: displaycontent,
                        MoTaHienThi: displaydescription,
                    },

                }).done(function (data) {
                    if (id == 0) {
                        if (data.status == "1") {
                            $('#ThemOrSuaModal').modal('hide');
                            $.toast({
                                heading: 'Insert',
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
                    } else {
                        if (data.status == "1") {
                            $('#ThemOrSuaModal').modal('hide');
                            $.toast({
                                heading: 'Update',
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
                    }

                })
            }
        })
    }


}


