import { Routes, Route } from 'react-router-dom'
// eslint-disable-next-line
import style from './index.css'
import HomePage from './pages/home/HomePage.jsx'

export default function Navigation() {
    return (
        <Routes>
            <Route path="/" element={<HomePage />} />
        </Routes>
    )
}