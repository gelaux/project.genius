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

    var tasks = $('#moduleTasks')
    .nestable()
    .on('change', function() {
        alert(window.JSON.stringify(tasks.nestable('serialize')));
    });
};

$(window).bind("resize", inboxWidthFunctions);

$(document).ready(function(){
    inboxWidthFunctions();
});