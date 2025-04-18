﻿using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class CPT1CodesEdited_HistoryPoco
    {
        public int CPTID { get; set; }
        public string CPTcode { get; set; }
        public string CPTdescription { get; set; }
        public bool CPTcommon { get; set; }
        public string Fee { get; set; }
        public float? RVU { get; set; }
        public bool personalcode { get; set; }
        public bool Deleted { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }
        public string PayorID { get; set; }
        public decimal? Charge { get; set; }

        public CPT1CodesEdited_HistoryDomain MapToDomainModel()
        {
            CPT1CodesEdited_HistoryDomain domain = new CPT1CodesEdited_HistoryDomain
            {
                CPTID = CPTID,
                CPTcode = CPTcode,
                CPTdescription = CPTdescription,
                CPTcommon = CPTcommon,
                Fee = Fee,
                RVU = RVU,
                personalcode = personalcode,
                Deleted = Deleted,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded,
                PayorID = PayorID,
                Charge = Charge
            };

            return domain;
        }

        public CPT1CodesEdited_HistoryPoco() { }

        public CPT1CodesEdited_HistoryPoco(CPT1CodesEdited_HistoryDomain domain)
        {
            CPTID = domain.CPTID;
            CPTcode = domain.CPTcode;
            CPTdescription = domain.CPTdescription;
            CPTcommon = domain.CPTcommon;
            Fee = domain.Fee;
            RVU = domain.RVU;
            personalcode = domain.personalcode;
            Deleted = domain.Deleted;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
            PayorID = domain.PayorID;
            Charge = domain.Charge;
        }
    }
}