function RERcalculator() {
    let weight = document.getElementById("WeightKg");
    let result = document.getElementById("DailyRER");

    let value = 70* Math.pow(weight.value,0.75);
    result.value = isNaN(Math.round(value)) ? "" : Math.round(value);
}
