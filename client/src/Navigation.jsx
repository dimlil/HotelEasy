import { Routes, Route } from 'react-router-dom'
// eslint-disable-next-line
import style from './index.css'
import Header from './components/Header/Header.jsx'
import HomePage from './pages/home/HomePage.jsx'

export default function Navigation() {
    return (
        <>
            <Header />
            <Routes>
                <Route path="/" element={<HomePage />} />
            </Routes>
        </>
    )
}