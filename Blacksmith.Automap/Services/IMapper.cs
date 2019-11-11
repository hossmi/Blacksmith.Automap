namespace Blacksmith.Automap.Services
{
    public interface IMapper
    {
        void map(object source, object target);
        IMapRepository Repository { get; set; }
    }
}
