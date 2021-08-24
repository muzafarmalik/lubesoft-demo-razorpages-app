function customDatatable_init(table){
    $('#search-string').on('keyup', function () {
        table
            .search(this.value)
            .draw();
    });
}