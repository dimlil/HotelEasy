import React, { useEffect, useState } from 'react'
import { useNavigate, useParams } from 'react-router-dom';
import FormContainer from '../../components/Forms/FormContainer.jsx';
import { getHotelById } from '../../services/getHotelById.js';
import { editHotelService } from '../../services/editHotel.js';

export default function EditHotelPage() {
    const [hotel, setHotel] = useState(null);
    let navigate = useNavigate();
    const { id } = useParams();

    useEffect(() => {
        const fetchHotel = async () => {
            try {
                const data = await getHotelById(id);

                if (!data || !data.hotelId) {
                    console.error("Хотелът не е намерен.");
                } else {
                    setHotel(data);
                }
            } catch (err) {
                console.error("Грешка при зареждане на хотела:", err);
            }
        };
        fetchHotel();
    }, [id]);

    const handleSubmit = async (formData) => {
        const result = await editHotelService(id, formData);

        if (result) {
            navigate('/hotels/' + id);
        } else {
            console.log("Грешка при редакция");
        }
    };

    if (!hotel) {
        return <section>
            <p>Зареждане...</p>
        </section>
    }

    return (
        <FormContainer
            formHeader="Редактирай Хотел"
            fields={[
                { name: 'name', label: 'Име' },
                { name: 'location', label: 'Локация' },
                { name: 'ImageFiles', label: 'Снимки', type: "file", accept: "image/*" }
            ]}
            initialValues={hotel}
            onSubmit={handleSubmit}
            buttonText="Запази"
        />
    )
}
