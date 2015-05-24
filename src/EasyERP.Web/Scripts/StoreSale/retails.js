/*客户信息*/
$(document).ready(function() {
    var dataSource = new kendo.data.DataSource({
        transport: {
            read: {
                url: "/StoreSale/RetailList",
                dataType: "json",
                contentType: "application/json; charset=utf-8"
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
                fields: {
                    Id: { editable: false, nullable: true },
                    ProductName: {type: "string"},
                    Price: { type: "number" },
                    Quantity: { type: "number" },
                    Date: { type: "string" },
                    TotalAmount:{type:"number"}
                }
            }
        }
    });

    //var selectedItems;

    $("#retailRecords").kendoGrid({
        dataSource: dataSource,
        selectable: "single",
        pageable: {
            refresh: true
        },
        columns: [
            { field: "ProductName", title: "商品" },
            { field: "Price", title: "售价" },
            { field: "Quantity", title: "数量" },
            { field: "Date", title: "日期" },
            { field: "TotalAmount", title: "总价" }
        ]
    });
});