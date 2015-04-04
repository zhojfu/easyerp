$(document).ready(function () {
    $("#datepicker").kendoDatePicker({
        format: "yyyy-MM-dd"
    });

    var dataSource = new kendo.data.DataSource({
        transport: {
            read: {
                url: "index",
                dataType: "jsonp"
            },
            update: {
                url: "update",
                dateType: "jsonp"
            }
        }
    });

    $("#timesheet").kendoGrid({
        dataSource: dataSource,
        height: 500,
        pageable: {
            refresh: true,
            pageSizes: true,
            buttonCount: 5
        },
        columns: [
        {
            title: "a",
            field: "d"
        }, {
            title: "c",
            field: "d"
        }]
    });
});