using AutoMapper;
using Businesslogic.Models;
using DataAccess;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Businesslogic.UrlService
{
    public class UrlService : IUrlService
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly Mapper _autoMapper;
        public UrlService(IApplicationDbContext applicationDbContex)
        {
            _applicationDbContext = applicationDbContex;

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<URL, URLDto>();

            });
            _autoMapper = new Mapper(mapperConfig);
        }
        public URLDto AddUrl(URLDto uRLDto)
        {
            var resultSearch = _applicationDbContext.URLs.FirstOrDefault(u => u.LongUrl == uRLDto.LongUrl);
            if (resultSearch == null)
            {
                string finalString; // нужно проверить короткий URL чтоб он был уникальный.

                do { var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"; //зарэндомим адрес, для особых юзеров можно позволить самим ввести желаемый адрес.
                    var stringChars = new char[10];
                    var random = new Random();
                    for (int i = 0; i < stringChars.Length; i++)
                    {
                        stringChars[i] = chars[random.Next(chars.Length)];
                    }

                    finalString = new String(stringChars);
                } while( _applicationDbContext.URLs.FirstOrDefault(u => u.ShortUrl == finalString) != null);                

                 var url = new URL
                {
                    LongUrl = uRLDto.LongUrl,
                    ShortUrl = "https//www.ShotCut/" + finalString    //здесь добавим наш домен, что б если переедем на другой то легко его сменить, а не в таблице все ячейки переписывать
                };

                _applicationDbContext.URLs.Add(url);
                _applicationDbContext.SaveChanges();
                return _autoMapper.Map<URL, URLDto>(url);
            }
            
            var resultSear = _autoMapper.Map<URL, URLDto>(resultSearch);
            var tempShortUrl = resultSear.ShortUrl;
            resultSear.ShortUrl = "https//www.ShotCut/" + tempShortUrl;
            return resultSear;
        }
    }
}
