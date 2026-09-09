function loadAttendance() {
    let year =
        $("#attendanceYear").val();

    let month =
        $("#attendanceMonth").val();

    let personnelId =
        $("#attendancePersonnel").val();


    if (!personnelId) {
        $("#attendanceTable").html("");

        return;
    }


    $.ajax({

        url: "/Attendance/List",

        data:
        {
            year: year,
            month: month,
            personnelId: personnelId
        },

        success: function (data) {
            $("#attendanceTable")
                .html(data);


            updateTitle();
        }

    });
}



function updateTitle() {
    let year =
        $("#attendanceYear option:selected")
            .text();

    let month =
        $("#attendanceMonth option:selected")
            .text();

    let personnel =
        $("#attendancePersonnel option:selected")
            .text();


    $("#attendanceTitle").text(
        `${personnel} - ${month} ${year}`
    );
}



// =====================================================
// SEARCH
// =====================================================

$("#btnSearchAttendance").click(
    function () {
        loadAttendance();
    }
);



// =====================================================
// MONTH
// =====================================================

function previousMonth() {
    let month =
        parseInt(
            $("#attendanceMonth").val()
        );

    let year =
        parseInt(
            $("#attendanceYear").val()
        );


    month--;

    if (month < 1) {
        month = 12;
        year--;
    }


    setYear(year);
    setMonth(month);

    loadAttendance();
}



function nextMonth() {
    let month =
        parseInt(
            $("#attendanceMonth").val()
        );

    let year =
        parseInt(
            $("#attendanceYear").val()
        );


    month++;

    if (month > 12) {
        month = 1;
        year++;
    }


    setYear(year);
    setMonth(month);

    loadAttendance();
}



// =====================================================
// YEAR
// =====================================================

function previousYear() {
    let year =
        parseInt(
            $("#attendanceYear").val()
        );

    setYear(year - 1);

    loadAttendance();
}



function nextYear() {
    let year =
        parseInt(
            $("#attendanceYear").val()
        );

    setYear(year + 1);

    loadAttendance();
}



function setYear(year) {
    let option =
        $("#attendanceYear option[value='" +
            year +
            "']");

    if (option.length) {
        $("#attendanceYear")
            .val(year);
    }
}



function setMonth(month) {
    $("#attendanceMonth")
        .val(month);
}



// =====================================================
// PERSONNEL
// =====================================================

function previousPersonnel() {
    let select =
        $("#attendancePersonnel");

    let index =
        select.prop("selectedIndex");


    if (index > 1) {
        select.prop(
            "selectedIndex",
            index - 1
        );

        loadAttendance();
    }
}



function nextPersonnel() {
    let select =
        $("#attendancePersonnel");

    let index =
        select.prop("selectedIndex");


    if (index <
        select[0].options.length - 1) {
        select.prop(
            "selectedIndex",
            index + 1
        );

        loadAttendance();
    }
}

$("#btnImportAttendance").click(function () {

    $("#importYear")
        .val($("#attendanceYear").val());

    $("#importMonth")
        .val($("#attendanceMonth").val());

    $("#attendanceExcel").val("");

    $("#importAttendanceModal").modal("show");
});


function importAttendanceExcel() {
    let file =
        $("#attendanceExcel")[0].files[0];


    if (!file) {
        Swal.fire({
            icon: "warning",
            title: "فایل Excel را انتخاب کنید"
        });

        return;
    }


    let formData =
        new FormData();


    formData.append(
        "file",
        file
    );


    formData.append(
        "year",
        $("#importYear").val()
    );


    formData.append(
        "month",
        $("#importMonth").val()
    );


    // Anti forgery token
    formData.append(
        "__RequestVerificationToken",
        $('input[name="__RequestVerificationToken"]')
            .val()
    );


    $.ajax({

        url:
            "/Attendance/Import",

        type:
            "POST",

        data:
            formData,

        processData:
            false,

        contentType:
            false,

        success:
            function (result) {
                if (result.success) {
                    $("#importAttendanceModal")
                        .modal("hide");


                    Swal.fire({
                        icon:
                            "success",

                        title:
                            "ورود اطلاعات انجام شد",

                        html:
                            "جدید: " +
                            result.imported +
                            "<br>" +
                            "به‌روزرسانی: " +
                            result.updated +
                            "<br>" +
                            "رد شده: " +
                            result.skipped
                    });


                    loadAttendance();
                }
                else {
                    Swal.fire({
                        icon:
                            "error",

                        title:
                            "خطا",

                        text:
                            result.message
                    });
                }
            },

        error:
            function () {
                Swal.fire({
                    icon:
                        "error",

                    title:
                        "خطا در ورود Excel"
                });
            }
    });
}


// =====================================================
// NEW
// =====================================================

$("#btnNewAttendance").click(
    function () {
        let year =
            $("#attendanceYear").val();

        let month =
            $("#attendanceMonth").val();

        let personnelId =
            $("#attendancePersonnel").val();


        if (!personnelId) {
            Swal.fire(
                {
                    icon: "warning",
                    title:
                        "ابتدا پرسنل را انتخاب کنید"
                });

            return;
        }


        $.get(
            "/Attendance/Create",
            {
                year: year,
                month: month,
                personnelId: personnelId
            },

            function (data) {
                $("#attendanceModalContent")
                    .html(data);


                $.validator.unobtrusive.parse(
                    "#attendanceForm"
                );


                $("#attendanceModal")
                    .modal("show");
            }
        );
    }
);



// =====================================================
// EDIT
// =====================================================

function editAttendance(id) {
    $.get(
        "/Attendance/Edit/" + id,

        function (data) {
            $("#attendanceModalContent")
                .html(data);


            $.validator.unobtrusive.parse(
                "#attendanceForm"
            );


            $("#attendanceModal")
                .modal("show");
        }
    );
}



// =====================================================
// DELETE
// =====================================================

function deleteAttendance(id) {
    Swal.fire(
        {
            title:
                "حذف شود؟",

            text:
                "رکورد حضور و غیاب حذف خواهد شد.",

            icon:
                "warning",

            showCancelButton:
                true,

            confirmButtonText:
                "بله، حذف شود",

            cancelButtonText:
                "انصراف",

            confirmButtonColor:
                "#d33"

        }).then(
            function (result) {
                if (result.isConfirmed) {
                    $.ajax(
                        {
                            url:
                                "/Attendance/Delete/" +
                                id,

                            type:
                                "DELETE",

                            success:
                                function () {
                                    loadAttendance();
                                    //document.getElementById("btnSearchAttendance").click();

                                    Swal.fire(
                                        {
                                            icon:
                                                "success",

                                            title:
                                                "حذف شد",

                                            timer:
                                                1000,

                                            showConfirmButton:
                                                false
                                        });
                                }
                        });
                }
            }
        );
}



// =====================================================
// INITIAL
// =====================================================

$(document).ready(
    function () {
        // Select first personnel automatically
        let select =
            $("#attendancePersonnel");


        if (select.val() === "" &&
            select[0].options.length > 1) {
            select.prop(
                "selectedIndex",
                1
            );
        }


        loadAttendance();
    }
);