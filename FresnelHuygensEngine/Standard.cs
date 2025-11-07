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

    public void DrawRectangle(double x_min, double x_max, double y_min, double y_max, int r, int g, int b)
    {
        byte rb = (byte)r;
        byte gb = (byte)g;
        byte bb = (byte)b;

        // Convert cm → pixel coordinates
        int px_min = (int)((x_min + width_cm / 2) * ppcm);
        int px_max = (int)((x_max + width_cm / 2) * ppcm);
        int py_min = (int)((y_min + height_cm / 2) * ppcm);
        int py_max = (int)((y_max + height_cm / 2) * ppcm);

        // Clamp to image boundaries
        px_min = Math.Max(0, px_min);
        px_max = Math.Min(pixel_width - 1, px_max);
        py_min = Math.Max(0, py_min);
        py_max = Math.Min(pixel_height - 1, py_max);

        for (int x = px_min; x <= px_max; x++)
        for (int y = py_min; y <= py_max; y++)
        {
            rgb_vals[x, y, 0] = rb;
            rgb_vals[x, y, 1] = gb;
            rgb_vals[x, y, 2] = bb;
        }
    }

    public void DrawCircle(double x0, double y0, double r0, byte r, byte g, byte b)
    {
        /*
        x0 is the left/right position on the aperture standard in cm
        y0 is the up-down position on the aperture standard in cm

        r0 is the radius of the circle, also in cm
        */

        // center of image in pixels
        double cx = pixel_width / 2.0;
        double cy = pixel_height / 2.0;

        double R = r0 * ppcm;
        double R2 = R * R;

        for (int x = 0; x < pixel_width; x++)
        for (int y = 0; y < pixel_height; y++)
        {
            double dx = x - cx;
            double dy = y - cy;

            if (dx * dx + dy * dy <= R2)
            {
                rgb_vals[x, y, 0] = r;
                rgb_vals[x, y, 1] = g;
                rgb_vals[x, y, 2] = b;
            }
        }
    }

    public void PPM_Output(string filename)
    {
        using (var writer = new StreamWriter(filename))
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
