import React, { useEffect, useState } from 'react';
import FormulaRow from './FormulaRow'
import Form from './Form'
import EditFormulaForm from './EditFormulaForm';
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
		const storedData = localStorage.getItem('formulas');
		if (storedData) {
			setRows(JSON.parse(storedData));
		}
	}, []);

	const addFormula = (newFormula) => {
		const updatedRows = [...rows, newFormula];
		setRows(updatedRows);
		localStorage.setItem('formulas', JSON.stringify(updatedRows));
	};

	const deleteFormula = (id) => {
		const updatedRows = rows.filter((formula) => formula.id !== id);
		setRows(updatedRows);
		localStorage.setItem('formulas', JSON.stringify(updatedRows));
	};

	const updateFormula = (updatedFormula) => {
		const updatedRows = rows.map((formula) =>
			formula.id === updatedFormula.id ? updatedFormula : formula
		);
		setRows(updatedRows);
		localStorage.setItem('formulas', JSON.stringify(updatedRows));
		setEditingFormula(null);
	};

	const handleUpdateClick = (id) => {
		const formulaToEdit = rows.find((formula) => formula.id === id);
		setEditingFormula(formulaToEdit);
	};

	const handleCancelEdit = () => {
		setEditingFormula(null); // Zatvaranje forme za uređivanje bez ažuriranja
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
						<th>Id</th>
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