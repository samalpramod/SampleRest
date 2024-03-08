namespace SampleRest.Entities
{
    public class RandomUserAPIResult
    {
        public List<User> results { get; set; }
        private RandomUserAPIInfo info { get; set; }
    }

    internal class RandomUserAPIInfo
    {
        public string seed { get; set; }
        public int results { get; set; }
        public int page { get; set; }
        public int version { get; set; }
    }
}
