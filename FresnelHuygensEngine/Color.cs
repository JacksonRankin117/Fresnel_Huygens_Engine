using System;

public class Color
{
    public byte R { get; private set; }
    public byte G { get; private set; }
    public byte B { get; private set; }

    public double Wavelength_nm { get; private set; }

    public Color(double wavelength_nm)
    {
        Wavelength_nm = wavelength_nm;
        SetRGBFromWavelength(wavelength_nm);
    }

    private void SetRGBFromWavelength(double lambda)
    {
        // Gaussian parameters: (peak wavelength, standard deviation)
        // Values chosen to roughly match human perception
        double r = Gaussian(lambda, 610, 35);  // Red peaks ~610 nm
        double g = Gaussian(lambda, 550, 30);  // Green peaks ~550 nm
        double b = Gaussian(lambda, 460, 20);  // Blue peaks ~460 nm

        // Normalize to 0-255
        double max = Math.Max(r, Math.Max(g, b));
        if (max > 0)
        {
            r = r / max * 255.0;
            g = g / max * 255.0;
            b = b / max * 255.0;
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
