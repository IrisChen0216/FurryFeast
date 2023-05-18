// google map

let map;

let MapData = [];
let selected = document.getElementById("ChooseCity");
let selected_County = document.getElementById("ChooseCounty");

//�{���i�J�I
$(function () {
    // �x�W��F��
    $.ajax({
        url: "https://localhost:7110/New_TaiwanAreaList.json",
        type: "GET",
        dataType: "json"
    })
        .done(getCityName)
        .fail(function () {
            console.log("error");
        });

    // �x�W�͵��a�I
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

//���o�����W��
function getCityName(Data) {
    MapData = Data;
    for (let i = 0; i < Data.length; i++) {
        let Option_City = Data[i].city;
        let MapOption = document.createElement('option') // �إ�option����
        MapOption.setAttribute("value", Data[i].cityID);
        MapOption.text = Option_City;
        $(MapOption).appendTo(selected);
    }
}

let MapCenter;

//�H�۫����W�ٿ�ܧ��� �ܤƹ������m��
$("#ChooseCity").change(function (event) {
    $('#ChooseCounty > :not(:first-child)').remove();
    //�M���ﶵ
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
    //�a�ϲ��ʨ�ҿ﫰��
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

// ���o�d���͵��a�I�M��
let PlaceData;
function getAreaPlace(data) {
    console.log('AreaPlace_got_it!');
    PlaceData = data;
}

// ���o�ҿ�m���l���ϸ�
// ��ӫ���o�Ӷm���d���͵��a�I
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

//�����d���͵��a�I���g�n��
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

// �j�M���s
// �I���إ�markers
document.getElementById("search").addEventListener("click", drop);

let markers = [];
let marker;

//marker�����ʵe
function drop() {
    deleteMarkers();
    for (let i = 0; i < AreaLatLng.length; i++) {
        CreateMarkers(AreaLatLng[i], i * 200);
    }
    // map.setCenter(markers[1].getPosition());
}


let placeID;

// �إ߷sMarker
function CreateMarkers() {
    AreaLatLng.forEach((AreaLatLng, timeout) => {
        marker = new google.maps.Marker({
            place_id: AreaLatLng[1],
            position: AreaLatLng[0],
            map,
            animation: google.maps.Animation.DROP
        }, timeout);

        //marker����ť�ƥ�
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

// ��X�Ӯa�Ӹ`
function OutputInfo() {
    let StarNum = ClickMarkerDetail.Rating * 20;

    $(".index-header.m-0").empty();
    $("#InfoBox > :first").empty();
    $("#InfoBoxBody").empty();
    let RatingBox = $("<div></div>").addClass("text-center");
    $(RatingBox).appendTo("#InfoBox");

    let strHTML = `<p class="px-2 py-0 m-0 fs-1 fw-bold">${ClickMarkerDetail.Rating}</p>
                    <div class="ratings">
                        <div class="empty_star">����������</div>
                        <div class="full_star" style="width:${StarNum}%">����������</div>
                        <p style="font-size:12px; color:#adadad;">${ClickMarkerDetail.User_ratings_total}�H����</p>
                    </div>`;

        $("#InfoBox > :first-child").append(strHTML);

    $(".index-header.m-0").append('<h4 class="index-header py-4">' + ClickMarkerDetail.Name + '</h4>');
    $("#InfoBoxBody").append('<div class="fs-6"><i class="bi bi-telephone-fill"></i><a href="tel:' + ClickMarkerDetail.PhoneNumber + '">' + ClickMarkerDetail.PhoneNumber + '</a></div>');
    $("#InfoBoxBody").append('<div class ="fs-6" ><i class="bi bi-house-fill"></i>' + ClickMarkerDetail.Address + "</div>");
}


//�R���аO
function deleteMarkers() {
    for (let i = 0; i < markers.length; i++) {
        markers[i].setMap(null);
    }
    markers = [];
}

// ��l�ƨå[�J�a��
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

//��l�Ƶ󴺦a��
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