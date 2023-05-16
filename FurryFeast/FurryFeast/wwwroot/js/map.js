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

//�H�۫����W�ٿ�ܧ��� �ܤƹ������m��
$("#ChooseCity").change(function (event) {
    $('#ChooseCounty > :not(:first-child)').remove();
    //�M���ﶵ
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

// ���o�d���͵��a�I�M��
function getAreaPlace(data) {
    console.log('AreaPlace_got_it!')
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
        let lng = area[i].lon;
        let LatLng = {
            lat: parseFloat(lat),
            lng: parseFloat(lng)
        };
        AreaLatLng.push(LatLng);
    }
}

// �j�M���s
// �I���إ�markers
document.getElementById("search").addEventListener("click", drop);

//marker�����ʵe
function drop() {
    clearMarkers();
    for (let i = 0; i < AreaLatLng.length; i++) {
        CreateMarkers(AreaLatLng[i], i * 200);
    }
}

let markers = [];
let marker;

// �إ߷sMarker
function CreateMarkers() {
    AreaLatLng.forEach((position, timeout) => {
        marker = new google.maps.Marker({
            position,
            map,
            animation: google.maps.Animation.DROP
        }, timeout);
        //mark����ť�ƥ�
        marker.addListener("click", () => {
            console.log('YOYOOYO')
            //�I���b�k��X�{���a��T
        });
    });

    markers.push(marker);
}

//�M���аO
function clearMarkers() {
    for (let i = 0; i < markers.length; i++) {
        markers[i].setMap(null);
    }
    markers = [];
}

// google map

let map;

// ��l�ƨå[�J�a��
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


//marker �^�줤�� �I����j
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