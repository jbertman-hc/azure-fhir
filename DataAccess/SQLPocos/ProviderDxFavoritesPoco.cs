using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class ProviderDxFavoritesPoco
    {
        public string ProviderCode { get; set; }
        public string DxCode { get; set; }
        public string DxDescription { get; set; }

        public ProviderDxFavoritesDomain MapToDomainModel()
        {
            ProviderDxFavoritesDomain domain = new ProviderDxFavoritesDomain
            {
                ProviderCode = ProviderCode,
                DxCode = DxCode,
                DxDescription = DxDescription
            };

            return domain;
        }

        public ProviderDxFavoritesPoco() { }

        public ProviderDxFavoritesPoco(ProviderDxFavoritesDomain domain)
        {
            ProviderCode = domain.ProviderCode;
            DxCode = domain.DxCode;
            DxDescription = domain.DxDescription;
        }
    }
}
