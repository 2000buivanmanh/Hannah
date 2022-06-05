
$(document).ready(function () {
    ReloadData('/Admin/HangMucSach/_DanhSachHangMucSach');
});


function ThemOrSuaHangMuc(id, url) {

    var categoriesname = $('#name').val();
    var trangthai = $('#trangthai').text();
    var bookcatalogcode = $('#bookcatalogcode').val();
    var description = $('#description').val();
    var seocontent = $('#seocontent').val();
    var seokeyword = $('#seokeyword').val();
    var seotitle = $('#seotitle').val();
    var seolink = $('#seolink').val();

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
                    url: '/Admin/HangMucSach/ThemHoacSuaHangMuc' + "/" + id,
                    data: {
                        MaHangMucSach: id,
                        TenHangMuc: categoriesname,
                        TrangThai: trangthai,
                        MaNhanDienHangMucSach: bookcatalogcode,
                        MoTa: description,
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




function ModalThemSuaHangMucSach(url,id) {
    if (!$('#ThemOrSuaModal').hasClass('in')) {
        $('#ThemOrSuaModal').modal('toggle');
    }

    $.ajax({
        type: "GET",
        url: url + "/" + id,
        async: false,
        success: function (data) {
            $('#themorsua').html(data);
        }, failure: function (response) {
            location.reload();
        },
    });
    if (id == null) {
        $("#tieude").text('Add New ');
        $("#btnsumit").text('Insert');
    } else {
        $("#tieude").text('Edit ');
        $("#btnsumit").text('Update');
    };


    //ckeditor
    document.querySelectorAll('.ckeditor').forEach(e => {
        ClassicEditor
            .create(e)
            .then(ckeditor => {
                ckeditor.model.document.on('change:data', () => {
                    e.value = ckeditor.getData();
                });
            })

            .catch(error => {
                console.error(error);
            });
    });
    let input = $('#name');
    let output = $('#seolink');
    input.keyup(function () {
        output.val(toSlug(input.val()));

    });
    $.validator.addMethod(
        "checkbookcatalogcode",
        function (value) {
            var maNhaDien = $("bookcatalogcode").val();
            $.ajax({
                type: "POST",
                async: false,
                url: '/HangMucSach/KiemTraTonTaiMaNhanDienHangMucSach',
                data: "bookcatalogcode=" + value,
                dataType: "html",
                success: function (data) {
                    if (data == 1)
                        success = true;
                    else
                        success = false;
                }
            });
            return success;
        },
    );

    $("#frmThemOrSua").validate({
        errorClass: "error fail-alert",
        validClass: "valid success-alert",
        rules: {

            categoriesname: {
                required: true,
            },
            bookcatalogcode: {
                required: true,
                checkbookcatalogcode: true,
                maxlength: 10,
            },

            seolink: {
                required: true,
            },
        },
        messages: {

            categoriesname: {
                required: "Please enter your Categories Name",

            },
            bookcatalogcode: {
                checkbookcatalogcode: "This Book Catalog Code is taken already",
                required: "Please enter your Book Catalog Code",
                maxlength: "Book Catalog Code field accept only 10 digits",
            },
            seolink: {
                required: "Please enter your SEO Link",
            },
        },
    })


}