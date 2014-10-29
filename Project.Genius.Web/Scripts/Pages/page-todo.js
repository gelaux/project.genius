function inboxWidthFunctions(e) {
	var contentHeightOuter = $('#content').outerHeight();
	var winWidth = $(window).width();
	var tasksHeight = $('.tasks').height();

	if (winWidth > 767) {
		if(tasksHeight < contentHeightOuter) {
			$('.tasks').css('min-height',contentHeightOuter-40);
		}
	} else {
		
	}
}

function activatEditableFunctions() {
    $.fn.editable.defaults.mode = 'inline';
    $.fn.editable.defaults.showbuttons = false;
    $('.editable').editable();

    var optional = $('#moduleOptional')
        .on('change', function () {
        $.ajax({
            type: 'post',
            url: optional.data('url'),
            data: {
                'pk': optional.data('pk'),
                'name': optional.data('name'),
                'value': optional.is(':checked')
            }
        });
    });
};

function activateNestableFunctions() {
    var tasks = $('#moduleTasks')
        .nestable()
        .on('change', function() {
            var arrTask = [];
            var url = tasks.data('arrange-url');

            $.each(tasks.nestable('serialize'), function (key, value) {
                arrTask.push(value.id);
            });
            var data = { 'tasks': arrTask };

            $.ajax({
                type: 'post',
                contentType: 'application/json',
                url: url,
                data: JSON.stringify(data)
        });
    });
}

function closeDialog() {
    $('#myModal').modal('hide');
    activateNestableFunctions();
}

function activateTasksFunctions() {
    $('.dd-item').click(function () {
        alert($(this).data('id'));
    });
}

function showModuleDetails() {
    inboxWidthFunctions();
    $('#moduleTypes').hide();
    $('#productVersions').hide();
    $('#detailsWrapper').show();
}

$(window).bind("resize", inboxWidthFunctions);

$(document).ready(function(){
    inboxWidthFunctions();
});