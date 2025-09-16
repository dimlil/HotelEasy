import { useEffect, useState } from "react";
import styles from "./formContainer.module.css";

const FormContainer = ({
  formHeader,
  fields,
  initialValues = {},
  onSubmit,
  buttonText,
}) => {
  const [formData, setFormData] = useState(initialValues);
  const [isInitialized, setIsInitialized] = useState(false);

  useEffect(() => {
    if (!isInitialized && Object.keys(initialValues).length > 0) {
      setFormData(initialValues);
      setIsInitialized(true);
    }
  }, [initialValues, isInitialized]);

  const handleChange = (e) => {
    if (e.target.type === "file") {
      setFormData({
        ...formData,
        [e.target.name]: e.target.files,
      });
    } else {
      setFormData({
        ...formData,
        [e.target.name]: e.target.value,
      });
    }
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
          {field.type === "select" ? (
            <select
              id={field.name}
              name={field.name}
              value={formData[field.name] || ""}
              onChange={handleChange}
            >
              <option value="">-- Избери --</option>
              {field.options?.map((opt) => (
                <option key={opt.value} value={opt.value}>
                  {opt.label}
                </option>
              ))}
            </select>
          ) : field.type === "file" ? (
            <input
              id={field.name}
              name={field.name}
              type="file"
              accept={field.accept}
              onChange={handleChange}
              multiple
            />
          ) : (
            <input
              id={field.name}
              name={field.name}
              type={field.type || "text"}
              value={formData[field.name] || ""}
              onChange={handleChange}
            />
          )}
        </div>
      ))}
      <button type="submit">{buttonText}</button>
    </form>
  );
};

export default FormContainer;