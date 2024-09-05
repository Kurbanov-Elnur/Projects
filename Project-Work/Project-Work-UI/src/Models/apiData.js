class ApiData {
    constructor(url, method = 'GET', data = null, headers = {}) {
        this.Url = url;
        this.Method = method;
        this.Data = data;
        this.Headers = headers;
    }
}

export default ApiData;