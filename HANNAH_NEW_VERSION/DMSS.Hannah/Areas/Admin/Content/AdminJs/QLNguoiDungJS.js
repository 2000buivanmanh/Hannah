$(document).ready(function () {
    ReloadData('/Admin/QLNguoiDung/_DanhSachNguoiDung');
});
var iconLoading = document.getElementById("loading");
var showLoading = () => {
    iconLoading.style.display = "block";
}
var hideLoading = () => {
    iconLoading.style.display = "none";
}

function ThemOrSuaNguoiDung(id, url) {
    var idnguoidung = $('#idnguoidung').val();
    var avatar = $('#img-preview').attr('src') == '' ? $('#save-img').val() : $('#img-preview').attr('src');
    var eruditepoin = $('#eruditepoin').val();
    var phone = $('#phone').val();
    var dob = $('#dob').val();
    var address = $('#address').val();
    matKhau = jQuery('#passwordUpdate').val();
    $("#frmThemOrSua").validate({
        errorClass: "error fail-alert",
        validClass: "valid success-alert",
        rules: {
            phone: {
                minlength: 10,
                maxlength: 10,
                number: true,
            },
            passwordUpdate: {
                minlength: 4,
            },
            confirmpasswordUpdate: {
                minlength: 4,
                equalTo: "#passwordUpdate"
            },
        },
        messages: {
            phone: {
                minlength: "Phone number field accept only 10 digits",
                maxlength: "Phone number field accept only 10 digits",
                number: "Can only enter numbers "
            },
           
        },
    })

    if ($("#frmThemOrSua").valid()) {
        Swal.fire({
            position: 'absolute',
            icon: 'question',
            title: 'Update',
            text: 'Are you sure you want to Update?',
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
                    url: '/Admin/QLNguoiDung/CapNhatAdmin',
                    data: {
                        MaNguoiDung: idnguoidung,
                        MatKhau: matKhau,
                        DiemTichluy: eruditepoin,
                        AnhDaiDien: avatar,
                        SoDienThoai: phone,
                        NgaySinh: dob,
                        DiaChi: address,
                    },

                }).done(function (data) {

                        if (data.status == "1") {
                            $('#ThemOrSuaModal').modal('hide');
                            $.toast({
                                heading: 'Update',
                                text: ' Update User successful.',
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


// thay doi trang thai
function DuyetTrangThai(id, url) {

        Swal.fire({
            position: 'absolute',
            icon: 'question',
            title: 'Accept',
            text: 'Are you sure you want to Accept?',
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
                    data: {
                        Id: id,
                    },
                }).done(ReloadData('/Admin/QLNguoiDung/_DanhSachNguoiDung'))
                $.toast({
                    heading: 'Accept',
                    text: 'Status accept successful.',
                    showhidetransition: 'fade',
                    icon: 'success'
                });
            }
        })
  
}

function CheckForm(url) {
    var fullname = $('#fullname').val();
    var username = $('#username').val();
    var email = $('#email').val();
    var phone = $('#phone').val();
    var dob = $('#dob').val();
    var address = $('#address').val();

    if ($("#frmThemOrSua").valid()) {
        Swal.fire({
            position: 'absolute',
            icon: 'question',
            title:'Insert',
            text:  'Are you sure you want to insert?',
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
                    url: '/QLNguoiDung/ThemNguoiDung',
                    data: {
                        HoTen: fullname,
                        TenDangNhap: username,
                        EmailNguoiDung: email,
                        SoDienThoai: phone,
                        NgaySinh: dob,
                        DiaChi: address,
                    },
                }).done(function (data) {
                    if (data.status == "-1") {
                        $('#ThemOrSuaModal').modal('hide');
                        $.toast({
                            heading: 'Insert',
                            text: data.message,
                            showhidetransition: 'fade',
                            icon: 'success'
                        });
                        ReloadData(url);

                    }
                    else {
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

function ModalThemAdmin(url) {
    if (!$('#ThemOrSuaModal').hasClass('in')) {
        $('#ThemOrSuaModal').modal('toggle');
    }
    $.ajax({
        type: "GET",
        url: url,
        async: false,
        success: function (data) {
            $('#themorsua').html(data);
        }, failure: function (response) {
            location.reload();
        },
    });
    var success = false;
    //username
    $.validator.addMethod(
        "checkUsername",
        function (value) {
            var user = $("username").val();
            $.ajax({
                type: "POST",
                async: false,
                url: '/Users/KiemTraTonTaiTenNguoiDung',
                data: "username=" + value,
                dataType: "html",
                success: function (data) {
                    //If username ton tai => false 
                    if (data == 1)
                        success = true;
                    else
                        success = false;
                }
            });
            return success;
        },
    );
    //email
    $.validator.addMethod(
        "checkEmail",
        function (value) {
            var email = $("email").val();
            $.ajax({
                type: "POST",
                async: false,
                url: '/Users/KiemTraTonTaiEmail',
                data: "email=" + value,
                dataType: "html",
                success: function (data) {
                    //If username ton tai => false 
                    if (data == 1)
                        success = true;
                    else
                        success = false;
                }
            });
            return success;
        },
    );

    //form validate
    $("#frmThemOrSua").validate({
        errorClass: "error fail-alert",
        validClass: "valid success-alert",
        rules: {
            fullname: {
                required: true,
            },
            username: {
                required: true,
                checkUsername: true
            },
            email: {
                required: true,
                email: true,
                checkEmail: true
            },
            phone: {
                number: true,
                minlength: 10,
                maxlength: 10,
            },


        },
        messages: {
            fullname: {
                required: "Please enter your Full name",
            },
            username: {
                required: "Please enter your User name",
                checkUsername: "This Username is taken already"
            },
            email: {
                required: "Please enter your Email",
                email: "The email should be in the format: abc@domain.com",
                checkEmail: "This Email is taken already"
            },
            phone: {
                minlength: "Phone number field accept only 10 digits",
                maxlength: "Phone number field accept only 10 digits",
                number: "Can only enter numbers "
            },


        },
    })
}



//reset pass
function ResetPass(url, urlLoad) {
    if (totalSelectedRows > 0) {
        Swal.fire({
            icon: 'warning',
            text: 'Do you want Reset Password ' + totalSelectedRows + ' row of data ?',
            showDenyButton: true,
            showLoaderOnConfirm: true,
            confirmButtonText: '<i class="fa fa-check"></i> Reset',
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
                    showLoading();
                    $.post(url, { data: data })
                        .done(function (data) {
                            if (data.status == "1") {
                                hideLoading();
                                $.toast({
                                    heading: ' Reset Password',
                                    text: data.message,
                                    showhidetransition: 'fade',
                                    icon: 'success'
                                });
                                Swal.fire({
                                    title: 'Update !',
                                    text: 'The new default password is:' + data.matKhau,
                                    icon: 'success',})
                                ReloadData(urlLoad);
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