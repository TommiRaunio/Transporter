namespace Transporter.JsonClasses
{

    public class HSLLine
    {
        public string code { get; set; }
        public string code_short { get; set; }
        public int transport_type_id { get; set; }
        public string line_start { get; set; }
        public string line_end { get; set; }
        public string name { get; set; }
        public string timetable_url { get; set; }
        public string line_shape { get; set; }
        public Line_Stops[] line_stops { get; set; }
    }

    public class Line_Stops
    {
        public string code { get; set; }
        public string codeShort { get; set; }
        public int time { get; set; }
        public string address { get; set; }
        public string name { get; set; }
        public string coords { get; set; }
        public string city_name { get; set; }
    }

}