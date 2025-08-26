import axios from "axios"
export const registerUser = async (formData) => {
    if (formData.username === '' || formData.password === '') {
        return "All fields must be filled"
    }

    try {
        const response = await axios.post('http://localhost:5276/register', {
            username: formData.username,
            password: formData.password
        }, { withCredentials: true });

        return response
    } catch (error) {
        if (error) {
            return error.massege
        }
    }
}