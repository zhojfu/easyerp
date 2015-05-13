/*客户信息*/
$(document).ready(function () {

    var dataSource = new kendo.data.DataSource({
        transport: {
            read: {
                url: "customer/customerList",
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
                    Name: { type: "string" },
                    Sex: { type: "string" },
                    IdNumber: { type: "string" },
                    Address: { type: "string" },
                    TelePhone: { type: "string" },
                    CreatedOn: { type: "string" }
                }
            }
        }
    });

    var selectedItems;

    $("#customerList").kendoGrid({
        dataSource: dataSource,
        selectable: "single",
        pageable: {
            refresh: true,
        },
        columns: [
            { field: "Name", title: "姓名", template: '<a href="/Customer/Edit/${Id}" target="_blank">${Name}</a>' },
            { field: "Sex", title: "性别" },
            { field: "TelePhone", title: "手机号" },
            { field: "IdNumber", title: "身份证号" },
            { field: "Address", title: "送货地址" },
            { field: "CreatedOn", title: "注册日" }
        ],
        change: function() {
            selectedItems = new Array();
            var selects = this.select();
            for (var i = 0; i < selects.length; ++i) {
                selectedItems.push(this.dataItem(selects[i]).Id);
            }
        }
    });

    /*删除员工*/
    $("#deleteCustomer").click(function () {
        $.ajax({
            type: "post",
            url: "/Customer/Delete",
            data: JSON.stringify({ ids: selectedItems }),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success : function() {
                $('#customerList').data('kendoGrid').dataSource.read();
            }
        });
    });
});