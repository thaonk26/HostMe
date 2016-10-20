namespace HostAPI.Models
{
    class Hosts
    {
        public int id { get; set; }
        public string country { get; set; }
        public string city { get; set; }
        public string address { get; set; }
        public int spaceAvailable { get; set; }
        public string work { get; set; }
        public string duration { get; set; }
        public int pay { get; set; }
        public string gender { get; set; }
        public string age { get; set; }
        public string[] datesAvailable { get; set; }
    }
}