import axios from "axios";

export const createRoom = async (formData, id) => {
    if (
        !formData.RoomNumber ||
        !formData.Capacity ||
        !formData.Price ||
        !formData.RoomType ||
        !formData.ImageFiles
    ) {
        return "All fields must be filled";
    }

    const data = new FormData();
    data.append("HotelId", id);
    data.append("RoomNumber", formData.RoomNumber);
    data.append("Capacity", formData.Capacity);
    data.append("Price", formData.Price);
    data.append("RoomType", formData.RoomType);
    data.append("IsAvailable", true);

    for (let i = 0; i < formData.ImageFiles.length; i++) {
        data.append("ImageFiles", formData.ImageFiles[i]);
    }

    try {
        const response = await axios.post(
            `${process.env.REACT_APP_API_URL}/api/rooms`,
            data,
            {
                withCredentials: true,
                headers: { "Content-Type": "multipart/form-data" },
            }
        );

        return response;
    } catch (error) {
        console.error(error);
        return error.message;
    }
};
