using System;

class Photon : Vec3
{   
    // Constants
    const double pi = 3.141592653589792328;
    const double c = 299792458.0;
    const double e = 2.71828182846;

    // Photon properties
    public double lambda { get; }  // Holds the value for wavelength
    public double delta { get; }  // Holds the value for phase at the aperture
    

    public Photon(double _x, double _y, double _z) : base(_x, _y, _z) { }  // Basic constructor

    public double Phase(double mag, double delta)
    {
        double k = 2 * pi / lambda;  // Wave number

        double delta_f = delta + (k * mag);
        return delta_f;
    }
}