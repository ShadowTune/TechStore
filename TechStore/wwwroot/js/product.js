$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            url: '/admin/product/getall', // Ensure this is the correct endpoint
            type: 'GET',                 // HTTP method
            datatype: 'json'             // Expected data type
        },
        "columns": [
            { data: 'category.name', "width": "10%" },
            { data: 'series', "width": "10%" },
            { data: 'model', "width": "10%" },
            { data: 'regularPrice', "width": "10%" },
            { data: 'discountPrice', "width": "10%" },
            { data: 'displayOrder', "width": "10%" },
            {
                data: 'productId', // This data field should correspond to the unique product ID from your backend
                "render": function (data) {
                    return `
                        <div class="w-75 btn-group" role="group">
                            <a href="/admin/product/updateadd/${data}" class="btn btn-primary mx-2">
                                <i class="bi bi-pencil-square"></i> Edit
                            </a>
                            <a href="javascript:void(0)" class="btn btn-danger mx-2" onclick="deleteProduct(${data})">
                                <i class="bi bi-trash-fill"></i> Delete
                            </a>
                        </div>`;
                },
                "width": "10%"
            }
        ]
    });
}
