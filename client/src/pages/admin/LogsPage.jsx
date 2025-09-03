import { useState, useEffect } from "react"
import styles from './adminPages.module.css'
import { getAllLogs } from "../../services/getAllLogs"


export default function LogsPage() {
    const [logs, setLogs] = useState([])
    useEffect(() => {
        const fetchUsers = async () => {
            setLogs(await getAllLogs())
        }
        fetchUsers()
    }, [logs])
    return (
        <section className={styles.adminPanelWrapper}>
            {
                logs?.data?.map(log => (
                    <div className={styles.row} key={log.userID}>
                        <div>
                            <h2>Таблица: {log.tableName}</h2>
                            <p>Операция: {log.operationType}</p>
                            <p>Дата и час: {log.operationDateTime}</p>
                        </div>
                    </div>
                ))
            }
        </section>
    )
}
