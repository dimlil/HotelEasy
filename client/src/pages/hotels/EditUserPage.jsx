import React, { useEffect, useState } from 'react'
import { useNavigate, useParams } from 'react-router-dom';
import FormContainer from '../../components/Forms/FormContainer.jsx';
import { getUserById } from '../../services/getUserById.js';
import { editUserService } from '../../services/editUser.js';

export default function EditUsersPage() {
    const [user, setUser] = useState(null);
    let navigate = useNavigate();
    const { id } = useParams();

    useEffect(() => {
        const fetchUsers = async () => {
            try {
                const data = await getUserById(id);

                if (!data) {
                    console.error("Потребителя не е намерена.");
                } else {
                    setUser(data);
                }
            } catch (err) {
                console.error("Грешка при зареждане на потребителя:", err);
            }
        };
        fetchUsers();
    }, []);

    const handleSubmit = async (formData) => {
        const result = await editUserService(id, formData);

        if (result) {
            navigate('/admin/panel/');
        } else {
            console.log("Грешка при редакция");
        }
    };

    if (!user) {
        return <section>
            <p>Зареждане...</p>
        </section>
    }

    return (
        <FormContainer
            formHeader="Редактирай стая"
            fields={[
                { name: 'email', label: 'Email' },
                { name: 'role', label: 'Роля' }
            ]}
            initialValues={user}
            onSubmit={handleSubmit}
            buttonText="Запази"
        />
    )
}
