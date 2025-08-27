import axios from "axios"
export const registerUser = async (formData) => {
    if (formData.username === '' || formData.password === '') {
        return "All fields must be filled"
    }

    try {
        const response = await axios.post(`${process.env.REACT_APP_API_URL}/api/auth/register`, {
            email: formData.username,
            password: formData.password
        }, { withCredentials: true });

        return response
    } catch (error) {
        if (error) {
            return error.massege
        }
    }
}