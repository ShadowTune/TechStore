var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    // Initialize DataTable
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/admin/product/getall'},
        "columns": [
            { data: 'category.name', "width": "10%" },
            { data: 'series', "width": "10%" },
            { data: 'model', "width": "10%" },
            { data: 'regularPrice', "width": "10%" },
            { data: 'discountPrice', "width": "10%" },
            { data: 'displayOrder', "width": "10%" },
            {
                data: 'productId',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                        <a href="/admin/product/updateadd/${data}" class="btn btn-primary mx-2">
                            <i class="bi bi-pencil-square"></i> Edit
                        </a>
                        <a onClick=Delete("/admin/product/delete/${data}") class="btn btn-danger mx-2">
                            <i class="bi bi-trash3-fill"></i> Delete
                        </a>
                    </div>`
                },
                "width": "10%"
            }
        ]
    });
}

function Delete(url) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => { 
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    dataTable.ajax.reload();
                    toastr.success(data.message);
                }
            })
        }
    });
}