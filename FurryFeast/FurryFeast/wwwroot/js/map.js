let map;
let MapData = [];
let MapCenter;
let ClickMarkerDetail;

// //程式進入點
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

        .done(getPlaceData)
        .fail(function () {
            console.log("errorrrrrrr");
        });
})

//dropdown getCityName
function getCityName(data) {
    MapData = data;
    $.each(MapData, function (i, item) {
        $('#ChooseCity').append($('<option>', {
            value: item.cityID,
            text: item.city
        }));
    });

    //地圖移動到所選城市
    // map.setZoom(9);
    // map.panTo();

    // 下拉美化
    DropDownStyle('#ChooseCity');
    DropDownStyle('#ChooseCounty');

}


//dropdown ChooseCity
$("#ChooseCity").change(function (event) {
    DropDownStyle("#ChooseCounty");

    let option_value = event.target.value;
    // 清除選項
    $('#ChooseCounty > :not(:first-child)').remove();

    for (let i = 0; i < MapData.length; i++) {
        if (MapData[i].cityID == option_value) {
            for (let j = 0; j < MapData[i].county.length; j++) {
                $("#ChooseCounty").append($('<option>', {
                    value: MapData[i].PostCode[j],
                    text: MapData[i].county[j]
                }))
                MapCenter = new google.maps.LatLng(MapData[i].cityLng, MapData[i].cityLat);
            }
        }
    }
    console.log("選擇了" + option_value);

    checkDropdown("#ChooseCounty");
    DropDownStyle("#ChooseCounty");

    //地圖移動到所選城市中心
    map.setZoom(9);
    map.panTo(MapCenter);

    deleteMarkers();
    Reset();
    $(".card-new #pp").remove();
});

// 取得所選鄉鎮的郵遞區號
// 對照後取得該鄉鎮的寵物友善地點
let area = [];
$("#ChooseCounty").change(function (e) {
    area = [];
    let Value = e.target.value;
    for (let i = 0; i < PlaceData.length; i++) {
        if (Value == PlaceData[i].postCode) {

            area.push(PlaceData[i]);
        }
    }
    console.log("選擇地區的郵遞區號是" + Value);
    PlaceLatLng();
    drop();
    Reset();
    $(".card-new #pp").remove();

    if (area.length == 1) {
        placeID = area[0].place_id;
        MarkerDetail();
        initStreetView();
        map.setZoom(12);
        $(".card-new").prepend($('<p>', { id: "pp", style: "font-size:12px; letter-spacing: 3px; color: gray;", text: `共${area.length}筆資料` }));

    }
    else
    {
        GetPlaceList();
        $(".card-new").prepend($('<p>', { id: "pp", style: "font-size:12px; letter-spacing: 3px; color: gray;", text: `共${area.length}筆資料` }));

    }

    if (area.length == 0) {
        map.setZoom(9);
    }
});

// 地點清單
function GetPlaceList() {
    Reset();
    for (let i = 0; i < area.length; i++) {
        let listItem = $('<li>', { text: area[i].name, id: area[i].place_id });
        $("#PlaceList > ul").append(listItem);
    }

    console.log(map.zoom);

    if (map.zoom == 16) {
        console.log('map.zoom='+map.zoom);
        map.setZoom(12);
        map.panTo(MapCenter);
    }
    if (map.zoom == 9) {
        map.setZoom(12);

    }

    //MapCenter = { lat: ClickMarkerDetail.Lat, lng: ClickMarkerDetail.Lng }
    //map.panTo(MapCenter);
}

//點擊list
window.addEventListener('click', (e) => {
    for (let i = 0; i < area.length; i++) {
        if (e.target.id == area[i].place_id) {
            ClickMarkerDetail = {
                Name: area[i].name,
                PhoneNumber: area[i].international_phone_number,
                Rating: area[i].rating,
                User_ratings_total: area[i].user_ratings_total,
                Address: area[i].formatted_address,
                Lat: area[i].lat,
                Lng: area[i].lng,
                Website: area[i].website,
                Description: area[i].description,
                OpenHours: area[i].opening_hours_weekday,
                Type: area[i].icon
            }
            MapCenter = { lat:ClickMarkerDetail.Lat, lng:ClickMarkerDetail.Lng}

            Reset();
            initStreetView();
            OutputInfo();

            map.setZoom(16);
            map.panTo(MapCenter);
        }
    }
});


//取得寵物友善地點清單
function getPlaceData(data) {
    console.log('AreaPlace_got_it!');
    PlaceData = data;
}

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
            PlaceID: area[i].place_id
        };
        AreaLatLng.push([LatLng, PlaceID]);
    }
}

let markers = [];
let marker;

// Marker animation
function drop() {
    deleteMarkers();
    for (let i = 0; i < AreaLatLng.length; i++) {
        CreateMarkers(AreaLatLng[i], i * 200);
    }
    if (AreaLatLng.length !== 0) {
        /*map.setZoom(13);*/
        map.setCenter(MapCenter);
    }
}

// CreateMarkers Method
function CreateMarkers() {
    AreaLatLng.forEach((AreaLatLng, timeout) => {
        marker = new google.maps.Marker({
            place_id: AreaLatLng[1],
            position: AreaLatLng[0],
            map,
            animation: google.maps.Animation.DROP
        }, timeout);

        MapCenter = marker.position;

        //marker的監聽事件
        marker.addListener("click", (e) => {
            Reset();
            if (!$(".card-new #pp")) {
                $(".card-new #pp").remove();
            }
            placeID = AreaLatLng[1].PlaceID;
            MarkerDetail();
            initStreetView();
            MapCenter = { lat: ClickMarkerDetail.Lat, lng: ClickMarkerDetail.Lng }

            map.setZoom(16);
            map.panTo(MapCenter);
        });

        markers.push(marker);
    });
}


// Delete Markers
function deleteMarkers() {
    for (let i = 0; i < markers.length; i++) {
        markers[i].setMap(null);
    }
    markers = [];
}

// 取得店家資訊
let request;

function MarkerDetail() {
    request = {
        place_id: placeID,
        fields: ["name", "formatted_address", "place_id"]
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
                Lng: area[i].lng,
                Website: area[i].website,
                Description: area[i].description,
                OpenHours: area[i].opening_hours_weekday,
                Type: area[i].icon
            }
        }
    }
    OutputInfo();
}

// 重置
function Reset() {
    $('#PlaceName.mt-2').empty();
    $("#infoHeader >.type_bar").remove();
    $("#RatingBox").empty();
    $("#InfoBoxBody >.col-9").empty();
    $("#PlaceList >ul").empty();
    /*$(".card-new #pp").remove();*/
    $(".card-new .BtnReturn").remove();


    if (!$('#btn_StreetView').hasClass("visually-hidden")) {
        $('#btn_StreetView').addClass("visually-hidden");
    }

    if (panorama) {
        panorama.setVisible(false);
    }

    if (!$("#InfoBoxBody").hasClass("visually-hidden")) {
        $("#InfoBoxBody").addClass("visually-hidden");
    }
}

// 輸出商家細節
function OutputInfo() {
    let StarWidth = ClickMarkerDetail.Rating * 20;

    Reset();

    $("#PlaceName.mt-2").append('<h4 class="py-2 px-4" style="color:white";>' + ClickMarkerDetail.Name + '</h4>');

    if (ClickMarkerDetail.Description) {
        $("#PlaceName.mt-2").append('<p style="font-size:12x; color:black; margin: 0px; padding: 0px 15px 15px 15px">' + ClickMarkerDetail.Description + '</p>');
    }
    $("#infoHeader").append(`<p class="type_bar mt-2">${ClickMarkerDetail.Type}</p>`);

    let strHTML =
        `<div class="col-5 p-0 fw-bold" style="text-align: right; display: inline-block;font-size:40px; padding-left: 30px;">${ClickMarkerDetail.Rating}</div>
        <div class="col" style="text-align: left">
        <div class="ratings">
        <div class="empty_star">★★★★★</div>
        <div class="full_star" style="width:${StarWidth}%">★★★★★</div></div>
        <div class="m-0" style="font-size:12px; color:#adadad;">${ClickMarkerDetail.User_ratings_total}人評分</div></div>`

    $("#RatingBox").append(strHTML);


    $("#InfoBoxBody >.col-9").append('<p class="fs-6 "><a href="tel:' + ClickMarkerDetail.PhoneNumber + '">'+ ClickMarkerDetail.PhoneNumber + '</a></p>');
    $("#InfoBoxBody >.col-9").append('<p class ="fs-6 " >' + ClickMarkerDetail.Address + "</p>");


    if (ClickMarkerDetail.OpenHours) {
        $("#InfoBoxBody >.col-9").append('<p class ="fs-6 " >' + ClickMarkerDetail.OpenHours + "</p>");
    } else {
        $("#InfoBoxBody >.col-9").append('<p class ="fs-6" style="color:gray">尚未提供</p>');
    }

    if (ClickMarkerDetail.Website) {
        $("#InfoBoxBody >.col-9").append($('<a>', { id: "btnWebsite", href: ClickMarkerDetail.Website, target: "_blank" }));
        $("a#btnWebsite").append($("<button>", { class: "type_bar mt-2", text: "點我前往官網" }));
    } 

    if (!$("#InfoBoxBody").hasClass("visually-hidden")) {
        $("#InfoBoxBody").addClass("visually-hidden");
    } else {
        $("#InfoBoxBody").removeClass("visually-hidden");
    }

    $('#btn_StreetView').removeClass("visually-hidden");

    if ($("#BtnReturn") && area.length>1) {
        $(".card-new").append($('<button>').text("Return").addClass("BtnReturn").on('click', GetPlaceList));
    }
} 


// 重置下拉選單樣式
function checkDropdown(target) {
    if ($(target).parent().hasClass('select')) {
        $(target).unwrap();
        $(target).siblings().remove();
    }
}

//美化 dropdown
function DropDownStyle(target) {

    var $this = $(target),
        numberOfOptions = $(target).children('option').length;

    $this.addClass('select-hidden');
    $this.wrap('<div class="select"></div>');
    $this.after('<div class="select-styled"></div>');

    var $styledSelect = $this.next('div.select-styled');
    $styledSelect.text($this.children('option').eq(0).text());

    var $list = $('<ul />', {
        'class': 'select-options'
    }).
        insertAfter($styledSelect);

    for (var i = 0; i < numberOfOptions; i++) {
        $('<li/>', {
            text: $this.children('option').eq(i).text(),
            rel: $this.children('option').eq(i).val()
        }).
            appendTo($list);
    }

    var $listItems = $list.children('li');

    $styledSelect.click(function (e) {
        e.stopPropagation();
        $('div.select-styled.active').not(this).each(function () {
            $(this).removeClass('active').next('ul.select-options').hide();
        });
        $(this).toggleClass('active').next('ul.select-options').toggle();
    });

    $listItems.click(function (e) {
        e.stopPropagation();
        $styledSelect.text($(this).text()).removeClass('active');
        $this.val($(this).attr('rel'));
        $this.change();
        $list.hide();
        //console.log($this.val());
    });

    $(document).click(function () {
        $styledSelect.removeClass('active');
        $list.hide();
    });
}

// Btn_StreetView
document.getElementById("btn_StreetView").addEventListener("click", view);

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

let panorama;

function initStreetView() {
    panorama = new google.maps.StreetViewPanorama(
        document.getElementById('map'), {
        position: {
            lat: ClickMarkerDetail.Lat,
            lng: ClickMarkerDetail.Lng
        },
        pov: {
            heading: 165,
            pitch: 0
        },
        visible: false
    });
}