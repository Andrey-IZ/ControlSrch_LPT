
using Catel;
using Catel.MVVM;

namespace Common.Messages
{
    public class ChangeStepBuilderMessage
    {
        /// <exception cref="System.ArgumentNullException">The <paramref name="sender"/> is <c>null</c>.</exception>
        public ChangeStepBuilderMessage(IViewModel sender, bool isIncrease, decimal value = -1)
        {
            Argument.IsNotNull(() => sender);

            Sender = sender;
            IsIncrease = isIncrease;
            Value = value;
        }

        public bool IsIncrease { get; private set; }
        public IViewModel Sender { get; private set; }
        public decimal Value { get; private set; }
    }
}