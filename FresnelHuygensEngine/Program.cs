using System;
using System.Numerics;

class Program
{
    static void Main()
    {
        Aperture aperture = new Aperture(2, 2, 100);
        aperture.FillAperture(new Color(0, 0));  // black background
        aperture.DrawCircle(0, 0, 0.5, new Color(620)); // 620 nm red

        // Convert to complex field
        Complex[,] apertureField = new Complex[aperture.pixel_width, aperture.pixel_height];
        for (int x = 0; x < aperture.pixel_width; x++)
            for (int y = 0; y < aperture.pixel_height; y++)
            {
                Color c = aperture.GetPixel(x, y);
                double amplitude = c.Wavelength_nm > 0 ? 1.0 : 0.0;
                apertureField[x, y] = new Complex(amplitude, 0);
            }

        Backstop backstop = new Backstop(aperture);
        double wavelength = 620e-9;
        double distance = 10;

        var field = backstop.FresnelHuygens(apertureField, distance, wavelength);
        var intensity = Backstop.NormalizeIntensity(field);

        // Map intensity back to RGB
        Color[,] rgbPattern = new Color[aperture.pixel_width, aperture.pixel_height];
        for (int x = 0; x < aperture.pixel_width; x++)
            for (int y = 0; y < aperture.pixel_height; y++)
                rgbPattern[x, y] = new Color(wavelength, intensity[x, y]);
        aperture.PPM_Output("/Users/jackson.rankin/Developer/Fresnel_Huygens_Engine/FresnelHuygensEngine/Output/Aperture.ppm");
        Backstop.PPM_Output("/Users/jackson.rankin/Developer/Fresnel_Huygens_Engine/FresnelHuygensEngine/Output/Diffraction.ppm", rgbPattern);
    }
}
