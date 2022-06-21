

$(document).ready(function () {
    ReloadData('/Admin/Sach/_DanhSachSach');
    
});



function prev() {
    document.getElementById('slider-container').scrollLeft -= 270;
}

function next() {
    document.getElementById('slider-container').scrollLeft += 270;
}


$(".slide img").on("click", function () {
    $(this).toggleClass('zoomed');
    $(".overlay").toggleClass('active');
})

// ckfinder
function onCkfider1() {
    selectFileWithCKFinder1("#slider-container");
}
var storedFiles = [];
function selectFileWithCKFinder1(id) {
    CKFinder.popup({
        chooseFiles: true,
        width: 800,
        height: 600,
        onInit: function (finder) {
            finder.on('files:choose', function (evt) {
                var files = evt.data.files;
                files.forEach(function (file) {
                    storedFiles.push(file.getUrl());
                        var output = $(id);
                    var html = "<div class=" + "slide" + "><img src=" + file.changed.url + "  class='selFile' ><a class='close'></a></div>";
                        output.append(html);

                });
              
            });

        }
    });
}

$("body").on("click", ".close", removeFile);

    function removeFile(e) {
        var file = $(this).data("file");
        for (var i = 0; i < storedFiles.length; i++) {
            if (storedFiles[i].name === file) {
                storedFiles.splice(i, 1);
                break;
            }
        }
        $(this).parent().remove();
}


function ThemOrSuaSach(id, url) {

    var img = $('#img-preview').attr('src') == '' ? $('#save-img').val() : $('#img-preview').attr('src');
    var bookname = $('#name').val();
    var descriptionvi = $('#descriptionvi').val();
    var price = $('#pricee').val();
    var trangthai = $('#trangthai').text();
    var trangthaimuonsach = $('#trangthaimuonsach').text();
    var descriptionen = $('#descriptionen').val();
    var agegroup = $('#agegroup').val();
    var bookcatagory = $('#bookcatagory').val();
    var author = $('#author').val();
    var booktype = $('#booktype').val();
    var publishers = $('#publishers').val();
    var datexb = $('#datexb').val();
    var datefirst = $('#datefirst').val();
    var infoxb = $('#infoxb').val();
    var standardcode = $('#standardcode').val();
    var amountbook = $('#amountbook').val();
    var sizebook = $('#sizebook').val();
    var seocontent = $('#seocontent').val();
    var seokeyword = $('#seokeyword').val();
    var seotitle = $('#seotitle').val();
    var seolink = $('#seolink').val();
    $("#frmLoadSach").validate({
        errorClass: "error fail-alert",
        validClass: "valid success-alert",
        rules: {
            bookname
                : {
                required: true,
            },
            price: {
                required: true,
                number: true,
                maxlength: 10,
            },
           
            seolink: {
                required: true,
            },
        },
        messages: {
            bookname: {
                required: "Please enter your Category Name",
            },
            price: {
                required: "Please enter your Categories",
                number:  "Can only enter numbers ",
                maxlength: "Category field only accepts 10 digits",
            },

 
            seolink: {
                required: "Please enter your SEO Link",
            },
        },
    })

    if ($("#frmLoadSach").valid()) {
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
                    url: '/Admin/Sach/ThemHoacSuaSach' + "/" + id,
                    data: {
                        MaSach: id,
                        Gia: price,
                        ThongTinAnhSach: img,
                        TenSach: bookname,
                        MoTaSachVi: descriptionvi,
                        TrangThai: trangthai,
                        TinhTrangMuonSach: trangthaimuonsach,
                        MoTaSachEn: descriptionen,
                        MaNhomTuoi: agegroup,
                        MaTheLoai: bookcatagory,
                        MaTacGia: author,
                        MaLoaiSach: booktype,
                        MaNhaXuatBan: publishers,
                        NgayXuatBan: datexb,
                        LanDauXuatBan: datefirst,
                        MaDiaChi: infoxb,
                        MaTieuChuanSach: standardcode,
                        SoLuongTrang: amountbook,
                        KichThuocSach: sizebook,
                        NoiDungSeo: seocontent,
                        TuKhoaSeo: seokeyword,
                        TieuDeSeo: seotitle,
                        DuongDanSeo: seolink
                    },
                    success: (function (data) {
                        if (data.status == "1") {
                            $.ajax({
                                type: "POST",
                                dataType: 'json',
                                async: false,
                                url: '/Admin/Sach/ListImg' + "/" + id,
                                data: {
                                    listImg: storedFiles,
                                }
                            })
                        }
                    })

                }).done(function (data) {
                    if (id == 0) {
                        if (data.status == "1") {
                            if (storedFiles != null) {
                                var storedFiles1 = [];
                                storedFiles = storedFiles1;
                            }       
                            $('#btnback').attr('hidden', true);
                            $('#btnthemsua').show();
                            $('#frmdata').hide();
                            $('#dttable').show();
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
                            if (storedFiles != null) {
                                var storedFiles1 = [];
                                storedFiles = storedFiles1;
                            }  
                            $('#btnback').attr('hidden', true);
                            $('#btnthemsua').show();
                            $('#frmdata').hide();
                            $('#dttable').show();
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

