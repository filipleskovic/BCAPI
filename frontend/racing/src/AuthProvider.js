import React, { createContext, useState, useContext } from 'react';
import { useNavigate } from 'react-router-dom';

export const AuthContext = createContext();

export const useAuth = () => {
	return useContext(AuthContext);
};

export const AuthProvider = ({ children }) => {
	const navigate = useNavigate();
	const [user, setUser] = useState(null);

	const login = (username, password) => {
		if (username === "user" && password === "password") {
			setUser({ username });
			navigate('/');
		} else {
			alert("Invalid credentials");
		}
	};

	const register = (username, password) => {
		if (username && password) {
			setUser({ username });
		}
	};

	const logout = () => {
		setUser(null);
	};

	return (
		<AuthContext.Provider value={{ user, login, register, logout }}>
			{children}
		</AuthContext.Provider>
	);
};
