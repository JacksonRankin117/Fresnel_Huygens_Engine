class Photon : Vec3
{   
    public double lambda { get; }  // Holds the value for wavelength
    public double delta { get; }  // Holds the value for phase at the aperture
    public Photon(double _x, double _y, double _z) : base(_x, _y, _z)  // Basic constructor
    {
    }

    
}