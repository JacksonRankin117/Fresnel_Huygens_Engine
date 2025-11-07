using System;
using System.IO;
using System.Numerics;

public class Backstop
{
    private double pixel_size; // meters per pixel

    public Backstop(Aperture aperture)
    {
        pixel_size = 1.0 / aperture.ppcm / 100.0;
    }

    public Complex[,] FresnelHuygens(Complex[,] apertureField, double dist, double wavelength)
    {
        int W = apertureField.GetLength(0);
        int H = apertureField.GetLength(1);
        Complex[,] field = new Complex[W, H];

        for (int x2 = 0; x2 < W; x2++)
            for (int y2 = 0; y2 < H; y2++)
            {
                Complex sum = Complex.Zero;
                for (int x1 = 0; x1 < W; x1++)
                    for (int y1 = 0; y1 < H; y1++)
                    {
                        double dx = (x2 - x1) * pixel_size;
                        double dy = (y2 - y1) * pixel_size;
                        double r = Math.Sqrt(dx * dx + dy * dy + dist * dist);
                        double phase = 2.0 * Math.PI * r / wavelength;

                        sum += apertureField[x1, y1] * Complex.Exp(Complex.ImaginaryOne * phase) / r;
                    }
                field[x2, y2] = sum;
            }
        return field;
    }

    public static double[,] NormalizeIntensity(Complex[,] field)
    {
        int W = field.GetLength(0);
        int H = field.GetLength(1);
        double[,] intensity = new double[W, H];
        double maxI = 0;

        for (int x = 0; x < W; x++)
            for (int y = 0; y < H; y++)
            {
                double val = field[x, y].Magnitude * field[x, y].Magnitude;
                intensity[x, y] = val;
                if (val > maxI) maxI = val;
            }

        if (maxI > 0)
        {
            for (int x = 0; x < W; x++)
                for (int y = 0; y < H; y++)
                    intensity[x, y] /= maxI;
        }
        return intensity;
    }

    public static void PPM_Output(string filename, Color[,] rgb_vals)
    {
        int W = rgb_vals.GetLength(0);
        int H = rgb_vals.GetLength(1);

        using var writer = new StreamWriter(filename);
        writer.WriteLine("P3");
        writer.WriteLine($"{W} {H}");
        writer.WriteLine("255");

        for (int y = 0; y < H; y++)
        {
            for (int x = 0; x < W; x++)
            {
                Color c = rgb_vals[x, y];
                writer.Write($"{c.R} {c.G} {c.B} ");
            }
            writer.WriteLine();
        }
    }
}