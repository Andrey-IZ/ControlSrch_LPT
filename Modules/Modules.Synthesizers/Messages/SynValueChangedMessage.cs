
namespace Modules.Synthesizers.Messages
{
    public class SynValueChangedMessage
    {
         public SynValueChangedMessage(int id, decimal synValue)
        {
            Id = id;
            SynValue = synValue;
        }

        public int Id { get; set; }
        public decimal SynValue { get; set; }
    }
}