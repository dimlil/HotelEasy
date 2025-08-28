import { useNavigate } from 'react-router-dom';
import FormContainer from '../../components/Forms/FormContainer.jsx'
import { CreateHotelService } from '../../services/CreateHotel.js';

export default function CreateHotel() {
    let navigate = useNavigate();

    const formConfig = {
        formHeader: 'Добави хотел',
        fields: [
            { name: 'HotelName', label: 'Име' },
            { name: 'location', label: 'Локация' },
            { name: 'ImageFiles', label: 'Снимки', type: "file", accept: "image/*" }
        ],
        buttonText: 'Добави',
        onSubmit: async (formData) => {
            try {
                const result = await CreateHotelService(formData);

                if (result.status === 200 || result.status === 201) {
                    navigate('/hotels');
                }
            } catch (error) {
                navigate('/register');
                console.log("Creation failed:", error);
            }
        },
    };
    return (
        <FormContainer
            formHeader={formConfig.formHeader}
            fields={formConfig.fields}
            onSubmit={formConfig.onSubmit}
            buttonText={formConfig.buttonText}
        />
    )
}
