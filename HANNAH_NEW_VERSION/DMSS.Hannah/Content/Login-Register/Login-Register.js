var tempEmail;
//validate form login
jQuery("#frmLogin").validate({
    rules: {
        usernameEmailLogin: "required",
        passwordLogin: {
            required: true,
            minlength: 4,
        }
    },
    messages: {
        usernameEmailLogin: "Please enter UserName or Email !",
        passwordLogin: {
            required: "Please enter your Password !",
            minlength: "Please enter min 4 character !",
        }
    }
});

var success = false;
//username
//jQuery.validator.addMethod(
//    "checkUsername",
//    function (value) {
//        var user = jQuery("username").val();
//        jQuery.ajax({
//            type: "POST",
//            async: false,
//            url: '/NguoiDung/KiemTraTonTaiTenNguoiDung',
//            data: "username=" + value,
//            dataType: "html",
//            success: function (data) {
//                //If username ton tai => false 
//                if (data == 1)
//                    success = true;
//                else
//                    success = false;
//            }
//        });
//        return success;
//    },
//);
////email
//jQuery.validator.addMethod(
//    "checkEmail",
//    function (value) {
//        var email = jQuery("email").val();
//        jQuery.ajax({
//            type: "POST",
//            async: false,
//            url: '/NguoiDung/KiemTraTonTaiEmail',
//            data: "email=" + value,
//            dataType: "html",
//            success: function (data) {
//                //If username ton tai => false 
//                if (data == 1)
//                    success = true;
//                else
//                    success = false;
//            }
//        });
//        return success;
//    },
//);
////validate form login | END -- validate form register \\
jQuery("#formRegister").validate({
    rules: {
        readerName: "required",
        readerEmail: {
            required: true,
            email: true,
        },
        readerUserName: "required",
        readerPassWord: {
            required: true,
            minlength: 4,
        },
        readerRePassWord: {
            required: true,
            minlength: 4,
            //equalTo: readerPassWord,
        },
        readerDOB: "date",
        readerPhoneNumber: {
            number: true,
            maxlength: 10,
        },
        readerAddress: "required",
        suggesterName: "required",
        suggesterPhoneMail: {
            required: true,
            maxlength: 10,
            number: true,
        },
        suggesterEmail: {
            required: true,
            email: true,
        },
        suggesterRelation: "required",
    },
    messages: {
        readerName: "Please enter your Full Name ! ",
        readerEmail: {
            required: "Please enter you Email !",
            email: "Please check your Email again ! "
        },
        readerUserName: "Please enter your User Name !",
        readerPassWord: {
            required: "Please enter your Password !",
            minlength: "Please enter least 4 characters !",
        },
        readerRePassWord: {
            required: "Please enter your RePassword !",
            minlength: "Please enter least 4 characters !",
            equalTo: "Please enter the matching Password !",
        },
        readerPhoneNumber: {
            number: "Please enter numeric character !",
            maxlength: "Please enter up to 10 characters",
        },
        readerAddress: "Please enter your Address !",
        suggesterName: "Please enter your Suggester Name !",
        suggesterPhoneMail: {
            required: "Please enter your Suggester Phone !",
            number: "Please enter numeric character !",
            maxlength: "Please enter up to 10 characters",
        },
        suggesterEmail: {
            required: "Please enter you Suggester Email !",
            email: "Please check your Suggester Email again ! "
        },
        suggesterRelation: "Please choose your Relationship With Sponsor",
    }
});
//validate form register \\
function DangNhap() {
    var tenDangNhap = jQuery('#usernameEmailLogin').val();
    var matKhau = jQuery('#passwordLogin').val();
    var success = true;
    if (success) {
        jQuery.ajax({
            type: "GET",
            url: "/Users/KiemTraDangNhap",
            data: {
                tenDangNhap: tenDangNhap,
                matKhau: matKhau
            },
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data.status == 1) {
                    if (data.isAdmin == true) {
                        jQuery.toast({
                            heading: 'success',
                            text: 'Login as Admin',
                            showhidetransition: 'fade',
                            position: 'bottom-right',
                            icon: 'success'
                        });
                        window.location.href = '/Admin/Admin/Index';
                    }
                    if (data.isAdmin == false) {
                        jQuery.toast({
                            heading: 'success',
                            text: 'Login as User',
                            position: 'bottom-right',
                            showhidetransition: 'fade',
                            icon: 'success'
                        });
                        window.location.href = '/';
                    }
                }
                if (data.status == 0) {
                    if (data.capcha == false) {
                        swal({
                            title: "error",
                            text: data.message,
                            icon: "error",
                            button: false,
                            timer: 3000
                        });
                    } else {
                        jQuery.toast({
                            heading: 'error',
                            text: data.message,
                            showhidetransition: 'fade',
                            position: 'bottom-right',
                            icon: 'error',
                        });
                    }
                }
                if (data.status == -1) {
                    swal({
                        title: "success",
                        text: "sent confirmation code",
                        icon: "success",
                        button: false,
                        timer: 1500
                    });
                    tempEmail = tenDangNhap;
                    jQuery('#modalAccount').modal('show');
                    //OpenModalUser('/NguoiDung/_ConfirmAccount');
                }
                if (data.status == 2) {
                    window.location.href = '/Users/WaitAccount';
                }
            },
            failure: function (response) {
                alert(response.responseText);
            },
            error: function (response) {
                alert(response.responseText);
            }
        });
    }
}
function DangKy() {
    var fullname = jQuery('#readerName').val();
    var username = jQuery('#readerUserName').val();
    var email = jQuery('#readerEmail').val();
    var phone = jQuery('#readerPhoneNumber').val();
    var password = jQuery('#readerPassWord').val();
    var confirmpassword = jQuery('#readerRePassWord').val();
    var dob = jQuery('#readerDOB').val();
    var address = jQuery('#readerAddress').val();
    var sname = jQuery('#suggesterName').val();
    var sphone = jQuery('#suggesterPhoneMail').val();
    var smail = jQuery('#suggesterEmail').val();
    var srelationship = jQuery('#suggestersRelation').val();
    if (jQuery("#formRegister").valid()) {
        //showLoading();
        jQuery.ajax({
            type: "POST",
            dataType: 'json',
            async: false,
            url: '/Users/ThemNguoiDung',
            data: {
                HoTen: fullname,
                TenDangNhap: username,
                EmailNguoiDung: email,
                SoDienThoai: phone,
                MatKhau: password,
                MatKhau: confirmpassword,
                NgaySinh: dob,
                DiaChi: address,
                TenNguoiGioiThieu: sname,
                SDTNguoiGioiThieu: sphone,
                EmailNguoiGioiThieu: smail,
                MoiQuanHe: srelationship
            },
        }).done(function (data) {
            //hideLoading();
            if (data.status == -1) {
                swal(data.message)
                    .then((value) => {
                       /* $('#_ConfirmAccount').modal('show');*/
                        //OpenModalUser('/NguoiDung/_ConfirmAccount');
                        window.location.href = '/Users/ConfirmAccount';
                        tempEmail = username;
                        success = true;
                    });
            }
            else
                success = false;
        }).fail.fail(function (jqXHR, textStatus, errorThrown) {
            swal({
                title: "Server Error",
                text: errorThrown,
                icon: "error",
                button: false,
                timer: 1500
            });
        });
    }
}
function GuiMaXacNhan() {
    jQuery.ajax({
        type: "GET",
        url: "/Users/GuiMaXacNhan",
        data: {
            tenDangNhap: tempEmail,
        },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.status == 0) {
                hideLoading();
                swal("", data.message, "warning");
            }
            if (data.status == 1) {
                jQuery('#some_div').hide();
                jQuery('#btnct').removeAttr('disabled');
                swal({
                    title: "success",
                    text: "sent confirmation code",
                    icon: "success",
                    button: false,
                    timer: 1500
                });
                countdownTimer();
            }
        }, failure: function (response) {
            alert(response.responseText);
        },
    });
}
function XacNhanMail() {
    var capcha = jQuery('#Inp-SecurityCode').val();
    jQuery.ajax({
        type: "GET",
        url: "/Users/XacNhanMail",
        data: {
            tenDangNhap: tempEmail,
            maXacNhan: capcha
        },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.status == 1) {
                window.location.href = '/Users/WaitAccount';
            }
            if (data.status == 0) {
                swal({
                    title: "error",
                    text: data.message,
                    icon: "error",
                    button: false,
                    timer: 1500
                });
            }
        }, failure: function (response) {
            alert(response.responseText);
        },
    });
}
function countdownTimer() {
    var timeLeft = jQuery('#thoiGianGuiMa').val();
    var thoigian = timeLeft.toString();
    var elem = document.getElementById('some_div');
    var timerId = setInterval(countdown, 1000);
    jQuery('#btnct').hide();
    function countdown() {
        jQuery('#some_div').show();
        if (thoigian == -1) {
            clearTimeout(timerId);
            jQuery('#some_div').hide();
            jQuery('#btnct').show();
        } else {
            elem.innerHTML = thoigian + '  left to resend capcha again';
            thoigian--;
        }
    }
}