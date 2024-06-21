import React, { Component } from 'react';
import './FormulaRow.css';

class FormulaRow extends Component {
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

		return (
			<tr>
				<td>{formulaRow.id}</td>
				<td>{formulaRow.name}</td>
				<td>{formulaRow.horsepower}</td>
				<td>{formulaRow.topspeed}</td>
				<td>{formulaRow.acceleration}</td>
				<td>
					<button onClick={this.handleUpdate}>Update</button>
					<button onClick={this.handleDelete}>Delete</button>
				</td>
			</tr>
		);
	}
}

export default FormulaRow;
