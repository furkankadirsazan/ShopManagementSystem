function MailControl(email) {
    var control = new RegExp(/^[^0-9][a-zA-Z0-9_]+([.][a-zA-Z0-9_]+)*[@Html.Raw('@')][a-zA-Z0-9_]+([.][a-zA-Z0-9_]+)*[.][a-zA-Z]{2,4}$/i);
    return control.test(email);
}

//var token = $("[name='__RequestVerificationToken']").val();
$(document).ready(function () {
    $("#btn-createaccount").click(function () {        
        if (document.getElementById("CrEmail").value == "") {
            $("#divResult2").show();
            window.scrollTo(0, 0);
            document.getElementById("divResult2").innerHTML = '<div class="alert alert-warning" role="alert"><div class="alert-text"> DİKKAT : İşaretli alanların doldurulması zorunludur!</div></div>';
            setTimeout(function () {
                $("#divResult2").hide();
            }, 5000);
        }
        else if (MailControl(document.getElementById("CrEmail").value)) {

            $.ajax({
                type: "POST",
                url: '/student/security/createaccount',
                data: {
                    Ssn: document.getElementById("CrSsn").value,
                    Name: document.getElementById("CrName").value,
                    Surname: document.getElementById("CrSurname").value,
                    BirthdayDate: document.getElementById("CrBirthdayYear").value,
                    Address: document.getElementById("CrAddress").value,
                    Email: document.getElementById("CrEmail").value,
                    Phone: document.getElementById("CrPhone").value,
                    Password: document.getElementById("CrPassword").value,
                    EducationStatusID: document.getElementById("CrEducationStatusID").value,
                    Gender: document.getElementById("CrGender").checked,
                    MaritalStatus: document.getElementById("CrMaritalStatus").checked,
                    IsHandicapped: document.getElementById("CrIsHandicapped").checked,
                    IsMartyrOrVeteranRelative: document.getElementById("CrIsMartyrOrVeteranRelative").checked,
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
                        document.getElementById("divResult2").innerHTML = '<div class="alert alert-success" role="alert"><div class="alert-text"> TEBRİKLER : Kayıt işlemi başarılı. Hesabınıza doğrudan giriş yapabilirsiniz.</div></div>';
                        setTimeout(function () {
                            window.location.href = result.Url;
                        }, 7000);
                    }
                    else if (result.Status === "Fail") {
                        $("#divResult2").show();
                        window.scrollTo(0, 0);
                        document.getElementById("divResult2").innerHTML = '<div class="alert alert-danger" role="alert"><div class="alert-text"> HATA : Bir hata ile karşılaşıldı! Sayfayı yenileyip,bilgileri kontrol edip tekrar deneyiniz!</div></div>';
                        setTimeout(function () {
                            $("#divResult2").hide();
                        }, 5000);
                    }
                    else if (result.Status === "CorrectThePhone") {
                        $("#divResult2").show();
                        window.scrollTo(0, 0);
                        document.getElementById("divResult2").innerHTML = '<div class="alert alert-warning" role="alert"><div class="alert-text"> DİKKAT : Lütfen telefonu istenen formatta giriniz. </br> <b>Örnek: 0546XXXXXXX <b></div></div>';
                        setTimeout(function () {
                            $("#divResult2").hide();
                        }, 5000);
                    }
                    else if (result.Status === "Warning") {
                        $("#divResult2").show();
                        window.scrollTo(0, 0);
                        document.getElementById("divResult2").innerHTML = '<div class="alert alert-warning" role="alert"><div class="alert-text"> DİKKAT : Bu Tc Kimlik numarasına veya Email adresine kayıtlı bir hesap zaten var!</div></div>';
                        setTimeout(function () {
                            $("#divResult2").hide();
                        }, 5000);
                    }
                    else if (result.Status === "MailNotSend") {
                        $("#divResult2").show();
                        window.scrollTo(0, 0);
                        document.getElementById("divResult2").innerHTML = '<div class="alert alert-brand" role="alert"><div class="alert-text"> TEBRİKLER : Kayıt işlemi başarılı fakat doğrulama maili girmiş olduğunuz mail adresine gönderilemedi! Hesabınızı merkeze gelerek doğrulatmalısınız.</div></div>';
                        setTimeout(function () {
                            $("#divResult2").hide();
                        }, 5000);
                    }
                    else if (result.Status === "NotValid") {
                        $("#divResult2").show();
                        window.scrollTo(0, 0);
                        document.getElementById("divResult2").innerHTML = '<div class="alert alert-brand" role="alert"><div class="alert-text"> DİKKAT : Bu T.C Kimlik numarasına ait kişi bilgileri doğrulanamıyor! Girdiğiniz bilgiler T.C kimlik bilgilerinizle aynı olmalıdır.</div></div>';
                        setTimeout(function () {
                            $("#divResult2").hide();
                        }, 5000);
                    }
                    else if (result.Status === "NotAccepted") {
                        $("#divResult2").show();
                        window.scrollTo(0, 0);
                        document.getElementById("divResult2").innerHTML = '<div class="alert alert-warning" role="alert"><div class="alert-text"> DİKKAT : Kayıt olmak için şartları kabul etmeniz gerekiyor!</div></div>';
                        setTimeout(function () {
                            $("#divResult2").hide();
                        }, 5000);
                    }
                    else if (result.Status === "TooLong") {
                        $("#divResult2").show();
                        window.scrollTo(0, 0);
                        document.getElementById("divResult2").innerHTML = '<div class="alert alert-warning" role="alert"><div class="alert-text"> DİKKAT : Şifre minimum 6 maksimum 10 karakter olmalıdır!</div></div>';
                        setTimeout(function () {
                            $("#divResult2").hide();
                        }, 5000);
                    }
                    else if (result.Status === "None") {
                        $("#divResult2").show();
                        window.scrollTo(0, 0);
                        document.getElementById("divResult2").innerHTML = '<div class="alert alert-warning" role="alert"><div class="alert-text"> DİKKAT : Tüm alanların doldurulması zorunludur!</div></div>';
                        setTimeout(function () {
                            $("#divResult2").hide();
                        }, 5000);
                    }
                },
                error: function () {
                    $("#divResult2").show();
                    window.scrollTo(0, 0);
                    document.getElementById("divResult2").innerHTML = '<div class="alert alert-danger" role="alert"><div class="alert-text"> HATA : Beklenilmeyen bir hata ile karşılaşıldı. Sayfayı yenileyip tekrar deneyin.Sorun devam ederse sistem yöneticinize başvurunuz!</div></div>';
                    setTimeout(function () {
                        $("#divResult2").hide();
                    }, 5000);
                }
            });

        }
        else {
            $("#divResult2").show();
            window.scrollTo(0, 0);
            document.getElementById("divResult2").innerHTML = '<div class="alert alert-warning" role="alert"><div class="alert-text"> DİKKAT : Girdiğiniz mail geçerli değil kontrol edin!</div></div>';
            setTimeout(function () {
                $("#divResult2").hide();
            }, 5000);
            return false;
        }
        return false;
    });
});

