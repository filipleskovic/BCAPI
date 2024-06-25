import { useState } from "react"



const FilterForm = ({ onSubmit }) => {

	const [formData, setFormData] = useState({});

	const handleChange = (e) => {
		const { name, value } = e.target;
		setFormData({
			...formData, [name]: value
		});
	};

	const handleSubmit = (e) => {
		e.preventDefault();
		const filter = {
			...formData
		};
		onSubmit(filter);
		setFormData({ name: '', minTopSpeed: '', maxTopSpeed: '', orderBy: '', orderDirection: '' });
	};

	return (
		<form id="insertFormula" onSubmit={handleSubmit}>
			<input type="text" name="name" placeholder="FilterByNname" value={formData.fileredName} onChange={handleChange} />
			<input type="number" name="minTopSpeed" placeholder="MinTopSpeed" value={formData.minTopSpeed} onChange={handleChange} />
			<input type="number" name="maxTopSpeed" placeholder="MaxTopSpeed" value={formData.maxTopSpeed} onChange={handleChange} />
			<input type="text" name="orderBy" placeholder="orderBy" value={formData.orderBy} onChange={handleChange} />
			<input type="text" name="orderDirection" placeholder="DESC/ASC" value={formData.direction} onChange={handleChange} />
			<button type="submit">Filter</button>
		</form>
	)
}
export default FilterForm;
