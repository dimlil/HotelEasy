import { useContext, useEffect, useState } from "react";
import { Link, useParams } from "react-router-dom";
import styles from './HotelsPage.module.css';
import { AuthContext } from "../../context/AuthContext.js";
import { getRoomById } from "../../services/getRoomById.js";

export default function RoomDetailsPage() {
    const [room, setRoom] = useState(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState("");
    const { id } = useParams();
    const { user } = useContext(AuthContext);

    useEffect(() => {
        const fetchRoom = async () => {
            try {
                const data = await getRoomById(id);

                if (!data || !data.roomId) {
                    setError("Стаята не е намерена.");
                } else {
                    setRoom(data);
                }
            } catch (err) {
                console.error("Грешка при зареждане на стаята:", err);
                setError("Възникна грешка при зареждане.");
            } finally {
                setLoading(false);
            }
        };
        fetchRoom();
    }, [id]);

    if (loading) {
        return (
            <section>
                <p>Зареждане...</p>
            </section>
        );
    }

    if (error) {
        return(
            <section>
                <p>{error}</p>
            </section>
        ); 
    }

    return (
        <section className={styles.detailsWrapper}>
            <div className={styles.detailsHeader}>
                {room.roomImages?.map((x, i) => (
                    <img key={i} src={x.url} alt={`Room Image ${i + 1}`} />
                ))}

                <h2>{room.roomNumber}</h2>
                <p>Цена: {room.price} лв.</p>
                <p>Вид: {room.roomType}</p>
            </div>

            <div>
                {user && room.owner && user.email === room.owner.email && (
                    <Link to={`/rooms/edit/${room.hotelId}`}>Редактирай</Link>
                )}
            </div>

        </section>
    );
}
