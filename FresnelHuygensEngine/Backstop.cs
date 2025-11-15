using System;
using System.IO;
using System.Numerics;

public class Backstop
{
    private double pixel_size; // meters per pixel
    private int Nx, Ny;

    public Backstop(Aperture aperture)
    {
        // aperture.ppcm is pixels per cm, so pixel_size = 1 cm / ppcm converted to meters
        pixel_size = 1.0 / aperture.ppcm / 100.0;
        Nx = aperture.pixel_width;
        Ny = aperture.pixel_height;
    }

    // Direct Fresnel–Huygens integral (slow for big Nx,Ny)
    public Complex[,] FresnelHuygens(Complex[,] apertureField, double z, double wavelength)
    {
        int nx = apertureField.GetLength(0);
        int ny = apertureField.GetLength(1);

        double k = 2.0 * Math.PI / wavelength;
        double dx = pixel_size;
        double dy = pixel_size;

        var outField = new Complex[nx, ny];

        // prefactor = exp(i k z) / (i lambda z)
        Complex prefactor = Complex.Exp(Complex.ImaginaryOne * k * z) / (new Complex(0, 1) * wavelength * z);

        // coordinates centered
        double cx = nx / 2.0;
        double cy = ny / 2.0;

        for (int u = 0; u < nx; u++)
        {
            double xObs = (u - cx) * dx;
            for (int v = 0; v < ny; v++)
            {
                double yObs = (v - cy) * dy;

                Complex sum = Complex.Zero;

                for (int x = 0; x < nx; x++)
                {
                    double xSrc = (x - cx) * dx;
                    for (int y = 0; y < ny; y++)
                    {
                        double ySrc = (y - cy) * dy;

                        // Fresnel approx phase term
                        double r2 = (xObs - xSrc) * (xObs - xSrc) + (yObs - ySrc) * (yObs - ySrc);
                        double phase = k * r2 / (2.0 * z);
                        Complex kernel = Complex.Exp(Complex.ImaginaryOne * phase);

                        sum += apertureField[x, y] * kernel;
                    }
                }

                // multiply by prefactor and pixel area
                outField[u, v] = prefactor * sum * (dx * dy);
            }
        }

        return outField;
    }

    // (Optional) your FFT-based FresnelFFT can remain but must implement FFT2D.
    // The original FresnelFFT in your code is left here if you want to implement FFT2D later.

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
                if (c == null)
                    writer.Write("0 0 0 ");
                else
                    writer.Write($"{c.R} {c.G} {c.B} ");
            }
            writer.WriteLine();
        }
    }

    // Normalize intensities to 0..1
    public static double[,] NormalizeIntensity(Complex[,] field)
    {
        int nx = field.GetLength(0);
        int ny = field.GetLength(1);
        double[,] I = new double[nx, ny];
        double max = 0.0;

        for (int x = 0; x < nx; x++)
            for (int y = 0; y < ny; y++)
            {
                double val = field[x, y].Magnitude * field[x, y].Magnitude; // |E|^2
                I[x, y] = val;
                if (val > max) max = val;
            }

        if (max <= 0.0) return I; // all zeros

        for (int x = 0; x < nx; x++)
            for (int y = 0; y < ny; y++)
                I[x, y] /= max;

        return I;
    }
}
