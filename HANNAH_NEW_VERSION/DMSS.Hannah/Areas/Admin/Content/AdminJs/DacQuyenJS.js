$(document).ready(function () {
    ReloadData('/Admin/DacQuyen/_DanhSachDacQuyen');
});


function ThemOrSuaDacQuyen(id, url) {

    var icon = $('#img-preview').attr('src') == '' ? $('#save-img').val() : $('#img-preview').attr('src');
    var benifitname = $('#name').val();
    var content = $('#content').val();
    var trangthai = $('#trangthai').text();
    var seocontent = $('#seocontent').val();
    var seokeyword = $('#seokeyword').val();
    var seotitle = $('#seotitle').val();
    var seolink = $('#seolink').val();
    $("#frmThemOrSua").validate({
        errorClass: "error fail-alert",
        validClass: "valid success-alert",
        rules: {
            benifitname: {
                required: true,
            },
            icon: {
                required: true,
            },
            seolink: {
                required: true,
            },
        },
        messages: {
            benifitname: {
                required: "Please enter your Benifit Name",
            },

            icon: {
                required: "Please enter your Image Icon",
            },
            seolink: {
                required: "Please enter your SEO Link",
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
                    url: '/Admin/DacQuyen/ThemHoacSuaDacQuyen' + "/" + id,
                    data: {
                        MaDacQuyen: id,
                        TenDacQuyen: benifitname,
                        TrangThai: trangthai,
                        NoiDung: content,
                        Icon: icon,
                        NoiDungSeo: seocontent,
                        TuKhoaSeo: seokeyword,
                        TieuDeSeo: seotitle,
                        DuongDanSeo: seolink
                    },

                }).done(function (data) {
                    if (id == null) {
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


