import { Routes, Route } from 'react-router-dom'
// eslint-disable-next-line
import style from './index.css'
import Header from './components/Header/Header.jsx'
import HomePage from './pages/home/HomePage.jsx'
import RegisterPage from './pages/register/RegisterPage.jsx'
import LoginPage from './pages/login/LoginPage.jsx'
import HotelsPage from './pages/hotels/HotelsPage.jsx'
import HotelDetailsPage from './pages/hotels/HotelDetailsPage.jsx'
import CreateHotel from './pages/hotels/CreateHotel.jsx'
import AdminPanelPage from './pages/admin/AdminPanelPage.jsx'

export default function Navigation() {
    return (
        <>
            <Header />
            <Routes>
                <Route path="/" element={<HomePage />} />
                <Route path="/register" element={<RegisterPage />} />
                <Route path="/login" element={<LoginPage />} />
                <Route path="/hotels" element={<HotelsPage />} />
                <Route path="/hotels/:id" element={<HotelDetailsPage />} />
                <Route path="/hotels/create" element={<CreateHotel />} />
                <Route path="/admin/panel" element={<AdminPanelPage />} />
            </Routes>
        </>
    )
}