const formulas = [
	{
		id: 3323,
		name: "Ferrari",
		horsepower: 760,
		topspeed: 401,
		acceleration: 2.5
	},
	{
		id: 2321,
		name: "Mercedes",
		horsepower: 740,
		topspeed: 420,
		acceleration: 3.1,
	},
	{
		id: 9916,
		name: "Redbull",
		horsepower: 730,
		topspeed: 399,
		acceleration: 2.3
	}
]
localStorage.setItem('formulas', JSON.stringify(formulas));
document.addEventListener('DOMContentLoaded', function () {
	let url = window.location.href;
	if (url.includes('formula.html')) {
		createFormulaTable();
		insertFormula();
	}
});


function createFormulaTable() {
	let formule = JSON.parse(localStorage.getItem('formulas')) || [];
	let tableBody = document.getElementById('id');
	tableBody.innerHTML = '';
	for (let i = 0; i < formule.length; i++) {
		let row = document.createElement('tr');
		let id = document.createElement('td');
		let name = document.createElement('td');
		let horsepower = document.createElement('td');
		let acceleration = document.createElement('td');
		let topspeed = document.createElement('td');
		let updateButton = document.createElement('button');
		let deleteButton = document.createElement('button');


		deleteButton.addEventListener('click', function () {
			deleteFormula(formule[i].id);
		});
		updateButton.addEventListener('click', function () {
			showUpdateForm(formule[i], i)
		});
		id.textContent = formule[i].id;
		name.textContent = formule[i].name;
		horsepower.textContent = formule[i].horsepower;
		acceleration.textContent = formule[i].acceleration;
		topspeed.textContent = formule[i].topspeed;
		updateButton.textContent = 'Update';
		deleteButton.textContent = 'Delete';
		row.appendChild(id)
		row.appendChild(name);
		row.appendChild(horsepower);
		row.appendChild(topspeed);
		row.appendChild(acceleration);
		row.append(updateButton, deleteButton)
		tableBody.appendChild(row);
	}

}


function insertFormula() {
	const forma = document.getElementById('insertFormula');
	forma.addEventListener('submit', function (event) {
		event.preventDefault();

		let name = document.getElementById('name').value;
		let horsepower = document.getElementById('horsepower').value;
		let topspeed = document.getElementById('topspeed').value;
		let acceleration = document.getElementById('acceleration').value;
		let id = Math.floor(1000 + Math.random() * 9000);

		let formula = {
			id: id,
			name: name,
			horsepower: horsepower,
			topspeed: topspeed,
			acceleration: acceleration
		}
		let formule = JSON.parse(localStorage.getItem('formulas'));
		formule.push(formula)
		localStorage.setItem('formulas', JSON.stringify(formule));
		createFormulaTable();
		forma.reset();
	});
}

function deleteFormula(formulaId) {
	let formule = JSON.parse(localStorage.getItem('formulas')) || [];
	formule = formule.filter(function (formula) {
		return formula.id !== formulaId;
	})
	localStorage.setItem('formulas', JSON.stringify(formule));
	createFormulaTable();
}


function updateFormula(formula, index) {
	debugger;
	let formule = JSON.parse(localStorage.getItem('formulas')) || [];
	let name = document.getElementById('updateName').value;
	let horsepower = document.getElementById('updateHorsepower').value;
	let topspeed = document.getElementById('updateTopspeed').value;
	let acceleration = document.getElementById('updateAcceleration').value;
	formule.splice(index, 1, {
		id: formula.id,
		name: name,
		horsepower: horsepower,
		topspeed: topspeed,
		acceleration: acceleration
	});
	console.log(formule[index])
	localStorage.setItem('formulas', JSON.stringify(formule));
	createFormulaTable();


}
function showUpdateForm(formula, index) {
	debugger;
	console.log("JA");
	let updateForm = document.getElementById('updateForm'); /*div */
	let closeButton = document.getElementById('closeButton');
	let updateButton = document.getElementById('updateButton');
	let id = document.getElementById('spanformulaid')
	id.textContent = formula.id;
	updateForm.style.display = 'block';

	updateButton.addEventListener('click', function () {
		updateFormula(formula, index)
		updateForm.style.display = 'none'
	});
	closeButton.addEventListener('click', function () {
		updateForm.style.display = 'none';
	});
}


