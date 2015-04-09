function Timesheet() {

    var currentDate = new Date();

    function generateColumns(selDay) {
        function getSelectedWeek() {
            var columns = [{ field: "EmployeeName", title: "姓名" }];
            var weeks = ["星期一", "星期二", "星期三", "星期四", "星期五", "星期六", "星期天"];
            var fields = ["Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun"];
            console.log(selDay);
            var firstDayOfWeek = selDay.getDate() - (selDay.getDay() + 7 - 1) % 7;
            for (var i = 0; i < 7; ++i) {
                var tempDate = new Date(selDay);
                tempDate.setDate((firstDayOfWeek + i));
                columns.push({ field: fields[i], title: (weeks[i] + "(" + tempDate.toLocaleDateString()) + ")" });
            }
            return columns;
        }

        return getSelectedWeek(selDay);
    }

    var dataSource = new kendo.data.DataSource({
        transport: {
            read: {
                url: "timesheet",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                data: {
                   date: currentDate.toLocaleDateString()
                }
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
                    EmployeeName: {type: "string"},
                    Mon: { type: "double" },
                    Tue: { type: "double" },
                    Wed: { type: "double" },
                    Thu: { type: "double" },
                    Fri: { type: "double" },
                    Sat: { type: "double" },
                    Sun: { type: "doubel" }
                }
            }
        }
    });

    function initialKendoGrid() {
        $("#timesheet").empty();
        $("#timesheet").kendoGrid({
            dataSource: dataSource,
            height: 400,
            selectable: "multiple",
            resizable: true,
            pageable: {
                refresh: true,
            },
            columns: generateColumns(currentDate)
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


