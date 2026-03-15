namespace FormulaAirline.Api.Services
{
    public interface IMessageProducer
    {
        Task SendMessage<T>(T message);
    }
}
