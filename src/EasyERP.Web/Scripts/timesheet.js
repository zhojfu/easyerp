function Timesheet(getUrl, editUrl) {

    var currentDate = new Date();

    var readUrl = getUrl;

    var updateUrl = editUrl;

    function generateColumns(selDay) {
        function getSelectedWeek() {
            var columns = [{ field: "Title", title: "名称" }];
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

    function generateDataSource(selectedDate) {

        return new kendo.data.DataSource({
            transport: {
                read: {
                    url: readUrl,
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    data: {
                        date: selectedDate.toLocaleDateString()
                    }
                },
                update: {
                    url: updateUrl,
                    dataType: "json",
                    type: "post",
                    data: function(option) {
                        return addAntiForgeryToken(option);
                    }
                },
                batch: true,
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
                        Id: { type: "string", editable: false },
                        DateOfWeek: { type: "string", editable: false},
                        Title: { type: "string", editable: false },
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

    }

    function moveNextWeek() {
        currentDate.setDate(currentDate.getDate() + 7);
        setupTimesheetGrid();
    };

    function movePrevWeek() {
        currentDate.setDate(currentDate.getDate() - 7);
        setupTimesheetGrid();
    };


    function setupTimesheetGrid() {
        $("#timesheet").empty();
        $("#timesheet").kendoGrid({
            dataSource: generateDataSource(currentDate),
            selectable: "single",
            resizable: true,
            pageable: {
                refresh: true,
            },
            columns: generateColumns(currentDate),
            editable: "inline"
        });
    }

    this.setSelectedDate = function(date) {
        currentDate.setDate(date.getDate());
    };

    this.InitialTimesheetGrid = function() {

        $("#timesheet").before("<a href=\"#\" id=\"preWeek\">上一周</a><a href=\"#\" id=\"nextWeek\">下一周</a>");

        $("#preWeek").bind("click", function() {
            movePrevWeek();
        });

        $("#nextWeek").bind("click", function() {
            moveNextWeek();
        });

        setupTimesheetGrid();
    };
}