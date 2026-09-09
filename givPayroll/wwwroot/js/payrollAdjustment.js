function loadPayrollAdjustments(page = 1) {

    let description =
        $("#searchDescription").val();

    $.get(
        "/PayrollAdjustment/List",
        {
            description: description,
            page: page,
            pageSize: 10
        },
        function (html) {

            $("#payrollAdjustmentList")
                .html(html);

        }
    );
}


// =========================================================
// CREATE
// =========================================================

function createPayrollAdjustment() {

    $.get(
        "/PayrollAdjustment/Create",
        function (html) {

            $("#payrollAdjustmentModalContent")
                .html(html);
            initContractDatePickers();
            $("#payrollAdjustmentModal")
                .modal("show");

        }
    );
}


// =========================================================
// EDIT
// =========================================================

function editPayrollAdjustment(id) {

    $.get(
        "/PayrollAdjustment/Edit",
        {
            id: id
        },
        function (html) {

            $("#payrollAdjustmentModalContent")
                .html(html);
            initContractDatePickers();
            $("#payrollAdjustmentModal")
                .modal("show");

        }
    );
}


// =========================================================
// FORM INITIALIZATION
// =========================================================

function initializePayrollAdjustmentForm() {

    calculateEndDate();

    $("#StartDate, #adjustmentCount")
        .off("change.payrollAdjustment")
        .on(
            "change.payrollAdjustment",
            function () {
                
                calculateEndDate();

            }
        );


    $("#payrollAdjustmentForm")
        .off("submit.payrollAdjustment")
        .on(
            "submit.payrollAdjustment",
            function (e) {

                e.preventDefault();

                savePayrollAdjustment(this);

            }
        );


    $.validator.unobtrusive.parse(
        "#payrollAdjustmentForm"
    );
}


// =========================================================
// CALCULATE END DATE
// =========================================================

function calculateEndDate() {
    
    let startDate = $("#StartDate").val();
    
    let count = parseInt($("#adjustmentCount").val());
    
    if (!startDate || !count || count <= 0) {
        $("#EndDate").val("");
        return;
    }
    
    let date = new Date(startDate + "T00:00:00");
    date.setMonth(date.getMonth() + count);
    date.setDate(date.getDate() - 1 );
    
    let year = date.getFullYear();
    let month = String(date.getMonth() + 1).padStart(2, "0");
    let day = String(date.getDate()).padStart(2, "0");

    let date2 = `${year}-${month}-${day}`;
   
    var endDateApp = contractDatePickers['#EndDateApp'];
    endDateApp.persianDate = moment(date2).format("jYYYY/jMM/jDD");
     
    // $("#EndDate")
    //     .val(
    //         `${year}-${month}-${day}`
    //     );
}


// =========================================================
// SAVE
// =========================================================

function savePayrollAdjustment(form) {

    if (!$(form).valid()) {

        return;
    }


    $.ajax({

        url: "/PayrollAdjustment/Save",

        type: "POST",

        data: $(form).serialize(),

        success: function (result) {

            if (result.success) {

                $("#payrollAdjustmentModal")
                    .modal("hide");

                loadPayrollAdjustments();

                Swal.fire({
                    icon: "success",
                    text: result.message,
                    confirmButtonText: "باشه"
                });

            }
            else {

                Swal.fire({
                    icon: "error",
                    text: result.message
                });

            }

        },

        error: function () {

            Swal.fire({
                icon: "error",
                text: "خطا در ذخیره اطلاعات"
            });

        }

    });
}


function loadAvailablePersonnel() {

    let search =
        $("#personnelSearch").val();


    $.get(
        "/PayrollAdjustment/PersonnelList",
        {
            search: search
        },
        function (data) {

            let html = "";

            data.forEach(function (item) {

                let alreadySelected =
                    $("#selectedPersonnelBody tr")
                        .filter(
                            `[data-personnel-id="${item.id}"]`
                        )
                        .length > 0;


                html += `
                 
                    <div class="d-flex
                                justify-content-between
                                align-items-center
                                border-bottom
                                p-2">

                        <span>
                            ${item.name}
                        </span>

                        <button
                            type="button"
                            class="btn btn-sm
                                   ${alreadySelected
                        ? "btn-secondary"
                        : "btn-success"}"
                            ${alreadySelected
                        ? "disabled"
                        : ""}
                            onclick="addPersonnel(
                                ${item.id},
                                '${escapeHtml(item.name)}'
                            )">

                            ${alreadySelected
                        ? "انتخاب شده"
                        : "افزودن"}

                        </button>

                    </div>
                    
                `;
            });


            $("#availablePersonnelList")
                .html(html);

        }
    );
}

function openPersonnelModal() {

    loadAvailablePersonnel();

    $("#payrollAdjustmentPersonnelModal")
        .modal("show");
}
function openPersonnelModal() {

    loadAvailablePersonnel();

    $("#payrollAdjustmentPersonnelModal")
        .modal("show");
}

function addPersonnel(
    personnelId,
    personnelName) {

    // Prevent duplicate
    if (
        $(`#selectedPersonnelBody tr[data-personnel-id="${personnelId}"]`)
            .length > 0
    ) {
        return;
    }


    let rowCount =
        $("#selectedPersonnelBody tr").length + 1;


    let row = `

        <tr data-personnel-id="${personnelId}">

            <td>
                ${rowCount}
            </td>

            <td>
                ${personnelName}
            </td>

            <td>

                <button type="button"
                        class="btn btn-sm btn-danger"
                        onclick="removeSelectedPersonnel(this)">

                    حذف

                </button>

            </td>

        </tr>

    `;


    $("#selectedPersonnelBody")
        .append(row);


    $("#personnelHiddenInputs")
        .append(`

            <input type="hidden"
                   name="PersonnelIds"
                   value="${personnelId}" />

        `);


    renumberPersonnelRows();


    // Refresh personnel modal
    loadAvailablePersonnel();
}


function removeSelectedPersonnel(button) {

    let row =
        $(button).closest("tr");

    let personnelId =
        row.data("personnel-id");


    row.remove();


    $(
        `#personnelHiddenInputs input[value="${personnelId}"]`
    ).remove();


    renumberPersonnelRows();

    loadAvailablePersonnel();
}


function renumberPersonnelRows() {

    $("#selectedPersonnelBody tr")
        .each(function (index) {

            $(this)
                .find("td:first")
                .text(index + 1);

        });
}

function escapeHtml(text) {

    return $("<div>")
        .text(text)
        .html();
}


$(document).on(
    "input",
    "#personnelSearch",
    function () {

        loadAvailablePersonnel();

    }
);


function deletePayrollAdjustment(id) {

    Swal.fire({

        title: "حذف تعدیل",

        text: "آیا از حذف این تعدیل اطمینان دارید؟",

        icon: "warning",

        showCancelButton: true,

        confirmButtonText: "بله، حذف شود",

        cancelButtonText: "انصراف"

    }).then(function (result) {

        if (!result.isConfirmed)
            return;


        let token =
            $('input[name="__RequestVerificationToken"]')
                .first()
                .val();


        $.ajax({

            url: "/PayrollAdjustment/Delete",

            type: "POST",

            data: {
                id: id,
                __RequestVerificationToken: token
            },

            success: function (result) {

                if (result.success) {

                    loadPayrollAdjustments();

                    Swal.fire({
                        icon: "success",
                        text: result.message
                    });

                }
                else {

                    Swal.fire({
                        icon: "error",
                        text: result.message
                    });

                }

            }

        });

    });
}