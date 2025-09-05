import React, { useEffect, useState } from 'react'
import { useNavigate, useParams } from 'react-router-dom';
import FormContainer from '../../components/Forms/FormContainer.jsx';
import { getRoomById } from '../../services/getRoomById.js';
import { editRoomService } from '../../services/editRoom.js';

export default function EditRoomPage() {
    const [room, setRoom] = useState(null);
    let navigate = useNavigate();
    const { id } = useParams();

    useEffect(() => {
        const fetchRoom = async () => {
            try {
                const data = await getRoomById(id);

                if (!data || !data.roomId) {
                    console.error("Стаята не е намерена.");
                } else {
                    setRoom(data);
                }
            } catch (err) {
                console.error("Грешка при зареждане на стая:", err);
            }
        };
        fetchRoom();
    }, [id]);

    const handleSubmit = async (formData) => {
        const result = await editRoomService(id, formData, room.hotel.hotelId);

        if (result) {
            navigate('/rooms/' + id);
        } else {
            console.log("Грешка при редакция");
        }
    };

    if (!room) {
        return <section>
            <p>Зареждане...</p>
        </section>
    }

    return (
        <FormContainer
            formHeader="Редактирай стая"
            fields={[
                { name: 'roomNumber', label: 'Номер на стаята' },
                { name: 'roomType', label: 'Тип стая' },
                { name: 'capacity', label: 'Капацитет' },
                { name: 'price', label: 'Цена' },
                { name: 'ImageFiles', label: 'Снимки', type: "file", accept: "image/*" }
            ]}
            initialValues={room}
            onSubmit={handleSubmit}
            buttonText="Запази"
        />
    )
}
