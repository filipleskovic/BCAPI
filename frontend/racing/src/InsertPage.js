import { useNavigate } from 'react-router-dom';
import './App.css';
import React, { useEffect } from "react";
import Form from './Form'
import { postFormula } from './services';
import { useAuth } from './AuthProvider';

const InsertPage = () => {
	const { user, logout } = useAuth();
	const navigate = useNavigate();
	const addFormula = async (newFormula) => {
		await postFormula(newFormula)
		navigate('/')
	};

	useEffect(() => {
		if (!user) {
			navigate('/login')
		}
	}, []);

	return (
		user && (
			<div>
				<Form onSubmit={addFormula} />
			</div>

		)
	)
}
export default InsertPage