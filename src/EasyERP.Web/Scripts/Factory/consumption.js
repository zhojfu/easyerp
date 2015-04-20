

function ConsumptionGrid() {
    var baseUrl = "";
    var dataSource = new kendo.data.DataSource({
        transport: {
            read: {
                url: baseUrl + "/Consumption/Get",
                dataType: "json"
            },
            update: {
                url: baseUrl + "/Consumption/Update",
                type: "post",
                dataType: "json"
            },
            destroy: {
                url: baseUrl + "/Consumption/Delete",
                type: "post",
                dataType: "json"
            },
            create: {
                url: baseUrl + "/Consumption/Create",
                type: "post",
                dataType: "json"
            },
            schema: {
                data: "data",
                total: "total"
            }
        },
        serverPaging: true,
        pageSize: 10,
        schema: {
            data: "data",
            total: "total",
            model: {
                id: "Id",
                fields: {
                    Id: { editable: false, nullable: true },
                    Name: { validation: { required: true } },
                    Unit: { type: "string", validation: { required: true, min: 1 } },
                    PriceOfUnit: { type: "number" }
                }
            }
        }
    });
    
    this.initiConsumptionGrid = function() {
        $("#consumptionList").kendoGrid({
            dataSource: dataSource,
            selectable: "multiple",
            pageable: {
                refresh: true,
            },
            columns: [
                { field: "Name", title: "类名", editable : false },
                { field: "Unit", title: "单位" },
                { field: "PriceOfUnit", title: "单价" },
                { command: ["edit", "destroy"]}
            ],
            editable: "popup",
            toolbar: ["create"]
        });
    };
}


$(document).ready(function () {
    var grid = new ConsumptionGrid();
    grid.initiConsumptionGrid();

    var timesheet = new Timesheet("/Consumption/GetStatistic", "/Consumption/UpdateStatistic");
    timesheet.InitialTimesheetGrid();
});