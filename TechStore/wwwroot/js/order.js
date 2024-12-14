var dataTable;

$(document).ready(function () {
    var url = window.location.search; 
    if (url.includes("pending")) {
        loadDataTable("pending");
    } else {
        if (url.includes("inprocess")) {
            loadDataTable("inprocess");
        } else {
            if (url.includes("completed")) {
                loadDataTable("completed");
            } else {
                if (url.includes("approved")) {
                    loadDataTable("approved");
                } else {
                    loadDataTable("all");
                }
            }
        }
    }
    // loadDataTable();
});

function loadDataTable(status) {
    // Initialize DataTable
    $('#tblData').DataTable({
        "ajax": { url: '/admin/order/getall?status=' + status },
        "columns": [
            { data: 'orderHeaderId', "width": "10%" },
            { data: 'customerName', "width": "10%" },
            { data: 'customerPhone', "width": "10%" },
            { data: 'applicationUser.email', "width": "10%" },
            { data: 'orderStatus', "width": "10%" },
            { data: 'orderCost', "width": "10%" },
            {
                data: 'orderheaderId',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                        <a href="/admin/order/details?orderheaderId=${data}" class="btn btn-primary mx-2">
                            <i class="bi bi-pencil-square"></i> Edit
                        </a>
                    </div>`
                },
                "width": "10%"
            }
        ]
    });
}
