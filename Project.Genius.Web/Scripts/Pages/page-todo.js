function inboxWidthFunctions(e) {
	var contentHeightOuter = $('#content').outerHeight();
	var winWidth = $(window).width();
	var tasksHeight = $('.tasks').height();

	if (winWidth > 767) {
		if(tasksHeight < contentHeightOuter) {
			$('.tasks').css('height',contentHeightOuter-40);
		}
	} else {
		
	}
}

function activatEditableFunctions() {
    $.fn.editable.defaults.mode = 'inline';
    $.fn.editable.defaults.showbuttons = false;
    $('.editable').editable();
};

$(window).bind("resize", inboxWidthFunctions);

$(document).ready(function(){
    inboxWidthFunctions();
});