namespace Chat_UPD
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.Write("Введіть ім'я: ");
            string? user = Console.ReadLine();
            Upd_client upd_Client = new Upd_client(user);
            Task.Run(upd_Client.ReceiveMessageAsync);
            await upd_Client.SendMessageAsync();
        }
    }
}