import { useContext } from "react";
import { AuthContext } from "../../context/AuthContext.js";
import { Link } from 'react-router-dom'
import styles from './Header.module.css'
import logo from '../../assets/logo.png'

export default function Header() {
  const { user } = useContext(AuthContext);

  return (
    <header>
      <div className={styles.headerWrapper}>
        <Link to="/">
          <img src={logo} alt="logo" />
        </Link>

        <nav>
          {user ? <>
            {user.role === "User" && <>
              <Link to="/hotels">Хотели</Link>
            </>}
          </> : <>
            <Link to="/login">Вход</Link>
            <Link to="/register">Регистрация</Link>
          </>}
        </nav>
      </div>
    </header>
  )
}