//function MailControl(email) {
//    var control = new RegExp(/^[^0-9][a-zA-Z0-9_]+([.][a-zA-Z0-9_]+)*[@Html.Raw('@')][a-zA-Z0-9_]+([.][a-zA-Z0-9_]+)*[.][a-zA-Z]{2,4}$/i);
//    return control.test(email);
//}


var token = $("[name='__RequestVerificationToken']").val();
var options = {
    startStep: 1,
    manualStepForward: false
};
var wizard = new KTWizard('kt_wizard_v1', options);
$(document).ready(function () {
    $("#btn-createaccount").click(function () {
        if (document.getElementById("Email").value == "") {
            $("#divResult2").show();
            window.scrollTo(0, 0);
            document.getElementById("divResult2").innerHTML = '<div class="alert alert-warning" role="alert"><div class="alert-text"> DİKKAT : İşaretli alanların doldurulması zorunludur!</div></div>';
            setTimeout(function () {
                $("#divResult2").hide();
            }, 5000);
        }
        else {

            $.ajax({
                type: "POST",
                url: location.origin + '/security/createaccount',
                data: {
                    __RequestVerificationToken: token,
                    Name: document.getElementById("Name").value,
                    Surname: document.getElementById("Surname").value,
                    Username: document.getElementById("Username").value,
                    Password: document.getElementById("Password").value,
                    PasswordAgain: document.getElementById("PasswordAgain").value,
                    Title: document.getElementById("Title").value,
                    TaxAdministration: document.getElementById("TaxAdministration").value,
                    TaxNumber: document.getElementById("TaxNumber").value,
                    ShopWebsite: document.getElementById("ShopWebsite").value,
                    ProvinceID: document.getElementById("ProvinceID").value,
                    CountyID: document.getElementById("CountyID").value,
                    Address: document.getElementById("Address").value,
                    Email: document.getElementById("Email").value,
                    Phone: document.getElementById("Phone").value,
                    IsAccepted: document.getElementById("IsAccepted").checked
                },
                dataType: 'json',
                beforeSend: function () {
                    $("#ajax-loader").show();
                },
                complete: function () {
                    $("#ajax-loader").hide();
                },
                success: function (result) {
                    if (result.Status === "Success") {
                        $("#divResult2").show();
                        window.scrollTo(0, 0);
                        document.getElementById("divResult2").innerHTML = '<div class="alert alert-success" role="alert"><div class="alert-text"> TEBRİKLER : Başvurunuz başarıyla iletildi. Hesabınız onaylandıktan hesabınıza giriş yapabilirsiniz.</div></div>';
                        setTimeout(function () {
                            window.location.href = result.Url;
                        }, 7000);
                    }
                    else if (result.Status === "NotAccepted") {
                        $("#divResult2").show();
                        window.scrollTo(0, 0);
                        document.getElementById("divResult2").innerHTML = '<div class="alert alert-warning" role="alert"><div class="alert-text"> DİKKAT : Başvuru yapabilmeniz için hizmet ve gizlilik şartlarını kabul etmeniz gerekiyor!</div></div>';
                        setTimeout(function () {
                            $("#divResult2").hide();
                        }, 5000);
                    }
                    else if (result.Status === "NoToPassMandatoryFields") {
                        $("#divResult2").show();
                        window.scrollTo(0, 0);
                        document.getElementById("divResult2").innerHTML = '<div class="alert alert-danger" role="alert"><div class="alert-text"> HATA : İşaretli alanlar boş geçilemez! Kontrol edip yeniden deneyiniz!</div></div>';
                        setTimeout(function () {
                            $("#divResult2").hide();
                        }, 5000);
                    }
                    else if (result.Status === "PasswordsAreNotSame") {
                        $("#divResult2").show();
                        window.scrollTo(0, 0);
                        document.getElementById("divResult2").innerHTML = '<div class="alert alert-warning" role="alert"><div class="alert-text"> DİKKAT : Girdiğiniz şifreler aynı olmalıdır.</div></div>';
                        wizard.goTo(1);
                        setTimeout(function () {
                            $("#divResult2").hide();
                        }, 5000);
                    }
                    else if (result.Status === "CorrectThePhone") {
                        $("#divResult2").show();
                        window.scrollTo(0, 0);
                        document.getElementById("divResult2").innerHTML = '<div class="alert alert-warning" role="alert"><div class="alert-text"> DİKKAT : Lütfen telefonunuzu istenen formatta giriniz. </br> <b>Örnek: 0546XXXXXXX <b></div></div>';
                        setTimeout(function () {
                            $("#divResult2").hide();
                        }, 5000);
                    }
                    else if (result.Status === "PasswordCheck") {
                        $("#divResult2").show();
                        window.scrollTo(0, 0);
                        document.getElementById("divResult2").innerHTML = '<div class="alert alert-warning" role="alert"><div class="alert-text"> DİKKAT : Şifre en az 7 en çok 15 karakter olmalı. <b></div></div>';
                        wizard.goTo(1);
                        setTimeout(function () {
                            $("#divResult2").hide();
                        }, 5000);
                    }
                    else if (result.Status === "NullObject") {
                        $("#divResult2").show();
                        window.scrollTo(0, 0);
                        document.getElementById("divResult2").innerHTML = '<div class="alert alert-danger" role="alert"><div class="alert-text"> HATA : İşlem sonucunda boş bir nesne döndürüldü! Yeniden deneyiniz!</div></div>';
                        setTimeout(function () {
                            $("#divResult2").hide();
                        }, 5000);
                    }
                    else if (result.Status === "Fail") {
                        $("#divResult2").show();
                        window.scrollTo(0, 0);
                        document.getElementById("divResult2").innerHTML = '<div class="alert alert-danger" role="alert"><div class="alert-text"> HATA : Bir hata ile karşılaşıldı! Sayfayı yenileyip,bilgileri kontrol edip tekrar deneyiniz!</div></div>';
                        setTimeout(function () {
                            $("#divResult2").hide();
                        }, 5000);
                    }
                    else if (result.Status === "Warning") {
                        $("#divResult2").show();
                        window.scrollTo(0, 0);
                        document.getElementById("divResult2").innerHTML = '<div class="alert alert-warning" role="alert"><div class="alert-text"> DİKKAT : Girmiş olduğunuz kullanıcı adı,şifre veya vergi numarasına ait bir kayır zaten var. Bilgilerinizi kontrol ediniz!</div></div>';
                        setTimeout(function () {
                            $("#divResult2").hide();
                        }, 5000);
                    }
                    else if (result.Status === "MailNotSend") {
                        $("#divResult2").show();
                        window.scrollTo(0, 0);
                        document.getElementById("divResult2").innerHTML = '<div class="alert alert-brand" role="alert"><div class="alert-text"> TEBRİKLER : Başvuru talebiniz başarı ile alındı fakat bilgilendirme maili, girmiş olduğunuz mail adresine gönderilemedi! Süreç ile ilgili sizlerle iletişime geçilecektir.</div></div>';
                        setTimeout(function () {
                            $("#divResult2").hide();
                        }, 5000);
                    }

                },
                error: function () {
                    $("#divResult2").show();
                    window.scrollTo(0, 0);
                    document.getElementById("divResult2").innerHTML = '<div class="alert alert-danger" role="alert"><div class="alert-text"> HATA : Sunucu ile iletişim kurulamadı. İnternet bağlantınızı kontrol ediniz. Sayfayı yenileyip tekrar deneyin.Sorun devam ederse sistem yöneticinize başvurunuz!</div></div>';
                    setTimeout(function () {
                        $("#divResult2").hide();
                    }, 5000);
                }
            });

        }
        //else {
        //    $("#divResult2").show();
        //    window.scrollTo(0, 0);
        //    document.getElementById("divResult2").innerHTML = '<div class="alert alert-warning" role="alert"><div class="alert-text"> DİKKAT : Girdiğiniz mail geçerli değil kontrol edin!</div></div>';
        //    setTimeout(function () {
        //        $("#divResult2").hide();
        //    }, 5000);
        //    return false;
        //}
        return false;
    });
});

