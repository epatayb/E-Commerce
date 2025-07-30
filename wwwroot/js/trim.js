 $('form').on('submit', function () 
{
    $(this).find("input[type='text'], textarea").each(function () {
        $(this).val($.trim($(this).val()));
    });
});