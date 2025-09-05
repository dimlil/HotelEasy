import { useContext, useEffect, useState } from "react";
import { getHotelById } from "../../services/getHotelById.js";
import { Link, useNavigate, useParams } from "react-router-dom";
import styles from './HotelsPage.module.css';
import RoomCard from "../../components/Cards/RoomCard.jsx";
import { AuthContext } from "../../context/AuthContext.js";
import { deleteHotel } from "../../services/deleteHotel.js";
import FormContainer from "../../components/Forms/FormContainer.jsx";
import { createRoom } from "../../services/createRoom.js";

export default function HotelDetailsPage() {
    const [hotel, setHotel] = useState(null);
    const [loading, setLoading] = useState(true);
    const [showForm, setShowForm] = useState(false);
    const [error, setError] = useState("");
    const { id } = useParams();
    const { user } = useContext(AuthContext);
    let navigate = useNavigate();

    useEffect(() => {
        const fetchHotels = async () => {
            try {
                const data = await getHotelById(id);

                if (!data || !data.hotelId) {
                    setError("Хотелът не е намерен.");
                } else {
                    setHotel(data);
                }
            } catch (err) {
                console.error("Грешка при зареждане на хотела:", err);
                setError("Възникна грешка при зареждане.");
            } finally {
                setLoading(false);
            }
        };
        fetchHotels();
    }, [id]);

    const deleteHotelHandler = async (id) => {
        deleteHotel(id)
        navigate('/hotels')
    }

    const showFormHandler = async () => {
        setShowForm(true)
    }

    const formConfig = {
        formHeader: 'Добави стая',
        fields: [
            { name: 'RoomNumber', label: 'Номер на стаята' },
            { name: 'RoomType', label: 'Тип стая' },
            { name: 'Capacity', label: 'Капацитет' },
            { name: 'Price', label: 'Цена' },
            { name: 'ImageFiles', label: 'Снимки', type: "file", accept: "image/*" }
        ],
        buttonText: 'Добави',
        onSubmit: async (formData) => {
            try {
                const result = await createRoom(formData, hotel.hotelId);

                if (result.status === 200 || result.status === 201) {
                    navigate('/rooms/' + result.data.roomId);
                }
            } catch (error) {
                navigate('/hotels/' + hotel.hotelId);
                console.log("Creation failed:", error);
            }
        },
    };

    if (loading) {
        return (
            <section>
                <p>Зареждане...</p>
            </section>
        );
    }

    if (error) {
        return (
            <section>
                <p>{error}</p>
            </section>
        );
    }

    return (
        <section className={styles.detailsWrapper}>
            <div className={styles.hotelDetailsHeader}>
                {hotel.hotelImages?.map((x, i) => (
                    <img key={i} src={x.url} alt={`Hotel Image ${i + 1}`} />
                ))}

                <h2>{hotel.name}</h2>
                <p>{hotel.location}</p>
            </div>

            <div>
                {user && hotel.owner && user.email === hotel.owner.email && (
                    <div className={styles.buttonsWrapper}>
                        <button onClick={() => showFormHandler(hotel.hotelId)} className={styles.CrateBtn}>Добави стая</button>
                        <Link to={`/hotels/edit/${hotel.hotelId}`}>Редактирай</Link>
                        <button onClick={() => deleteHotelHandler(hotel.hotelId)}>Изтрий</button>
                    </div>
                )}
            </div>

            {
                showForm && (
                    <>
                        <FormContainer
                            formHeader={formConfig.formHeader}
                            fields={formConfig.fields}
                            onSubmit={formConfig.onSubmit}
                            buttonText={formConfig.buttonText}
                        />
                    </>
                )
            }

            <div>
                <h3>Стаи:</h3>
                <div className={styles.boxesWrapper}>
                    {hotel.rooms?.length > 0 ? (
                        hotel.rooms.map((x, i) => (
                            <RoomCard key={i} id={x.roomId} name={x.roomNumber} price={x.price} />
                        ))
                    ) : (
                        <p>Няма налични стаи.</p>
                    )}
                </div>
            </div>
        </section>
    );
}
