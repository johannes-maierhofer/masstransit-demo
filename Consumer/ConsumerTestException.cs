namespace Consumer
{
    public class ConsumerTestException : Exception
    {
        public ConsumerTestException() : base("Error for testing Consumer retries.")
        {
        }
    }
}
