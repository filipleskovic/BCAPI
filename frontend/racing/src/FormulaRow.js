import React, { Component } from 'react';


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
				<td>{formulaRow.name}</td>
				<td>{formulaRow.horsepower}</td>
				<td>{formulaRow.topSpeed}</td>
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
