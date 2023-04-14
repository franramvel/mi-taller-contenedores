using AutoMapper;
using mi_taller_contenedores.DB.Model;

namespace mi_taller_contenedores.ApiModels.MappingProfiles
{
    public class MainMappingProfile : Profile
    {
        public MainMappingProfile()
        {
            //Mapeos de Forms
            FormMappingProfiles();
            //Mapeos de request
            HttpRequestMappingProfiles();
            //Mapeos de re´sponse
            HttpResponseMappingProfiles();

        }

        private void FormMappingProfiles()
        {
            //Mapeos de forms
            //Convierte FacturaFormModel a Factura
            CreateMap<FacturaFormModel,Factura>().ReverseMap();
            CreateMap<UsuarioFormModel,Usuario>().ReverseMap();
            CreateMap<RolFormModel,Rol>().ReverseMap();
        }

        private void HttpResponseMappingProfiles()
        {
            //Mapeos de Response
            CreateMap<Usuario, UsuarioResponseFrontModel>()
                .ForMember(destino => destino.RolId, origen => origen.MapFrom(source => source.Rol.Id));
            CreateMap<Usuario, UsuarioResponseWSModel>()
                .ForMember(destino => destino.RolNombre, origen => origen.MapFrom(source => source.Rol.Nombre));
        }

        private void HttpRequestMappingProfiles()
        {
            //Mapeos de Request
            CreateMap<PagoPaypalRequestModel, PagoPaypal>()
                .ForMember(destino => destino.Monto, origen => origen.MapFrom(source => source.ammount_purchase))
                .ForMember(destino=>destino.PaypalId, origen=>origen.MapFrom(source=>source.paypal_id));
        }
    }
}
