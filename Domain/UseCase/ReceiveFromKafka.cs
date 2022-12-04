using Domain.Ports;

namespace Domain.UseCase;
public class SaveMessageHandler
{

    private readonly IMessageRepository _messageRepository;

    public SaveMessageHandler(IMessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }

    public void Handle(string content)
    {
        _messageRepository.Add(new Message(content));
        throw new NotImplementedException();
    }
}
