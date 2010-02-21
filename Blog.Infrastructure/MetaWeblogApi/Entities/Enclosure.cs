using CookComputing.XmlRpc;

namespace Blog.Infrastructure.MetaWeblogApi.Entities
{
    [XmlRpcMissingMapping(MappingAction.Ignore)]
    public struct Enclosure
    {
        public int length;
        public string type;
        public string url;
    }
}