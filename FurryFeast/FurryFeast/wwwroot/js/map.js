let MapData = [];
let selected = document.getElementById("ChooseCity");
let selected_County = document.getElementById("ChooseCounty");

//程式進入點
$(function () {
    // 台灣行政區
    $.ajax({
        url: "https://localhost:7110/New_TaiwanAreaList.json",
        type: "GET",
        dataType: "json"
    })
        .done(getCityName)
        .fail(function () {
            console.log("error");
        });

    // 台灣友善地點
    $.ajax({
        url: "https://localhost:7110/pet_friendly_places_new.json",
        type: "GET",
        dataType: "json"
    })
        .done(getAreaPlace)
        .fail(function () {
            console.log("errorrrrrrr");
        });


})

//取得城市名稱
function getCityName(Data) {
    MapData = Data;
    for (let i = 0; i < Data.length; i++) {
        let Option_City = Data[i].city;
        let MapOption = document.createElement('option') // 建立option元素
        MapOption.setAttribute("value", Data[i].cityID);
        MapOption.text = Option_City;
        $(MapOption).appendTo(selected);
    }
}

//隨著城市名稱選擇改變 變化對應的鄉鎮
$("#ChooseCity").change(function (event) {
    $('#ChooseCounty > :not(:first-child)').remove();
    //清除選項
    let option_value = event.target.value;
    for (let i = 0; i < MapData.length; i++) {
        if (MapData[i].cityID == option_value) {
            for (let j = 0; j < MapData[i].county.length; j++) {
                let countyName = MapData[i].county[j];
                let MapOption = document.createElement('option')
                MapOption.setAttribute("value", MapData[i].PostCode[j])
                MapOption.text = countyName;
                $(MapOption).appendTo(selected_County);
            }
        }
    }
});

// 取得寵物友善地點清單
function getAreaPlace(data) {
    console.log('AreaPlace_got_it!')
    PlaceData = data;
}

// 取得所選鄉鎮的郵遞區號
// 對照後取得該鄉鎮的寵物友善地點
let area = [];
$("#ChooseCounty").change(function (e) {
    area = [];
    let PostCode = e.target.value;
    for (let i = 0; i < PlaceData.length; i++) {
        if (PostCode == PlaceData[i].postCode) {
            area.push(PlaceData[i]);
        }
    }
    PlaceLatLng();
});

//紀錄寵物友善地點的經緯度
let AreaLatLng = [];

function PlaceLatLng() {
    AreaLatLng = [];
    for (let i = 0; i < area.length; i++) {
        let lat = area[i].lat;
        let lng = area[i].lon;
        let LatLng = {
            lat: parseFloat(lat),
            lng: parseFloat(lng)
        };
        AreaLatLng.push(LatLng);
    }
}

// 搜尋按鈕
// 點擊建立markers
document.getElementById("search").addEventListener("click", drop);

//marker掉落動畫
function drop() {
    clearMarkers();
    for (let i = 0; i < AreaLatLng.length; i++) {
        CreateMarkers(AreaLatLng[i], i * 200);
    }
}

let markers = [];
let marker;

// 建立新Marker
function CreateMarkers() {
    AreaLatLng.forEach((position, timeout) => {
        marker = new google.maps.Marker({
            position,
            map,
            animation: google.maps.Animation.DROP
        }, timeout);
        //mark的監聽事件
        marker.addListener("click", () => {
            console.log('YOYOOYO')
            //點擊在右方出現店家資訊
        });
    });

    markers.push(marker);
}

//清除標記
function clearMarkers() {
    for (let i = 0; i < markers.length; i++) {
        markers[i].setMap(null);
    }
    markers = [];
}

// google map

let map;

// 初始化並加入地圖
function initMap() {
    const Taipei = {
        lat: 25.03746,
        lng: 121.564558
    };

    // The map
    map = new google.maps.Map(document.getElementById("map"), {
        zoom: 7,
        center: Taipei,
    });
}


//marker 回到中心 點擊放大
// map.addListener("center_changed", () => {
//     // 3 seconds after the center of the map has changed, pan back to the
//     // marker.
//     window.setTimeout(() => {
//         map.panTo(marker.getPosition());
//     }, 2000);
// });
// marker.addListener("click", () => {
//     map.setZoom(14);
//     map.setCenter(marker.getPosition());
// });

window.initMap = initMap;