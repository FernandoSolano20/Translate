using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity_POJOS;
using Newtonsoft.Json;

namespace ConsoleTranslate
{
    class Program
    {
        private const string URL = "https://localhost:44342/api/";
        private static UserType actualUser;
        static void Main(string[] args)
        {
            ProcessProgram();
        }

        private static string AskName()
        {
            Console.WriteLine("Digite su nombre");
            return Console.ReadLine();
        }

        private static void GreetUser(string userName)
        {
            var user = new UserType
            {
                Name = userName
            };
            HttpRestClient.PostAsyncMethod<ApiResponse, UserType>(URL + "User/Post", user);
            Console.WriteLine("Hola " + userName);
            actualUser = user;
        }

        private static async void ProcessProgram()
        {
            var option = 0;
            var phraseTranslation = new StringBuilder();
            do
            {
                var name = AskName();
                GreetUser(name);

                Console.WriteLine("Digite el leguaje al que desea traducir");
                var destLanguage = Console.ReadLine().Trim().ToLower();
                var destLang = await AskLanguage(destLanguage);

                var originLang = "Español";

                if (originLang != null && destLang != null)
                {
                    Console.WriteLine("Digite la frase o palabra a traducir");
                    var phrase = Console.ReadLine().Trim().ToLower().Split(' ');
                    foreach (var word in phrase)
                    {
                        var taskWord = HttpRestClient.GetAsyncMethod<ApiResponse>(
                            URL + "Word/Get?originWord=" + word.Trim() + "" +
                            "&originLang=" + originLang + "" +
                            "&destLang=" + destLang.Name + "" +
                            "&userName=" + actualUser.Name);
                        var result = taskWord.GetAwaiter().GetResult();

                        if (result.Data != null)
                        {
                            var translation = JsonConvert.DeserializeObject<WordType>(result.Data.ToString());
                            phraseTranslation.Append(translation.DestinationWord + " ");
                        }
                        else
                        {
                            Console.WriteLine("No se cual es el significado de " + word);
                            Console.WriteLine("¿Conoce la traducción de la palabra?");
                            Console.WriteLine("1- Si");
                            Console.WriteLine("2- No");
                            var opc = int.Parse(Console.ReadLine());
                            if (opc == 1)
                            {
                                Console.WriteLine("¿Cual es el significado?");
                                var meaning = Console.ReadLine().Trim().ToLower();
                                var wordType = new WordType();
                                wordType.OriginWord= word;
                                wordType.OriginLanguage = new LanguageType {Name = originLang};
                                wordType.DestinationWord = meaning;
                                wordType.DestinationLanguage = destLang;

                                var data = HttpRestClient.PostAsyncMethod<ApiResponse, WordType>(URL + "Word/Post", wordType);
                                Task.WaitAll(data);

                                var taskWordCreate = HttpRestClient.GetAsyncMethod<ApiResponse>(
                                    URL + "Word/Get?originWord=" + word.Trim() + "" +
                                    "&originLang=" + originLang + "" +
                                    "&destLang=" + destLang.Name + "" +
                                    "&userName=" + actualUser.Name);
                                var response = taskWordCreate.GetAwaiter().GetResult();
                                var wordTranslate = JsonConvert.DeserializeObject<WordType>(response.Data.ToString());
                                phraseTranslation.Append(wordTranslate.OriginWord + " ");
                            }
                        }
                    }

                    Console.WriteLine(phraseTranslation.ToString());
                    phraseTranslation = new StringBuilder();
                }

                Console.WriteLine("¿Desea continuar?");
                Console.WriteLine("1- Si");
                Console.WriteLine("2- No");
                option = int.Parse(Console.ReadLine());
            } while (option == 1);
        }

        private static async Task<LanguageType> AskLanguage(string lang)
        {
            LanguageType language = null;
            var response = HttpRestClient.GetAsyncMethod<ApiResponse>(URL + "Language/Get?name=" + lang + "");
            var result = response.GetAwaiter().GetResult();
            
            if (result.Data != null)
            {
                language = JsonConvert.DeserializeObject<LanguageType>(result.Data.ToString());
            }
            else
            {
                Console.WriteLine("El lenguaje no esta disponible. ¿Desea agregarlo?");
                Console.WriteLine("1- Si");
                Console.WriteLine("2- No");
                var opc = int.Parse(Console.ReadLine());
                if (opc != 1) return null;
                language = new LanguageType { Name = lang };
                HttpRestClient.PostAsyncMethod<ApiResponse, LanguageType>(URL + "Language/Post", language);
            }
            return language;
        }
    }
}
