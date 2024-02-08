using System;
using System.Net;
using System.IO;
using System.Threading;

namespace ConsoleApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = "head.txt"; 

            try
            {
                string[] usernames = File.ReadAllLines(filePath); // Считывания никнеймов с файла head.txt

                foreach (string username in usernames)
                {
                    DownloadHeadImage(username);
                    Thread.Sleep(500);
                }

                Console.WriteLine("Скачивание завершено.");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"Файл не найден: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
            }
        }

        static void DownloadHeadImage(string username)
        {
            var heads = $"https://skin.vimeworld.ru/helm/3d/{username}.png";
            try
            {
                Console.WriteLine($"Загрузка изображения для пользователя {username}...");
                var downloadsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");

                var saveFolderPath = Path.Combine(downloadsPath, "heads");
                Directory.CreateDirectory(saveFolderPath);

                var savePath = Path.Combine(saveFolderPath, $"{username}.png");

                using (var webClient = new WebClient())
                {
                    webClient.DownloadFile(heads, savePath);
                }

                Console.WriteLine($"Изображение для пользователя {username} успешно скачано.");
                Console.WriteLine($"Путь сохранения: {savePath}");
            }
            catch (WebException ex)
            {
                Console.WriteLine($"Ошибка при загрузке изображения для пользователя {username}: {ex.Message}");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Ошибка при сохранении изображения для пользователя {username}: {ex.Message}");
            }
        }
    }
}
