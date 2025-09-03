import axios from "axios"
export const getAllLogs = async () => {
    try {
        const response = await axios.get(`${process.env.REACT_APP_API_URL}/api/logs`, { withCredentials: true });

        return response
    } catch (error) {
        if (error) {
            return error.massege
        }
    }
}