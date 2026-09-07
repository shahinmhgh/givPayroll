// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function loadContractNotification() {
    $("#tableLoading").show();
    $.ajax({
        url: '/PersonnelContract/GetContractNotificationCount',
        type: 'GET',
        success: function (result) {

            let badge = $("#contractBadge");

            if (result.count > 0) {
                badge.text(result.count);
                badge.show();
            }
            else {
                badge.hide();
            }
            $("#tableLoading").hide();
        }
    });

}


// Load when page openss
$(document).ready(function () {
    
    if (window.isAuthenticated) {
       // alert('sfd))');
        
        loadContractNotification();
        //$('#contractBadge').text(count).show();
    }
    else {
        $('#contractBadge').hide();
    }

});