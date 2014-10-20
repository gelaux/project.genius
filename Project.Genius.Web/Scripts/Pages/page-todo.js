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

function activateNestableFunctions() {
    var tasks = $('#moduleTasks')
        .nestable()
        .on('change', function() {
            var arrTask = [];

            $.each(tasks.nestable('serialize'), function (key, value) {
                arrTask.push(value.id);
            });
            var data = { 'tasks': arrTask };

            $.ajax({
                type: 'post',
                contentType: 'application/json',
                url: 'Modules/SortTasks',
                data: JSON.stringify(data)
        });
    });
}

$(window).bind("resize", inboxWidthFunctions);

$(document).ready(function(){
    inboxWidthFunctions();
});