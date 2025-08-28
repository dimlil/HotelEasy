import { Link } from "react-router-dom";
import styles from './hotelCard.module.css'


export default function HotelCard(props) {
    return (
        <div className={styles.box}>
            <h2>
                {props.name}
            </h2>
            <p>
                Локация: {props.location}
            </p>
            <Link to={`/hotels/${props.id}`}>Виж Още</Link>
        </div>
    )
}