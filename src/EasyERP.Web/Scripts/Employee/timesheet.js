function Timesheet() {

    var currentDate = new Date();

    function generateColumns(selDay) {
        function getSelectedWeek() {
            var columns = [{ field: "EmployeeName", title: "姓名" }];
            var weeks = ["星期一", "星期二", "星期三", "星期四", "星期五", "星期六", "星期天"];
            var fields = ["Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun"];
            var firstDayOfWeek = selDay.getDate() - (selDay.getDay() + 7 - 1) % 7;
            for (var i = 0; i < 7; ++i) {
                var tempDate = new Date(selDay);
                tempDate.setDate((firstDayOfWeek + i));
                columns.push({ field: fields[i], title: (weeks[i] + "(" + tempDate.toLocaleDateString()) + ")" });
            }
            columns.push({ command: "edit" });
            return columns;
        }

        return getSelectedWeek(selDay);
    }

    var dataSource = new kendo.data.DataSource({
        transport: {
            read: {
                url: "Timesheet/GetTimeSheetByDate",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                data: {
                    date: currentDate.toLocaleDateString()
                }
            },
            update: {
                url: "Timesheet/UpdateTimesheet",
                dataType: "json",
                type: "post",
               
                parameterMap: function(options, operation) {
                    if (operation !== "read" && options.models) {
                        options.models.date = currentDate;
                        return { models: kendo.stringify(options.models) };
                    }
                    return options.models;
                }
            },
            batch: true,
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
                id: "Id",
                fields: {
                    Id: { type: "string", editable: false },
                    DateOfWeek: { type: "string", editable: false, },
                    EmployeeName: { type: "string", editable: false },
                    Mon: { type: "number", validation: { min: 0, max: 24 } },
                    Tue: { type: "number", validation: { min: 0, max: 24 } },
                    Wed: { type: "number", validation: { min: 0, max: 24 } },
                    Thu: { type: "number", validation: { min: 0, max: 24 } },
                    Fri: { type: "number", validation: { min: 0, max: 24 } },
                    Sat: { type: "number", validation: { min: 0, max: 24 } },
                    Sun: { type: "number", validation: { min: 0, max: 24 } }
                }, 
            }
        }
    });

    function initialKendoGrid() {
        $("#timesheet").empty();
        $("#timesheet").kendoGrid({
            dataSource: dataSource,
            height: 400,
            selectable: "single",
            resizable: true,
            pageable: {
                refresh: true,
            },
            columns: generateColumns(currentDate),
            editable: "inline"
    });
    }

    this.setSelectedDate = function (date) {
        currentDate.setDate(date.getDate());
    };

    this.moveNextWeek = function () {
        currentDate.setDate(currentDate.getDate() - 7);
        initialKendoGrid();
    };

    this.movePrevWeek = function () {
        currentDate.setDate(currentDate.getDate() + 7);
        initialKendoGrid();
    };

    this.InitialCurrentWeek = function () {
        initialKendoGrid();
    };
}

$(document).ready(function () {
    var timesheet = new Timesheet();
    timesheet.InitialCurrentWeek();

    $("#preWeek").click(function () {
        timesheet.movePrevWeek();
    });

    $("#nextWeek").click(function () {
        timesheet.moveNextWeek();
    });
});


