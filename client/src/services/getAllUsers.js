import axios from "axios"
export const getAllUsers = async () => {
    try {
        const response = await axios.get(`${process.env.REACT_APP_API_URL}/api/auth`, { withCredentials: true });

        return response
    } catch (error) {
        if (error) {
            return error.massege
        }
    }
}