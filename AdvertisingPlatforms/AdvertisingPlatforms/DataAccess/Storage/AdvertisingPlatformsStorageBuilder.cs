using AdvertisingPlatforms.Core.Abstractions;
using AdvertisingPlatforms.Core.Models;
using AdvertisingPlatforms.DataAccess.Entities;
using AdvertisingPlatforms.Utilities;
using System;

namespace AdvertisingPlatforms.DataAccess.Storage
{
    /// <summary>
    /// Сборщик конечного атомата для рекламных площадок
    /// </summary>
    public class AdvertisingPlatformsStorageBuilder : IAdvertisingPlatformsStorageBuilder
    {
        public List<string> namesPlatforms;

        public List<NodePrototype> nodes;
        public NodePrototype StartNode { 
            get
            {
                return nodes[0];
            } 
        }

        private IAppSettings _appSettings;
        private readonly IAdvertisingPlatformsStorage _storage;
        private int _sizeTransitionArray;
        private char _startIndex;

        public AdvertisingPlatformsStorageBuilder(IAppSettings appSettings, IAdvertisingPlatformsStorage storage)
        {
            _appSettings = appSettings;
            _storage = storage;

            if (_appSettings.CapitaLetterSensitivity)
            {
                _sizeTransitionArray = 58;
                _startIndex = 'A';
            }
            else
            {
                _sizeTransitionArray = 26;
                _startIndex = 'a';
            }

            namesPlatforms = new();
            nodes = new();
            NodePrototype startNode = new(_sizeTransitionArray);
            startNode.id = 0;
            startNode.isTerminal = true;
            startNode.location = null;
            startNode.idPlatforms = null;
            startNode.idParentTerminal = null;
            nodes.Add(startNode);
        }

        /// <summary>
        /// Добавление рекламной площадки в префиксный массив
        /// </summary>
        public void AddAdvertisingPlatform(AdvertisingPlatformDTO platform)
        {
            // Сохраняем название платформы и получаем его индетификатор в массиве
            int idName = namesPlatforms.Count;
            namesPlatforms.Add(platform.name);

            // Перебираем все локации вещания
            foreach (string[] subLocation in platform.locations)
            {
                // Последний терминальный узел
                NodePrototype lastTerminalNode = StartNode;
                // Родительский узел
                NodePrototype parentNode = StartNode;

                // Перебираем вложенные локации
                foreach (string location in subLocation)
                {
                    // Перебираем по символьно локацию
                    foreach (char symbol in location)
                    {
                        int tempId = parentNode.transitionArray[symbol - _startIndex];

                        if (tempId == -1)
                        {
                            NodePrototype temp = new(_sizeTransitionArray);

                            int id = nodes.Count;

                            temp.id = id;
                            temp.isTerminal = false;
                            temp.location = null;
                            temp.idPlatforms = null;
                            temp.idParentTerminal = null;

                            nodes.Add(temp);

                            // Фиксируем переход в родительском узле
                            parentNode.transitionArray[symbol - _startIndex] = id;

                            parentNode = temp;
                        }
                        else
                        {
                            parentNode = nodes[tempId];
                        }
                    }

                    // Последний узел локации является терминальным
                    parentNode.isTerminal = true;
                    parentNode.location = location;
                    parentNode.idParentTerminal = lastTerminalNode.id;
                    lastTerminalNode = parentNode;

                    // Если повторяющиеся имена локаций во сложениях запрещены, то скидываем до стартовой позиции
                    if (!_appSettings.LocationsWithTheSameName)
                    {
                        parentNode = StartNode;
                    }
                }

                lastTerminalNode.idPlatforms ??= new List<int>();
                // Последний терминальный узел хранит ссылку рекламной площадки
                lastTerminalNode.idPlatforms.Add(idName);
            }
        }

        public IAdvertisingPlatformsStorage Build()
        { 
            _storage.NamesPlatforms = namesPlatforms;

            _storage.Nodes = nodes.Select<NodePrototype, Node>(i =>
            {
                Node advertisingPlatform = new Node(_sizeTransitionArray)
                {
                    isTerminal = i.isTerminal,
                    transitionArray = i.transitionArray
                };

                if (i.isTerminal)
                {
                    advertisingPlatform.idParentTerminal = i.idParentTerminal==-1 ? null : i.idParentTerminal;
                    advertisingPlatform.location = i.location;
                    i.idPlatforms?.Sort(new AdvertisingPlatformsComparer(namesPlatforms));
                    advertisingPlatform.idPlatforms = i.idPlatforms?.ToArray();
                }

                return advertisingPlatform;
            }).ToList<Node>();

            return _storage;
        }
    }
}
