function RERcalculator(variableNum) {
    let weight = document.getElementById("WeightKg");
    let result = document.getElementById("DailyRER");

    let value = 70 * Math.pow(weight.value, 0.75);
    //isNan判斷值是否為Nan
    result.value = isNaN(Math.round(value)) ? "" : Math.round(value);

    let DailyCal = document.getElementById("DailyCal")

    ClearOption();

    DailyCal.value=  isNaN(Math.round(value*variableNum)) ? "" : Math.round(value*variableNum);
}


//用來獲取外部json資料的方法
async function fetchVarData(PetType) {
    // 用fetch抓取json文件
    let response = await fetch('PetWeightVar.json');
    // 解析json文件
    let Vardata = await response.json();

    // 用陣列的方式取得寵物資料
    ChooseState(Vardata[PetType]);
}

let PetState = document.getElementById('PetState');

function ChooseState(VarData) {
    //取得下拉選單 select 元素
    PetState = document.getElementById('PetState');
    //取得第一個option的值
    let choseTypeText = document.getElementById('choseTypeText');

    // 將解析完的jsona每一個元素都跑一次
    VarData.forEach(item => {

        // 建立一個新的option元素
        let n_option = document.createElement('option');

        // 取得json的id對應option的value
        n_option.value = item.ID;

        // 將json的State對應option選項內容
        n_option.text = item.State;

        // 將 VariableNum 和 MaxVariableNum 添加為選項元素的自定義數據屬性 "dataset"
        n_option.dataset.variableNum = item.VariableNum;
        n_option.dataset.maxVariableNum = item.MaxVariableNum;

        //將每一個新增的option元素 加入到select元素中 且放在choseTypeText的後面
        PetState.insertBefore(n_option, choseTypeText.nextSibling);
    });
}

// 清空選項
function ClearOption(){

    while(PetState.options.length > 1){
        PetState.remove(PetState.options.length - 1);
    }
}

let btn_DogCal = document.getElementById("Btn_DogCal");
let btn_CatCal = document.getElementById("Btn_CatCal");
let TypeChoose = document.body;

TypeChoose.addEventListener('click', function (event) {
    switch (event.target.id) {
        case 'Btn_DogCal':
            btn_DogCal.classList.add("active");
            btn_CatCal.classList.remove("active");
            ClearOption();
            fetchVarData('dog');
            break;
        case 'Btn_CatCal':
            btn_CatCal.classList.add("active");
            btn_DogCal.classList.remove("active");
            ClearOption();
            fetchVarData('cat');
            break;
    }
})

PetState.addEventListener('change', function (event) {
    let selectedOption = event.target.selectedOptions[0];
    let variableNum = selectedOption.dataset.variableNum;
    let maxVariableNum = selectedOption.dataset.maxVariableNum;
    
    // 將 VariableNum 和 MaxVariableNum 傳遞給 handleVariableData 函數
    RERcalculator(variableNum);
});