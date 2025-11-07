using System;
using System.IO;

class Program
{
    static void Main()
    {
        Standard aperture = new Standard(2, 2, 100);

        Color redLight   = new Color(620);
        Color greenLight = new Color(540);
        Color blueLight  = new Color(470);

        aperture.FillStandard(new Color(0)); // black
        aperture.DrawCircle(1.0, redLight);
        aperture.DrawCircle(0.7, greenLight);
        aperture.DrawCircle(0.5, blueLight);
        aperture.PPM_Output("ColorTest.ppm");

    }
}
