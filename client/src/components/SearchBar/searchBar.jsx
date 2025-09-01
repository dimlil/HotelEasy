import React, { useState } from "react";
import {
    Box,
    TextField,
    IconButton,
    Divider,
} from "@mui/material";
import SearchIcon from "@mui/icons-material/Search";
import { DatePicker } from "@mui/x-date-pickers/DatePicker";
import styles from './searchBar.module.css'
import { searchRoom } from "../../services/searchRoom";
import RoomCard from "../Cards/RoomCard";

export default function SearchBar() {
    const [location, setLocation] = useState("");
    const [checkIn, setCheckIn] = useState(null);
    const [checkOut, setCheckOut] = useState(null);
    const [guests, setGuests] = useState("");
    const [rooms, setRooms] = useState([])

    const handleSearch = async () => {
        const result = await searchRoom(location, checkIn, checkOut, guests);
        if (result.data) {
            setRooms(result.data);
        }
    };

    return (
        <>
            <Box className={styles.searchBarWrapper}>
                <TextField
                    variant="standard"
                    placeholder="Къде отиваш?"
                    value={location}
                    onChange={(e) => setLocation(e.target.value)}
                    InputProps={{ disableUnderline: true }}
                    sx={{ flex: 1 }}
                />

                <Divider orientation="vertical" flexItem />

                <DatePicker
                    label="Настаняване"
                    value={checkIn}
                    onChange={(newValue) => setCheckIn(newValue)}
                    slotProps={{
                        textField: {
                            variant: "standard",
                            InputProps: { disableUnderline: true },
                        },
                    }}
                />

                <Divider orientation="vertical" flexItem />

                <DatePicker
                    label="Напускане"
                    value={checkOut}
                    onChange={(newValue) => setCheckOut(newValue)}
                    slotProps={{
                        textField: {
                            variant: "standard",
                            InputProps: { disableUnderline: true },
                        },
                    }}
                />

                <Divider orientation="vertical" flexItem />

                <TextField
                    type="number"
                    variant="standard"
                    placeholder="Гости"
                    value={guests}
                    onChange={(e) => {
                        const value = e.target.value;
                        if (/^\d*$/.test(value)) {
                            setGuests(value);
                        }
                    }}
                    InputProps={{ disableUnderline: true }}
                    sx={{ width: 80 }}
                />


                <IconButton
                    onClick={handleSearch}
                    className={styles.searchButton}
                >
                    <SearchIcon />
                </IconButton>
            </Box>
            {rooms.length > 0 && (
                <Box className={styles.searchResults}>
                    <h2>Резултати от търсенето:</h2>
                    {rooms.map((x,i) => (
                        <RoomCard key={i} id={x.roomId} name={x.roomNumber} price={x.price} />
                    ))}
                </Box>
            )}
        </>
    );
}
