export const AbortExceptionInstance = new DOMException('Aborted', 'AbortError');

export const BuildURL = (baseURL, postBaseURL, correlationId) => {
    let msg = "Data Source: " + baseURL + "/api/" + postBaseURL;
    console.log(msg);
    return baseURL + "/api/" + postBaseURL;
}
