import { useContext } from "react";
import { AuthContext } from "../../context/AuthContext.js";
import { Link } from 'react-router-dom'
import styles from './Header.module.css'
import logo from '../../assets/logo.png'

export default function Header() {
  const { user } = useContext(AuthContext);

  const logoutHandler = () => {
    localStorage.removeItem("userToken");
  }

  return (
    <header>
      <div className={styles.headerWrapper}>
        <Link to="/">
          <img src={logo} alt="logo" />
        </Link>

        <nav>
          {user ? <>
            {
              user.role === "User" &&
              <>
                <Link to="/hotels">Хотели</Link>
              </>
            }
            {
              user.role === "Owner" &&
              <>
                <Link to="/hotels/create">Добави хотел</Link>
              </>
            }
            {
              user.role === "Admin" &&
              <>
                <Link to="/admin/panel">Контролен панел</Link>
                <Link to="/admin/logs">Логове</Link>
              </>
            }
            <Link to='/' onClick={logoutHandler}>Изход</Link>
          </> : <>
            <Link to="/login">Вход</Link>
            <Link to="/register">Регистрация</Link>
          </>}
        </nav>
      </div>
    </header>
  )
}