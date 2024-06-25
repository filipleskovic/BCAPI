import React, { useEffect, useState } from 'react';
import "./Form.css"

export default function Form({ onSubmit }) {
	const [formData, setFormData] = useState({});

		const handleChange = (e) => {
			const { name, value } = e.target;
			setFormData({
				...formData, [name]: value
			});
		};

	const handleSubmit = (e) => {
		e.preventDefault();
		const newFormula = {
			...formData
		};
		onSubmit(newFormula);
		setFormData({ name: '', horsepower: '', topSpeed: '', acceleration: '' });
	};

	return (
		<div>
			<form id="insertFormula" onSubmit={handleSubmit}>
				<input type="text" name="name" placeholder="Ime Formule" value={formData.name} onChange={handleChange} required />
				<input type="number" name="horsepower" placeholder="Konjska snaga" value={formData.horsepower} onChange={handleChange} required />
				<input type="number" name="topSpeed" placeholder="Brzina" value={formData.topSpeed} onChange={handleChange} required />
				<input type="number" name="acceleration" placeholder="Akceleracija" value={formData.acceleration} onChange={handleChange} required />
				<button type="submit">Submit</button>
			</form>
		</div>
	);
}