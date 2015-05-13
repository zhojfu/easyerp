function OrderGrid() {
    // var baseUrl = "";
    var dataSource = new kendo.data.DataSource({
        transport: {
            read: {
                //url: baseUrl + "/Consumption/Get",
                dataType: "json"
            },
            update: {
                url: "/StoreSale/UpdateOrderItem",
                type: "post",
                dataType: "json"
            },
            destroy: {
                url: "/StoreSale/DeleteOrderItem",
                type: "post",
                dataType: "json"
            },
            create: {
                url: "/StoreSale/AddOrderItem",
                type: "post",
                dataType: "json"
            },
            schema: {
                data: "data",
                total: "total"
            }
        },
        //serverPaging: true,
        pageSize: 10,
        schema: {
            data: "data",
            total: "total",
            model: {
                id: "Id",
                fields: {
                    Id: { editable: false, nullable: true },
                    Name: { type: "string", validation: { required: true } },
                    Quantity: { type: "number", validation: { required: true, min: 1 } },
                    PriceOfUnit: { type: "number", validation: { required: true, min: 1 } }
                }
            }
        }
    });

    this.initiOrderGrid = function () {
        $("#order").kendoGrid({
            dataSource: dataSource,
            pageable: {
                refresh: true,
            },
            columns: [
                { field: "Name", title: "产品" },
                { field: "PriceOfUnit", title: "单价" },
                { field: "Quantity", title: "数量" },
                { command: ["edit", "destroy"] }
            ],
            editable: "popup",
            toolbar: ["create"]
        });
    };
}


$(document).ready(function() {
    //create order
    var grid = new OrderGrid();
    grid.initiOrderGrid();
    
    $("#submitOrder").click(function () {

       // console.log(orderItems);

        var order = new Object();
        order.owner = $("#orderOwner").val();
        order.address = $("#address").val();
        order.orderTitle = $("#orderTitle").val();
        order.orderItems = new Array();
        var orderItems = $("#order").data("kendoGrid").dataSource.data();

        for (var i = 0; i < orderItems.length; i++) {
            var item = new Object();
            item.Name = orderItems[i].Name;
            item.PriceOfUnit = orderItems[i].PriceOfUnit;
            item.Quantity = orderItems[i].Quantity;
            order.orderItems.push(item);
        }

        console.log(order);

    });

});