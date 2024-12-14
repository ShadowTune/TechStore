$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    // Initialize DataTable
    $('#tblData').DataTable({
        "ajax": { url: '/admin/company/getall' },
        "columns": [
            { data: 'name', "width": "10%" },
            { data: 'branch', "width": "10%" },
            { data: 'state', "width": "10%" },
            { data: 'support', "width": "10%" },
            {
                data: 'companyId',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                        <a href="/admin/company/updateadd/${data}" class="btn btn-primary mx-2">
                            <i class="bi bi-pencil-square"></i> Edit
                        </a>
                        <a href="/admin/company/delete/${data}" class="btn btn-danger mx-2">
                            <i class="bi bi-trash3-fill"></i> Delete
                        </a>
                    </div>`
                },
                "width": "10%"
            }
        ]
    });
}
