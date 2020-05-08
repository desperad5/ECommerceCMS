using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceCMS.Helpers
{
    public class Enums
    {
        public enum OrderCartStatusTypes
        {
            ACTIVE = 0,
            ABANDON = 1,
            EXPIRED = 2,
            COMPLETED = 3,
            PAYMENT_FAILED = 4
        }
        //public enum EntityTypes
        //{
        //    QuestionCard=0,
        //    ExamCard=1,
        //    TopicCard=2,
        //    Bundle=3
        //}

        //public enum ExamTypes
        //{
        //    YKS = 0,
        //    TYT = 1,
        //    AYT = 2,
        //    LGS = 3,
        //    KPSS = 4,
        //    AÖÖ = 5,
        //    AÖL = 6,
        //    MAÖL = 7,
        //    AÖİHL = 8
        //}
        public enum ProductSpecValueTypes
        {
            Number=0,
            Text=1,
            Decimal=2,
            SelectList=3,
            Datetime=4,
            Multiselect=5,
            Boolean=6

        }
        public enum MenuLocations
        {
            Header=0,
            LeftAside=1,
            RightAside=2
        }
        public enum SizeValues
        {
            XS=0,
            S=1,
            M=2,
            L=3,
            XL=4,
            XXL=5
        }
        public enum ColorValues
        {
            red=0,
            green=1,
            yellow=2,
            brown=3,
            black=4,
            grey=5,
            blue=6


        }
        public enum TemplateTypes
        {
            Fashion=0,
            Food=1,
            Digital=2
        }
        


    }
}
