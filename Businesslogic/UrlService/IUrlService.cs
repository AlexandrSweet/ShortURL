using Businesslogic.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Businesslogic.UrlService
{
    public interface IUrlService
    {
        public URLDto AddUrl(URLDto uRLDto);
    }
}
