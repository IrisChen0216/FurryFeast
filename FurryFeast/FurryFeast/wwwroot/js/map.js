let Selection = document.getElementById('ChooseCity');

let n_option;

$.ajax({
        url: "https://localhost:7110/TaiwanArea.json",
        type: "POST",
        dataType: "json",
        success: function (data) {
            console.log([data]);
            for (let i = 0; i < data.length; i++) {
                n_option = document.createElement('option');
                n_option.value = data[i].city;
                Selection.appendChild(n_option);
            }

        },
    error: function() {
        console.log("Error");
    }
});

//$(document).ready(function() {)
//    $("#b01").click(function() {

//        htmlobj =
//            $.ajax({ url: "https://localhost:7110/TaiwanArea.json", async: false });

//        $("#myDiv").html(htmlobj.responseJSON[0].city);
//    });
//});


// let Selection = document.getElementById["ChooseCity"]

// for (let i = 0; i < MapData.length; i++) {
//     let Option_City = MapData[i].city;
//     let MapOption = document.createElement('option').value
//     MapOption = Option_City;
//     $(MapOption).appendTo(Selection);
// }