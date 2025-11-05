class Standard
{
    public float width_cm { get; }   
    public float height_cm { get; }  
    public int ppcm { get; }         

    public int pixel_width { get; }
    public int pixel_height { get; }

    public Standard(float width_cm, float height_cm, int ppcm)
    {
        this.width_cm = width_cm;
        this.height_cm = height_cm;
        this.ppcm = ppcm;

        pixel_width = (int)(ppcm * width_cm);
        pixel_height = (int)(ppcm * height_cm);
    }

    
}
