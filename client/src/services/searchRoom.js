import axios from "axios"
export const searchRoom = async (location, checkIn, checkOut, guests) => {
    if (!location || !checkIn || !checkOut || !guests) {
        return "All fields must be filled"
    }
    try {
        const response = await axios.post(`${process.env.REACT_APP_API_URL}/api/rooms/search`, {
            location, checkIn, checkOut, guests: Number(guests)
        }, { withCredentials: true });

        return response
    } catch (error) {
        if (error) {
            return error.massege
        }
    }
}