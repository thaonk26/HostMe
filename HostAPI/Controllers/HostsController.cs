using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HostAPI.Models;
using System.IO;
using System.Net.Http.Headers;
using RestSharp;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;

namespace HostAPI.Controllers
{
    public class HostsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        Hosts[] hosts = new Hosts[]

{
    new Hosts
    {
        country = "Japan",
        id = 1,
        city = "Tokyo",
        address = "2 Chome-18-6 Nakano, 中野区 Tokyo 164-0001, Japan",
        spaceAvailable = 4,
        work = "cleaning",
        duration = "3 days",
        pay = 0,
        gender = "mixed",
        age = "any, as long as 18+",
        datesAvailable = new string[1] { "11/1/2016 - 4 slots" }
    },

    new Hosts
    {
        id = 2,
        country = "Japan",
        city = "Tokyo",
        address = "2 Chome-2-24-2 Asakusa, Taito, Tokyo 111-0032, Japan",
        spaceAvailable = 3,
        work = "cleaning",
        duration = "5 days",
        pay = 5,
        gender = "mixed",
        age = "any, as long as 18+",
        datesAvailable = new string[2] { "11/3/2016 - 3 slots", "11/5/2016 - 5 slots" }
    },

    new Hosts
    {
        id = 3,
        country = "Japan",
        city = "Tokyo",
        address = "2 Chome-21-14 Yanagibashi, Taito, Tokyo 111-0052, Japan",
        spaceAvailable = 5,
        work = "cleaning",
        duration = "1 days",
        pay = 0,
        gender = "mixed",
        age = "any, as long as 18+",
        datesAvailable = new string[2] {"10/25/2016 - 5 slots", "11/5/2016 - 3 slots" }
    },

    new Hosts
    {
        id = 4,
        country = "Japan",
        city = "Tokyo",
        address = "2 Chome-21-14 Yanagibashi, Taito, Tokyo 111-0052, Japan",
        spaceAvailable = 5,
        work = "cleaning",
        duration = "1 days",
        pay = 0,
        gender = "mixed",
        age = "any, as long as 18+",
        datesAvailable = new string[2] {"10/25/2016 - 5 slots", "11/5/2016 - 3 slots" }
    },

    new Hosts
    {
        id = 5,
        country = "Japan",
        city = "Nagoya",
        address = "2-4-2 Kanayama Naka-ku, Nagoya-shi Aichi, 2 Chome-4 Kanayama, Naka Ward, Nagoya, Aichi Prefecture 460-0022, Japan",
        spaceAvailable = 5,
        work = "cleaning/painting",
        duration = "3 days",
        pay = 0,
        gender = "mixed",
        age = "any, as long as 18+",
        datesAvailable = new string[2] {"10/30/2016 - 3 slots", "11/3/2016 - 5 slots"}
    },

    new Hosts
    {
        id = 6,
        country = "Japan",
        city = "Nagoya",
        address = "Japan, 〒460-0024 Aichi Prefecture, Nagoya, Naka Ward, Masaki, 3−7−15",
        spaceAvailable = 5,
        work = "cleaning/painting",
        duration = "3 days",
        pay = 0,
        gender = "mixed",
        age = "any, as long as 18+",
        datesAvailable = new string[2] {"10/30/2016 - 3 slots", "11/3/2016 - 5 slots"}
    },

    new Hosts
    {
        id = 7,
        country = "Japan",
        city = "Kyoto",
        address = "Japan, 〒605-0079 Kyōto-fu, Kyōto-shi, Higashiyama-ku, Tokiwachō (Yamatoōjidōri), 川端四条上ル常磐町170",
        spaceAvailable = 4,
        work = "cleaning",
        duration = "2 days",
        pay = 0,
        gender = "mixed",
        age = "any, as long as 18+",
        datesAvailable = new string[2] {"10/15/2016 - 2 slots", "11/3/2016 - 4 slots"}
    },

    new Hosts
    {
        id = 8,
        country = "Japan",
        city = "Kyoto",
        address = "Japan, 〒600-8142 京都府京都市下京区土手町通七条上る納屋町418",
        spaceAvailable = 2,
        work = "cleaning",
        duration = "2 days",
        pay = 0,
        gender = "mixed",
        age = "any, as long as 18+",
        datesAvailable = new string[2] {"10/15/2016 - 2 slots", "11/3/2016 - 1 slots"}
    },

    new Hosts
    {
        id = 9,
        country = "China",
        city = "Shanghai",
        address = "China, Shanghai, Pudong, 浦东大道834弄840号甲号",
        spaceAvailable = 6,
        work = "cleaning/yard work",
        duration = "4 days",
        pay = 2,
        gender = "mixed",
        age = "any, as long as 18+",
        datesAvailable = new string[2] {"11/8/2016 - 2 slots", "11/10/2016 - 4 slots"}
    },

    new Hosts
    {
        id = 11,
        country = "China",
        city = "Shanghai",
        address = "China, Shanghai, Huangpu, Shanxi S Rd, 350号物资大厦 邮政编码: 200001",
        spaceAvailable = 4,
        work = "cooking",
        duration = "3 days",
        pay = 0,
        gender = "mixed",
        age = "any, as long as 18+",
        datesAvailable = new string[2] {"11/5/2016 - 2 slots", "11/13/2016 - 4 slots"}
    },

    new Hosts
    {
        id = 12,
        country = "China",
        city = "Beijing",
        address = "29 Renmin Shichang W Alley, Dongcheng, Beijing, China, 100010",
        spaceAvailable = 3,
        work = "bartending",
        duration = "3 days",
        pay = 8,
        gender = "mixed",
        age = "any, as long as 18+",
        datesAvailable = new string[2] {"11/8/2016 - 1 slots", "11/12/2016 - 3 slots"}
    },

    new Hosts
    {
        id = 13,
        country = "China",
        city = "Beijing",
        address = "78 Dongsi 9th Alley, Dongcheng, Beijing, China",
        spaceAvailable = 2,
        work = "cleaning",
        duration = "3 days",
        pay = 0,
        gender = "mixed",
        age = "any, as long as 18+",
        datesAvailable = new string[2] {"11/11/2016 - 1 slots", "11/12/2016 - 2 slots"}
    },

    new Hosts
    {
        id = 14,
        country = "Taiwan",
        city = "Taichung City",
        address = "No. 16, Zhongming S Rd, West District, Taichung City, Taiwan 403",
        spaceAvailable = 6,
        work = "cleaning/serving",
        duration = "4 days",
        pay = 5,
        gender = "mixed",
        age = "any, as long as 18+",
        datesAvailable = new string[3] {"11/8/2016 - 2 slots", "11/11/2016 - 4 slots", "11/12/2016 - 6 slots"}
    },

    new Hosts
    {
        id = 15,
        country = "Taiwan",
        city = "Taichung City",
        address = "407, Taiwan, Taichung City, Xitun District, 文華路150巷31號",
        spaceAvailable = 4,
        work = "cleaning",
        duration = "3 days",
        pay = 0,
        gender = "female",
        age = "any, as long as 18+",
        datesAvailable = new string[2] {"11/12/2016 - 2 slots", "11/15/2016 - 4 slots"}
    },

    new Hosts
    {
        id = 16,
        country = "Taiwan",
        city = "Taipei City",
        address = "Lane 54, Taishun Street, Da’an District, Taipei City, Taiwan 106",
        spaceAvailable = 4,
        work = "cleaning/babysitting",
        duration = "3 days",
        pay = 5,
        gender = "mixed",
        age = "any, as long as 18+",
        datesAvailable = new string[2] {"11/2016/2016 - 2 slots", "11/18/2016 - 4 slots"}
    },

    new Hosts
    {
        id = 17,
        country = "Taiwan",
        city = "Taipei City",
        address = "103, Taiwan, Taipei City, Datong District, 華陰街50號4樓",
        spaceAvailable = 3,
        work = "cleaning",
        duration = "1 days",
        pay = 0,
        gender = "mixed",
        age = "any, as long as 18+",
        datesAvailable = new string[2] {"11/20/2016 - 2 slots", "11/26/2016 - 3 slots"}
    },

    new Hosts
    {
        id = 18,
        country = "Taiwan",
        city = "Taipei City",
        address = "No. 316, Kunming St, Wanhua District, Taipei City, Taiwan 108",
        spaceAvailable = 3,
        work = "teaching English",
        duration = "2 weeks",
        pay = 15,
        gender = "mixed",
        age = "any, as long as 18+",
        datesAvailable = new string[2] {"11/26/2016 - 2 slots", "12/12/2016 - 3 slots"}
    },

    new Hosts
    {
        id = 19,
        country = "South Korea",
        city = "Seoul",
        address = "160 Sapyeong-daero, Seocho-gu, Seoul, South Korea",
        spaceAvailable = 5,
        work = "teaching English",
        duration = "7 days",
        pay = 18,
        gender = "mixed",
        age = "any, as long as 18+",
        datesAvailable = new string[3] {"11/30/2016 - 1 slots", "12/10/2016 - 3 slots", "12/23/2016 - 5 slots"}
    },

    new Hosts
    {
        id = 20,
        country = "South Korea",
        city = "Seoul",
        address = "South Korea, Seoul, Jung-gu, 충무로2가 51-10",
        spaceAvailable = 3,
        work = "cleaning",
        duration = "3 days",
        pay = 0,
        gender = "mixed",
        age = "any, as long as 18+",
        datesAvailable = new string[3] {"11/24/2016 - 1 slots", "12/1/2016 - 2 slots", "12/12/2016 - 3 slots"}
    },

    new Hosts
    {
        id = 21,
        country = "South Korea",
        city = "Busan",
        address = "59 Bujeon-ro, Busanjin-gu, Busan, South Korea",
        spaceAvailable = 5,
        work = "gardening",
        duration = "7 days",
        pay = 10,
        gender = "mixed",
        age = "any, as long as 18+",
        datesAvailable = new string[2] {"12/8/2016 - 2 slots", "12/24/2016 - 5 slots"}
    },

    new Hosts
    {
        id = 22,
        country = "South Korea",
        city = "Busan",
        address = "1162-3 Choryang-dong, Dong-gu, Busan, South Korea",
        spaceAvailable = 3,
        work = "cleaning",
        duration = "5 days",
        pay = 5,
        gender = "mixed",
        age = "any, as long as 18+",
        datesAvailable = new string[2] {"12/24/2016 - 2 slots", "1/2/2017 - 3 slots"}
    },

    new Hosts
    {
        id = 23,
        country = "Philippines",
        city = "Manila",
        address = "Adriatico St, Malate, Manila, Metro Manila, Philippines",
        spaceAvailable = 5,
        work = "teaching English",
        duration = "2 weeks",
        pay = 15,
        gender = "mixed",
        age = "any, as long as 18+",
        datesAvailable = new string[3] {"1/12/2017 - 2 slots", "1/15/2017 - 4 slots", "1/22/2017 - 5 slots"}
    },

    new Hosts
    {
        id = 24,
        country = "Philippines",
        city = "Manila",
        address = "1776 Adriatico St, Malate, Manila, 1004 Metro Manila, Philippines",
        spaceAvailable = 2,
        work = "bartending",
        duration = "7 days",
        pay = 20,
        gender = "mixed",
        age = "any, as long as 18+",
        datesAvailable = new string[2] {"1/10/2017 - 1 slots", "1/22/2017 - 2 slots"}
    },

    new Hosts
    {
        id = 25,
        country = "Philippines",
        city = "Manila",
        address = "Malvar Street, Manila, Metro Manila, Philippines",
        spaceAvailable = 4,
        work = "bartending",
        duration = "7 days",
        pay = 26,
        gender = "female",
        age = "any, as long as 18+",
        datesAvailable = new string[3] {"1/15/2017 - 1 slots", "1/18/2017 - 2 slots", "1/26/2017 - 4 slots"}
    },

    new Hosts
    {
        id = 26,
        country = "Malaysia",
        city = "Singapore",
        address = "285 Beach Rd, Singapore 199550",
        spaceAvailable = 4,
        work = "cleaning",
        duration = "3 days",
        pay = 0,
        gender = "mixed",
        age = "any, as long as 18+",
        datesAvailable = new string[3] {"1/15/2017 - 2 slots", "1/20/2017 - 3 slots", "2/2/2017 - 4 slots"}
    },

    new Hosts
    {
        id = 27,
        country = "Malaysia",
        city = "Singapore",
        address = "95 Owen Rd, Singapore 218919",
        spaceAvailable = 4,
        work = "babysitting",
        duration = "7 days",
        pay = 0,
        gender = "mixed",
        age = "any, as long as 18+",
        datesAvailable = new string[3] {"1/12/2017 - 2 slots", "1/20/2017 - 5 slots", "2/8/2017 - 3 slots"}
    },

    new Hosts
    {
        id = 28,
        country = "Malaysia",
        city = "Singapore",
        address = "11 Mackenzie Rd, Singapore 228675",
        spaceAvailable = 2,
        work = "cashier",
        duration = "5 days",
        pay = 10,
        gender = "mixed",
        age = "any, as long as 18+",
        datesAvailable = new string[3] {"1/12/2017 - 2 slots", "1/20/2017 - 1 slot", "2/8/2017 - 2 slots"}
    },

    new Hosts
    {
        id = 29,
        country = "Malaysia",
        city = "Singapore",
        address = "73 Dunlop St, Singapore 209401",
        spaceAvailable = 3,
        work = "cook",
        duration = "7 days",
        pay = 15,
        gender = "mixed",
        age = "any, as long as 18+",
        datesAvailable = new string[3] {"1/12/2017 - 2 slots", "1/20/2017 - 1 slot", "2/8/2017 - 3 slots"}
    },

    new Hosts
    {
        id = 30,
        country = "Malaysia",
        city = "Singapore",
        address = "15 Upper Weld Rd, Singapore 207372",
        spaceAvailable = 3,
        work = "cook/babysitting",
        duration = "3 days",
        pay = 5,
        gender = "mixed",
        age = "any, as long as 18+",
        datesAvailable = new string[3] {"1/8/2017 - 2 slots", "1/15/2017 - 1 slot", "1/26/2017 - 3 slots"}
    },

    new Hosts
    {
        id = 31,
        country = "Malaysia",
        city = "Singapore",
        address = "257 Jln Besar, Singapore 208930",
        spaceAvailable = 3,
        work = "cleaning",
        duration = "3 days",
        pay = 0,
        gender = "mixed",
        age = "any, as long as 18+",
        datesAvailable = new string[3] {"1/13/2017 - 2 slots", "1/2017/2017 - 1 slot", "2/6/2017 - 3 slots"}
    },

    new Hosts
    {
        id = 32,
        country = "Thailand",
        city = "Pattaya",
        address = "247/65 Pattay, Thanon Pattaya Sai Nueang, Bang Lamung District, Chon Buri, Thailand",
        spaceAvailable = 4,
        work = "bartending",
        duration = "7 days",
        pay = 10,
        gender = "mixed",
        age = "any, as long as 18+",
        datesAvailable = new string[3] {"2/13/2017 - 2 slots", "2/2017/2017 - 1 slot", "2/26/2017 - 4 slots"}
    },

    new Hosts
    {
        id = 33,
        country = "Thailand",
        city = "Pattaya",
        address = "420/142 M.9 Soi Buakhoa, Nongprua Pattaya, Chon Buri 20150, Thailand",
        spaceAvailable = 5,
        work = "cleaning",
        duration = "5 days",
        pay = 0,
        gender = "mixed",
        age = "any, as long as 18+",
        datesAvailable = new string[3] {"2/18/2017 - 2 slots", "2/20/2017 - 1 slot", "2/24/2017 - 4 slots"}
    },

    new Hosts
    {
        id = 34,
        country = "Australia",
        city = "Melbourne",
        address = "24 Grey St, St Kilda VIC 3182, Australia",
        spaceAvailable = 5,
        work = "cleaning",
        duration = "5 days",
        pay = 0,
        gender = "mixed",
        age = "any, as long as 18+",
        datesAvailable = new string[3] {"2/18/2017 - 2 slots", "2/20/2017 - 1 slot", "3/4/2017 - 5 slots"}
    },

    new Hosts
    {
        id = 35,
        country = "Australia",
        city = "Melbourne",
        address = "17 Carlisle St, St Kilda VIC 3182, Australia",
        spaceAvailable = 3,
        work = "cleaning",
        duration = "5 days",
        pay = 0,
        gender = "mixed",
        age = "any, as long as 18+",
        datesAvailable = new string[3] {"2/10/2017 - 2 slots", "2/21/2017 - 1 slot", "3/1/2017 - 3 slots"}
    },

    new Hosts
    {
        id = 36,
        country = "Australia",
        city = "Melbourne",
        address = "205 King St, Melbourne VIC 3000, Australia",
        spaceAvailable = 4,
        work = "cooking",
        duration = "3 days",
        pay = 0,
        gender = "mixed",
        age = "any, as long as 18+",
        datesAvailable = new string[3] {"2/23/2017 - 2 slots", "2/24/2017 - 3 slots", "3/8/2017 - 4 slots"}
    },

    new Hosts
    {
        id = 37,
        country = "Australia",
        city = "Melbourne",
        address = "Southbank, 26 Southgate Ave, Melbourne VIC 3006, Australia",
        spaceAvailable = 6,
        work = "cooking/serving",
        duration = "2 weeks",
        pay = 20,
        gender = "mixed",
        age = "any, as long as 18+",
        datesAvailable = new string[3] {"3/2/2017 - 4 slots", "3/14/2017 - 3 slots", "3/20/2017 - 6 slots"}
    },

    new Hosts
    {
        id = 38,
        country = "Australia",
        city = "Sydney",
        address = "110 Cumberland St, The Rocks NSW 2000, Australia",
        spaceAvailable = 6,
        work = "cooking/serving",
        duration = "2 weeks",
        pay = 15,
        gender = "mixed",
        age = "any, as long as 18+",
        datesAvailable = new string[3] {"3/8/2017 - 4 slots", "3/12/2017 - 3 slots", "3/29/2017 - 6 slots"}
    },

    new Hosts
    {
        id = 39,
        country = "Australia",
        city = "Sydney",
        address = "477 Kent St, Sydney NSW 2000, Australia",
        spaceAvailable = 2,
        work = "cleaning/clerk",
        duration = "7 days",
        pay = 10,
        gender = "mixed",
        age = "any, as long as 18+",
        datesAvailable = new string[2] {"3/6/2017 - 1 slots", "4/9/2017 - 2 slots"}
    },

    new Hosts
    {
        id = 40,
        country = "Australia",
        city = "Sydney",
        address = "8/10 Lee St, Sydney NSW 2000, Australia",
        spaceAvailable = 3,
        work = "cleaning",
        duration = "4 days",
        pay = 0,
        gender = "mixed",
        age = "any, as long as 18+",
        datesAvailable = new string[2] {"3/2016/2017 - 1 slots", "4/9/2017 - 3 slots"}
    },

    new Hosts
    {
        id = 41,
        country = "Australia",
        city = "Sydney",
        address = "87 Macleay St, Potts Point, Sydney NSW 2011, Australia",
        spaceAvailable = 4,
        work = "cleaning",
        duration = "5 days",
        pay = 0,
        gender = "mixed",
        age = "any, as long as 18+",
        datesAvailable = new string[2] {"3/20/2017 - 2 slots", "4/1/2017 - 4 slots"}
    }

};

        public IHttpActionResult GetHost(string search)
        {

            List<Hosts> myList = new List<Hosts>();
            string[] param = search.Split('$');
            for (var i = 0; i < param.Length; i++)
            {
                for (int j = 0; j < hosts.Length; j++)
                {
                    if (param.Length == 4)
                    {
                        if (hosts[j].city.ToLower() == param[0].ToLower() && hosts[j].country.ToLower() == param[1].ToLower() && hosts[j].gender.ToLower() == param[2].ToLower() && hosts[j].work.ToLower() == param[3].ToLower()
                                    || hosts[j].city.ToLower() == param[0].ToLower() && hosts[j].country.ToLower() == param[1].ToLower() && hosts[j].work.ToLower() == param[2].ToLower() && hosts[j].gender.ToLower() == param[3].ToLower()
                                    || hosts[j].city.ToLower() == param[0].ToLower() && hosts[j].gender.ToLower() == param[1].ToLower() && hosts[j].country.ToLower() == param[2].ToLower() && hosts[j].work.ToLower() == param[3].ToLower()
                                    || hosts[j].city.ToLower() == param[0].ToLower() && hosts[j].gender.ToLower() == param[1].ToLower() && hosts[j].work.ToLower() == param[2].ToLower() && hosts[j].country.ToLower() == param[3].ToLower()
                                    || hosts[j].city.ToLower() == param[0].ToLower() && hosts[j].work.ToLower() == param[1].ToLower() && hosts[j].country.ToLower() == param[2].ToLower() && hosts[j].gender.ToLower() == param[3].ToLower()
                                    || hosts[j].city.ToLower() == param[0].ToLower() && hosts[j].work.ToLower() == param[1].ToLower() && hosts[j].gender.ToLower() == param[2].ToLower() && hosts[j].country.ToLower() == param[3].ToLower()
                                    || hosts[j].work.ToLower() == param[0].ToLower() && hosts[j].city.ToLower() == param[1].ToLower() && hosts[j].country.ToLower() == param[2].ToLower() && hosts[j].gender.ToLower() == param[3].ToLower()
                                    || hosts[j].work.ToLower() == param[0].ToLower() && hosts[j].city.ToLower() == param[1].ToLower() && hosts[j].gender.ToLower() == param[2].ToLower() && hosts[j].country.ToLower() == param[3].ToLower()
                                    || hosts[j].work.ToLower() == param[0].ToLower() && hosts[j].gender.ToLower() == param[1].ToLower() && hosts[j].city.ToLower() == param[2].ToLower() && hosts[j].country.ToLower() == param[3].ToLower()
                                    || hosts[j].work.ToLower() == param[0].ToLower() && hosts[j].gender.ToLower() == param[1].ToLower() && hosts[j].country.ToLower() == param[2].ToLower() && hosts[j].city.ToLower() == param[3].ToLower()
                                    || hosts[j].work.ToLower() == param[0].ToLower() && hosts[j].country.ToLower() == param[1].ToLower() && hosts[j].city.ToLower() == param[2].ToLower() && hosts[j].gender.ToLower() == param[3].ToLower()
                                    || hosts[j].work.ToLower() == param[0].ToLower() && hosts[j].country.ToLower() == param[1].ToLower() && hosts[j].gender.ToLower() == param[2].ToLower() && hosts[j].city.ToLower() == param[3].ToLower()
                                    || hosts[j].gender.ToLower() == param[0].ToLower() && hosts[j].work.ToLower() == param[1].ToLower() && hosts[j].city.ToLower() == param[2].ToLower() && hosts[j].country.ToLower() == param[3].ToLower()
                                    || hosts[j].gender.ToLower() == param[0].ToLower() && hosts[j].work.ToLower() == param[1].ToLower() && hosts[j].country.ToLower() == param[2].ToLower() && hosts[j].city.ToLower() == param[3].ToLower()
                                    || hosts[j].gender.ToLower() == param[0].ToLower() && hosts[j].city.ToLower() == param[1].ToLower() && hosts[j].country.ToLower() == param[2].ToLower() && hosts[j].work.ToLower() == param[3].ToLower()
                                    || hosts[j].gender.ToLower() == param[0].ToLower() && hosts[j].city.ToLower() == param[1].ToLower() && hosts[j].work.ToLower() == param[2].ToLower() && hosts[j].country.ToLower() == param[3].ToLower()
                                    || hosts[j].gender.ToLower() == param[0].ToLower() && hosts[j].country.ToLower() == param[1].ToLower() && hosts[j].work.ToLower() == param[2].ToLower() && hosts[j].city.ToLower() == param[3].ToLower()
                                    || hosts[j].gender.ToLower() == param[0].ToLower() && hosts[j].country.ToLower() == param[1].ToLower() && hosts[j].city.ToLower() == param[2].ToLower() && hosts[j].work.ToLower() == param[3].ToLower()
                                    || hosts[j].country.ToLower() == param[0].ToLower() && hosts[j].work.ToLower() == param[1].ToLower() && hosts[j].city.ToLower() == param[2].ToLower() && hosts[j].gender.ToLower() == param[3].ToLower()
                                    || hosts[j].country.ToLower() == param[0].ToLower() && hosts[j].work.ToLower() == param[1].ToLower() && hosts[j].gender.ToLower() == param[2].ToLower() && hosts[j].city.ToLower() == param[3].ToLower()
                                    || hosts[j].country.ToLower() == param[0].ToLower() && hosts[j].gender.ToLower() == param[1].ToLower() && hosts[j].work.ToLower() == param[2].ToLower() && hosts[j].city.ToLower() == param[3].ToLower()
                                    || hosts[j].country.ToLower() == param[0].ToLower() && hosts[j].gender.ToLower() == param[1].ToLower() && hosts[j].city.ToLower() == param[2].ToLower() && hosts[j].work.ToLower() == param[3].ToLower()
                                    || hosts[j].country.ToLower() == param[0].ToLower() && hosts[j].city.ToLower() == param[1].ToLower() && hosts[j].work.ToLower() == param[2].ToLower() && hosts[j].gender.ToLower() == param[3].ToLower()
                                    || hosts[j].country.ToLower() == param[0].ToLower() && hosts[j].city.ToLower() == param[1].ToLower() && hosts[j].gender.ToLower() == param[2].ToLower() && hosts[j].work.ToLower() == param[3].ToLower())

                        {
                            if (!myList.Contains(hosts[j]))
                            {
                                myList.Add(hosts[j]);
                                
                            }
                        }

                    }
                    else if (param.Length == 3)
                    {

                        if (hosts[j].city.ToLower() == param[0].ToLower() && hosts[j].country.ToLower() == param[1].ToLower() && hosts[j].gender.ToLower() == param[2].ToLower()
                        || hosts[j].city.ToLower() == param[0].ToLower() && hosts[j].country.ToLower() == param[1].ToLower() && hosts[j].work.ToLower() == param[2].ToLower()
                        || hosts[j].city.ToLower() == param[0].ToLower() && hosts[j].gender.ToLower() == param[1].ToLower() && hosts[j].country.ToLower() == param[2].ToLower()
                        || hosts[j].city.ToLower() == param[0].ToLower() && hosts[j].gender.ToLower() == param[1].ToLower() && hosts[j].work.ToLower() == param[2].ToLower()
                        || hosts[j].city.ToLower() == param[0].ToLower() && hosts[j].work.ToLower() == param[1].ToLower() && hosts[j].country.ToLower() == param[2].ToLower()
                        || hosts[j].city.ToLower() == param[0].ToLower() && hosts[j].work.ToLower() == param[1].ToLower() && hosts[j].gender.ToLower() == param[2].ToLower()
                        || hosts[j].work.ToLower() == param[0].ToLower() && hosts[j].city.ToLower() == param[1].ToLower() && hosts[j].country.ToLower() == param[2].ToLower()
                        || hosts[j].work.ToLower() == param[0].ToLower() && hosts[j].city.ToLower() == param[1].ToLower() && hosts[j].gender.ToLower() == param[2].ToLower()
                        || hosts[j].work.ToLower() == param[0].ToLower() && hosts[j].gender.ToLower() == param[1].ToLower() && hosts[j].city.ToLower() == param[2].ToLower()
                        || hosts[j].work.ToLower() == param[0].ToLower() && hosts[j].gender.ToLower() == param[1].ToLower() && hosts[j].country.ToLower() == param[2].ToLower()
                        || hosts[j].work.ToLower() == param[0].ToLower() && hosts[j].country.ToLower() == param[1].ToLower() && hosts[j].city.ToLower() == param[2].ToLower()
                        || hosts[j].work.ToLower() == param[0].ToLower() && hosts[j].country.ToLower() == param[1].ToLower() && hosts[j].gender.ToLower() == param[2].ToLower()
                        || hosts[j].gender.ToLower() == param[0].ToLower() && hosts[j].work.ToLower() == param[1].ToLower() && hosts[j].city.ToLower() == param[2].ToLower()
                        || hosts[j].gender.ToLower() == param[0].ToLower() && hosts[j].work.ToLower() == param[1].ToLower() && hosts[j].country.ToLower() == param[2].ToLower()
                        || hosts[j].gender.ToLower() == param[0].ToLower() && hosts[j].city.ToLower() == param[1].ToLower() && hosts[j].country.ToLower() == param[2].ToLower()
                        || hosts[j].gender.ToLower() == param[0].ToLower() && hosts[j].city.ToLower() == param[1].ToLower() && hosts[j].work.ToLower() == param[2].ToLower()
                        || hosts[j].gender.ToLower() == param[0].ToLower() && hosts[j].country.ToLower() == param[1].ToLower() && hosts[j].work.ToLower() == param[2].ToLower()
                        || hosts[j].gender.ToLower() == param[0].ToLower() && hosts[j].country.ToLower() == param[1].ToLower() && hosts[j].city.ToLower() == param[2].ToLower()
                        || hosts[j].country.ToLower() == param[0].ToLower() && hosts[j].work.ToLower() == param[1].ToLower() && hosts[j].city.ToLower() == param[2].ToLower()
                        || hosts[j].country.ToLower() == param[0].ToLower() && hosts[j].work.ToLower() == param[1].ToLower() && hosts[j].gender.ToLower() == param[2].ToLower()
                        || hosts[j].country.ToLower() == param[0].ToLower() && hosts[j].gender.ToLower() == param[1].ToLower() && hosts[j].work.ToLower() == param[2].ToLower()
                        || hosts[j].country.ToLower() == param[0].ToLower() && hosts[j].gender.ToLower() == param[1].ToLower() && hosts[j].city.ToLower() == param[2].ToLower()
                        || hosts[j].country.ToLower() == param[0].ToLower() && hosts[j].city.ToLower() == param[1].ToLower() && hosts[j].work.ToLower() == param[2].ToLower()
                        || hosts[j].country.ToLower() == param[0].ToLower() && hosts[j].city.ToLower() == param[1].ToLower() && hosts[j].gender.ToLower() == param[2].ToLower())

                        {
                            if (!myList.Contains(hosts[j]))
                            {
                                myList.Add(hosts[j]);
                            }
                        }


                    }
                    else if (param.Length == 2)
                    {

                        if (hosts[j].city.ToLower() == param[0].ToLower() && hosts[j].country.ToLower() == param[1].ToLower()
                        || hosts[j].city.ToLower() == param[0].ToLower() && hosts[j].gender.ToLower() == param[1].ToLower()
                        || hosts[j].city.ToLower() == param[0].ToLower() && hosts[j].work.ToLower() == param[1].ToLower()
                        || hosts[j].work.ToLower() == param[0].ToLower() && hosts[j].city.ToLower() == param[1].ToLower()
                        || hosts[j].work.ToLower() == param[0].ToLower() && hosts[j].gender.ToLower() == param[1].ToLower()
                        || hosts[j].work.ToLower() == param[0].ToLower() && hosts[j].country.ToLower() == param[1].ToLower()
                        || hosts[j].gender.ToLower() == param[0].ToLower() && hosts[j].work.ToLower() == param[1].ToLower()
                        || hosts[j].gender.ToLower() == param[0].ToLower() && hosts[j].city.ToLower() == param[1].ToLower()
                        || hosts[j].gender.ToLower() == param[0].ToLower() && hosts[j].country.ToLower() == param[1].ToLower()
                        || hosts[j].country.ToLower() == param[0].ToLower() && hosts[j].work.ToLower() == param[1].ToLower()
                        || hosts[j].country.ToLower() == param[0].ToLower() && hosts[j].gender.ToLower() == param[1].ToLower()
                        || hosts[j].country.ToLower() == param[0].ToLower() && hosts[j].city.ToLower() == param[1].ToLower())

                        {
                            if (!myList.Contains(hosts[j]))
                            {
                                myList.Add(hosts[j]);
                            }
                        }

                    }
                    else if (param.Length == 1)
                    {

                        if (hosts[j].city.ToLower() == param[i]
                            || hosts[j].country.ToLower() == param[i]
                            || hosts[j].gender.ToLower() == param[i]
                            || hosts[j].work.ToLower() == param[i])
                        {
                            myList.Add(hosts[j]);
                        }
                    }
                }
                //var host = hosts.Where((x) =>
                //(x.work.ToLower() == param[i])
                //|| (x.country.ToLower() == param[i])
                //|| (x.gender.ToLower() == param[i])
                //|| (x.city.ToLower() == param[i]));
                //    myList.Concat(hosts).ToList();
            }
            if (myList == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(myList);
            }
        }
        public IHttpActionResult PostHost(string address, string country)
        {
          
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Hosts hosts = new Hosts();
            var test = db.Hosts.Where(x => x.pay == hosts.pay);
            hosts.address = address;
            hosts.country = country;

            db.Hosts.Add(hosts);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = hosts.id }, hosts);
        }
        
        public IHttpActionResult testing(string chicken)
        {
            List<Hosts> myList = new List<Hosts>();
            Hosts hosts = new Hosts();
            string[] param = chicken.Split('$');

            for (var i = 0; i < param.Length; i++)
            {
                var host = db.Hosts.Where(x => x.address == chicken).FirstOrDefault();
                myList.Add(host);
            }

            if (myList == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(myList);
            }
        }
    }
}
