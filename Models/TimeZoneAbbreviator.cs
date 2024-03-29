using System;
using System.Collections.Generic;

namespace activity_planner.Models
{

    public static class TimeZoneAbbreviator
        {
            public static string Convertion(TimeZoneInfo Source)
            {
                Dictionary<string, string> Map = new Dictionary<string, string>()
                {
                {"alpha time zone","A"},
                {"australian central daylight time","ACDT"},
                {"australian central standard time","ACST"},
                {"acre time","ACT"},
                {"australian central time","ACT"},
                {"australian central western standard time","ACWST"},
                {"arabia daylight time","ADT"},
                {"atlantic daylight time","ADT"},
                {"australian eastern daylight time","AEDT"},
                {"australian eastern standard time","AEST"},
                {"australian eastern time","AET"},
                {"afghanistan time","AFT"},
                {"alaska daylight time","AKDT"},
                {"alaska standard time","AKST"},
                {"alma-ata time","ALMT"},
                {"amazon summer time","AMST"},
                {"armenia summer time","AMST"},
                {"amazon time","AMT"},
                {"armenia time","AMT"},
                {"anadyr summer time","ANAST"},
                {"anadyr time","ANAT"},
                {"aqtobe time","AQTT"},
                {"argentina time","ART"},
                {"arabia standard time", "AST"},
                {"atlantic standard time","AST"},
                {"atlantic time","AT"},
                {"australian western daylight time","AWDT"},
                {"australian western standard time","AWST"},
                {"azores summer time","AZOST"},
                {"azores time","AZOT"},
                {"azerbaijan summer time","AZST"},
                {"azerbaijan time","AZT"},
                {"anywhere on earth","AoE"},
                {"bravo time zone","B"},
                {"brunei darussalam time","BNT"},
                {"bolivia time","BOT"},
                {"brasília summer time","BRST"},
                {"brasília time","BRT"},
                {"bangladesh standard time","BST"},
                {"bougainville standard time","BST"},
                {"british summer time","BST"},
                {"bhutan time","BTT"},
                {"charlie time zone","C"},
                {"casey time","CAST"},
                {"central africa time","CAT"},
                {"cocos islands time","CCT"},
                {"central daylight time","CDT"},
                {"cuba daylight time","CDT"},
                {"central european summer time","CEST"},
                {"central european time","CET"},
                {"chatham island daylight time","CHADT"},
                {"chatham island standard time","CHAST"},
                {"choibalsan summer time","CHOST"},
                {"choibalsan time","CHOT"},
                {"chuuk time","CHUT"},
                {"cayman islands daylight saving time","CIDST"},
                {"cayman islands standard time","CIST"},
                {"cook island time","CKT"},
                {"chile summer time","CLST"},
                {"chile standard time","CLT"},
                {"colombia time","COT"},
                {"central standard time","CST"},
                {"china standard time","CST"},
                {"cuba standard time","CST"},
                {"central time","CT"},
                {"cape verde time","CVT"},
                {"christmas island time","CXT"},
                {"chamorro standard time","ChST"},
                {"delta time zone","D"},
                {"davis time","DAVT"},
                {"dumont-d'urville time","DDUT"},
                {"echo time zone","E"},
                {"easter island summer time","EASST"},
                {"easter island standard time","EAST"},
                {"eastern africa time","EAT"},
                {"ecuador time","ECT"},
                {"eastern daylight time","EDT"},
                {"eastern european summer time","EEST"},
                {"eastern european time","EET"},
                {"eastern greenland summer time","EGST"},
                {"east greenland time","EGT"},
                {"eastern standard time","EST"},
                {"eastern time","ET"},
                {"foxtrot time zone","F"},
                {"further-eastern european time","FET"},
                {"fiji summer time","FJST"},
                {"fiji time","FJT"},
                {"falkland islands summer time","FKST"},
                {"falkland island time","FKT"},
                {"fernando de noronha time","FNT"},
                {"golf time zone","G"},
                {"galapagos time","GALT"},
                {"gambier time","GAMT"},
                {"georgia standard time","GET"},
                {"french guiana time","GFT"},
                {"gilbert island time","GILT"},
                {"greenwich mean time","GMT"},
                {"gulf standard time","GST"},
                {"south georgia time","GST"},
                {"guyana time","GYT"},
                {"hotel time zone","H"},
                {"hawaii-aleutian daylight time","HDT"},
                {"hong kong time","HKT"},
                {"hovd summer time","HOVST"},
                {"hovd time","HOVT"},
                {"hawaii standard time","HST"},
                {"india time zone","I"},
                {"indochina time","ICT"},
                {"israel daylight time","IDT"},
                {"indian chagos time","IOT"},
                {"iran daylight time","IRDT"},
                {"irkutsk summer time","IRKST"},
                {"irkutsk time","IRKT"},
                {"iran standard time","IRST"},
                {"india standard time","IST"},
                {"irish standard time","IST"},
                {"israel standard time","IST"},
                {"japan standard time","JST"},
                {"kilo time zone","K"},
                {"kyrgyzstan time","KGT"},
                {"kosrae time","KOST"},
                {"krasnoyarsk summer time","KRAST"},
                {"krasnoyarsk time","KRAT"},
                {"korea standard time","KST"},
                {"kuybyshev time","KUYT"},
                {"lima time zone","L"},
                {"lord howe daylight time","LHDT"},
                {"lord howe standard time","LHST"},
                {"line islands time","LINT"},
                {"mike time zone","M"},
                {"magadan summer time","MAGST"},
                {"magadan time","MAGT"},
                {"marquesas time","MART"},
                {"mawson time","MAWT"},
                {"mountain daylight time","MDT"},
                {"marshall islands time","MHT"},
                {"myanmar time","MMT"},
                {"moscow daylight time","MSD"},
                {"moscow standard time","MSK"},
                {"mountain standard time","MST"},
                {"mountain time","MT"},
                {"mauritius time","MUT"},
                {"maldives time","MVT"},
                {"malaysia time","MYT"},
                {"november time zone","N"},
                {"new caledonia time","NCT"},
                {"newfoundland daylight time","NDT"},
                {"norfolk daylight time","NFDT"},
                {"norfolk time","NFT"},
                {"novosibirsk summer time","NOVST"},
                {"novosibirsk time","NOVT"},
                {"nepal time","NPT"},
                {"nauru time","NRT"},
                {"newfoundland standard time","NST"},
                {"niue time","NUT"},
                {"new zealand daylight time","NZDT"},
                {"new zealand standard time","NZST"},
                {"oscar time zone","O"},
                {"omsk summer time","OMSST"},
                {"omsk standard time","OMST"},
                {"oral time","ORAT"},
                {"papa time zone","P"},
                {"pacific daylight time","PDT"},
                {"peru time","PET"},
                {"kamchatka summer time","PETST"},
                {"kamchatka time","PETT"},
                {"papua new guinea time","PGT"},
                {"phoenix island time","PHOT"},
                {"philippine time","PHT"},
                {"pakistan standard time","PKT"},
                {"pierre & miquelon daylight time","PMDT"},
                {"pierre & miquelon standard time","PMST"},
                {"pohnpei standard time","PONT"},
                {"pacific standard time","PST"},
                {"pitcairn standard time","PST"},
                {"pacific time","PT"},
                {"palau time","PWT"},
                {"paraguay summer time","PYST"},
                {"paraguay time","PYT"},
                {"pyongyang time","PYT"},
                {"quebec time zone","Q"},
                {"qyzylorda time","QYZT"},
                {"romeo time zone","R"},
                {"reunion time","RET"},
                {"rothera time","ROTT"},
                {"sierra time zone","S"},
                {"sakhalin time","SAKT"},
                {"samara time","SAMT"},
                {"south africa standard time","SAST"},
                {"solomon islands time","SBT"},
                {"seychelles time","SCT"},
                {"singapore time","SGT"},
                {"srednekolymsk time","SRET"},
                {"suriname time","SRT"},
                {"samoa standard time","SST"},
                {"syowa time","SYOT"},
                {"tango time zone","T"},
                {"tahiti time","TAHT"},
                {"french southern and antarctic time","TFT"},
                {"tajikistan time","TJT"},
                {"tokelau time","TKT"},
                {"east timor time","TLT"},
                {"turkmenistan time","TMT"},
                {"tonga summer time","TOST"},
                {"tonga time","TOT"},
                {"turkey time","TRT"},
                {"tuvalu time","TVT"},
                {"uniform time zone","U"},
                {"ulaanbaatar summer time","ULAST"},
                {"ulaanbaatar time","ULAT"},
                {"coordinated universal time","UTC"},
                {"uruguay summer time","UYST"},
                {"uruguay time","UYT"},
                {"uzbekistan time","UZT"},
                {"victor time zone","V"},
                {"venezuelan standard time","VET"},
                {"vladivostok summer time","VLAST"},
                {"vladivostok time","VLAT"},
                {"vostok time","VOST"},
                {"vanuatu time","VUT"},
                {"whiskey time zone","W"},
                {"wake time","WAKT"},
                {"western argentine summer time","WARST"},
                {"west africa summer time","WAST"},
                {"west africa time","WAT"},
                {"western european summer time","WEST"},
                {"western european time","WET"},
                {"wallis and futuna time","WFT"},
                {"western greenland summer time","WGST"},
                {"west greenland time","WGT"},
                {"western indonesian time","WIB"},
                {"eastern indonesian time","WIT"},
                {"central indonesian time","WITA"},
                {"west samoa time","WST"},
                {"western sahara summer time","WST"},
                {"western sahara standard time","WT"},
                {"x-ray time zone","X"},
                {"yankee time zone","Y"},
                {"yakutsk summer time","YAKST"},
                {"yakutsk time","YAKT"},
                {"yap time","YAPT"},
                {"yekaterinburg summer time","YEKST"},
                {"yekaterinburg time","YEKT"},
                {"zulu time zone","Z"}
            };
            return Map[Source.Id.ToLower()];
        }
    }
}
