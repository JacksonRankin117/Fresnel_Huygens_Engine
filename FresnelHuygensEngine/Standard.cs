using System;
using System.IO;

class Standard
{
    // Standard properties
    public float width_cm { get; }    // Width, in centimeters
    public float height_cm { get; }   // Height, in centimeters
    public int ppcm { get; }          // Resolution, in pixels per centimeter
    public int pixel_width { get; }   // Width, measured in pixels.
    public int pixel_height { get; }  // Height, measured in pixels
    private byte[,,] rgb_vals;

    public Standard(float width_cm, float height_cm, int ppcm)  // Basic constructor
    {
        this.width_cm = width_cm;
        this.height_cm = height_cm;
        this.ppcm = ppcm;

        pixel_width = (int)(ppcm * width_cm);
        pixel_height = (int)(ppcm * height_cm);

        rgb_vals = new byte[pixel_width, pixel_height, 3];
    }

    public void FillStandard(int r, int g, int b)
    {
        byte rb = (byte)r;
        byte gb = (byte)g;
        byte bb = (byte)b;

        for (int x = 0; x < pixel_width; x++)
        for (int y = 0; y < pixel_height; y++)
        {
            rgb_vals[x, y, 0] = rb;
            rgb_vals[x, y, 1] = gb;
            rgb_vals[x, y, 2] = bb;
        }
    }

    public void DrawRectangle()
    {
        
    }
    public void DrawCircle(double x0, double y0, double r0)
    {
        
    }
    public void PPM_Output(string filename)
    {
        using (var writer = new StreamWriter("output.ppm"))
        {
            writer.WriteLine("P3");
            writer.WriteLine($"{pixel_width} {pixel_height}");
            writer.WriteLine("255");

            for (int y = 0; y < pixel_height; y++)
            {
                for (int x = 0; x < pixel_width; x++)
                {
                    byte r = rgb_vals[y, x, 0];
                    byte g = rgb_vals[y, x, 1];
                    byte b = rgb_vals[y, x, 2];

                    writer.Write($"{r} {g} {b} ");
                }
                writer.WriteLine();
            }
        }
    }
}
