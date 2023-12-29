(function ($) {
    $.fn.tableColumn = function (options) {
        var defaults = { columns: 1 };
        var settings = $.extend({}, defaults, options);
        var tables = this;
        return tables.each(function (tableIndex, table) {
            var test = settings;
            var rows = $(table).find('tbody > tr');
            var positionOfMainTable = $(table).find('tbody tr td:first-child').offset();
            var tableColumn = $('<table id="table_column" class="alter" style="position: absolute; display: none;">');
            $('body').append(tableColumn);
            tableColumn.css('top', positionOfMainTable.top);
            tableColumn.css('left', positionOfMainTable.left);
            $.each(rows, function (rowIndex, rowRunner) {
                var originalDayCells = $(rowRunner).find('td');
                var newRow = $('<tr>');
                var rowCells = $(rowRunner).children();
                for (var cellIndex = 0; cellIndex < test.columns; cellIndex++) {
                    var clonedDayCell = $(rowCells[cellIndex]).clone();
                    clonedDayCell.css('height', $(rowCells[0]).height());
                    clonedDayCell.css('width', $(rowCells[cellIndex]).width());
                    clonedDayCell.addClass('table_cell');
                    newRow.append(clonedDayCell);
                }
                tableColumn.append(newRow);
            });

            $(document).scroll(function () {
                var scrollposition = $(window).scrollLeft();

                if (scrollposition > 50) {
                    var positionOfMainTable = $(table).find('tbody tr td:first-child').offset();
                    tableColumn.css('top', positionOfMainTable.top);
                    tableColumn.css('left', scrollposition);

                    tableColumn.show();
                }
                else {
                    tableColumn.hide();
                }
            });
        });
    };
} (jQuery));