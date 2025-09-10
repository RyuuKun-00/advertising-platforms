using AdvertisingPlatforms.Core.Abstractions;
using AdvertisingPlatforms.Core.Models;
using AdvertisingPlatforms.DataAccess.Entities;

namespace AdvertisingPlatforms.DataAccess.Storage
{
    /// <summary>
    /// Хранилище префиксного графа для работы конечного автомата
    /// <para>Singleton реализуется через Dependency Injection</para>
    /// </summary>
    public class AdvertisingPlatformsStorage : IAdvertisingPlatformsStorage
    {
        public List<string> NamesPlatforms { get; set; }
        public List<Node> Nodes { get; set; }
        public Node StartNode
        {
            get
            {
                return Nodes[0];
            }
        }

        public char StartChar;
        public AdvertisingPlatformsStorage(IAppSettings appSettings)
        {
            NamesPlatforms = new();
            Nodes = new();
            Node temp;
            if (appSettings.CapitaLetterSensitivity)
            {
                temp = new(58);
                StartChar = 'A';
            }
            else
            {
                temp = new(26);
                StartChar = 'a';
            }

            temp.idParentTerminal = null;
            temp.idPlatforms = null;
            temp.isTerminal = true;
            temp.location = null;
            Nodes.Add(temp);
        }

        public async Task<List<APLocation>> Search(string location)
        {
            var result = await Task.Run(() =>
                {

                    Node? terminal = IsExists(location);
                    if (terminal == null)
                    {
                        return new List<APLocation>();
                    }

                    var advertisingPlatforms = GetAdvertisingPlatforms(terminal!);
                    return advertisingPlatforms;
                }
            );

            return result;
        }
        /// <summary>
        /// Проверяет на наличие локации в префиксном массиве
        /// </summary>
        private Node? IsExists(string location)
        {
            Node temp = StartNode;

            // Перебираем по символьно строку локации, для перемещенитя по префиксному графу
            foreach (char item in location)
            {
                // Получаем id следующего узла
                int id = temp.transitionArray[item - StartChar];
                // Проверка на наличие перехода
                if (id > -1)
                {
                    temp = Nodes[id];
                }
                else
                {
                    // Если перехода нет, то такой локации нет в префиксном массиве
                    return null;
                }
            }

            // Если конечный узел не терминальный, то такой локации нет в префиксном массиве
            if (!temp.isTerminal) return null;

            return temp;
        }

        private List<APLocation> GetAdvertisingPlatforms(Node terminal)
        {
            List<APLocation> advertisingPlatforms = new();
            Node temp = terminal;
            while (temp.isTerminal && temp.idParentTerminal is not null)
            {
                if (temp.idPlatforms is not null)
                {
                    string location = temp.location!;

                    List<string> namesPlatformsInLocation = new();

                    foreach (int id in temp.idPlatforms!)
                    {
                        namesPlatformsInLocation.Add(NamesPlatforms[id]);
                    }

                    advertisingPlatforms.Add(new APLocation(location, namesPlatformsInLocation));
                }


                int idParentTerminal = temp.idParentTerminal.Value;
                temp = Nodes[idParentTerminal];
            }

            return advertisingPlatforms;
        }
    }
}
