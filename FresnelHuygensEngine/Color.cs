using System;

public class Color
{
    public byte R { get; private set; }
    public byte G { get; private set; }
    public byte B { get; private set; }

    public double Wavelength_nm { get; private set; }
    public double Amplitude { get; private set; }

    // wavelength_nm: use 0 to indicate "no wavelength / black"
    // amplitude: 0..1
    public Color(double wavelength_nm, double amplitude = 1.0)
    {
        Wavelength_nm = wavelength_nm;
        Amplitude = Math.Clamp(amplitude, 0.0, 1.0);
        SetRGBFromWavelength(wavelength_nm, Amplitude);
    }

    private void SetRGBFromWavelength(double lambda, double amplitude)
    {
        if (lambda <= 0 || amplitude <= 0)
        {
            R = G = B = 0;
            return;
        }

        double r = Gaussian(lambda, 630, 35);  // Red
        double g = Gaussian(lambda, 540, 30);  // Green
        double b = Gaussian(lambda, 460, 20);  // Blue

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

        R = (byte)Math.Clamp((int)Math.Round(r), 0, 255);
        G = (byte)Math.Clamp((int)Math.Round(g), 0, 255);
        B = (byte)Math.Clamp((int)Math.Round(b), 0, 255);
    }

    private double Gaussian(double x, double mean, double sigma)
    {
        double diff = x - mean;
        return Math.Exp(-0.5 * diff * diff / (sigma * sigma));
    }

    public Color Scaled(double amplitude)
    {
        return new Color(this.Wavelength_nm, amplitude);
    }
}
