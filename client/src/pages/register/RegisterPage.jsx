import { useNavigate } from "react-router-dom";
import FormContainer from "../../components/Forms/FormContainer";
import { registerUser } from "../../services/registerUser";

export default function RegisterPage() {
    let navigate = useNavigate();

    const materialsFormConfig = {
        formHeader: 'Регистрация',
        fields: [
            { name: 'username', label: 'Име' },
            { name: 'password', label: 'Парола', type: "password" }
        ],
        buttonText: 'Регистрация',
        onSubmit: async (formData) => {
            const result = await registerUser(formData);

            if (result.status === 200 || result.status === 201) {
                localStorage.setItem("user", formData.password)
                navigate('/');
            }
            else {
                console.log(result);
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
