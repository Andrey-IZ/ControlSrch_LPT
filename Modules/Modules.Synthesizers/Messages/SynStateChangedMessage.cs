namespace Modules.Synthesizers.Messages
{
    public class SynStateChangedMessage
    {
        public SynStateChangedMessage(int id, bool activeSynState)
        {
            Id = id;
            ActiveSynState = activeSynState;
        }

        public int Id { get; set; }
        public bool ActiveSynState { get; set; }
    }
}