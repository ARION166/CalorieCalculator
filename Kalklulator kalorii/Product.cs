namespace Kalkulator_kalorii
{
    internal class Product
    {
        public string Name { get; set; }
        public int Carbohydrates { get; set; }
        public int Fat { get; set; }
        public int TotalCalories { get; internal set; }
        public int Protein { get; internal set; }
        public int OriginalProtein { get; set; }
        public int OriginalCarbohydrates { get; set; }
        public int OriginalFat { get; set; }
        public int OriginalTotalCalories { get; set; }
    }
}