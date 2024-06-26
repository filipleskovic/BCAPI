import './App.css';
import FormulaTable from './FormulaTable'
import React from "react";
import { Routes, Route, Link } from "react-router-dom";
import "bootstrap/dist/css/bootstrap.min.css";
import EditPage from './EditPage';
import InsertPage from './InsertPage';
import HomePage from './HomePage';
import DriverTable from './DriverTable';
import { AuthProvider } from './AuthProvider';
import Login from './Login'
import Navigation from './Navigation';

function App() {

  return (
    <AuthProvider>
      <Navigation />
      <div className="container mt-3">
        <Routes>
          <Route path="/" element={<HomePage />} />
          <Route path="/formulas" element={<FormulaTable />} />
          <Route path="/edit/:formulaId" element={<EditPage />} />
          <Route path="/insert/" element={<InsertPage />} />
          <Route path="/drivers" element={<DriverTable />} />
          <Route path="/login" element={<Login />} />
        </Routes>
      </div>
    </AuthProvider >
  );
}

export default App;
