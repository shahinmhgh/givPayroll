// function PayrollInsuranceTaxProc(personnelId) {
//     const select = document.querySelector('select[name="year"]');
//     const year = select.options[select.selectedIndex].text;

//     var month = $("#attendanceMonth").val();

//     $.ajax({
//         type: "GET",
//         url: "/Payroll/PayrollInsuranceTaxPreview",
//         data:
//         {
//             year: year,
//             month: month,
//             personnelId: personnelId
//         },

//         success: function (data) {


//             $("#attendanceInsuranceModalContent")
//                 .html(data);



//             $("#attendanceInsuranceModal")
//                 .modal("show");
//         }

//     });
// }

function PayrollProc(personnelId) {
    const select = document.querySelector('select[name="year"]');
    const year = select.options[select.selectedIndex].text;

    var month = $("#attendanceMonth").val();

    $.ajax({
        type: "GET",
        url: "/Payroll/PayrollPreview",
        data:
        {
            year: year,
            month: month,
            personnelId: personnelId
        },

        success: function (data) {


            $("#attendanceCheckModalContent")
                .html(data);



            $("#attendanceCheckModal")
                .modal("show");
        }

    });
}

function PayrollInsuranceTaxProc(insuranceTax) {
    // const select = document.querySelector('select[name="year"]');
    // const year = select.options[select.selectedIndex].text;

    // var month = $("#attendanceMonth").val();
    //alert(insuranceTax);
    $.ajax({
        type: "GET",
        url: "/Payroll/PayrollInsuranceTaxPreview",
        data:
        {
            insuranceTax: insuranceTax,

        },

        success: function (data) {


            $("#insuranceTaxModalContent")
                .html(data);



            $("#insuranceTaxModal")
                .modal("show");
        }

    });
}

function buttonProc(personnelId, correctErrText, hasWorkedText) {


    const select = document.querySelector('select[name="year"]');
    const year = select.options[select.selectedIndex].text;

    var month = $("#attendanceMonth").val();
    $("#attendancePersonnel").val(personnelId);

    $.ajax({

        url: "/Attendance/CheckList",

        data:
        {
            year: year,
            month: month,
            personnelId: personnelId,
            CorrectErrText: correctErrText,
            HasWorkedText: hasWorkedText
        },

        success: function (data) {


            $("#attendanceCheckModalContent")
                .html(data);

            //var height = $("#tableResult").height();
            //alert(height);
            //$("#tableResult").height(500);
            //height = $("#tableResult").height();
            //alert(height);

            // $.validator.unobtrusive.parse(
            //     "#personnelOrderForm"
            // );

            // initContractSelect2();
            // initContractDatePickers();
            //initPersonnelOrderForm();

            $("#attendanceCheckModal")
                .modal("show");
        }

    });

}