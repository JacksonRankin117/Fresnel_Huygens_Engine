using System;
using System.IO;

class Program
{
    static void Main()
    {
        Vec3 p1 = new Vec3(1, 2, 3);
        Vec3 p2 = new Vec3(-1, 2, -3);

        Vec3 test = p1.Cross(p2);
        Console.WriteLine(test.ToString());
    }
}
