namespace MyWebPlay.Model
{
    public class SettingAdmin
    {
        public List<Topic> Topics { get; set; }
        public class Topic
        {
            public Topic (string ID, string noidung, bool option)
            {
                this.ID = ID;
                this.NoiDung = noidung;
                this.Option = option;
            }

        public string ID { get; set; }
        public string NoiDung { get; set; }
        public bool Option { get; set; }
        }
    }
}
