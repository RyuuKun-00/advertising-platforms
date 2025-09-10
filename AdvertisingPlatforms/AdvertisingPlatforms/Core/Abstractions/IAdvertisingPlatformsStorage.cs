using AdvertisingPlatforms.Core.Models;
using AdvertisingPlatforms.DataAccess.Entities;

namespace AdvertisingPlatforms.Core.Abstractions
{
    public interface IAdvertisingPlatformsStorage
    {
        List<string> NamesPlatforms { get; set; }
        List<Node> Nodes { get; set; }
        Node StartNode { get; }

        Task<List<APLocation>> Search(string location);
    }
}