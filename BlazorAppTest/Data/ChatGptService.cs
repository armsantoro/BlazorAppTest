using System.Text.Json;

namespace BlazorAppTest.Data
{
    public class ChatGptService
    {
        private readonly HttpClient _httpClient;
        public ChatGptService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<string> GetResponseChatGpt(string prompt)
        {
            string apiUser = "user";
            string apiKey = "INSERISCI LA CHIAVE";
            string apiUrl = "https://api.openai.com/v1/chat/completions";

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);

            ChatGptModelRequest chatGptModelRequest = new ChatGptModelRequest();
            List<Message> messages = new List<Message>();
            Message message = new Message();
            message.role = apiUser;
            message.content = prompt;
            messages.Add(message);
            
            chatGptModelRequest.model = "gpt-3.5-turbo";
            chatGptModelRequest.messages = messages;

            var response = await _httpClient.PostAsJsonAsync(apiUrl, chatGptModelRequest);
            var json = await response.Content.ReadAsStringAsync();

            // Deserializza la stringa JSON in un oggetto ChatGptModelResponse
            var chatGptModelResponse = JsonSerializer.Deserialize<ChatGptModelResponse>(json);

            // Ottiene il contenuto del primo messaggio nella lista di messaggi
            var content = chatGptModelResponse.choices[0].message.content;

            return content;
        }
    }
}
