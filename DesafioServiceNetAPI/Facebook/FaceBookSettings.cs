using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioServiceNetAPI.Facebook
{
    public class FaceBookSettings
    {
        public string AppId { get; set; }

        public string AppSecret { get; set; }

        public string tokenAppUrl { get; set; }

        public string tokenValidationUrl { get; set; }
    }
}
