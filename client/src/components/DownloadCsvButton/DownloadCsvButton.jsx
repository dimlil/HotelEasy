import { useState } from "react";
import { getExportData } from "../../services/getExportData.js";
import styles from './DownloadBtn.module.css';

export default function DownloadCsvButton() {
    const [loading, setLoading] = useState(false);

    const handleDownload = async () => {
        setLoading(true);
        try {
            const response = await getExportData();

            const blob = new Blob([response.data], { type: "text/csv" });
            const url = window.URL.createObjectURL(blob);
            const a = document.createElement("a");
            a.href = url;
            a.download = "logs.csv";
            document.body.appendChild(a);
            a.click();
            a.remove();
            window.URL.revokeObjectURL(url);
        } catch (err) {
            console.error("Грешка при сваляне на файла:", err);
        } finally {
            setLoading(false);
        }
    };

    return (
        <button onClick={handleDownload} disabled={loading} className={styles.downloadBtn}>
            {loading ? "Сваляне..." : "Свали CSV"}
        </button>
    );
}
