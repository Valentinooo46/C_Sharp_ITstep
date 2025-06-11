using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SimpleHTTPClient
{
    public class Category //клас для десеріалізації категорії
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("urlSlug")]
        public string UrlSlug { get; set; }
        [JsonPropertyName("priority")]
        public int Priority { get; set; }
        [JsonPropertyName("image")]
        public string Image { get; set; }
    }

    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8; // для коректного відображення кирилиці
            Console.InputEncoding = Encoding.UTF8; // для коректного введення кирилиці
            while (true)
            {
                Console.WriteLine("1. Додати категорію");
                Console.WriteLine("2. Вивести список категорій");
                Console.WriteLine("0. Вийти");
                Console.Write("Оберіть опцію: ");
                var option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        await AddCategoryAsync();
                        break;
                    case "2":
                        await ListCategoriesAsync();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Невірна опція.");
                        break;
                }
            }
        }

        static async Task AddCategoryAsync()
        {
            Console.Write("Введіть назву категорії (title): ");
            var title = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(title))
            {
                Console.WriteLine("Title обов'язковий!");
                return;
            }

            Console.Write("Введіть urlSlug: ");
            var urlSlug = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(urlSlug))
            {
                Console.WriteLine("urlSlug обов'язковий!");
                return;
            }

            Console.Write("Введіть шлях до зображення: ");
            var imagePath = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(imagePath) || !File.Exists(imagePath))
            {
                Console.WriteLine("Файл не знайдено!");
                return;
            }
            Console.WriteLine("Введіть пріорітет категорії");
            Int32 priority;
            if(!Int32.TryParse(Console.ReadLine(), out priority))
            {
                Console.WriteLine("Пріорітет має бути числом!");
                return;
            }
            string base64Image;
            try
            {
                var imageBytes = await File.ReadAllBytesAsync(imagePath);
                base64Image = Convert.ToBase64String(imageBytes);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка читання файлу: {ex.Message}");
                return;
            }

            var category = new Category()
            {
                Title = title,
                Priority = priority,
                UrlSlug = urlSlug,
                Image = base64Image
            };

            var json = JsonSerializer.Serialize(category);
            using var client = new HttpClient();
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://lohika.itstep.click/api/Categories/add", content);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Категорію додано успішно!");
            }
            else
            {
                Console.WriteLine($"Помилка: {response.StatusCode}");
                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine(error);
            }
        }

        static async Task ListCategoriesAsync()
        {
            using var client = new HttpClient();
            var response = await client.GetAsync("https://lohika.itstep.click/api/Categories/list");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Помилка: {response.StatusCode}");
                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine(error);
                return;
            }

            var json = await response.Content.ReadAsStringAsync();
            var categories = JsonSerializer.Deserialize<List<Category>>(json);

            if (categories == null || categories.Count == 0)
            {
                Console.WriteLine("Список категорій порожній.");
                return;
            }

            foreach (var cat in categories)
            {
                Console.WriteLine($"ID: {cat.Id}, Title: {cat.Title}, UrlSlug: {cat.UrlSlug}, Priority: {cat.Priority}, Image: {cat.Image}");
            }
        }
    }
}
