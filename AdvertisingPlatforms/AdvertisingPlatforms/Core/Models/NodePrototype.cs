namespace AdvertisingPlatforms.Core.Models
{
    /// <summary>
    /// Модель узла для рекламной площадки
    /// </summary>
    public class NodePrototype
    {
        /// <summary>
        /// Идентификатор в массиве
        /// </summary>
        public int id;
        /// <summary>
        /// Является ли элемент терминальным(конечным)
        /// </summary>
        public bool isTerminal;
        /// <summary>
        /// Хранит строку локации, если узел терминальный
        /// </summary>
        public string? location;
        /// <summary>
        /// Идентификаторы рекламных платформ в хранилище
        /// </summary>
        public List<int>? idPlatforms;
        /// <summary>
        /// Индетификатор последнего терминальноого узла
        /// </summary>
        public int? idParentTerminal;
        /// <summary>
        /// Массив переходов для построения префиксного дерева. Длиной 26, так как в латинице 26 букв
        /// </summary>
        public int[] transitionArray;

        public NodePrototype(int sizeTransitionArray)
        {
            transitionArray = new int[sizeTransitionArray];
            for(int i =0;i< sizeTransitionArray; i++)
            {
                transitionArray[i] = -1;
            }
        }
    }
}
