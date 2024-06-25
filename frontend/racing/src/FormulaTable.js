import React, { useEffect, useState } from 'react';
import FormulaRow from './FormulaRow'
import Form from './Form'
import EditFormulaForm from './EditFormulaForm';
import FilterForm from './FilterForm'
import { fetchFormulas, postFormula, putFormula, removeFormula, filteredFormulas } from './services';
import './FormulaRow.css'



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
	const filterFormulas = async (filter) => {
		const formulas = await filteredFormulas(filter)
		setRows(formulas)
	}

	return (
		<div class="divRow">
			{editingFormula && (
				<EditFormulaForm
					formula={editingFormula}
					onUpdate={updateFormula}
					onCancel={handleCancelEdit}
				/>
			)}
			<FilterForm onSubmit={filterFormulas}></FilterForm>
			<table >
				<thead>
					<tr>
						<th>Ime</th>
						<th>Horsepower</th>
						<th>TopSpeed</th>
						<th>Acceleration</th>
						<th>Actions</th>
					</tr>
				</thead>
				<tbody style={{ border: "2px solid #2E294E" }}>
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
		</div >
	);
};

export default FormulaTable;