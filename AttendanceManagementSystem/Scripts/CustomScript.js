$('.datePickAd').datepicker({
	format: 'yyyy-mm-dd',
	changeMonth: true,
	changeYear: true,
	autoclose: true
});
$('.datePickBs').nepaliDatePicker({
	npdMonth: true,
	npdYear: true
});
$('.datePickAd').on("change",function ()
{
	var a = $(this).val();
	$(this).siblings("input").val(AD2BS(a));
});
$('.datePickBs').change(function () {
	alert("");
	var a = $(this).val();
	$(this).siblings("input").val(BS2AD(a));
});

$('.numeric').keypress(function (event) {
	return isNumber(event, this)
});
function isNumber(evt, element) {
	var charCode = (evt.which) ? evt.which : event.keyCode
	if (
		(charCode != 45 || $(element).val().indexOf('-') != -1) &&      // “-” CHECK MINUS, AND ONLY ONE.
		(charCode != 46 || $(element).val().indexOf('.') != -1) &&      // “.” CHECK DOT, AND ONLY ONE.
		(charCode != 8) &&                                              //Backsapce,
		(charCode < 48 || charCode > 57)
	)
		return false;
	return true;
}
