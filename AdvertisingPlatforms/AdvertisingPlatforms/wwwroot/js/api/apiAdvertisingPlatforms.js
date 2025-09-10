


async function searchAdvertisingPlatforms(searchString) {
    // отправляет запрос и получаем ответ
    const response = await fetch("/api/AdvertisingPlatforms?search="+searchString, {
        method: "GET",
        headers: { "Accept": "application/json" }
    });
    


    var AdvertisingPlatforms;
    if (response.ok === true) {
        AdvertisingPlatforms = await response.json();
    }else{
        AdvertisingPlatforms = {error:await response.json()};
    }

    return AdvertisingPlatforms;
}

async function getParameters() {
    // отправляет запрос и получаем ответ
    const response = await fetch("/api/AdvertisingPlatforms/SearchParameters", {
        method: "GET",
        headers: { "Accept": "application/json" }
    });

    var appParams;
    if (response.ok === true) {
        // получаем данные
        appParams = await response.json();
    }else{
        appParams={
            allowingTheUseOfCapitalLetters:false,
            capitaLetterSensitivity:false,
            locationsWithTheSameName:false,
            allowedExtensions:[".txt"],
            allowedMimeTypes:["text/plain"],
            maxSize: 50*1024*124}// 50 Мб
    }
    return appParams;
}