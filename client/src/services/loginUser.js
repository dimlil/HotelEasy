import axios from "axios"
export const loginUser = async (formData) => {
    if (formData.email === '' || formData.password === '') {
        return "All fields must be filled"
    }
    console.log(process.env.REACT_APP_API_URL);
    try {
        const response = await axios.post(`${process.env.REACT_APP_API_URL}/api/auth/login`, {
            email: formData.email,
            password: formData.password
        }, { withCredentials: true });

        return response
    } catch (error) {
        if (error) {
            return error.massege
        }
    }
}