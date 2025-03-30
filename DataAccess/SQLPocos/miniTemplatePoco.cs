using POC.Domain.DomainModels;

namespace POC.DataAccess.SQLPocos
{
    public class miniTemplatePoco
    {
        public int miniKey { get; set; }
        public string ProviderName { get; set; }
        public string mTemplateName { get; set; }
        public string m0cap { get; set; }
        public string m1cap { get; set; }
        public string m2cap { get; set; }
        public string m3cap { get; set; }
        public string m4cap { get; set; }
        public string m5cap { get; set; }
        public string m6cap { get; set; }
        public string m7cap { get; set; }
        public string m8cap { get; set; }
        public string m9cap { get; set; }
        public string m10cap { get; set; }
        public string m11cap { get; set; }
        public string m12cap { get; set; }
        public string m13cap { get; set; }
        public string m14cap { get; set; }
        public string m15cap { get; set; }
        public string m16cap { get; set; }
        public string m17cap { get; set; }
        public string m18cap { get; set; }
        public string m19cap { get; set; }
        public string m0text { get; set; }
        public string m1text { get; set; }
        public string m2text { get; set; }
        public string m3text { get; set; }
        public string m4text { get; set; }
        public string m5text { get; set; }
        public string m6text { get; set; }
        public string m7text { get; set; }
        public string m8text { get; set; }
        public string m9text { get; set; }
        public string m10text { get; set; }
        public string m11text { get; set; }
        public string m12text { get; set; }
        public string m13text { get; set; }
        public string m14text { get; set; }
        public string m15text { get; set; }
        public string m16text { get; set; }
        public string m17text { get; set; }
        public string m18text { get; set; }
        public string m19text { get; set; }
        public string m0location { get; set; }
        public string m1location { get; set; }
        public string m2location { get; set; }
        public string m31location { get; set; }
        public string m4location { get; set; }
        public string m5location { get; set; }
        public string m6location { get; set; }
        public string m7location { get; set; }
        public string m8location { get; set; }
        public string m9location { get; set; }
        public string m10location { get; set; }
        public string m11location { get; set; }
        public string m12location { get; set; }
        public string m13location { get; set; }
        public string m14location { get; set; }
        public string m15location { get; set; }
        public string m16location { get; set; }
        public string m17location { get; set; }
        public string m18location { get; set; }
        public string m19location { get; set; }
        public DateTime? DateLastTouched { get; set; }
        public string LastTouchedBy { get; set; }
        public DateTime? DateRowAdded { get; set; }

        public miniTemplateDomain MapToDomainModel()
        {
            miniTemplateDomain domain = new miniTemplateDomain
            {
                miniKey = miniKey,
                ProviderName = ProviderName,
                mTemplateName = mTemplateName,
                m0cap = m0cap,
                m1cap = m1cap,
                m2cap = m2cap,
                m3cap = m3cap,
                m4cap = m4cap,
                m5cap = m5cap,
                m6cap = m6cap,
                m7cap = m7cap,
                m8cap = m8cap,
                m9cap = m9cap,
                m10cap = m10cap,
                m11cap = m11cap,
                m12cap = m12cap,
                m13cap = m13cap,
                m14cap = m14cap,
                m15cap = m15cap,
                m16cap = m16cap,
                m17cap = m17cap,
                m18cap = m18cap,
                m19cap = m19cap,
                m0text = m0text,
                m1text = m1text,
                m2text = m2text,
                m3text = m3text,
                m4text = m4text,
                m5text = m5text,
                m6text = m6text,
                m7text = m7text,
                m8text = m8text,
                m9text = m9text,
                m10text = m10text,
                m11text = m11text,
                m12text = m12text,
                m13text = m13text,
                m14text = m14text,
                m15text = m15text,
                m16text = m16text,
                m17text = m17text,
                m18text = m18text,
                m19text = m19text,
                m0location = m0location,
                m1location = m1location,
                m2location = m2location,
                m31location = m31location,
                m4location = m4location,
                m5location = m5location,
                m6location = m6location,
                m7location = m7location,
                m8location = m8location,
                m9location = m9location,
                m10location = m10location,
                m11location = m11location,
                m12location = m12location,
                m13location = m13location,
                m14location = m14location,
                m15location = m15location,
                m16location = m16location,
                m17location = m17location,
                m18location = m18location,
                m19location = m19location,
                DateLastTouched = DateLastTouched,
                LastTouchedBy = LastTouchedBy,
                DateRowAdded = DateRowAdded
            };

            return domain;
        }

        public miniTemplatePoco() { }

        public miniTemplatePoco(miniTemplateDomain domain)
        {
            miniKey = domain.miniKey;
            ProviderName = domain.ProviderName;
            mTemplateName = domain.mTemplateName;
            m0cap = domain.m0cap;
            m1cap = domain.m1cap;
            m2cap = domain.m2cap;
            m3cap = domain.m3cap;
            m4cap = domain.m4cap;
            m5cap = domain.m5cap;
            m6cap = domain.m6cap;
            m7cap = domain.m7cap;
            m8cap = domain.m8cap;
            m9cap = domain.m9cap;
            m10cap = domain.m10cap;
            m11cap = domain.m11cap;
            m12cap = domain.m12cap;
            m13cap = domain.m13cap;
            m14cap = domain.m14cap;
            m15cap = domain.m15cap;
            m16cap = domain.m16cap;
            m17cap = domain.m17cap;
            m18cap = domain.m18cap;
            m19cap = domain.m19cap;
            m0text = domain.m0text;
            m1text = domain.m1text;
            m2text = domain.m2text;
            m3text = domain.m3text;
            m4text = domain.m4text;
            m5text = domain.m5text;
            m6text = domain.m6text;
            m7text = domain.m7text;
            m8text = domain.m8text;
            m9text = domain.m9text;
            m10text = domain.m10text;
            m11text = domain.m11text;
            m12text = domain.m12text;
            m13text = domain.m13text;
            m14text = domain.m14text;
            m15text = domain.m15text;
            m16text = domain.m16text;
            m17text = domain.m17text;
            m18text = domain.m18text;
            m19text = domain.m19text;
            m0location = domain.m0location;
            m1location = domain.m1location;
            m2location = domain.m2location;
            m31location = domain.m31location;
            m4location = domain.m4location;
            m5location = domain.m5location;
            m6location = domain.m6location;
            m7location = domain.m7location;
            m8location = domain.m8location;
            m9location = domain.m9location;
            m10location = domain.m10location;
            m11location = domain.m11location;
            m12location = domain.m12location;
            m13location = domain.m13location;
            m14location = domain.m14location;
            m15location = domain.m15location;
            m16location = domain.m16location;
            m17location = domain.m17location;
            m18location = domain.m18location;
            m19location = domain.m19location;
            DateLastTouched = domain.DateLastTouched;
            LastTouchedBy = domain.LastTouchedBy;
            DateRowAdded = domain.DateRowAdded;
        }
    }
}
