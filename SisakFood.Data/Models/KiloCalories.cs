namespace SisakFood.Data.Models
{
    public static class KiloCalories
    {
        public const int CARBOHYDRATES = 4;
        public const int FAT = 9;
        public const int PROTEIN = 4;
        public const int ALCOHOL = 7;

        public static int CalculateKiloCalories(this Nutrition nutrition)
        {
            return nutrition.Carbohydrates * CARBOHYDRATES +
                nutrition.Fat * FAT +
                nutrition.Protein * PROTEIN +
                nutrition.Alcohol * ALCOHOL;
        }
    }
}