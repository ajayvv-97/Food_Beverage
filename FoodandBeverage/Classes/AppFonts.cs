using System;
using System.Drawing;

namespace FoodandBeverage.Classes
{
    public static class AppFonts
    {
        private const string PreferredFontName = "Poppins ExtraBold";
        private const string FallbackFontName = "Microsoft Sans Serif";

        /// <summary>
        /// Gets the default app font (12pt).
        /// </summary>
        public static Font DefaultFont => GetFont(12F);

        /// <summary>
        /// Returns a font with the given size using the preferred app font (Poppins ExtraBold),
        /// and falls back to Microsoft Sans Serif if unavailable.
        /// </summary>
        public static Font GetFont(float size)
        {
            try
            {
                return new Font(PreferredFontName, size, FontStyle.Bold, GraphicsUnit.Point);
            }
            catch
            {
                return new Font(FallbackFontName, size, FontStyle.Bold, GraphicsUnit.Point);
            }
        }
    }
}
