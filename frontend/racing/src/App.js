import './App.css';
import FormulaTable from './FormulaTable'
import Form from './Form'
import React from "react";
import { Routes, Route, Link } from "react-router-dom";
import "bootstrap/dist/css/bootstrap.min.css";
import EditFormulaForm from './EditFormulaForm';

function App() {

  return (
    <div>
      <nav className="navbar navbar-expand navbar-dark bg-dark">
        <div className="navbar-nav mr-auto">
          <li className="nav-item">
            <Link to={"/formulas"} className="nav-link">
              Formulas
            </Link>
          </li>
          <li className="nav-item">
            <Link to={"/drivers"} className="nav-link">
              Drivers
            </Link>
          </li>
        </div>
      </nav>

      <div className="container mt-3">
        <Routes>
          <Route path="/" element={<FormulaTable />} />
          <Route path="/formulas" element={<FormulaTable />} />
          <Route path="/edit/:formulaId" element={<EditFormulaForm />} />
        </Routes>
      </div>
    </div>

  );
}
export default App;
