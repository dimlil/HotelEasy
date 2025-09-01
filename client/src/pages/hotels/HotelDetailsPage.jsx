import { useEffect, useState } from "react"
import { getHotelById } from "../../services/getHotelById.js"
import { useParams } from "react-router-dom";
import styles from './HotelsPage.module.css'
import RoomCard from "../../components/Cards/RoomCard.jsx";

export default function HotelDetailsPage() {
    const [hotel, setHotel] = useState({})
    const { id } = useParams();
    useEffect(() => {
        const fetchHotels = async () => {
            setHotel(await getHotelById(id))
        }
        fetchHotels()

    }, [id])
    return (
        <section className={styles.hotelDetailsWrapper}>
            <div className={styles.hotelDetailsHeader}>
                {hotel.hotelImages && hotel.hotelImages.map((x, i) => (
                    <img key={i} src={x.url} alt={`Hotel Image ${i + 1}`} />
                ))}

                <h2>
                    {hotel.name}
                </h2>
                <p>
                    {hotel.location}
                </p>
            </div>
            <div >
                <h3>
                    Стай:
                </h3>
                <div className={styles.boxesWrapper}>
                    {hotel.rooms && hotel.rooms.map((x, i) => (
                        <RoomCard key={i} id={x._id} name={x.roomNumber} price={x.price} />
                    ))}
                </div>
            </div>
        </section>
    )
}