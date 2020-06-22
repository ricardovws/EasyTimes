//to enable tooltip
$(".tooltip").tooltipster({
    theme: "tooltipster-noir"
});


$("#_Name").dblclick(function () {
    callInput($(this));
});

$("#_Email").dblclick(function () {
    callInput($(this));
});

$("#_Phone").dblclick(function () {
    callInput($(this));
});

$("#_Bank").dblclick(function () {
    callInput($(this));
});

$("#_Agency").dblclick(function () {
    callInput($(this));
});

$("#_CurrentAccount").dblclick(function () {
    callInput($(this));
});

$("#_PricePerHour").dblclick(function () {
    callInput($(this));
});

$("#_GasPrice").dblclick(function () {
    callInput($(this));
});

$("#_OvertimeProfitRate").dblclick(function () {
    callInput($(this));
});

$("#_NormalTime").dblclick(function () {
    callInput($(this));
});

$("#_TimeToReceiveMealTicket").dblclick(function () {
    callInput($(this));
});

$("#_MealTicket").dblclick(function () {
    callInput($(this));
});


var param1 = "#input";
var param2 = ".option_to_edit";

//to call edition
function callInput(element) {
    var tag = GetId(element);
    $(param1 + tag).css("display", "initial");
    $(param2 + tag).css("display", "initial");
    $(param1 + tag).focus();
}
//to confirm edition
$(".done").click(function () {

    var param = GetId($(this));
    
    var url = "/Owners/Edit";
    var newInfo = $(param1 + param).val();
    $.post(url, { infoToEdit: param, newInfo: newInfo });
    RefreshCss(param);
    setTimeout(function () { location.reload(); }, 1000);
});
//to clear edition
$(".clear").click(function () {

    var param = GetId($(this));
    RefreshCss(param);
});

//function to get id and keep working
function GetId(element) {
    var id = "#" + element.attr("id");
    var param = id.substring(id.indexOf("_"));
    return param;
}

//function to refresh css style after some edit:
function RefreshCss(param) {
    
    $(param1 + param).css("display", "none");
    $(param2 + param).css("display", "none"); 
}