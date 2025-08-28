import axios from "axios"
export const getAllHotels = async () => {
    try {
        const response = await axios.get(`${process.env.REACT_APP_API_URL}/api/hotels`, { withCredentials: true });

        return response
    } catch (error) {
        if (error) {
            return error.massege
        }
    }
}