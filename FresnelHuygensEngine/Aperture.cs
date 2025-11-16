using System;
using System.IO;
using System.Numerics;

public class Aperture
{
    public float width_cm { get; }
    public float height_cm { get; }
    public int ppcm { get; }
    public int pixel_width { get; }
    public int pixel_height { get; }

    private Color[,] rgb_vals;

    // Basic Aperture constructor
    public Aperture(float width_cm, float height_cm, int ppcm)
    {
        this.width_cm = width_cm;
        this.height_cm = height_cm;
        this.ppcm = ppcm;

        pixel_width = (int)(ppcm * width_cm);
        pixel_height = (int)(ppcm * height_cm);

        rgb_vals = new Color[pixel_width, pixel_height];
    }

    // Fill entire aperture with a Color
    public void FillAperture(Color color)
    {
        for (int x = 0; x < pixel_width; x++)
            for (int y = 0; y < pixel_height; y++)
                rgb_vals[x, y] = color;
    }

    // Draw rectangle
    public void DrawRectangle(double x_min, double x_max, double y_min, double y_max, Color color)
    {
        int px_min = (int)((x_min + width_cm / 2) * ppcm);
        int px_max = (int)((x_max + width_cm / 2) * ppcm);
        int py_min = (int)((height_cm / 2 - y_max) * ppcm);
        int py_max = (int)((height_cm / 2 - y_min) * ppcm);

        px_min = Math.Max(0, px_min);
        px_max = Math.Min(pixel_width - 1, px_max);
        py_min = Math.Max(0, py_min);
        py_max = Math.Min(pixel_height - 1, py_max);

        for (int x = px_min; x <= px_max; x++)
            for (int y = py_min; y <= py_max; y++)
                rgb_vals[x, y] = color;
    }

    // Draw circle centered on image
    public void DrawCircle(double x0_cm, double y0_cm, double r0_cm, Color color)
    {
        // Convert center from cm to pixel coordinates
        double cx = (x0_cm + width_cm / 2.0) * ppcm;
        double cy = (-y0_cm + height_cm / 2.0) * ppcm;

        double R = r0_cm * ppcm;
        double R2 = R * R;

        for (int x = 0; x < pixel_width; x++)
            for (int y = 0; y < pixel_height; y++)
            {
                double dx = x - cx;
                double dy = y - cy;

                if (dx * dx + dy * dy <= R2)
                    rgb_vals[x, y] = color;
            }
    }

    // Build field as Complex[,]
    public Complex[,] BuildField()
    {
        var field = new Complex[pixel_width, pixel_height];

        for (int x = 0; x < pixel_width; x++)
            for (int y = 0; y < pixel_height; y++)
            {
                Color c = rgb_vals[x, y];
                if (c == null)
                {
                    field[x, y] = Complex.Zero;
                    continue;
                }

                // Interpret Color.Amplitude as amplitude (scale from 0 to 1)
                double A = c.Amplitude;
                field[x, y] = new Complex(A, 0.0);
            }

        return field;
    }

    public Color GetPixel(int x, int y) => rgb_vals[x, y];

    // Output to PPM
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
                    Color c = rgb_vals[x, y];
                    if (c == null)
                        writer.Write("0 0 0 ");
                    else
                        writer.Write($"{c.R} {c.G} {c.B} ");
                }
                writer.WriteLine();
            }
        }
    }
}
