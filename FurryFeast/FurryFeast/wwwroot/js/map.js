let MapData = [];
let selected = document.getElementById("ChooseCity");
let selected_County = document.getElementById("ChooseCounty");

//程式進入點
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
            let MapOption = document.createElement('option') // 建立option元素
            MapOption.setAttribute("value", Data[i].cityID);
            MapOption.text = Option_City;
            $(MapOption).appendTo(selected);
        }
    }
})

$("#ChooseCity").change(function (event) {
    $('#ChooseCounty > :not(:first-child)').remove();
    //清除選項
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