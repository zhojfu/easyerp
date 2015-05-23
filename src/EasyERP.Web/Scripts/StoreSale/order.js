function OrderGrid() {
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
                    ProductId: { type: "number", validation: { require: true } },
                    Name: { type: "string", validation: { required: true } },
                    Quantity: { type: "number", validation: { required: true, min: 1 } },
                    TotalPrice: { type: "number", validation: { required: true, min: 1 } },
                    PriceOfUnit: { type: "number", validation: { required: true, min: 1 } }
                }
            }
        }
    });

    this.initiOrderGrid = function() {
        function productAutoCompleteEditor(container, options) {
            $("<input data-text-field='Name' data-value-field='Name' data-bind='value:" + options.field + "'/>")
                .appendTo(container)
                .kendoAutoComplete({
                    autoBind: false,
                    placeholder: "输入产品名称",
                    filter: "contains",
                    select: function(e) {
                        var dataItem = this.dataItem(e.item.index());
                        var grid = $("#order").data("kendoGrid");

                        var dataSource = grid.dataSource;
                        var selectedRowIndex = grid.select().index();
                        var gridRow = null;
                        if (selectedRowIndex === -1) {
                            gridRow = dataSource.data()[0];
                        } else {
                            gridRow = dataSource.data()[selectedRowIndex];
                        }
                        gridRow.set("ProductId", dataItem.Id);
                        gridRow.set("PriceOfUnit", dataItem.Price);
                        gridRow.set("TotalPrice", dataItem.Price * gridRow.get("Quantity"));

                        console.log(gridRow);
                    },
                    dataSource: {
                        serverFiltering: true,
                        transport: {
                            read: "/StoreSale/AutoCompleteProducts",
                            dataType: "json",
                            parameterMap: function(data, action) {
                                if (action === "read") {
                                    return {
                                        name: data.filter.filters[0].value
                                    };
                                } else {
                                    return data;
                                }
                            }
                        },
                        schema: {
                            model: {
                                fields: {
                                    Id: { type: "number" },
                                    Name: { type: "string" },
                                    Price: { type: "number" }
                                }
                            }
                        }
                    }
                });
        }

        function dataSourceChange() {

            var orderItems = $("#order").data("kendoGrid").dataSource.data();
            var totalCost = 0;
            for (var i = 0; i < orderItems.length; i++) {
                totalCost += orderItems[i].TotalPrice;
            }

            $("#totalcost").val(totalCost);
        }

        dataSource.bind("change", dataSourceChange);

        $("#order").kendoGrid({
            dataSource: dataSource,
            selectable: "single",
            pageable: {
                refresh: true
            },
            columns: [
                { field: "Name", title: "产品", editor: productAutoCompleteEditor },
                { field: "PriceOfUnit", title: "单价", editor: function(container, options) { $("#order").data("kendoGrid").closeCell(); } },
                { field: "Quantity", title: "数量" },
                { field: "TotalPrice", title: "总价", editor: function(container, options) { $("#order").data("kendoGrid").closeCell(); } },
                { command: ["destroy"] }
            ],
            editable: true,
            save: function(data) {
                if (data.values.Quantity) {
                    data.model.set("TotalPrice", data.values.Quantity * data.model.PriceOfUnit);
                }
            },
            toolbar: ["create"]
        });
    };
}

function CustomerAutoComplete() {
    var dataSource = new kendo.data.DataSource({
        serverFiltering: true,

        transport: {
            read: "/StoreSale/AutoCompleteCustomers",

            parameterMap: function(data, action) {
                if (action === "read") {
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

    this.initalCustomerAutoComplete = function() {

        function onSelect(e) {
            var dataItem = this.dataItem(e.item.index());
            $("#customerId").val(dataItem.Id);
            $("#address").val(dataItem.Address);
        }

        $("#orderOwner").kendoAutoComplete({
            dataTextField: "Name",
            filter: "contains",
            select: onSelect,
            dataSource: dataSource
        });

    };
}


$(document).ready(function() {
    var grid = new OrderGrid();
    grid.initiOrderGrid();

    var cAutoComplete = new CustomerAutoComplete();
    cAutoComplete.initalCustomerAutoComplete();

    $("#submitOrder").click(function() {

        var order = new Object();
        order.customerId = $("#customerId").val();
        order.orderTitle = $("#orderTitle").val();
        order.orderItems = new Array();
        var orderItems = $("#order").data("kendoGrid").dataSource.data();

        for (var i = 0; i < orderItems.length; i++) {
            var item = new Object();
            item.Name = orderItems[i].Name;
            item.PriceOfUnit = orderItems[i].PriceOfUnit;
            item.Quantity = orderItems[i].Quantity;
            item.ProductId = orderItems[i].ProductId;
            order.orderItems.push(item);
        }

        $.ajax({
            type: "post",
            url: "/StoreSale/Create",
            data: JSON.stringify(order),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function() {
                //$('#customerList').data('kendoGrid').dataSource.read();
            }
        });

    });

});