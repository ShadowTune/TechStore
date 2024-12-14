$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    // Initialize DataTable
    $('#tblData').DataTable({
        "ajax": { url: '/admin/category/getall' },
        "columns": [
            { data: 'name', "width": "10%" },
            { data: 'series', "width": "10%" },
            { data: 'displayOrder', "width": "10%" },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                        <a href="/admin/category/edit/${data}" class="btn btn-primary mx-2">
                            <i class="bi bi-pencil-square"></i> Edit
                        </a>
                        <a href="/admin/category/delete/${data}" class="btn btn-danger mx-2">
                            <i class="bi bi-trash3-fill"></i> Delete
                        </a>
                    </div>`
                },
                "width": "10%"
            }
        ]
    });
}
