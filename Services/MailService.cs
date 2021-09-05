namespace dotnetApp.Services
{

  public interface IMailService
  {
    void send(string from, string to);
  }
  public class MailService : IMailService
  {
    public MailService()
    {

    }

    public void send(string from, string to)
    {
      throw new System.NotImplementedException();
    }
  }
}
