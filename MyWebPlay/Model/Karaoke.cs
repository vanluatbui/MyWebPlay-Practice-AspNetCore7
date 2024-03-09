namespace MyWebPlay.Model
{
    public class Karaoke
    {
        public Karaoke()
        {
            mausac = new List<string>();
            member = new List<string>();
            text = new List<string>();
        }

        public List<string> member { get; set; }

        public List<string> mausac { get; set; }

        public List<string> text { get; set; }
    }
}
