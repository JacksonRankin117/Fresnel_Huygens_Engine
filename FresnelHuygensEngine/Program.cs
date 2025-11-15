using System;
using System.Numerics;

class Program
{
    static void Main()
    {
        Aperture aperture = new Aperture(0.1f, 0.1f, 400);
        aperture.FillAperture(new Color(0, 0));  // black background
        aperture.DrawCircle(0, 0, 0.5, new Color(400, 0.001));

        // Convert to complex field (using wavelength presence as mask)
        Complex[,] apertureField = new Complex[aperture.pixel_width, aperture.pixel_height];
        for (int x = 0; x < aperture.pixel_width; x++)
        for (int y = 0; y < aperture.pixel_height; y++)
        {
            Color c = aperture.GetPixel(x, y);
            double amplitude = (c != null && c.Wavelength_nm > 0 && c.Amplitude > 0) ? c.Amplitude : 0.0;
            apertureField[x, y] = new Complex(amplitude, 0.0);
        }

        Backstop backstop = new Backstop(aperture);
        double wavelength_m = 400e-9;   // physics wavelength in meters
        double wavelength_nm = 400.0;   // for Color mapping (nm)
        double distance = 10;         // 10 cm observation plane (adjust as needed)

        // Use direct Fresnel-Huygens (slow for large arrays). For bigger arrays, switch to FFT-based implementation.
        var field = backstop.FresnelHuygens(apertureField, distance, wavelength_m);
        var intensity = Backstop.NormalizeIntensity(field); // normalized 0..1

        // Map intensity back to RGB using the wavelength in nm
        Color[,] rgbPattern = new Color[aperture.pixel_width, aperture.pixel_height];
        for (int x = 0; x < aperture.pixel_width; x++)
        for (int y = 0; y < aperture.pixel_height; y++)
        {
            double amp = Math.Sqrt(intensity[x, y]); // amplitude = sqrt(intensity)
            rgbPattern[x, y] = new Color(wavelength_nm, amp);
        }

        // Write the original aperture and diffraction pattern
        aperture.PPM_Output("/Users/jackson.rankin/Developer/Fresnel_Huygens_Engine/FresnelHuygensEngine/Output/Aperture.ppm");
        Backstop.PPM_Output("/Users/jackson.rankin/Developer/Fresnel_Huygens_Engine/FresnelHuygensEngine/Output/Diffraction.ppm", rgbPattern);

        Console.WriteLine("Done. Wrote Aperture.ppm and Diffraction.ppm");
    }
}
