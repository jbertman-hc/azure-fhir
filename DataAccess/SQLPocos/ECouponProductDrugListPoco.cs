using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class ECouponProductDrugListPoco
    {
        public int PDL_Id { get; set; }
        public string ProductType { get; set; }
        public string CodeSet { get; set; }
        public string Code { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public ECouponProductDrugListDomain MapToDomainModel()
        {
            ECouponProductDrugListDomain domain = new ECouponProductDrugListDomain
            {
                PDL_Id = PDL_Id,
                ProductType = ProductType,
                CodeSet = CodeSet,
                Code = Code,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public ECouponProductDrugListPoco() { }

        public ECouponProductDrugListPoco(ECouponProductDrugListDomain domain)
        {
            PDL_Id = domain.PDL_Id;
            ProductType = domain.ProductType;
            CodeSet = domain.CodeSet;
            Code = domain.Code;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}