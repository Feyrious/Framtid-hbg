// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.


$(document).ready(function() {
    $('#PhoneGroup').hide();
    $('#AdressGroup').hide();

    $('input[type="radio"]').click(function() {
        if ($('#other-radio').is(":checked")) {
            $('#PhoneGroup').hide();
            $('#AdressGroup').hide();
        } else {
            $('#PhoneGroup').show();
            $('#AdressGroup').show();
        }
    });
});
