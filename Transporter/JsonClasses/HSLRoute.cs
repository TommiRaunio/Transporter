namespace Transporter.JsonClasses
{

    public class HSLRoute
    {
        public float length { get; set; }
        public int duration { get; set; }
        public Leg[] legs { get; set; }
    }

    public class Leg
    {
        public int length { get; set; }
        public float duration { get; set; }
        public string type { get; set; }
        public Loc[] locs { get; set; }
        public string code { get; set; }
        public string shortCode { get; set; }
    }

    public class Loc
    {
        public Coord coord { get; set; }
        public string arrTime { get; set; }
        public string depTime { get; set; }
        public string name { get; set; }
        public string code { get; set; }
        public string shortCode { get; set; }
        public string stopAddress { get; set; }
    }

    public class Coord
    {
        public float x { get; set; }
        public float y { get; set; }
    }

}
