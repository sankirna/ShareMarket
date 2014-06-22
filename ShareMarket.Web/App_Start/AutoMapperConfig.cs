
using AutoMapper;

namespace ShareMarket.Web
{
    public class AutoMapperConfig
    {
        /// <summary>
        /// Create Mapping Model To Entity and Entity To Model
        /// </summary>
        public static void Mapping()
        {

        }

        /// <summary>
        /// Set Model to Entity and Entity to Model
        /// </summary>
        /// <typeparam name="T1">Model/Entity</typeparam>
        /// <typeparam name="T2">Model/Entity</typeparam>
        protected virtual void ViceVersa<T1, T2>()
        {

            Mapper.CreateMap<T1, T2>();
            Mapper.CreateMap<T2, T1>();
        }
    }
}
