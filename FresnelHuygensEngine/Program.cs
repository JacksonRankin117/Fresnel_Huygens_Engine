using System;
using System.Numerics;

class Program
{
    static void Main()
    {
        Aperture aperture = new Aperture(1f, 1f, 100);
        double wavelength_m = 650e-9;   // physics wavelength in meters
        double wavelength_nm = 650e-9;   // for Color mapping (nm)

        aperture.FillAperture(new Color(0, 0));  // black background

        // Double slit
        // aperture.DrawRectangle(-0.1, -0.09, -0.3, 0.3, new Color(wavelength_nm, 1f));
        // aperture.DrawRectangle(0.09, 0.1, -0.3, 0.3, new Color(wavelength_nm, 1f));
        
        // Circle
        aperture.DrawCircle(0, 0, 0.2, new Color(wavelength_nm, 1f));

        // Convert to complex field
        Complex[,] apertureField = new Complex[aperture.pixel_width, aperture.pixel_height];
        for (int x = 0; x < aperture.pixel_width; x++)
        for (int y = 0; y < aperture.pixel_height; y++)
        {
            Color c = aperture.GetPixel(x, y);
            double amplitude = (c != null && c.Wavelength_nm > 0 && c.Amplitude > 0) ? c.Amplitude : 0.0;
            apertureField[x, y] = new Complex(amplitude, 0.0);
        }

        Backstop backstop = new Backstop(aperture);

        int multiple_width = 4;
        int multiple_height = 4;

        // expanded aperture field
        var expandedAperture = backstop.ExpandAperture(apertureField, multiple_width, multiple_height);

        double distance = 5; // meters

        // Fresnel propagation on expanded aperture
        var field = backstop.FresnelHuygens(expandedAperture, distance, wavelength_m);
        var intensity = Backstop.NormalizeIntensity(field); // 0..1

        int Nx = intensity.GetLength(0);
        int Ny = intensity.GetLength(1);

        // RGB output grid matches expanded size
        Color[,] rgbPattern = new Color[Nx, Ny];

        for (int x = 0; x < Nx; x++)
        for (int y = 0; y < Ny; y++)
        {
            double amp = Math.Sqrt(intensity[x, y]);
            rgbPattern[x, y] = new Color(wavelength_nm, amp);
        }

        // Write results
        aperture.PPM_Output("/Users/jackson.rankin/Developer/Fresnel_Huygens_Engine/FresnelHuygensEngine/Output/Test_Aperture_Circle.ppm");
        Backstop.PPM_Output("/Users/jackson.rankin/Developer/Fresnel_Huygens_Engine/FresnelHuygensEngine/Output/Test_Pattern_Circle.ppm", rgbPattern);

        Console.WriteLine("Done");
    }
}
