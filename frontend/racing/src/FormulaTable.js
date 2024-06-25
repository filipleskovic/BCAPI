import React, { useEffect, useState } from 'react';
import FormulaRow from './FormulaRow'
import Form from './Form'
import EditFormulaForm from './EditFormulaForm';
import axios from 'axios';
import { fetchFormulas, postFormula, putFormula, removeFormula } from './services';

const columns = [
	{ key: 'id', label: 'Id' },
	{ key: 'name', label: 'Name' },
	{ key: 'horsepower', label: 'Horsepower' },
	{ key: 'topspeed', label: 'TopSpeed' },
	{ key: 'accelearation', label: 'Acceleration' }
];
const formulas = [
	{
		id: 3323,
		name: "Ferrari",
		horsepower: 760,
		topspeed: 401,
		acceleration: 2.5
	},
	{
		id: 2321,
		name: "Mercedes",
		horsepower: 740,
		topspeed: 420,
		acceleration: 3.1,
	},
	{
		id: 9916,
		name: "Redbull",
		horsepower: 730,
		topspeed: 399,
		acceleration: 2.3
	}
]
localStorage.setItem('formulas', JSON.stringify(formulas));


const FormulaTable = () => {
	const [rows, setRows] = useState([]);
	const [editingFormula, setEditingFormula] = useState(null);

	useEffect(() => {
		const fetchData = async () => {
			try {
				const formulas = await fetchFormulas();
				setRows(formulas);
			} catch (error) {
				console.error("Nije dohvaceno", error);
			}
		};

		fetchData();
	}, []);

	const addFormula = async (newFormula) => {
		await postFormula(newFormula)
		const formulas = await fetchFormulas();
		setRows(formulas)
	};

	const deleteFormula = async (id) => {
		const updatedRows = rows.filter((formula) => formula.id !== id);
		await removeFormula(id)
		setRows(updatedRows);

	};

	const updateFormula = async (updatedFormula) => {
		const updatedRows = rows.map((formula) =>
			formula.id === updatedFormula.id ? updatedFormula : formula
		);
		console.log(updatedFormula)
		await putFormula(updatedFormula)
		const formulas = await fetchFormulas();
		setRows(formulas)
		setEditingFormula(null);
	};

	const handleUpdateClick = (id) => {
		const formulaToEdit = rows.find((formula) => formula.id === id);
		setEditingFormula(formulaToEdit);
	};

	const handleCancelEdit = () => {
		setEditingFormula(null);
	};

	return (
		<div>
			{editingFormula && (
				<EditFormulaForm
					formula={editingFormula}
					onUpdate={updateFormula}
					onCancel={handleCancelEdit}
				/>
			)}
			<table>
				<caption>Formule</caption>
				<thead>
					<tr>
						<th>Ime</th>
						<th>Horsepower</th>
						<th>Topspeed</th>
						<th>Acceleration</th>
						<th>Actions</th>
					</tr>
				</thead>
				<tbody>
					{rows.map((formula) => (
						<FormulaRow
							key={formula.id}
							formulaRow={formula}
							onDelete={deleteFormula}
							onUpdate={() => handleUpdateClick(formula.id)}
						/>
					))}
				</tbody>
			</table>
			<Form onSubmit={addFormula} />
		</div>
	);
};

export default FormulaTable;