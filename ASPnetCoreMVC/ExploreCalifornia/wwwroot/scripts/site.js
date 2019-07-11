$(function () {

    $('#mainContent').on('click', '.pager a', function () {//so page wont refresh
        var url = $(this).attr('href');

        $('#mainContent').load(url);

        return false;
    })

});