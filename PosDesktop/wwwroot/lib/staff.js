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