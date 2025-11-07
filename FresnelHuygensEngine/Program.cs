using System;
using System.IO;

class Program
{
    static void Main()
    {
        Aperture aperture = new Aperture(2, 2, 100);

        Color redLight   = new Color(620);
        Color greenLight = new Color(540);
        Color blueLight  = new Color(470);

        aperture.FillAperture(new Color(590));  // any wavelength, zero intensity

        
        aperture.PPM_Output("/Users/jackson.rankin/Developer/Fresnel_Huygens_Engine/FresnelHuygensEngine/Output/ColorTest.ppm");

    }
}
