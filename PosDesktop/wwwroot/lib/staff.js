
window.initDataTable = function (staffList) {
    $(document).ready(function () {
        if ($.fn.DataTable.isDataTable('#staffTable')) {
            $('#staffTable').DataTable().destroy(); // Destroy existing DataTable
        }
        $('#staffTable').DataTable({
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
                    render: function (data,type, row) {
                        return `
                            <div class="d-flex align-items-center">
                                <img src="${row.profilePicture}" class="rounded-circle me-2 " alt="User" style="width: 40px; height: 40px; object-fit: cover; border:2px solid blue;">
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
                            <button class="btn btn-sm btn-outline-info me-1">
                                <i class="bi bi-eye"></i>
                            </button>
                            <button class="btn btn-sm btn-outline-success me-1"  @onclick="() => EditStaff(staff.Id)">
                                <i class="bi bi-pencil"></i>
                            </button>
                            <button class="btn btn-sm btn-outline-danger">
                                <i class="bi bi-trash"></i>
                            </button>`;
                    }
                }
            ]
        });
    });
};
