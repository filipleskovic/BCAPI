import React, { useEffect, useState } from 'react';
import Formula from './FormulaRow'
import "./Form.css"

export default function Form({ onSubmit }) {
	const [formData, setFormData] = useState({});

	const handleChange = (e) => {
		const { name, value } = e.target;
		setFormData({...formData,[name]: value
		});
	};

	const handleSubmit = (e) => {
		const newFormula = {
			id: Math.floor(1000 + Math.random() * 9000),
			...formData
		};
		onSubmit(newFormula);
		setFormData({ name: '', horsepower: '', topspeed: '', acceleration: '' }); 
	};

	return (
		<div>
			<form id="insertFormula" onSubmit={handleSubmit}>
				<input type="text" name="name" placeholder="Ime Formule" value={formData.name} onChange={handleChange} required />
				<input type="number" name="horsepower" placeholder="Konjska snaga" value={formData.horsepower} onChange={handleChange} required />
				<input type="number" name="topspeed" placeholder="Brzina" value={formData.topspeed} onChange={handleChange} required />
				<input type="number" name="acceleration" placeholder="Akceleracija" value={formData.acceleration} onChange={handleChange} required />
				<button type="submit">Submit</button>
			</form>
		</div>
	);
}