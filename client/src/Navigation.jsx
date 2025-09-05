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
import RoomDetailsPage from './pages/hotels/RoomDetailsPage.jsx'
import SuccessPage from './pages/hotels/SuccessPage.jsx'
import LogsPage from './pages/admin/LogsPage.jsx'
import EditHotelPage from './pages/hotels/EditHotelPage.jsx'
import EditRoomPage from './pages/hotels/EditRoomPage.jsx'
import EditUsersPage from './pages/hotels/EditUserPage.jsx'
import { useContext } from 'react'
import { AuthContext } from './context/AuthContext.js'
import ProtectedRoute from './ProtectedRoute.jsx'

export default function Navigation() {
      const { user } = useContext(AuthContext);
    
    return (
        <>
            <Header />
            <Routes>
                <Route path="/" element={<HomePage />} />
                <Route path="/register" element={<RegisterPage />} />
                <Route path="/login" element={<LoginPage />} />

                <Route path="/hotels" element={<HotelsPage />} />
                <Route path="/hotels/:id" element={<HotelDetailsPage />} />

                <Route element={<ProtectedRoute isAllowed={user?.role === "Owner"} />}>
                    <Route path="/hotels/edit/:id" element={<EditHotelPage />} />
                    <Route path="/hotels/create" element={<CreateHotel />} />
                    <Route path="/rooms/edit/:id" element={<EditRoomPage />} />
                </Route>

                <Route element={<ProtectedRoute isAllowed={user?.role === "Admin"} redirectPath="/" />}>
                    <Route path="/admin/panel" element={<AdminPanelPage />} />
                    <Route path="/admin/logs" element={<LogsPage />} />
                    <Route path="/admin/edit/:id" element={<EditUsersPage />} />
                </Route>

                <Route path="/rooms/:id" element={<RoomDetailsPage />} />
                <Route path="/success" element={<SuccessPage />} />
            </Routes>
        </>
    )
}