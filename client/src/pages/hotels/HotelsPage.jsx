import { useState, useEffect } from "react"
import { getAllHotels } from "../../services/getAllHotels"
import HotelCard from "../../components/Cards/HotelCard"
import styles from './HotelsPage.module.css'


export default function HotelsPage() {
    const [hotels, setHotels] = useState([])
    useEffect(() => {
        const fetchHotels = async () => {
            const hotels = await getAllHotels()
            setHotels(hotels)
        }
        fetchHotels()
    }, [])
    return (
        <section className={styles.boxesWrapper}>
            {
                hotels?.data?.map(hotel => (
                    <HotelCard key={hotel.hotelId} id={hotel.hotelId} name={hotel.name} location={hotel.location} />
                ))
            }
        </section>
    )
}
