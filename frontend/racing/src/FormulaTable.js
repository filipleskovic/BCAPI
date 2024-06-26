import React, { useEffect, useState } from 'react';
import FormulaRow from './FormulaRow'
import FilterForm from './FilterForm'
import { Link } from 'react-router-dom';
import { fetchFormulas, postFormula, removeFormula, filteredFormulas } from './services';
import './FormulaRow.css'
import { useAuth } from './AuthProvider';



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
	const [deleteDiv, setDeleteDiv] = useState(false);
	const [formulaToDelete, setFormulaToDelete] = useState()

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
		setDeleteDiv(false)

	};
	const deleteApprove = (id) => {
		setDeleteDiv(true);
		setFormulaToDelete(id);
	}
	const deleteCancel = () => {
		setDeleteDiv(false)
	}
	const handleUpdateClick = (id) => {
		const formulaToEdit = rows.find((formula) => formula.id === id);
		setEditingFormula(formulaToEdit);
	};

	const filterFormulas = async (filter) => {
		const formulas = await filteredFormulas(filter)
		setRows(formulas)
	}
	const { user, logout } = useAuth();
	return (

		<div class="divRow">
			<FilterForm onSubmit={filterFormulas}></FilterForm>
			<table >
				<thead>
					<tr>
						<th>Name</th>
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
							onDelete={deleteApprove}
							onUpdate={() => handleUpdateClick(formula.id)}
						/>
					))}
				</tbody>
			</table>
			{
				deleteDiv && (
					<div className='deleteDiv'>
						<h2>Sure you want to delete this item ?</h2>
						<button type="button" style={{ color: "green" }} onClick={() => deleteFormula(formulaToDelete)}>Yes</button>
						<button type="button" style={{ color: "red" }} onClick={deleteCancel}>No</button>
					</div>
				)
			}
			<data value="">

			</data>
			{user && (<div>
				<Link to={'/insert'} className="nav-link">
					<button className="buttonForm" >Insert</button>
				</Link>
			</div>
			)}
		</div >
	);
};

export default FormulaTable;