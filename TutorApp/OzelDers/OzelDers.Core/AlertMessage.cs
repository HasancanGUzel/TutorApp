using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzelDers.Core
{
    public class AlertMessage
    {
        public string Title { get; set; }//Uyarı mesajının başlığı
        public string Message { get; set; }//Uyarı mesajı
        public string AlertType { get; set; }//Uyarı mesajının tipi, görünümü
    }
}
