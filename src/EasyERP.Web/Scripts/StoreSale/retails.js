/*客户信息*/
$(document).ready(function() {
    var dataSource = new kendo.data.DataSource({
        transport: {
            read: {
                url: "storesale/orderList",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
            },
            schema: {
                data: "data",
                total: "total"
            }
        },
        serverPaging: true,
        pageSize: 2,
        schema: {
            data: "data",
            total: "total",
            model: {
                fields: {
                    Id: { editable: false, nullable: true },
                    Product: { type: "string" },
                    Price: { type: "number" },
                    Quantity: { type: "number" },
                    CreatedOn: { type: "string" }
                }
            }
        }
    });

    var selectedItems;

    $("#retailRecords").kendoGrid({
        dataSource: dataSource,
        selectable: "single",
        pageable: {
            refresh: true,
        },
        columns: [
            { field: "Product", title: "商品" },
            { field: "Price", title: "售价" },
            { field: "Quantity", title: "数量" },
            { field: "CreatedOn", title: "日期" }
        ],
        change: function() {
            selectedItems = new Array();
            var selects = this.select();
            for (var i = 0; i < selects.length; ++i) {
                selectedItems.push(this.dataItem(selects[i]).Id);
            }
        }
    });

    /*$("#deleteOrder").click(function () {
        $.ajax({
            type: "post",
            url: "/StoreSale/Delete",
            data: JSON.stringify({ ids: selectedItems }),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success : function() {
                $('#orderList').data('kendoGrid').dataSource.read();
            }
        });
    });*/
});