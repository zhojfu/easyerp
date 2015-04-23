/*员工信息*/
$(document).ready(function() {

    var dataSource = new kendo.data.DataSource({
        transport: {
            read: {
                url: "employee/employeeList",
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
                    FullName: { type: "string" },
                    Sex: { type: "string" },
                    IdNumber: { type: "string" },
                    Address: { type: "string" },
                    CellPhone: { type: "string" },
                    Education: { type: "string" },
                    NativePlace: { type: "string" }
                }
            }
        }
    });

    var selectedItems;

    $("#employeesList").kendoGrid({
        dataSource: dataSource,
        height: 400,
        selectable: "multiple",
        pageable: {
            refresh: true,
        },
        columns: [
            { field: "FullName", title: "姓名", template: "<a href=\"/Employee/Edit/${Id}\" target=\"_blank\">${FullName}</a>" },
            { field: "Sex", title: "性别" },
            { field: "CellPhone", title: "手机号" },
            { field: "NativePlace", title: "籍贯" },
            { field: "IdNumber", title: "身份证号" },
            { field: "Address", title: "现住地址" },
            { field: "Department", title: "部门" },
            { field: "Education", title: "学历" }
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
    $("#deleteEmployee").click(function() {
        $.ajax({
            type: "post",
            url: "/Employee/Delete",
            data: JSON.stringify({ ids: selectedItems }),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function() {
                $("#employeesList").data("kendoGrid").dataSource.read();
            }
        });
    });

});