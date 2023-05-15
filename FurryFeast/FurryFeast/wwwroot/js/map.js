let MapData = [];
let selected = document.getElementById("ChooseCity");
let selected_County = document.getElementById("ChooseCounty");

//�{���i�J�I
$(function () {
    $.ajax({
        url: "https://localhost:7110/TaiwanArea.json",
            type: "GET",
            dataType: "json"
        })
        .done(getCityName)
        .fail(function () {
            console.log("error");
        });

    function getCityName(Data) {
        console.log("success");
        MapData = Data;
        for (let i = 0; i < Data.length; i++) {
            let Option_City = Data[i].city;
            let MapOption = document.createElement('option') // �إ�option����
            MapOption.setAttribute("value", Data[i].cityID);
            MapOption.text = Option_City;
            $(MapOption).appendTo(selected);
        }
    }
})

$("#ChooseCity").change(function (event) {
    $('#ChooseCounty > :not(:first-child)').remove();
    //�M���ﶵ
    let option_value = event.target.value;
    for (let i = 0; i < MapData.length; i++) {
        if (MapData[i].cityID == option_value) {
            for (let j = 0; j < MapData[i].county.length; j++) {
                let countyName = MapData[i].county[j];
                let MapOption = document.createElement('option')
                MapOption.text = countyName;
                $(MapOption).appendTo(selected_County);
            }
        }
    }
});