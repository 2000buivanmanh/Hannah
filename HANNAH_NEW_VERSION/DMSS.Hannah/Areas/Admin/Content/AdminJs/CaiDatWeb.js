$(document).ready(function () {
    loadMap();
});
document.getElementById('iconWebSite').onclick = function () {
    selectFileWithCKFinder('txtIcon');
};
document.getElementById('hinhAnhWebsite').onclick = function () {
    selectFileWithCKFinder('txthinhAnhWebsite');
};
function selectFileWithCKFinder(elementId) {
    CKFinder.popup({
        chooseFiles: true,
        width: 800,
        height: 600,
        onInit: function (finder) {
            finder.on('files:choose', function (evt) {
                var file = evt.data.files.first();
                if (elementId == 'txtIcon') {
                    var output = document.getElementById(elementId);
                    output.value = file.getUrl();
                    $('#showIcon').attr('src', file.getUrl());
                }
                if (elementId == 'txthinhAnhWebsite') {
                    var output = document.getElementById(elementId);
                    output.value = file.getUrl();
                    $('#showhinhAnhWebsite').attr('src', file.getUrl());
                }

            });
            finder.on('file:choose:resizedImage', function (evt) {
                var output = document.getElementById(elementId);
                output.value = evt.data.resizedUrl;
            });
        }
    });
}

function loadMap() {
    var _link_map = $('#linkMap').val();
    if (_link_map != undefined && _link_map != '') {
        $('.map').empty().append(_link_map);
    }
}
document.querySelectorAll('.editor').forEach(e => {
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
function CapNhatThongTinWeb() {
    var Check = true;
    var id = $('#maCaiDat').val();
    var diaChiCuaHang = $('#diaChi').val();
    var sDTLienHe = $('#sdt').val();
    var emailCuaHang = $('#emailCuaHang').val();
    var faceBook = $('#facebook').val();
    var youtube = $('#youtube').val();
    var twitter = $('#twitter').val();
    var zalo = $('#zalo').val();
    var slogan = $('#slogan').val();
    var emailGoiMail = $('#emailGoiMail').val();
    var matKhauEmail = $('#matKhauEmail').val();
    var banDo = $('#linkMap').val();
    var tieuDeWebSite = $('#tieuDeWebsite').val();
    var moTaNgan = $('#moTaNgan').val();
    var gioiThieuWebsite = $('#gioiThieuWebsite').val();
    var gioiThieuHinhAnhWebsite = $('#txthinhAnhWebsite').val();
    var diemMacDinh = $('#diemMacDinh').val();
    var diemTichLuy = $('#diemTichLuy').val();
    var diemDanhGia = $('#diemDanhGia').val();
    var dieuKhoan = $('#dieuKhoan').val();
    var noiDugSeo = $('#noiDugSeo').val();
    var tuKhoaSeo = $('#tuKhoaSeo').val();
    var tieuDeSeo = $('#tieuDeSeo').val();
    var duongDanSeo = $('#duongDanSeo').val();
    var soLanGuiMa = $('#soLanGuiMa').val();
    var soLanXacNhanSai = $('#soLanXacNhanSai').val();
    var thoiHanMaXacNhan = $('#thoiHanMaXacNhan').val();
    var thoiGianHetHanTaiKhoan = $('#thoiGianHetHanTaiKhoan').val();
    var iconWeb = $('#txtIcon').val();
    if (Check) {
        Swal.fire({
            title: "WARNING",
            text: "Do you want update ",
            showDenyButton: true,
            showLoaderOnConfirm: true,
            confirmButtonText: '<i class="fa fa-check"></i> Update',
            denyButtonText: '<i class="fa fa-ban"></i> Cancel',
            allowOutsideClick: false
        }).then((yes) => {
            if (yes.isConfirmed) {
                $.post('/CaiDat/CapNhatWebsite',
                    {
                        MaCaiDat: id,
                        DiaChiCuaHang: diaChiCuaHang,
                        SDTLienHe: sDTLienHe,
                        EmailCuaHang: emailCuaHang,
                        FaceBook: faceBook,
                        Youtube: youtube,
                        Twitter: twitter,
                        Zalo: zalo,
                        Slogan: slogan,
                        EmailGoiMail: emailGoiMail,
                        MatKhauEmail: matKhauEmail,
                        BanDo: banDo,
                        TieuDeWebSite: tieuDeWebSite,
                        MoTaNgan: moTaNgan,
                        GioiThieuWebsite: gioiThieuWebsite,
                        GioiThieuHinhAnhWebsite: gioiThieuHinhAnhWebsite,
                        DieuKhoan: dieuKhoan,
                        NoiDugSeo: noiDugSeo,
                        TuKhoaSeo: tuKhoaSeo,
                        TieuDeSeo: tieuDeSeo,
                        DuongDanSeo: duongDanSeo,
                        DiemMacDinh: diemMacDinh,
                        DiemTichLuy: diemTichLuy,
                        DiemDanhGia: diemDanhGia,
                        SoLanGuiMa: soLanGuiMa,
                        SoLanXacNhanSai: soLanXacNhanSai,
                        ThoiHanMaXacNhan: thoiHanMaXacNhan,
                        ThoiGianHetHanTaiKhoan: thoiGianHetHanTaiKhoan,
                        IconWebSite: iconWeb
                    })
                    .done(function (data) {
                        /*  $body.removeClass("loading");*/
                        if (data.status == "1") {
                            Swal.fire({
                                position: 'absolute',
                                icon: 'success',
                                title: 'Updated!',
                                showConfirmButton: false,
                                timer: 1500
                            })
                            //$("#dongcapnhat").click();
                            //ReloadData();
                        }
                        if (data.status == "0") {
                            $.toast({
                                heading: 'Error',
                                text: 'Error! Please try again.',
                                showhidetransition: 'fade',
                                icon: 'error'
                            });
                        }
                    })
            }
        });
    }
}