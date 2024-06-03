/*
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
            formula.Id1 = newID;
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
        
            return formula;
        }
    }
}
*/