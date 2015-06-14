/*客户信息*/
$(document).ready(function() {

    var dataSource = new kendo.data.DataSource({
        transport: {
            read: {
                url: "storesale/orderList",
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
                    Title: { type: "string" },
                    TotalPrice: { type: "string" },
                    Owner: { type: "string" },
                    Address: { type: "string" },
                    CreatedOn: { type: "string" }
                }
            }
        }
    });

    var selectedItems;

    $("#orderList").kendoGrid({
        dataSource: dataSource,
        selectable: "single",
        pageable: {
            refresh: true
        },
        columns: [
            { field: "Title", title: "订单名称", template: "<a href=\"/StoreSale/Order/${Id}\" target=\"_blank\">${Title}</a>" },
            { field: "Owner", title: "收货人" },
            { field: "TotalPrice", title: "总价" },
            { field: "Address", title: "送货地址" },
            { field: "CreatedOn", title: "订单生产日期" }
        ],
        change: function() {
            selectedItems = new Array();
            var selects = this.select();
            for (var i = 0; i < selects.length; ++i) {
                selectedItems.push(this.dataItem(selects[i]).Id);
            }
        }
    });

    $("#deleteOrder").click(function() {
        $.ajax({
            type: "post",
            url: "StoreSale/Delete",
            data: addAntiForgeryToken({ ids: selectedItems }),
            dataType: "json",
            success: function() {
                $("#orderList").data("kendoGrid").dataSource.read();
            }
        });
    });


});