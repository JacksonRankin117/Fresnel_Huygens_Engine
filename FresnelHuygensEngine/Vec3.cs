

class Vec3
{
    public double x { get; }
    public double y { get; }
    public double z { get; }

    public Vec3(double _x, double _y, double _z)
    {
        x = _x;
        y = _y;
        z = _z;
    }

    // Operator overloading
    public static Vec3 operator +(Vec3 a, Vec3 b) => new Vec3(a.x + b.x, a.y + b.y, a.z + b.z);
    public static Vec3 operator -(Vec3 a, Vec3 b) => new Vec3(a.x - b.x, a.y - b.y, a.z - b.z);
    public static Vec3 operator -(Vec3 v) => new Vec3(-v.x, -v.y, -v.z);
    public static Vec3 operator *(Vec3 v, double scalar) => new Vec3(v.x * scalar, v.y * scalar, v.z * scalar);
    public static Vec3 operator *(double scalar, Vec3 v) => v * scalar;
    public static Vec3 operator /(Vec3 v, double scalar) => new Vec3(v.x / scalar, v.y / scalar, v.z / scalar);


    // Vector Operations
    public double Dot(Vec3 other)
    {
        return x * other.x + y * other.y + z * other.z;
    }

    public Vec3 Cross(Vec3 other)
    {
        double x_comp = y * other.z - z * other.y;
        double y_comp = z * other.x - x * other.z;
        double z_comp = x * other.y - y * other.x;

        return new Vec3(x_comp, y_comp, z_comp);
    }

    public double Magnitude()
    {
        return Math.Sqrt(x * x + y * y + z * z);
    }

    public double Distance(Vec3 other)
    {
        return (this - other).Magnitude();
    }

    

}
