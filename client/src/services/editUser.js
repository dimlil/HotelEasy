import axios from "axios"

export const editUserService = async (id, formData) => {
    if (
        !formData.email ||
        !formData.role
    ) {
        return "All fields must be filled";
    }

    const data = new FormData();
    data.append("Email", formData.email);
    data.append("Role", formData.role);

    try {
        const response = await axios.put(`${process.env.REACT_APP_API_URL}/api/auth/${id}`,
            data,
            {
                withCredentials: true,
                headers: { "Content-Type": "multipart/form-data" },
            });

        return response
    } catch (error) {
        if (error) {
            return error.massege
        }
    }
}