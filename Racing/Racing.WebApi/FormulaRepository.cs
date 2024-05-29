namespace Racing.WebApi
{
    public static class FormulaRepository
    {
        public static IList<Formula> formulas;

        static FormulaRepository()
        {
            formulas = new List<Formula>();
        }
        public static void AddFormula(Formula formula)
        {
            int newID = formulas.Any() ? formulas.Max(f => f.ID) + 1 : 1;
            formula.ID = newID;
            formulas.Add(formula);
        }
        public static void DeleteFormula(Formula formula)
        {
            formulas.Remove(formula);
        }
        public static Formula GetFormulaByiD(int id)
        {
            return formulas.FirstOrDefault(f => f.ID == id);
        }
        public static Formula UpdateFormula(Formula formula, Formula newFormula)
        {
            formula.Name = newFormula.Name;
            formula.Horsepower = newFormula.Horsepower;
            formula.TopSpeed = newFormula.TopSpeed;
            formula.Driver = newFormula.Driver;
            formula.Acceleration = newFormula.Acceleration;
            formula.IsPraticipating=newFormula.IsPraticipating;
            formula.Driver.FirstName=newFormula.Driver.FirstName;
            formula.Driver.LastName=newFormula.Driver.LastName;
            formula.Driver.Age= newFormula.Driver.Age;
            return formula;
        }
    }
}
