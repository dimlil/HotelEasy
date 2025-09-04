import axios from "axios"
export const deleteHotel = async (id) => {
    try {
        const response = await axios.delete(`${process.env.REACT_APP_API_URL}/api/hotels/${id}`, { withCredentials: true });

        return response
    } catch (error) {
        if (error) {
            return error.massege
        }
    }
}