import axios from "axios"
export const getExportData = async () => {
    try {
        const response = await axios.get(`${process.env.REACT_APP_API_URL}/api/logs`, { withCredentials: true, responseType: "blob" });

        return response
    } catch (error) {
        if (error) {
            return error.massege
        }
    }
}