using Domain.Ports;

namespace Domain.UseCase;
public class SaveMessageHandler1
{
    private readonly IMessageRepository _repository;

    public SaveMessageHandler1(IMessageRepository messageRepository)
    {
        _repository = messageRepository;
    }
    public void Handle(Message message)
    {
        _repository.Add(message);
    }
}
