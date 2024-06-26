import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import './FormulaRow.css';
import { AuthContext } from './AuthProvider';

class FormulaRow extends Component {
	static contextType = AuthContext;

	handleDelete = () => {
		const { onDelete, formulaRow } = this.props;
		onDelete(formulaRow.id);
	};

	handleUpdate = () => {
		const { onUpdate, formulaRow } = this.props;
		onUpdate(formulaRow.id);
	};

	render() {
		const { formulaRow } = this.props;
		const { user, logout } = this.context;

		return (
			<tr>
				<td className="table-cell">{formulaRow.name}</td>
				<td className="table-cell">{formulaRow.horsepower}</td>
				<td className="table-cell">{formulaRow.topSpeed}</td>
				<td className="table-cell">{formulaRow.acceleration}</td>
				{user && (<td className="table-cell">
					<Link to={`/edit/${formulaRow.id}`} className="nav-link">
						<button className="button" onClick={this.handleUpdate}>Update</button>
					</Link>
					<button className="button" onClick={this.handleDelete}>Delete</button>
				</td>)}
			</tr>
		);
	}
}

export default FormulaRow;
