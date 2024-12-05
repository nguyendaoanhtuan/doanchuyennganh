
let confirmpassword = document.getElementById('confirmpassword');
let confirmshowEye = document.getElementById('showEyeconfirm');
let confrimhideEye = document.getElementById('hideEyeconfirm');

function passwordShowconfirm() {
	confirmpassword.type = 'text';
	confirmshowEye.style.display = "none";
	confrimhideEye.style.display = "inline";
	confirmpassword.focus();
}
function passwordHideconfirm() {
	confirmpassword.type = 'password';
	confirmshowEye.style.display = "inline";
	confrimhideEye.style.display = "none";
	confirmpassword.focus();
}