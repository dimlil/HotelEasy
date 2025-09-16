import { useState, useEffect, useContext } from "react"
import HotelCard from "../../components/Cards/HotelCard.jsx"
import styles from './HotelsPage.module.css'
import { getOwnerHotels } from '../../services/getOwnerHotels.js';
import { AuthContext } from "../../context/AuthContext.js"


export default function MyHotelsPage() {
    const [hotels, setHotels] = useState([])
    const { user } = useContext(AuthContext);

    useEffect(() => {
        const fetchHotels = async () => {
            const ownersHotel = await getOwnerHotels(user.id);
            setHotels(ownersHotel)
        }
        fetchHotels()
    }, [])
    return (
        <section className={styles.boxesWrapper}>
            {
                hotels?.map(hotel => (
                    <HotelCard key={hotel.hotelId} id={hotel.hotelId} name={hotel.name} location={hotel.location} />
                ))
            }
        </section>
    )
}
