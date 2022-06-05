﻿
$(document).ready(function () {
    ReloadData('/Admin/NhaXuatBan/_DanhSachNhaXuatBan ');
});


function ThemOrSuaNhaXuatBan(id, url) {

    var publishername = $('#name').val();
    var trangthai = $('#trangthai').text();
    var publisherinfo = $('#publisherinfo').val();
    var seocontent = $('#seocontent').val();
    var seokeyword = $('#seokeyword').val();
    var seotitle = $('#seotitle').val();
    var seolink = $('#seolink').val();
    $("#frmThemOrSua").validate({
        errorClass: "error fail-alert",
        validClass: "valid success-alert",
        rules: {

            publishername: {
                required: true,
            },

            seolink: {
                required: true,
            },
        },
        messages: {

            publishername: {
                required: "Please enter your Publisher Name",

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
                    url: '/Admin/NhaXuatBan/ThemHoacSuaNhaXuatBan' + "/" + id,
                    data: {
                        MaNhaXuatBan: id,
                        TenNhaXuatBan: publishername,
                        TrangThai: trangthai,
                        ThongTinNXB: publisherinfo,
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




