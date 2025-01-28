using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam_CS
{
    class Officiant
    {
        public string PIB { get; set; }
        public string phone { get; set; }
        public Order order { get; set; }
        public IBuilder builder { get; set; }

        public Officiant(string PIB, string phone)
        {
            this.PIB = PIB;
            this.phone = phone;
            order = null;
        }
        public void SetBuilder(IBuilder builder)
        {
            this.builder = builder;
        }
        public Order BuildOrder()
        {
            return builder.GetResult();
        }
    }

}
