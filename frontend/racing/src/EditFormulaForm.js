import React, { useState, useEffect } from 'react';
import axios from 'axios';

const EditFormulaForm = ({ formula, onUpdate, onCancel }) => {
	const [editedFormula, setEditedFormula] = useState({
		id: formula.id,
		name: formula.name,
		horsepower: formula.horsepower,
		topspeed: formula.topspeed,
		acceleration: formula.acceleration,
	});

	useEffect(() => {
		setEditedFormula({
			id: formula.id,
			name: formula.name,
			horsepower: formula.horsepower,
			topSpeed: formula.topSpeed,
			acceleration: formula.acceleration,
		});
	}, [formula]);

	const handleChange = (e) => {
		const { name, value } = e.target;
		setEditedFormula({ ...editedFormula, [name]: value });
	};

	const handleUpdate = () => {
		onUpdate(editedFormula);
	};

	return (
		<div>
			<form>
				<label>Name</label>
				<input type="text" name="name" value={editedFormula.name} onChange={handleChange} />
				<label>Horsepower:</label>
				<input type="number" name="horsepower" value={editedFormula.horsepower} onChange={handleChange} />
				<label>Topspeed:</label>
				<input type="number" name="topSpeed" value={editedFormula.topSpeed} onChange={handleChange} />
				<label>Acceleration:</label>
				<input type="number" name="acceleration" value={editedFormula.acceleration} onChange={handleChange} />
				<button type="button" onClick={handleUpdate}>Update</button>
				<button type="button" onClick={onCancel}>Cancel</button>
			</form>
		</div>
	);
};

export default EditFormulaForm;
