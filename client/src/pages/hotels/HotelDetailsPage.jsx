import { useEffect, useState } from "react"
import { getHotelById } from "../../services/getHotelById.js"
import { useParams } from "react-router-dom";

export default function HotelDetailsPage() {
    const [hotel, setHotel] = useState({})
    const { id } = useParams();
    useEffect(() => {
        const fetchHotels = async () => {
            setHotel(await getHotelById(id) )
        }
        fetchHotels()
    }, [id])
    return (
        <section>
            <h2>
                {hotel.name}
            </h2>
            <p>
                {hotel.location}
            </p>
        </section>
    )
}