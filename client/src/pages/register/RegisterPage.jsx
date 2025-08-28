import { useNavigate } from "react-router-dom";
import FormContainer from "../../components/Forms/FormContainer";
import { registerUser } from "../../services/registerUser";

export default function RegisterPage() {
    let navigate = useNavigate();

    const materialsFormConfig = {
        formHeader: 'Регистрация',
        fields: [
            { name: 'email', label: 'Имейл' },
            { name: 'password', label: 'Парола', type: "password" }
        ],
        buttonText: 'Регистрация',
        onSubmit: async (formData) => {
            try {
                const result = await registerUser(formData);

                if (result.status === 200 || result.status === 201) {
                    localStorage.setItem("userToken", result.data);
                    navigate('/');
                }
            } catch (error) {
                navigate('/register');
                console.log("Registration failed:", error);
            }
        },
    };
    return (
        <FormContainer
            formHeader={materialsFormConfig.formHeader}
            fields={materialsFormConfig.fields}
            onSubmit={materialsFormConfig.onSubmit}
            buttonText={materialsFormConfig.buttonText}
        />
    )
}
