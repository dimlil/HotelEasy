import { useNavigate } from "react-router-dom";
import FormContainer from "../../components/Forms/FormContainer";
import { loginUser } from "../../services/loginUser";

export default function LoginPage() {
    let navigate = useNavigate();

    const materialsFormConfig = {
        formHeader: 'Вход',
        fields: [
            { name: 'email', label: 'Имейл' },
            { name: 'password', label: 'Парола', type: "password" }
        ],
        buttonText: 'Вход',
        onSubmit: async (formData) => {
            try {
                const result = await loginUser(formData);

                if (result.status === 200 || result.status === 201) {
                    localStorage.setItem("userToken", result.data);
                    navigate('/');
                }
            } catch (error) {
                navigate('/login');
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
