import { useAuth } from './AuthProvider';
import React from "react";
import { Routes, Route, Link } from "react-router-dom";

const Navigation = () => {
	const { user, logout } = useAuth();

	return (
		<nav className="navbar navbar-expand navbar-dark bg-dark">
			<div className="navbar-nav mr-auto">
				<li className="nav-item">
					<Link to="/" className="nav-link">
						Home
					</Link>
				</li>
				<li className="nav-item">
					<Link to="/formulas" className="nav-link">
						Formulas
					</Link>
				</li>
				<li className="nav-item">
					<Link to="/drivers" className="nav-link">
						Drivers
					</Link>
				</li>
				{!user && (
					<li className="nav-item">
						<Link to="/login" className="nav-link">
							Login
						</Link>
					</li>
				)}
				{user && (
					<li className="nav-item">
						<span className="nav-link" onClick={logout}>
							Logout
						</span>
					</li>
				)}
			</div>
		</nav>
	);
};
export default Navigation;