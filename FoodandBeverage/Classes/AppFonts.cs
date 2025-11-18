using System;
using System.Drawing;

namespace FoodandBeverage.Classes
{
    public static class AppFonts
    {
        private const string PreferredFontName = "Poppins ExtraBold";
        private const string FallbackFontName = "Microsoft Sans Serif";

        /// <summary>
        /// Default app font (12pt, Bold)
        /// </summary>
        public static Font DefaultFont => GetFont(12F);

        /// <summary>
        /// Returns a font with given size and style.
        /// Default style = Bold. 
        /// Strikeout or any other style works only if passed explicitly.
        /// </summary>
        public static Font GetFont(float size, FontStyle style = FontStyle.Bold)
        {
            try
            {
                return new Font(PreferredFontName, size, style, GraphicsUnit.Point);
            }
            catch
            {
                return new Font(FallbackFontName, size, style, GraphicsUnit.Point);
            }
        }
    }
}
