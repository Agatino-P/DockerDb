namespace Domain;
public class Message
{
    private readonly string _content;
    private Guid _id;

    public Message(string content)
    {
        _content = content;
        _id=Guid.NewGuid();
    }
}
