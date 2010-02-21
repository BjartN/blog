using CookComputing.XmlRpc;

namespace Blog.Infrastructure.MetaWeblogApi.Entities
{
    [XmlRpcMissingMapping(MappingAction.Ignore)]
    public struct Source
    {
        public string name;
        public string url;
    }
}