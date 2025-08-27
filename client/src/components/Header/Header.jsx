import { Link } from 'react-router-dom'
import styles from './Header.module.css'
import logo from '../../assets/logo.png'

export default function Header() {
  return (
    <header>
      <div className={styles.headerWrapper}>
        <Link to="/">
          <img src={logo} alt="logo" />
        </Link>


        <nav>
          <Link to="/login">Вход</Link>
          <Link to="/register">Регистрация</Link>
        </nav>
      </div>
    </header>
  )
}