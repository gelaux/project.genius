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

function editableFunctions() {
    
};

$(window).bind("resize", inboxWidthFunctions);

$(document).ready(function(){
    inboxWidthFunctions();
    $('#modelCaption').editableFunctions();
});