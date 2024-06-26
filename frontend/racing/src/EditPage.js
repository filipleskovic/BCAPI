import EditFormulaForm from "./EditFormulaForm";
import { fetchFormula, putFormula } from "./services";
import React, { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import './FormulaRow.css'


const EditPage = () => {
	const navigate = useNavigate();
	const { formulaId } = useParams();
	const [editingFormula, setEditingFormula] = useState({});
	useEffect(() => {

		const fetchData = async () => {
			try {
				const formula = await fetchFormula(formulaId);
				setEditingFormula(formula);
			} catch (error) {
				console.error("Nije dohvaceno", error);
			}
		};
		fetchData();

	}, []);
	const updateFormula = async (updatedFormula) => {
		await putFormula(updatedFormula)
		navigate("/")
	};
	const handleCancelEdit = () => {
		navigate("/")
	}
	return (
		<div>
			<EditFormulaForm
				formula={editingFormula}
				onUpdate={updateFormula}
				onCancel={handleCancelEdit}></EditFormulaForm>
		</div>
	)
}
export default EditPage