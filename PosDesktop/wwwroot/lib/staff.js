window.initDataTable = function (staffList, dotNetHelper) {
    $(document).ready(function () {
        // Destroy existing DataTable if it exists
        if ($.fn.DataTable.isDataTable('#staffTable')) {
            $('#staffTable').DataTable().destroy();
        }

        // Initialize DataTable
        const table = $('#staffTable').DataTable({
            data: staffList,
            columns: [
                {
                    data: null,
                    render: function (data, type, row, meta) {
                        return meta.row + 1;
                    }
                },
                {
                    data: 'name',
                    render: function (data, type, row) {
                        return `
                            <div class="d-flex align-items-center">
                                <img src="${row.profilePicture}" class="rounded-circle me-2" alt="User" style="width: 40px; height: 40px; object-fit: cover; border: 2px solid blue;">
                                <span class="fw-semibold">${data}</span>
                            </div>`;
                    }
                },
                { data: 'email' },
                { data: 'phone' },
                { data: 'age' },
                {
                    data: 'salary',
                    render: $.fn.dataTable.render.number(',', '.', 2, '₦')
                },
                { data: 'timings' },
                {
                    data: null,
                    render: function (data, type, row) {
                        return `
                            <div class="btn-group p-2">
                                <button class="btn btn-sm btn-outline-info me-1">
                                    <i class="bi bi-eye"></i>
                                </button>
                                <button class="btn btn-sm btn-outline-success edit-btn me-1" data-staff-id="${row.id}">
                                    <i class="bi bi-pencil"></i>
                                </button>
                                <button class="btn btn-sm btn-outline-danger">
                                    <i class="bi bi-trash"></i>
                                </button>
                            </div>`;
                    }
                }
            ]
        });

        // Event delegation for edit buttons
        $('#staffTable').on('click', '.edit-btn', function () {
            const staffId = $(this).data('staff-id');
            if (dotNetHelper) {
                dotNetHelper.invokeMethodAsync('EditStaffFromJs', staffId)
                    .catch(err => console.error("JS->Blazor error:", err));
            } else {
                console.error("DotNetHelper not initialized!");
            }
        });

        // Cleanup on DataTable destroy
        table.on('destroy', function () {
            $('#staffTable').off('click', '.edit-btn');
        });
    });
};

//product table
window.initProductDataTable = function (productList, dotNetHelper) {
    $(document).ready(function () {
        // Destroy existing DataTable if it exists
        if ($.fn.DataTable.isDataTable('#productTable')) {
            $('#productTable').DataTable().destroy();
        }

        // Initialize DataTable
        const table = $('#productTable').DataTable({
            data: productList,
            columns: [
                {
                    data: null,
                    render: function (data, type, row, meta) {
                        return meta.row + 1;
                    }
                },
                {
                    data: 'name',
                    render: function (data, type, row) {
                        return `
                            <div class="d-flex align-items-center">
                                <img src="${row.image}" class="rounded-circle me-2" alt="${row.name}" style="width: 40px; height: 40px; object-fit: cover; border: 2px solid blue;">
                                <span class="fw-semibold">${data}</span>
                            </div>`;
                    }
                },
                {
                    data: null,
                    render: function (data, type, row) {
                        let color = "secondary"; 
                        if (row.status === 0) { 
                            color = "yellow";
                        } else if (row.status === 1) {
                            color = "success";
                        }
                        return `
                        <span>Status: <span class="badge rounded-pill bg-${color}"> ${row.statusText} </span></span><br>
                        <span>Stocked Product: <span class="badge rounded-pill bg-primary">${row.stock}</span> In Stock</span>
                        `;
                    }
                },
                {
                    data: null,
                    render: function (data, type, row) {
                        var category = row.category.name;
                        return `
                            ${category}
                        `;
                    }
                },
                {
                    data: 'price',
                    render: $.fn.dataTable.render.number(',', '.', 2, '₦')
                },
                {
                    data: null,
                    render: function (data, type, row) {
                        return `
                            <div class="btn-group p-2">
                                <button class="btn btn-sm btn-outline-info me-1 view-btn" data-product-id="${row.productId}">
                                    <i class="bi bi-eye"></i>
                                </button>
                                <button class="btn btn-sm btn-outline-success edit-btn me-1" data-product-id="${row.productId}">
                                    <i class="bi bi-pencil"></i>
                                </button>
                                <button class="btn btn-sm btn-outline-danger delete-btn" data-product-id="${row.productId}">
                                    <i class="bi bi-trash"></i>
                                </button>
                            </div>`;
                    }
                }
            ]
        });

        // Event delegation for edit buttons
        $('#productTable').on('click', '.edit-btn', function () {
            const productId = $(this).data('product-id');
            if (dotNetHelper) {
                dotNetHelper.invokeMethodAsync('EditProductFromJs', productId)
                    .catch(err => console.error("JS->Blazor error:", err));
            } else {
                console.error("DotNetHelper not initialized!");
            }
        });

        // Cleanup on DataTable destroy
        table.on('destroy', function () {
            $('#productTable').off('click', '.edit-btn');
        });

        // Event delegation for view buttons
        $('#productTable').on('click', '.view-btn', function () {
            const productId = $(this).data('product-id');
            if (dotNetHelper) {
                dotNetHelper.invokeMethodAsync('ViewProductFromJs', productId)
                    .catch(err => console.error("JS->Blazor error:", err));
            } else {
                console.error("DotNetHelper not initialized!");
            }
        });

        // Cleanup on DataTable destroy
        table.on('destroy', function () {
            $('#productTable').off('click', '.view-btn');
        });


        // Event delegation for delete button
        $('#productTable').on('click', '.delete-btn', function () {
            const productId = $(this).data('product-id');
            if (dotNetHelper) {
                dotNetHelper.invokeMethodAsync('DeleteProductFromJs', productId)
                    .catch(err => console.error("JS->Blazor error:", err));
            } else {
                console.error("DotNetHelper not initialized!");
            }
        });

        // Cleanup on DataTable destroy
        table.on('destroy', function () {
            $('#productTable').off('click', '.delete-btn');
        });
    });
};