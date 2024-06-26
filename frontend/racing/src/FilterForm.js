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
			<select name="orderBy" value={formData.orderBy} onChange={handleChange}>
				<option value="">OrderBy</option>
				<option value="name">Name</option>
				<option value="horsepower">Horsepower</option>
				<option value="topSpeed">TopSpeed</option>
				<option value="acceleration">Acceleration</option>
			</select>
			<select name="orderDirection" value={formData.orderDirection} onChange={handleChange}>
				<option value="">OrderDirection</option>
				<option value="ASC">ASC</option>
				<option value="DESC">DESC</option>
			</select>
			<button type="submit" className="buttonForm">Filter</button>
		</form>
	)
}
export default FilterForm;
