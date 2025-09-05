import axios from "axios"
export const getUserById = async (id) => {
    try {
        const response = await axios.get(`${process.env.REACT_APP_API_URL}/api/auth/${id}`, { withCredentials: true });
        
        return response.data
    } catch (error) {
        if (error) {
            return error.massege
        }
    }
}