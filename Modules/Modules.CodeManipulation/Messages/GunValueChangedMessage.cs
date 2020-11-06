namespace Modules.ManipulationsCode.Messages
{
    public class GunValueChangedMessage
    {
        public int Id { get; set; }
        public decimal GunValue { get; set; }

        public GunValueChangedMessage(int id, decimal gunValue)
        {
            Id = id;
            GunValue = gunValue;
        }
    }
}