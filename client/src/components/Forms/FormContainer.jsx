import { useEffect, useState } from "react";
import styles from "./formContainer.module.css";

const FormContainer = ({
    formHeader,
    fields,
    onSubmit,
    buttonText
}) => {
    const [formData, setFormData] = useState({});

    useEffect(() => {
    }, []);

    const handleChange = (e) => {
        setFormData({
            ...formData,
            [e.target.name]: e.target.value,
        });
    };

    const handleSubmit = (e) => {
        e.preventDefault();
        onSubmit(formData);
    };

    return (
        <form className={styles.form} onSubmit={handleSubmit}>
            <h2>{formHeader}</h2>
            {fields.map((field) => (
                <div key={field.name}>
                    <label htmlFor={field.name}>{field.label}</label>
                    <input
                        id={field.name}
                        name={field.name}
                        type={field.type || "text"}
                        value={formData[field.name] || ""}
                        onChange={handleChange}
                    />
                </div>
            ))}
            <button type="submit">{buttonText}</button>
        </form>
    );
};

export default FormContainer;