var tempEmail;
var iconLoading = document.getElementById("loading");
var showLoading = () => {
    iconLoading.style.display = "block";
}
var hideLoading = () => {
    iconLoading.style.display = "none";
}
function Login() {
    var tenDangNhap = jQuery('#username-lg').val();
    var matKhau = jQuery('#password-lg').val();
        jQuery("#frmLogin").validate({
            errorClass: "error fail-alert",
            validClass: "valid success-alert",
            rules: {
                username: {
                    required: true,
                },
                password: {
                    required: true,
                },
            },
            messages: {
                username: {
                    required: "Please enter your User name",
                },
                password: {
                    required: "Please enter your Password",
                },
            },
        })
    var success = true;
    if (jQuery("#frmLogin").valid()) {
        if (success) {
            showLoading();
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
                    hideLoading();
                    if (data.status == 1) {
                        if (data.isAdmin == true) {
                            jQuery.toast({
                                heading: 'success',
                                text: 'Login as Admin',
                                showhidetransition: 'fade',
                                position: 'bottom-right',
                                icon: 'success'
                            });
                            window.location.href = 'Admin/Admin/Index';
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
                            });}
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
                        OpenModalUser('/Users/_ConfirmAccount');
                    }
                    if (data.status == 2) {
                        window.location.href = 'Users/WaitAccount';
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
                swal("",data.message, "warning");
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
                window.location.href = 'Users/WaitAccount';
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