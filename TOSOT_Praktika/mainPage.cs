using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TOSOT_Praktika
{
       public static class mainPage
        {
            public static System.Windows.Controls.Frame MainPage;
        }
    public static class pageAuthorisation
    {
        public static System.Windows.Controls.Frame PageAuthorisation;
        
    }
    public static class pageRegistration
    {
        public static System.Windows.Controls.Frame PageRegistration;
    }
    public static class CertificateLogg
    {
        public static System.Windows.Controls.Frame CertificateLogPage;
    }
    partial class Worker
    {
        public string FullName
        {
            get
            {
                return $"{LastName} {FirstName} {MiddleName}";
            }
        }
    }
}
