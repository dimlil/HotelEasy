import axios from "axios"

export const editRoomService = async (id, formData, hotelId) => {
    if (
        !formData.roomNumber ||
        !formData.capacity ||
        !formData.price ||
        !formData.roomType ||
        !formData.ImageFiles
    ) {
        return "All fields must be filled";
    }

    const data = new FormData();
    data.append("HotelId", hotelId);
    data.append("RoomNumber", formData.roomNumber);
    data.append("Capacity", formData.capacity);
    data.append("Price", formData.price);
    data.append("RoomType", formData.roomType);
    data.append("IsAvailable", true);

    for (let i = 0; i < formData.ImageFiles.length; i++) {
        data.append("ImageFiles", formData.ImageFiles[i]);
    }

    try {
        const response = await axios.put(`${process.env.REACT_APP_API_URL}/api/rooms/${id}`,
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