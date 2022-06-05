$(document).ready(function () {
    ReloadData('/Admin/TheLoaiSach/_DanhSachTheLoaiSach');
});


function ThemOrSuaTheLoai(id, url) {

    var icon = $('#img-preview').attr('src') == '' ? $('#save-img').val() : $('#img-preview').attr('src');
    var categoryname = $('#name').val();
    var categories = $('#categories').val();
    var trangthai = $('#trangthai').text();
    var bookitemidentifier = $('#bookitemidentifier').val();
    var description = $('#description').val();
    var seocontent = $('#seocontent').val();
    var seokeyword = $('#seokeyword').val();
    var seotitle = $('#seotitle').val();
    var seolink = $('#seolink').val();
    $("#frmThemOrSua").validate({
        errorClass: "error fail-alert",
        validClass: "valid success-alert", 
        rules: {categoryname
            : {
                required: true,
            },
            categories: {
                required: true,
                maxlength: 10,
            },
            icon: {
                required: true,
            },
            bookitemidentifier: {
                required: true,
            },
          
            seolink: {
                required: true,
            },
        },
        messages: {
            categoryname: {
                required: "Please enter your Category Name",
            },
            categories: {
                required: "Please enter your Categories",
                maxlength: "Category field only accepts 10 digits",
            },
            bookitemidentifier: {
                required: "Please enter your Book Catalog Code",
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
            title: id == 0 || id == null ?'Insert' : 'Update',
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
            url: '/Admin/TheLoaiSach/ThemHoacSuaTheLoai' + "/" + id,
            data: {
                MaTheLoai: id,
                HangMuc: categories,
                TenTheLoai: categoryname,
                TrangThai: trangthai,
                MaNhanDienHangMucSach: bookitemidentifier,
                Icon: icon,
                MoTa: description,
                NoiDungSeo: seocontent,
                TuKhoaSeo: seokeyword,
                TieuDeSeo: seotitle,
                DuongDanSeo: seolink
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


