import { useContext, useEffect, useState } from "react";
import { Link, useParams } from "react-router-dom";
import styles from './HotelsPage.module.css';
import { AuthContext } from "../../context/AuthContext.js";
import { getRoomById } from "../../services/getRoomById.js";
import { bookRoom } from "../../services/bookRoom.js";

export default function RoomDetailsPage() {
    const [room, setRoom] = useState(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState("");
    const { id } = useParams();
    const { user } = useContext(AuthContext);
    const searchParams = new URLSearchParams(window.location.search);
    const checkInParam = searchParams.get('checkIn');
    const checkOutParam = searchParams.get('checkOut');

    let checkIn = null;
    let checkOut = null;

    if (checkInParam) {
        const parsedCheckIn = new Date(checkInParam);
        if (!isNaN(parsedCheckIn)) {
            checkIn = parsedCheckIn.toISOString().split("T")[0];
        } else {
            console.error("Invalid check-in date format:", checkInParam);
        }
    }

    if (checkOutParam) {
        const parsedCheckOut = new Date(checkOutParam);
        if (!isNaN(parsedCheckOut)) {
            checkOut = parsedCheckOut.toISOString().split("T")[0];
        } else {
            console.error("Invalid check-out date format:", checkOutParam);
        }
    }

    const bookRoomHandler = async () => {
        const result = await bookRoom(user.id, room.roomId, checkIn, checkOut)
        window.location = result.data
    }

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
        return (
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
                {user && (
                    <button onClick={bookRoomHandler}>Резервирай</button>
                )}
            </div>

        </section>
    );
}
