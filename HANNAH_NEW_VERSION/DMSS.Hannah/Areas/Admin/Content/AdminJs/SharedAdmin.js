var iconLoading = document.getElementById("loading");
var showLoading = () => {
    iconLoading.style.display = "block";
}
var hideLoading = () => {
    iconLoading.style.display = "none";
}

numberOfSelectedRows = $('#numberOfSelectedRows');
function ReloadDT(idtable) {
    $(idtable).DataTable({
        dom: 'Bfrtip',
        "language": {
            url: '/Areas/Admin/Content/AdminJson/en-GB.json',
        buttons: [
            'copy', 'csv', 'excel', 'pdf', 'print'
        ]
        }
    });
}
function ReloadData(url) {
    $.get(url, {})
        .done(function (data) {
            if (data) {
                $("#data").html(data);
               $(function () { $('.check-toggle').bootstrapToggle() });
                ReloadDT('#tableLoad');
                updateNumberOfSelectedRows();
            }
        }).fail(function () {
            
        })
}

function Check(id) {
    const self = $("#check-" + id);
    const isChecked = self.is(':checked');
    numberOfSelectedRows.text(0);
    var checkcount = isChecked ? 1 : -1;
    totalSelectedRows = totalSelectedRows + checkcount;
    updateNumberOfSelectedRows();
    if (checkcount == -1) {
        $('#checkall').each(function () {
            $(this).prop('checked', isChecked);
        });
    };
}

function updateNumberOfSelectedRows() {
    totalSelectedRows = 0;
    $('.check-field').each(function () {
        const self = $(this);
        const isChecked = self.is(':checked');
        if (isChecked) {
            totalSelectedRows = totalSelectedRows + 1;
        }

    })
    numberOfSelectedRows.text(totalSelectedRows > 0 ? totalSelectedRows : 0);
    if (totalSelectedRows > 0) {
        $("#show-dele").show();
    } else {
        $("#show-dele").hide();
    }

}

function SelectAll() {
    const self = $("#checkall");
    const isChecked = self.is(':checked');
    numberOfSelectedRows.text(0);
    totalSelectedRows = isChecked ? $('.check-field').length : 0;
    $('.check-field').each(function () {
        $(this).prop('checked', isChecked);
    });
    updateNumberOfSelectedRows();
    
}

//tao seo link
function toSlug(str) {
    str = str.toLowerCase();

    str = str
        .normalize('NFD')
        .replace(/[\u0300-\u036f]/g, '');

    str = str.replace(/[đĐ]/g, 'd');

    str = str.replace(/([^0-9a-z-\s])/g, '');

    str = str.replace(/(\s+)/g, '-');

    str = str.replace(/-+/g, '-');

    str = str.replace(/^-+|-+$/g, '');

    return str;
}

function ModalThemSua(url, id) {
    if (!$('#ThemOrSuaModal').hasClass('in')) {
        $('#ThemOrSuaModal').modal('toggle');
    }

    $.ajax({
        type: "GET",
        url: url + "/" + id,
        async: false,
        success: function (data) {
            $('#themorsua').html(data);
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
        }, failure: function (response) {
            location.reload();
        },
    });

}

function OpenModal(url) {
    if (!$('#AddExcel').hasClass('in')) {
        $('#AddExcel').modal('toggle');
    }

    $.ajax({
        type: "GET",
        url: url,
        async: false,
        success: function (data) {
            $('#addexcel').html(data);
        }, failure: function (response) {
            location.reload();
        },
    });

}


// ckfinder

function onCkfider() {
    selectFileWithCKFinder('#img-preview');
}

function selectFileWithCKFinder(elementId) {
    CKFinder.popup({
        chooseFiles: true,
        width: 800,
        height: 600,
        onInit: function (finder) {
            finder.on('files:choose', function (evt) {
                var file = evt.data.files.first();
                var output = $(elementId)
                output.attr('src',file.getUrl());
            });
        }
    });
}

// thay doi trang thai
function CheckTrangThai(id, url) {
    $.ajax({
        type: "POST",
        dataType: 'json',
        async: false,
        url: url,
        data: {
            Id: id,
        },
    }).done(function (data) {
        if (data.status == "1") {
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
    })

}



//xoa du lieu
function XoaData(url, urlload) {
    if (totalSelectedRows > 0) {
        Swal.fire({
            icon: 'warning',
            text: 'Do you want delete ' + totalSelectedRows + ' row of data ?',
            showDenyButton: true,
            showLoaderOnConfirm: true,
            confirmButtonText: '<i class="fa fa-check"></i> Delete',
            denyButtonText: '<i class="fa fa-ban"> Cancel',
            allowOutsideClick: false,
        })
            .then((result) => {
                if (result.isConfirmed) {
                    var data = [];
                    $('.check-field').each(function () {
                        const self = $(this);
                        const isChecked = self.is(':checked');
                        if (isChecked) {
                            const id = self.data("id");
                            data.push(id);
                        }
                    });
                    $.post(url, { data: data })
                        .done(function (data) {
                            if (data.status == "1") {
                                $.toast({
                                    heading: 'Delete',
                                    text: data.message,
                                    showhidetransition: 'fade',
                                    icon: 'success'
                                });
                                ReloadData(urlload);

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
            });
    } else {
        $.toast({
            heading: 'Eror',
            text: 'You have not selected any data lines!',
            showhidetransition: 'fade',
            icon: 'error'
        });
    }
}





function ImportExcel(url, urlLoad) {
    if ($("#frmThemOrSua").valid()) {
        Swal.fire({
            position: 'absolute',
            icon: 'question',
            title: 'Insert',
            text:  'Are you sure you want to insert?' ,
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
                    url: url,
                }).done(function (data) {
                    if (data.status == "1") {
                        $('#AddExcel').modal('hide');
                        $.toast({
                            heading: 'Insert',
                            text: data.message,
                            showhidetransition: 'fade',
                            icon: 'success'
                        });
                        $.get(urlLoad, {})
                            .done(function (data) {
                                if (data) {
                                    $("#data").html(data);
                                    $(function () { $('.check-toggle').bootstrapToggle() });
                                    ReloadDT('#tableLoad');
                                    updateNumberOfSelectedRows();
                                }
                            }).fail(function () {
                                $body.removeClass("loading");
                            })

                    }
                    if (data.status == "0") {
                        $('#AddExcel').modal('hide');
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


function ImportEx(data, url) {
    var formData = new FormData();
    formData.append('myExcelData', $('#ImportEx')[0].files[0]);
    $.ajax({
        type: 'POST',
        url: data,
        contentType: false,
        processData: false,
        data: formData,
    }).done(function (data) {
        if (data.status == "1") {
            $.toast({
                heading: 'Insert',
                text: data.message,
                showhidetransition: 'fade',
                icon: 'success'
            });
            OpenModal(url)

        }
        if (data.status == "0") {
            $.toast({
                heading: 'Error',
                text: data.message,
                showhidetransition: 'fade',
                icon: 'error'
            });
        }})
}


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
                $.post('/Admin/PhanHoiAdmin/TraLoiPhanHoi',
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


function ThemOrSuaTacGia(id, url) {

    var authorsname = $('#name').val();
    var trangthai = $('#trangthai').text();
    var authorsinfo = $('#authorsinfo').val();
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

            authorsname: {
                required: "Please enter your Authors Name",

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
                    url: '/Admin/TacGia/ThemHoacSuaTacGia' + "/" + id,
                    data: {
                        MaTacGia: id,
                        TenTacGia: authorsname,
                        TrangThai: trangthai,
                        ThongTinTacGia: authorsinfo,
                        NoiDungSeo: seocontent,
                        TuKhoaSeo: seokeyword,
                        TieuDeSeo: seotitle,
                        DuongDanSeo: seolink
                    },


                }).done(function (data) {
                    if (id == 0) {
                        if (data.status == "1") {
                            $('#ThemOrSuaModal').modal('hide');
                            $("#author").prepend("<option  value='" + data.idTacGia + "'>" + authorsname + "</option>");
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
                    if (id == 0) {
                        if (data.status == "1") {
                            $('#ThemOrSuaModal').modal('hide');
                            $("#publishers").prepend("<option  value='" + data.idNXB + "'>" + publishername + "</option>");
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

function ThemOrSuaNhomTuoi(id, url) {
    var trangthai = $('#trangthai').val();
    var agemin = Number($('#agemin').val());
    var agemax = Number($('#agemax').val());
    var description = $('#description').val();
    var seocontent = $('#seocontent').val();
    var seokeyword = $('#seokeyword').val();
    var seotitle = $('#seotitle').val();
    var seolink = $('#seolink').val();
    $.validator.addMethod(
        "checkage",
        function (value) {
            if (agemin < agemax)
                return true;
        })
    $.validator.addMethod(
        "checkage0",
        function (value) {
            if (agemin > 0)
                return true;
        })
    $("#frmThemOrSua").validate({
        errorClass: "error fail-alert",
        validClass: "valid success-alert",
        rules: {
            agemin
                : {
                required: true,
                number: true,
                maxlength: 3,
                checkage: true,
                checkage0: true,
            },
            agemax: {
                required: true,
                number: true,
                maxlength: 3,
                checkage: true,

            },

            seolink: {
                required: true,
            },
        },
        messages: {
            agemin: {
                required: "Please enter your Age Min",
                maxlength: "Age Min field only accepts 3 digits",
                number: "Can only enter numbers ",
                checkage: "Minimum age must not be older than maximum age",
                checkage0: "Age cannot enter a negative number",
            },
            agemax: {
                required: "Please enter your Age Max",
                maxlength: "Age Max field only accepts 3 digits",
                number: "Can only enter numbers ",
                checkage: "Minimum age must not be older than maximum age",
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
                    url: '/Admin/NhomTuoi/ThemHoacSuaNhomTuoi' + "/" + id,
                    data: {
                        MaNhomTuoi: id,
                        DoTuoiMin: agemin,
                        DoTuoiMax: agemax,
                        TrangThai: trangthai,
                        MoTa: description,
                        NoiDungSeo: seocontent,
                        TuKhoaSeo: seokeyword,
                        TieuDeSeo: seotitle,
                        DuongDanSeo: seolink
                    },

                }).done(function (data) {
                    if (id == 0 || id == null) {
                        if (data.status == "1") {
                            $('#ThemOrSuaModal').modal('hide');
                            $("#agegroup").prepend("<option  value='" + data.idNhomTuoi + "'>" + agemin + "-" + agemax + "</option>");
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

function ModalThemSuaNhomTuoi(url, id) {
    if (!$('#ThemOrSuaModal').hasClass('in')) {
        $('#ThemOrSuaModal').modal('toggle');
    }

    $.ajax({
        type: "GET",
        url: url + "/" + id,
        async: false,
        success: function (data) {
            $('#themorsua').html(data);
            if (id == null) {
                $("#tieude").text('Add New ');
                $("#btnsumit").text('Insert');
            } else {
                $("#tieude").text('Edit ');
                $("#btnsumit").text('Update');
            };
            let a = $('#agemin');
            let b = $('#agemax');

            let output = $('#seolink');
            a.add(b).keyup(function () {
                output.val(toSlug(a.val() + " " + b.val() + " " + "years old"));
            });
            //ckeditor
            document.querySelectorAll('.ckeditor').forEach(e => {
                ClassicEditor
                    .create(e)
                    .then(editor => {
                        editor.model.document.on('change:data', () => {
                            e.value = editor.getData();
                        });
                    })

                    .catch(error => {
                        console.error(error);
                    });
            })
        }, failure: function (response) {
            location.reload();
        },
    });

}

function ThemOrSuaLoaiSach(id, url) {

    var booktypename = $('#name').val();
    var trangthai = $('#trangthai').text();
    var description = $('#description').val();
    var seocontent = $('#seocontent').val();
    var seokeyword = $('#seokeyword').val();
    var seotitle = $('#seotitle').val();
    var seolink = $('#seolink').val();
    $("#frmThemOrSua").validate({
        errorClass: "error fail-alert",
        validClass: "valid success-alert",
        rules: {

            booktypename: {
                required: true,
            },
            seolink: {
                required: true,
            },
        },
        messages: {

            booktypename: {
                required: "Please enter your Book Type Name",

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
                    url: '/Admin/LoaiSach/ThemHoacSuaLoaiSach' + "/" + id,
                    data: {
                        MaLoaiSach: id,
                        TenLoaiSach: booktypename,
                        TrangThai: trangthai,
                        MoTa: description,
                        NoiDungSeo: seocontent,
                        TuKhoaSeo: seokeyword,
                        TieuDeSeo: seotitle,
                        DuongDanSeo: seolink
                    },

                }).done(function (data) {
                    if (id == null || id == "") {
                        if (data.status == "1") {
                            $('#ThemOrSuaModal').modal('hide');
                            $("#booktype").prepend("<option  value='" + data.idLoaiSach + "'>" + booktypename + "</option>");
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
        rules: {
            categoryname
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
                            $("#bookcatagory").prepend("<option  value='" + data.idTheLoai + "'>" + categoryname + "</option>");
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

function ThemOrSuaDiaChi(id, url) {

    var publishingaddress = $('#name').val();
    var detailedaddress = $('#detailedaddress').text();
    var seocontent = $('#seocontent').val();
    var seokeyword = $('#seokeyword').val();
    var seotitle = $('#seotitle').val();
    var seolink = $('#seolink').val();
    $("#frmThemOrSua").validate({
        errorClass: "error fail-alert",
        validClass: "valid success-alert",
        rules: {

            publishingaddress: {
                required: true,
            },


            seolink: {
                required: true,
            },
        },
        messages: {

            publishingaddress: {
                required: "Please enter your Publishing Address",

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
                    url: '/Admin/DiaChiXuatBan/ThemHoacSuaDiaChiXuatBan' + "/" + id,
                    data: {
                        MaDiaChi: id,
                        TenDiaChi: publishingaddress,
                        ChiTietDiaChi: detailedaddress,
                        NoiDungSeo: seocontent,
                        TuKhoaSeo: seokeyword,
                        TieuDeSeo: seotitle,
                        DuongDanSeo: seolink
                    },

                }).done(function (data) {
                    if (id == 0) {
                        if (data.status == "1") {
                            $('#ThemOrSuaModal').modal('hide');
                            $("#infoxb").prepend("<option  value='" + data.idDiaChi + "'>" + publishingaddress + "</option>");
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

