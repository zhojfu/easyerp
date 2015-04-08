$(document).ready(function () {
   
    var dataSource = new kendo.data.DataSource({
        transport: {
            read: {
                //url: "employee/employeeList",
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

    function generateColumns(selDay) {
        function getSelectedWeek() {
            var columns = [{field: "EmployeeName", title: "姓名"}];
            var weeks = ["星期一", "星期二", "星期三", "星期四", "星期五", "星期六", "星期天"];
            var fields = ["Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun"];
            console.log(selDay);
            var firstDayOfWeek = selDay.getDate() - (selDay.getDay() + 7 - 1) % 7;
            for (var i = 0; i < 7; ++i) {
                //console.log(firstDayOfWeek);
                var tempDate = new Date(selDay);
                tempDate.setDate((firstDayOfWeek + i));
                columns.push({ field: fields[i], title: (weeks[i] + "(" + tempDate.toLocaleDateString()) + ")" });
            }
            return columns;
        }

        return getSelectedWeek(selDay);
    }

    var CurrentDate = new Date();

    initialKendoGrid();

    function initialKendoGrid() {
        $("#timesheet").kendoGrid({
            dataSource: dataSource,
            height: 400,
            selectable: "multiple",
            resizable: true,
            pageable: {
                refresh: true,
            },
            columns: generateColumns(CurrentDate)
        });
    }

    $("#preWeek").click(function() {
        CurrentDate.setDate(CurrentDate.getDate() - 7);
        $("#timesheet").empty();
        initialKendoGrid();
    });

    $("#nextWeek").click(function() {
        CurrentDate.setDate(CurrentDate.getDate() + 7);
        $("#timesheet").empty();
        initialKendoGrid();
    });

});