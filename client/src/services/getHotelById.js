import axios from "axios"
export const getHotelById = async (id) => {
    try {
        const response = await axios.get(`${process.env.REACT_APP_API_URL}/api/hotels/${id}`, { withCredentials: true });
        
        return response.data
    } catch (error) {
        if (error) {
            return error.massege
        }
    }
}