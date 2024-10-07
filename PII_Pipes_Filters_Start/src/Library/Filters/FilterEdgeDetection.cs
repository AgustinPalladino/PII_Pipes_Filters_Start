using System;
using System.Drawing;

namespace CompAndDel.Filters
{
    /// <summary>
    /// Implementa un filtro de detección de bordes que resalta los bordes de una imagen.
    /// </summary>
    public class FilterEdgeDetection : IFilter
    {
        public IPicture Filter(IPicture image)
        {
            // Crear una nueva imagen para almacenar el resultado
            IPicture result = image.Clone();

            // Iterar sobre cada píxel de la imagen (excepto los bordes)
            for (int x = 1; x < image.Width - 1; x++)
            {
                for (int y = 1; y < image.Height - 1; y++)
                {
                    // Obtener los colores de los píxeles vecinos
                    Color current = image.GetColor(x, y);
                    Color right = image.GetColor(x + 1, y);
                    Color bottom = image.GetColor(x, y + 1);

                    // Calcular la diferencia de color entre el píxel actual y sus vecinos
                    int diffR = Math.Abs(current.R - right.R) + Math.Abs(current.R - bottom.R);
                    int diffG = Math.Abs(current.G - right.G) + Math.Abs(current.G - bottom.G);
                    int diffB = Math.Abs(current.B - right.B) + Math.Abs(current.B - bottom.B);

                    // Promediar las diferencias para obtener la intensidad del borde
                    int intensity = (diffR + diffG + diffB) / 3;

                    // Definir un umbral para decidir si es un borde o no
                    if (intensity > 50) // Puedes ajustar este umbral
                    {
                        result.SetColor(x, y, Color.Black); // Borde -> Negro
                    }
                    else
                    {
                        result.SetColor(x, y, Color.White); // No borde -> Blanco
                    }
                }
            }

            return result;
        }
    }
}