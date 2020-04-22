function MailControl(email) {
	var control = new RegExp(/^[^0-9][a-zA-Z0-9_]+([.][a-zA-Z0-9_]+)*[@Html.Raw('@')][a-zA-Z0-9_]+([.][a-zA-Z0-9_]+)*[.][a-zA-Z]{2,4}$/i);
	return control.test(email);
}

var token = $("[name='__RequestVerificationToken']").val();
$(document).ready(function () {
	$("#btn-send-message").click(function () {
		if (document.getElementById("VisitorEmail").value == "") {
			$("#divResult").show();
			document.getElementById("divResult").innerHTML = '<div class="alert alert-warning" role="alert"><div class="alert-text"> DİKKAT : İşaretli alanların doldurulması zorunludur!</div></div>';
			setTimeout(function () {
				$("#divResult").hide();
			}, 5000);
		}
		else if (MailControl(document.getElementById("VisitorEmail").value)) {
			$.ajax({
				type: "POST",
				url: '/Contact/Send',
				data: {
					__RequestVerificationToken: token,
					VisitorName: document.getElementById("VisitorName").value,
					VisitorEmail: document.getElementById("VisitorEmail").value,
					VisitorPhone: document.getElementById("VisitorPhone").value,
					VisitorMessage: document.getElementById("VisitorMessage").value
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
						$("#divResult").show();
						document.getElementById("divResult").innerHTML = '<div class="alert alert-success" role="alert"><div class="alert-text"> TEBRİKLER : Mesajınız başarıyla iletildi. En kısa zamanda bilgileriniz üzerinden dönüş yapacağız.</div></div>';
						$('#VisitorName').val("");
						$('#VisitorEmail').val("");
						$('#VisitorPhone').val("");
						$('#VisitorMessage').val("");
						setTimeout(function () {
							$("#divResult").hide();
						}, 3000);
					}
					else if (result.Status === "Fail") {
						$("#divResult").show();
						document.getElementById("divResult").innerHTML = '<div class="alert alert-danger" role="alert"><div class="alert-text"> HATA : Bir hata oluştu! Yeniden deneyiniz! </div></div>';
						setTimeout(function () {
							$("#divResult").hide();
						}, 3000);
					}
					else if (result.Status === "None") {
						$("#divResult").show();
						document.getElementById("divResult").innerHTML = '<div class="alert alert-warning" role="alert"><div class="alert-text"> DİKKAT : İşaretli alanların doldurulması zorunludur!</div></div>';
						setTimeout(function () {
							$("#divResult").hide();
						}, 3000);
					}
				},
				error: function () {
					$("#divResult").show();
					document.getElementById("divResult").innerHTML = '<div class="alert alert-danger" role="alert"><div class="alert-text"> HATA : Beklenilmeyen bir hata ile karşılaşıldı.Yeniden deneyin.Sorun devam ederse sistem yöneticinize başvurunuz!</div></div>';
					setTimeout(function () {
						$("#divResult").hide();
					}, 3000);
				}
			});
		}
		else {
			$("#divResult").show();
			document.getElementById("divResult").innerHTML = '<div class="alert alert-warning" role="alert"><div class="alert-text"> DİKKAT : Girdiğiniz mail geçerli değil kontrol edin!</div></div>';
			setTimeout(function () {
				$("#divResult").hide();
			}, 3000);
			return false;
		}
		return false;
	});
});
