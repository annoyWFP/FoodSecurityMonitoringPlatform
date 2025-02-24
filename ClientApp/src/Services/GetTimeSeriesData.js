import { AbortExceptionInstance,BuildURL } from "./Common";

export const GetTimeSeriesData = (baseURL,country,startDate,endDate,correlationId) => {
    let contextURL = 'FoodSecurity/timeseries/' + country + '?startDate=' + startDate + '&endDate=' + endDate;
    console.log("Context URL from GetTimeSeriesData: " + contextURL);

    const url = BuildURL(baseURL, contextURL, correlationId);
    console.log("GetTimeSeries URL: " + url);

    return new Promise((resolve, reject) => {
        fetch(url, {
            method: 'GET',            
            headers: {
                'Content-Type': 'application/json',
                'correlationId':correlationId
            }
        }).then(response => {
            console.log(response);
            console.log(response.ok);
            response.json().then(json => {
                console.log(JSON.stringify(json));
                resolve(json);
            }).catch(err => { reject(err) });
        }).catch(err => { reject(err) });
    });

}