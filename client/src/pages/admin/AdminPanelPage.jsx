import { useState, useEffect } from "react"
import styles from './adminPages.module.css'
import { getAllUsers } from "../../services/getAllUsers.js"
import { Link } from "react-router-dom"
import { deleteUser } from "../../services/deleteUsers.js"


export default function AdminPanelPage() {
    const [users, setUsers] = useState([])
    useEffect(() => {
        const fetchUsers = async () => {
            setUsers(await getAllUsers())
        }
        fetchUsers()
    }, [users])
    const deleteUserHandler = async (id) => {
        // e.target.querySelector('id').value
        deleteUser(id)
        setUsers(await getAllUsers())
    }
    return (
        <section className={styles.adminPanelWrapper}>
            {
                users?.data?.map(user => (
                    <div className={styles.row} key={user.userID}>
                        <div>
                            <h2>Email: {user.email}</h2>
                            <p>Роля: {user.role}</p>
                        </div>
                        <div>
                            <Link to={`/admin/edit/${user.userID}`}>Редактирай</Link>
                            <button onClick={() => deleteUserHandler(user.userID)} >Изтрий</button>
                        </div>
                    </div>
                ))
            }
        </section>
    )
}
