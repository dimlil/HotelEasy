import axios from "axios"
import { decodeUserToken } from "../utils/auth.js";

export const editHotelService = async (id,formData) => {
    if (formData.name === '' || formData.location === '' || formData.ImageFiles === '') {
        return "All fields must be filled"
    }
    const user = await decodeUserToken();

    const data = new FormData();
    data.append("OwnerId", user.id);
    data.append("Name", formData.name);
    data.append("Location", formData.location);

    for (let i = 0; i < formData.ImageFiles.length; i++) {
        data.append("ImageFiles", formData.ImageFiles[i]);
    }

    try {
        const response = await axios.put(`${process.env.REACT_APP_API_URL}/api/hotels/${id}`, {
            OwnerId: user.id,
            Name: formData.name,
            Location: formData.location,
            ImageFiles: formData.ImageFiles
        }, {
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