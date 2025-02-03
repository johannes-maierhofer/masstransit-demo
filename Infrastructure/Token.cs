namespace Infrastructure
{
    public class Token
    {
        public string Value { get; set; } = Guid.NewGuid().ToString();
    }
}