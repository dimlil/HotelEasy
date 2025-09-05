import { Link } from "react-router-dom";
import styles from './hotelCard.module.css'


export default function RoomCard(props) {
    return (
        <div className={styles.box}>
            <h2>
                {props.name}
            </h2>
            <p>
                Цена: {props.price} лв.
            </p>
            {
                props.checkIn && props.checkOut ? (
                    <Link to={`/rooms/${props.id}?checkIn=${props.checkIn}&checkOut=${props.checkOut}`}>Виж Още</Link>
                ) : (
                    <Link to={`/rooms/${props.id}`}>Виж Още</Link>
                )
            }
        </div>
    )
}