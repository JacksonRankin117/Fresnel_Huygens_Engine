using System;

public class Color
{
    public byte R { get; }
    public byte G { get; }
    public byte B { get; }
    public double? Wavelength_nm { get; } // optional, nullable

    public Color(int r, int g, int b, double? wavelength_nm = null)
    {
        R = (byte)r;
        G = (byte)g;
        B = (byte)b;
        Wavelength_nm = wavelength_nm;
    }
}
