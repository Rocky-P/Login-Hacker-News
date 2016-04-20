$(function () {
    $("#Link-button").on('click', function () {
        $("#myModal-Link").show();
    });

    //$("#Submit").on('click', function () {
    //    $.post("/home/ShowPeople", function (person) {
    //        var row = $("<tr><td>" + person.Name +
    //            "</td><td>" + person.Age + "</td><td>" + "<button class='btn btn-primary btn-lg' data-delete='person.ID'>Delete</button>" + "<button class='btn btn-info btn-lg EditPerson' data-personid='person.ID' data-toggle='modal' data-target='#myModal-Edit'>Edit Person</button>" + "</td></tr>");
    //        $("table").append(row);
    //        clearModals();
    //    });
    //});
});
