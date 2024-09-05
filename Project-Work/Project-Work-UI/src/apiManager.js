import axios from 'axios';

class ApiManager {
    static async apiRequest(apiData) {
        try {
            const response = await axios({
                url: apiData.Url,
                method: apiData.Method,
                headers: {
                    'Content-Type': 'application/json',
                    ...apiData.Headers
                },
                data: apiData.Data
            });

            return response.data;
        } catch (error) {
            throw error;
        }
    }
}

export const apiRequest = ApiManager.apiRequest;

export default ApiManager;