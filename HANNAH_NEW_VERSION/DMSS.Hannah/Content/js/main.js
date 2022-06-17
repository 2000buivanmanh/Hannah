var startPage = 1;
LoadBookListHomePage(startPage);
LoadBookCaseHeader();
LoadBookCase();

jQuery("#formDatSach").validate({
    rules: {
        basketPhone: {
            minlength: 10,
            maxlength: 10,
            required: true,
        },
        basketNote: {
            required: true,
        }
    },
    messages: {
        basketPhone: {
            required: "Please enter up to 10 characters !",
            minlength: "Phone number field accept only 10 digits",
            maxlength: "Phone number field accept only 10 digits",
        },
        basketNote: {
            required: "Please enter your note !",
        }
    }
});
function LoadBookListHomePage(page) {
    jQuery.ajax({
        type: "GET",
        url: "/Home/_ListBookHompage",
        data: {
            page: page,
        },
        success: function (data) {
            jQuery("#danh-sach-sach").html(data);
        }, failure: function (response) {
            swal({
                title: "error",
                text: "server error!",
                icon: "error",
                button: false,
                timer: 3000
            });
        },
    });
}

function LoadBookCaseHeader() {
    jQuery.ajax({
        type: "GET",
        url: "/BookCases/_BookCaseHeader",
        data: {
        },
        success: function (data) {
            jQuery("#gioHangTam").html(data);
        }, failure: function (response) {
            swal({
                title: "error",
                text: "server error!",
                icon: "error",
                button: false,
                timer: 3000
            });
        },
    });
}

function LoadBookCase() {
    jQuery.ajax({
        type: "GET",
        url: "/BookCases/_BookCase",
        data: {
        },
        success: function (data) {
            jQuery("#load-book-cases").html(data);
        }, failure: function (response) {
            swal({
                title: "error",
                text: "server error!",
                icon: "error",
                button: false,
                timer: 3000
            });
        },
    });
}
function ShowBooking(id) {
    jQuery.get('/Bookcases/_BookLoanAppointment',
        {
            Id: id
        }).done(function (data) {
            jQuery('#modal-Booking').attr('style', 'display: block;');
            jQuery('#contentBooking').html(data);
        }).fail(function (response) {
            swal({
                title: "error",
                text: "server error!",
                icon: "error",
                button: false,
                timer: 3000
            });
        });

}

function AddIntoBookCase(id) {

    jQuery("#formDatSach").validate({
        rules: {
            receivetime: "required",

        },
        messages: {
            receivetime: "Please enter receive time !",

        }
    });
    if (jQuery("#formDatSach").valid()) {
        var ngayNhan = jQuery('#receive-time').val();
        var ngayTra = jQuery('#return-time').val();
        jQuery.post('/BookCases/AddIntoBookCase',
            {
                maSach: id,
                ngayNhan: ngayNhan,
                ngayTra: ngayTra

            }).done(function (data) {
                if (data.status == "1") {
                    LoadBookListHomePage(1);
                    LoadBookCaseHeader();
                    LoadBookCase();
                    swal({
                        position: 'absolute',
                        icon: 'success',
                        title: 'success',
                        text: 'Add to cart successfully'
                    })
                    window.location.href = '/BookCases/BookCase';
                }
                if (data.status == "-1") {
                    swal({
                        title: "Send your book loan?",
                        text: "This time has a other bookloan, you maybe cannot borrow this book, continue?",
                        icon: "warning",
                        buttons: true,
                        dangerMode: true,
                    }).then((willDelete) => {
                        if (willDelete) {
                            jQuery("td.chuaduyet").removeClass();
                            window.location.href = '/Book/BookDetail/' + id;
                        } else {
                            RemoveBookCase(id)
                            window.location.href = '/Book/BookDetail/' + id;
                        }
                    });

                }
                if (data.status == "2") {
                    swal({
                        position: 'absolute',
                        icon: 'error',
                        title: 'error',
                        text: 'books have been lent!!!'

                    })
                }
                if (data.status == "0") {
                    swal({
                        position: 'absolute',
                        icon: 'error',
                        title: 'error',
                        text: 'Accumulated points are not enough to make a booking'

                    })
                }
            });
    }


}

function DatSach(e) {
    var ghiChu = jQuery('#basketNote').val();
    var ngayNhan = jQuery('#receive-time').val();
    var ngayTra = jQuery('#return-time').val();
    var diaChiNhan = jQuery('#basketAddress').val();
    var diaChiTra = jQuery('#basketReturnAddress').val();
    var dieuKhoan = jQuery('#dieuKhoan').val();
    var gioNhan = jQuery('#GioNhan').val();
    var gioTra = jQuery('#GioTra').val();

    if (jQuery("#formDatSach").valid()) {
        if (dieuKhoan == "true") {
            swal({
                title: "Send your book loan?!",
                text: "Do you want to send your book loan?",
                icon: "warning",
                buttons: true,
                dangerMode: true,
            })
                .then((willDelete) => {
                    if (willDelete) {
                        jQuery.post('/BookCases/DatSach',
                            {
                                GhiChu: ghiChu,
                                gioNhan: gioNhan,
                                gioTra: gioTra,
                                NgayNhan: ngayNhan,
                                NgayTra: ngayTra,
                                DiaChiNhan: diaChiNhan,
                                DiaChiTra: diaChiTra

                            }).done(function (data) {
                                if (data.status == "1") {
                                    LoadBookCaseHeader();
                                    swal({
                                        position: 'absolute',
                                        icon: 'success',
                                        title: 'success',
                                        text: 'successfully'
                                    })
                                    window.location.href = '/Users/BookLoanHistori';
                                }
                                if (data.status == "0") {
                                }
                            });
                    } else {
                        swal("Your imaginary file is safe!");
                    }
                });
        } else {
            swal({
                position: 'absolute',
                icon: 'error',
                title: 'error',
                text: 'you have not accepted the terms!'

            })
        }
    }
    else {
        swal({
            position: 'absolute',
            icon: 'error',
            title: 'error',
            text: 'you have not accepted the terms!'

        })
    }


}

function HuyDon(id) {
    jQuery.post('/Users/HuyDon', {
        maDonHang: id
    }).done(function (data) {
        if (data.status == "1") {
            swal({
                position: 'absolute',
                icon: 'success',
                title: 'success',
                text: 'Add to cart successfully'
            })
            swal({
                title: "warning",
                text: "Add to cart successfully?",
                icon: "warning",
                buttons: true,
                dangerMode: true,
            }).then((willDelete) => {
                if (willDelete) {
                    window.location.href = '/Users/BookLoanHistori';
                } else {

                }
            });
        }
    });
}

function RemoveBookCase(id) {
    jQuery.post('/BookCases/RemoveOfBookCase',
        {
            maSach: id
        }).done(function (data) {
            if (data.status == "1") {
                LoadBookListHomePage(1);
                LoadBookCaseHeader();
                LoadBookCase();
                swal({
                    position: 'absolute',
                    icon: 'success',
                    title: '',
                    text: 'Remove to cart successfully'
                })
            }
            if (data.status == "0") {
                swal({
                    position: 'absolute',
                    icon: 'Error',
                    title: 'Error',
                    text: 'Server error!'

                })
            }
        });
}

function RemoveBookAll() {
    jQuery.post('/BookCases/RemoveBookAll', {
    }).done(function (data) {
        LoadBookListHomePage(1);
        LoadBookCaseHeader();
        LoadBookCase();
        swal({
            position: 'absolute',
            icon: 'success',
            title: '',
            text: 'Remove all to cart successfully'
        })
    });
}

function DieuKhoan(e) {
    modal.style.display = "block";
    jQuery('#DongY').click(function () {
        jQuery(e).attr('value', 'true');
        jQuery('#dieuKhoan').prop('checked', true);
        CloseDieuKhoan();
    })
    jQuery(e).attr('value', 'false');
    jQuery('#dieuKhoan').prop('checked', false);
}
var modal = document.getElementById("myModal");
var btn = document.getElementById("myBtn");
var span = document.getElementsByClassName("close")[0];
function CloseDieuKhoan() {
    modal.style.display = "none";
}
var receivetime;
var returntime;
function checkRTT() {
    receivetime = document.getElementById("return-time").value;
    receivetime1 = document.getElementById("receive-time").value;
    var min1 = Date.parse(receivetime1);
    var rtd = Date.parse('2000-01-14');
    var rtd1 = Date.parse('2000-01-21');
    rtd = rtd1 - rtd;
    rtd = min1 + rtd;
    var min = Date.parse(receivetime);
    var rd = min1;
    var max = rtd;
    if (receivetime == "") {
        jQuery("#lb-return-time").addClass('required');
        jQuery("#lb-return-time").focus();
    } else {
        if (min < rd || min > max) {
            jQuery("#lb-return-time").addClass('required');
            jQuery("#lb-return-time").focus();
        } else {
            jQuery("#lb-return-time").removeClass('required');
        }
    }
}
Date.prototype.addDays = function (days) {
    var date = new Date(this.valueOf());
    date.setDate(date.getDate() + days);
    return date;
}
function checkRcT() {
    receivetime = document.getElementById("receive-time").value;
    var min = Date.parse(receivetime);
    var rd = Date.parse('2022-05-29');
    if (receivetime == "") {
        jQuery("#lb-receive-time").addClass('required');
        jQuery("#lb-receive-time").focus();
    } else {
        if (min < rd) {
            jQuery("#lb-receive-time").addClass('required');
            jQuery("#lb-receive-time").focus();
        } else {
            jQuery("#lb-receive-time").removeClass('required');
            var rtd = Date.parse('2000-01-14');
            var rtd1 = Date.parse('2000-01-21');
            rtd = rtd1 - rtd;
            rtd = min + rtd;
            var d = new Date(rtd);
            document.getElementById("return-time").value = d.toISOString().substring(0, 10);
            document.getElementById("return-time").setAttribute("min", receivetime);
            document.getElementById("return-time").setAttribute("max", d.toISOString().substring(0, 10));
        }
    }
}