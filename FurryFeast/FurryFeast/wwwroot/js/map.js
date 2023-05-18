// google map

let map;

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

let MapCenter;

//隨著城市名稱選擇改變 變化對應的鄉鎮
$("#ChooseCity").change(function (event) {
    $('#ChooseCounty > :not(:first-child)').remove();
    //清除選項
    let option_value = event.target.value;
    for (let i = 0; i < MapData.length; i++) {
        if (MapData[i].cityID == option_value) {
            for (let j = 0; j < MapData[i].county.length; j++) {
                let countyName = MapData[i].county[j];
                let MapOption = document.createElement('option');
                MapOption.setAttribute("value", MapData[i].PostCode[j]);
                MapOption.text = countyName;
                $(MapOption).appendTo(selected_County);

                MapCenter = new google.maps.LatLng(MapData[i].cityLng, MapData[i].cityLat);
            }
        }
    }
    //地圖移動到所選城市
    map.setZoom(9);
    map.panTo(MapCenter);

});

// map.addListener("center_changed", () => {
//     // 3 seconds after the center of the map has changed, pan back to the
//     // marker.
//     window.setTimeout(() => {
//         map.panTo(marker.getPosition());
//     }, 2000);
// });

// 取得寵物友善地點清單
let PlaceData;
function getAreaPlace(data) {
    console.log('AreaPlace_got_it!');
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
        let lng = area[i].lng;
        let LatLng = {
            lat: parseFloat(lat),
            lng: parseFloat(lng)
        };
        let PlaceID = {
            PlaceID: area[i].place_id,
        };
        AreaLatLng.push([LatLng, PlaceID]);

    }
}

// 搜尋按鈕
// 點擊建立markers
document.getElementById("search").addEventListener("click", drop);

let markers = [];
let marker;

//marker掉落動畫
function drop() {
    deleteMarkers();
    for (let i = 0; i < AreaLatLng.length; i++) {
        CreateMarkers(AreaLatLng[i], i * 200);
    }
    // map.setCenter(markers[1].getPosition());
}


let placeID;

// 建立新Marker
function CreateMarkers() {
    AreaLatLng.forEach((AreaLatLng, timeout) => {
        marker = new google.maps.Marker({
            place_id: AreaLatLng[1],
            position: AreaLatLng[0],
            map,
            animation: google.maps.Animation.DROP
        }, timeout);

        //marker的監聽事件
        marker.addListener("click", () => {
            placeID = AreaLatLng[1].PlaceID;
            MarkerDetail();
            $('#btn_StreetView').removeClass("visually-hidden");
            map.setZoom(12);
            map.setCenter(marker.position);
            initStreetView();
        });
        markers.push(marker);
    });
}


let request;
let ClickMarkerDetail;

function MarkerDetail() {
    request = {
        place_id: placeID,
        fields: ["name", "formatted_address", "place_id"],
    };
    for (let i = 0; i < area.length; i++) {
        if (request.place_id == area[i].place_id) {
            ClickMarkerDetail = {
                Name: area[i].name,
                PhoneNumber: area[i].international_phone_number,
                Rating: area[i].rating,
                User_ratings_total: area[i].user_ratings_total,
                Address: area[i].formatted_address,
                Lat: area[i].lat,
                Lng: area[i].lng
            };
        }
    }
    OutputInfo();
}

// 輸出商家細節
function OutputInfo() {
    let StarNum = ClickMarkerDetail.Rating * 20;

    $(".index-header.m-0").empty();
    $("#InfoBox > :first").empty();
    $("#InfoBoxBody").empty();
    let RatingBox = $("<div></div>").addClass("text-center");
    $(RatingBox).appendTo("#InfoBox");

    let strHTML = `<p class="px-2 py-0 m-0 fs-1 fw-bold">${ClickMarkerDetail.Rating}</p>
                    <div class="ratings">
                        <div class="empty_star">★★★★★</div>
                        <div class="full_star" style="width:${StarNum}%">★★★★★</div>
                        <p style="font-size:12px; color:#adadad;">${ClickMarkerDetail.User_ratings_total}人評分</p>
                    </div>`;

        $("#InfoBox > :first-child").append(strHTML);

    $(".index-header.m-0").append('<h4 class="index-header py-4">' + ClickMarkerDetail.Name + '</h4>');
    $("#InfoBoxBody").append('<div class="fs-6"><i class="bi bi-telephone-fill"></i><a href="tel:' + ClickMarkerDetail.PhoneNumber + '">' + ClickMarkerDetail.PhoneNumber + '</a></div>');
    $("#InfoBoxBody").append('<div class ="fs-6" ><i class="bi bi-house-fill"></i>' + ClickMarkerDetail.Address + "</div>");
}


//刪除標記
function deleteMarkers() {
    for (let i = 0; i < markers.length; i++) {
        markers[i].setMap(null);
    }
    markers = [];
}

// 初始化並加入地圖
function initMap() {
    const TaiwanCenter = {
        lat: 23.973875,
        lng: 120.982025
    };

    // The map
    map = new google.maps.Map(document.getElementById("map"), {
        zoom: 7,
        center: TaiwanCenter,
    });
}

let panorama;

function initStreetView() {
    panorama = new google.maps.StreetViewPanorama(
        document.getElementById('map'), {
        position: {
            lat: ClickMarkerDetail.Lat,
            lng: ClickMarkerDetail.Lng,
        },
        pov: {
            heading: 165,
            pitch: 0
        },
        visible: false
    });
}

//初始化街景地圖
function view() {

    //
    let toggle = panorama.getVisible();

    if (toggle == false) {
        panorama.setVisible(true);
    } else {
        panorama.setVisible(false);
    }
}
document.getElementById("btn_StreetView").addEventListener("click", view);