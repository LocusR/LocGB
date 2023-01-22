using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Whetstone.ChatGPT;
using Whetstone.ChatGPT.Models;

namespace LDGB
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TelegramBotClient client = new TelegramBotClient("5671406166:AAEgxV8YNlDTjaeKti76vZ95kj13bHkY4PU");
            client.StartReceiving(Update, Error);
            Console.ReadLine();
        }

  
        async static Task Update(ITelegramBotClient botClient, Update update, CancellationToken token)
        {
            if (update.Type != UpdateType.Message)
                return;
            IChatGPTClient client = new ChatGPTClient("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");

            var message = update.Message;
            ChatGPTCompletionRequest completionRequest = new ChatGPTCompletionRequest
            {
                Model = "text-davinci-003",
                Prompt = message.Text,
                Temperature = 0.5f,
                MaxTokens = 120,
                TopP = 0.3f,
                FrequencyPenalty = 0.5f,
                PresencePenalty = 0
            };
            var response = await client.CreateCompletionAsync(completionRequest);
            Console.WriteLine(response);
            await botClient.SendTextMessageAsync(message.Chat.Id, "Let me see...");
            await botClient.SendTextMessageAsync(message.Chat.Id, response.GetCompletionText());
        }
        private static Task Error(ITelegramBotClient arg1, Exception arg2, CancellationToken arg3)
        {
            throw new NotImplementedException();
        }

    }
}


