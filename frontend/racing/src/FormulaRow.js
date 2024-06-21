import React from 'react';
import './FormulaRow.css';
function Button({ text }) {
	<button>{text}</button>
}
const FormulaRow = ({ formulaRow, onDelete, onUpdate }) => {
	const handleDelete = () => {
		onDelete(formulaRow.id);
	};
	const handleUpdate = () => {
		onUpdate(formulaRow.id);
	}

	return (
		<tr>
			<td>{formulaRow.id}</td>
			<td>{formulaRow.name}</td>
			<td>{formulaRow.horsepower}</td>
			<td>{formulaRow.topspeed}</td>
			<td>{formulaRow.acceleration}</td>
			<td>
				<button onClick={handleUpdate}>Update</button>
				<button onClick={handleDelete}>Delete</button>
			</td>
		</tr>
	);
};

export default FormulaRow;
	