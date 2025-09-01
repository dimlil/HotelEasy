import { useState, useEffect } from "react"
// import styles from './HotelsPage.module.css'
import { getAllUsers } from "../../services/getAllUsers.js"


export default function AdminPanelPage() {
    const [users, setUsers] = useState([])
    useEffect(() => {
        const fetchUsers = async () => {
            setUsers(await getAllUsers())
        }
        fetchUsers()
    }, [])
    return (
        <section >
            {
                users?.data?.map(user => (
                    <div>
                        <h2 key={user._id}>Email: {user.email}</h2>
                        <p key={user._id}>Роля: {user.role}</p>
                    </div>
                ))
            }
        </section>
    )
}
