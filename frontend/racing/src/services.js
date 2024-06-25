

import axios from 'axios';

const baseURL = 'https://localhost:7014/Formula/';

const formulaClient = axios.create({
	baseURL,
	headers: {
		'Content-Type': 'application/json'
	}
});

export const fetchFormulas = async () => {
	try {
		const response = await formulaClient.get('Formulas');
		return response.data;
	} catch (error) {
		console.error('Error fetching formulas:', error);
		throw error;
	}
};

export const postFormula = async (newFormula) => {
	try {
		const response = await formulaClient.post('AddNewFormula', newFormula);
		console.log('Formula successfully added:', response.data);
	} catch (error) {
		console.error('Error adding the formula:', error);
		throw error;
	}
};

export const putFormula = async (newFormula) => {
	try {
		const response = await formulaClient.put(`UpdateFormula/${newFormula.id}`, newFormula);
	} catch (error) {
		console.error('There was an error deleting the formula!', error);
	}
}
export const removeFormula = async (id) => {
	try {
		const response = formulaClient.delete(`DeleteFormula/${id}`);
		console.log('Response:', response.data);

	} catch (error) {
		console.error('There was an error deleting the formula!', error);
	}
}
export const filteredFormulas = async (filter) => {
	try {
		const response = await formulaClient.get('FilteredFormulas', { params: filter });
		return response.data;
	} catch (error) {
		console.error('There was an error filtering the formulas!', error);
		throw error;
	}
}