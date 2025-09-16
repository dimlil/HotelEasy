import axios from "axios"
export const bookRoom = async (UserId, RoomId, CheckInDate, CheckOutDate) => {
    try {
        const response = await axios.post(`${process.env.REACT_APP_API_URL}/api/reservation`, {
            UserId: Number(UserId), RoomId: Number(RoomId), CheckInDate, CheckOutDate
        }, { withCredentials: true });

        return response
    } catch (error) {
        if (error) {
            return error.massege
        }
    }
}