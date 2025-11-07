using System;
using System.IO;

class Program
{
    static void Main()
    {
        Standard aperture = new Standard(2, 2, 100);

        aperture.FillStandard(0, 0, 0);  // Makes a black aperture
        aperture.DrawCircle(0, 0, 1, 255, 255, 255);
        aperture.DrawCircle(0, 0, .9, 255, 0, 255);
        aperture.DrawCircle(0, 0, .8, 0, 255, 255);
        aperture.DrawCircle(0, 0, .7, 255, 0, 0);
        aperture.DrawCircle(0, 0, .6, 0, 0, 255);
        aperture.DrawCircle(0, 0, .5, 0, 255, 0);
        aperture.DrawCircle(0, 0, .4, 0, 0, 0);
        aperture.PPM_Output("../ColorTest.ppm");
    }
}
