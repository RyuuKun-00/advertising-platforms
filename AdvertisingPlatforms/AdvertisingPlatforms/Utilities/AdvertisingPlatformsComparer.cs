namespace AdvertisingPlatforms.Utilities
{
    class AdvertisingPlatformsComparer : IComparer<int>
    {
        private List<string> _namePlatforms;

        public AdvertisingPlatformsComparer(List<string> namePlatforms)
        {
            _namePlatforms = namePlatforms;
        }
        public int Compare(int x, int y)
        {
            return String.Compare(_namePlatforms[x], _namePlatforms[y]);
        }
    }
}
