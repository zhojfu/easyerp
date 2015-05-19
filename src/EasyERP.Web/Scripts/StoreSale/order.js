function OrderGrid() {
    // var baseUrl = "";
    var dataSource = new kendo.data.DataSource({
        transport: {
            read: {
                //url: baseUrl + "/Consumption/Get",
                //dataType: "json"
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
        serverPaging: true,
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
                refresh: true
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

function CustomerAutoComplete() {
    var dataSource = new kendo.data.DataSource({
        
        serverFiltering: true,

        transport: {
            read: "/StoreSale/AutoCompleteCustomers",
               
            parameterMap: function (data, action) {
                if(action === "read") {
                    return {
                        name: data.filter.filters[0].value
                    };
                } else {
                    return data;
                }
            },

            dataType: "json"
        },
        schema: {
            model: {
                fields: {
                    Id: { type: "number" },
                    Name: { type: "string" },
                    Address: { type: "string" }
               }
            }
        }
    });

    this.initalCustomerAutoComplete = function () {

        function onSelect(e) {
            var dataItem = this.dataItem(e.item.index());
            $("#customerId").val(dataItem.Id);
            $("#address").val(dataItem.Address);
        }

        $("#orderOwner").kendoAutoComplete({
            dataTextField: "Name",
            filter: "contains",
            minLength: 2,
            select: onSelect,
            dataSource: dataSource
        });
        
    }
}


$(document).ready(function() {
    //create order
    var grid = new OrderGrid();
    grid.initiOrderGrid();


    var cAutoComplete = new CustomerAutoComplete();
    cAutoComplete.initalCustomerAutoComplete();



    $("#submitOrder").click(function () {

       // console.log(orderItems);

        var order = new Object();
        //order.owner = $("#orderOwner").val();
        //order.address = $("#address").val();
        order.customerId = $("#customerId").val();
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

        $.ajax({
            type: "post",
            url: "/StoreSale/Create",
            data: JSON.stringify(order),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function () {
                //$('#customerList').data('kendoGrid').dataSource.read();
            }
        });

        console.log(order);

    });

});