namespace AdvertisingPlatforms.DataAccess.Entities
{
    /// <summary>
    /// Сущность узла конечного атомата
    /// </summary>
    public class Node
    {
        /// <summary>
        /// Индетификатор терминального элемента
        /// </summary>
        public bool isTerminal;
        /// <summary>
        /// Хранит последний терминальный элемент
        /// </summary>
        public int? idParentTerminal;
        /// <summary>
        /// Хранит строку локации, если узел терминальный
        /// </summary>
        public string? location;
        /// <summary>
        /// Идентификаторы рекламных платформ в хранилище
        /// </summary>
        public int[]? idPlatforms;
        /// <summary>
        /// Массив переходов для построения префиксного дерева. Длиной 26, так как в латинице 26 букв
        /// </summary>
        public int[] transitionArray;

        public Node(int sizeTransitionArray)
        {
            transitionArray = new int[sizeTransitionArray];
            for (int i = 0; i < sizeTransitionArray; i++)
            {
                transitionArray[i] = -1;
            }
        }
    }
}
