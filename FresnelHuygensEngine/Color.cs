using System;

public class Color
{
    public byte R { get; private set; }
    public byte G { get; private set; }
    public byte B { get; private set; }

    public double Wavelength_nm { get; private set; }

    public Color(double wavelength_nm, double amplitude = 1.0)
    {
        Wavelength_nm = wavelength_nm;
        SetRGBFromWavelength(wavelength_nm, amplitude);
    }

    private void SetRGBFromWavelength(double lambda, double amplitude)
    {
        double r = Gaussian(lambda, 630, 35);  // Red
        double g = Gaussian(lambda, 540, 30);  // Green
        double b = Gaussian(lambda, 460, 20);  // Blue

        // Normalize as before
        double max = Math.Max(r, Math.Max(g, b));
        if (max > 0)
        {
            r = r / max * 255.0 * amplitude;
            g = g / max * 255.0 * amplitude;
            b = b / max * 255.0 * amplitude;
        }
        else
        {
            r = g = b = 0;
        }

        R = (byte)Math.Clamp(r, 0, 255);
        G = (byte)Math.Clamp(g, 0, 255);
        B = (byte)Math.Clamp(b, 0, 255);
    }

    private double Gaussian(double x, double mean, double sigma)
    {
        double diff = x - mean;
        return Math.Exp(-0.5 * diff * diff / (sigma * sigma));
    }
}
