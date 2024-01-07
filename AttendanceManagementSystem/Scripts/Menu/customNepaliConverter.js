function ConvEnglishToNepaliNum(number) {
    let nepaliNumbersList = ['0', '१', '२', '३', '४', '५', '६', '७', '८', '९'];
    const numberToConvert = number.toString().split('');
    let result = numberToConvert.map((num, i) => {
        return nepaliNumbersList[i, num];
    });
    return result.join('');
}


function ConvNepaliMonth(number) {
    let nepaliNumbersList = ['बैशाख', 'जेष्ठ ', 'आषाढ़ ', 'श्रावण ', 'भाद्र', 'आश्विन', 'कार्तिक', 'मंसिर', 'पौष', 'माघ', 'फाल्गुन', 'चैत्र'];
    number = number - 1;
    const numberToConvert = nepaliNumbersList[number];
    return numberToConvert;
}

function ConvEnglishToNepaliNum1(number) {
    let nepaliNumbersList = ['०', '१', '२', '३', '४', '५', '६', '७', '८', '९'];
    return number
        .split('')
        .map(char => {
            if (!isNaN(parseInt(char))) {
                return nepaliNumbersList[parseInt(char)];
            } else {
                return char;
            }
        })
        .join('');
}


function ConvertNepaliDayName(number) {
    const currentDate = new Date();
    const daysOfWeek = ['आइतबार', 'सोमबार', 'मंगलबार', 'बुधबार', 'बिहिबार', 'शुक्रबार', 'शनिबार'];
    const dayName = daysOfWeek[number];
    return dayName;
}