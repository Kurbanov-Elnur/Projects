import axios, { AxiosRequestConfig, AxiosResponse } from 'axios';

const baseUrl = process.env.NEXT_PUBLIC_API_URL;

import { ApiRequestData } from '../../Data/Models/API.Model';

class ApiManager {
    static async apiRequest<T>(apiData: ApiRequestData): Promise<T> {
        try {
            const url = ApiManager.buildUrlWithParams(apiData.Url, apiData.Params);

            const config: AxiosRequestConfig = {
                url: `${baseUrl}/${url}`,
                method: apiData.Method,
                headers: {
                    'Content-Type': 'application/json',
                    ...apiData.Headers
                },
                withCredentials: apiData.WithCredentials ?? false,
                data: ApiManager.buildRequestData(apiData.Data, apiData.Headers?.['Content-Type']),
            };

            const response: AxiosResponse<T> = await axios(config);
            return response.data;
        } catch (error: any) {
            throw {
                error: error.response.data,
            };
        }
    }

    private static buildUrlWithParams(url: string, params?: Record<string, any>): string {
        if (!params) return url;
        const queryString = new URLSearchParams(params).toString();
        return queryString ? `${url}?${queryString}` : url;
    }

    private static buildRequestData(data: any, contentType: string | undefined): any {
        if (contentType === 'application/x-www-form-urlencoded') {
            return new URLSearchParams(data).toString();
        }
        return data;
    }
}

export default ApiManager;