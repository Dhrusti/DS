using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.CommonModels
{
    public class CommonConstant
    {
        #region Reponse Messages
        public const string LoggedInSuccessfully = "Logged-In successfully!";
        public const string SomethingWentWrongPleaseTryAgain = "Something Went Wrong, Please try again!";
        public const string DataFoundSuccessfully = "Data found successfully!";
        public const string DataNotFound = "Data not found!";
        public const string DataSavedSuccessfully = "Data saved successfully!";
        public const string DataDeletedSuccessfully = "Data deleted successfully!";
        #endregion

        #region Coutries
        public const string UK = "UK";
        public const string CHINA = "China";
        public const string INDIA = "India";
        #endregion

        #region LogType
        public const string ActivityLog = "ActivityLog";
        public const string ExceptionLog = "ExceptionLog";
        public const string CustomLog = "CustomLog";
        public const string SchedulerLog = "SchedulerLog";
        public const string EmailLog = "EmailLog";
        #endregion

        #region TimeZone Ids
        public const string DATELINE_STANDARD_TIME = "Dateline Standard Time";
        public const string UTC_11 = "UTC-11";
        public const string ALEUTIAN_STANDARD_TIME = "Aleutian Standard Time";
        public const string HAWAIIAN_STANDARD_TIME = "Hawaiian Standard Time";
        public const string MARQUESAS_STANDARD_TIME = "Marquesas Standard Time";
        public const string ALASKAN_STANDARD_TIME = "Alaskan Standard Time";
        public const string UTC_09 = "UTC-09";
        public const string PACIFIC_STANDARD_TIME_MEXICO = "Pacific Standard Time (Mexico)";
        public const string UTC_08 = "UTC-08";
        public const string PACIFIC_STANDARD_TIME = "Pacific Standard Time";
        public const string US_MOUNTAIN_STANDARD_TIME = "US Mountain Standard Time";
        public const string MOUNTAIN_STANDARD_TIME_MEXICO = "Mountain Standard Time (Mexico)";
        public const string MOUNTAIN_STANDARD_TIME = "Mountain Standard Time";
        public const string YUKON_STANDARD_TIME = "Yukon Standard Time";
        public const string CENTRAL_AMERICA_STANDARD_TIME = "Central America Standard Time";
        public const string CENTRAL_STANDARD_TIME = "Central Standard Time";
        public const string EASTER_ISLAND_STANDARD_TIME = "Easter Island Standard Time";
        public const string CENTRAL_STANDARD_TIME_MEXICO = "Central Standard Time (Mexico)";
        public const string CANADA_CENTRAL_STANDARD_TIME = "Canada Central Standard Time";
        public const string SA_PACIFIC_STANDARD_TIME = "SA Pacific Standard Time";
        public const string EASTERN_STANDARD_TIME_MEXICO = "Eastern Standard Time (Mexico)";
        public const string EASTERN_STANDARD_TIME = "Eastern Standard Time";
        public const string HAITI_STANDARD_TIME = "Haiti Standard Time";
        public const string CUBA_STANDARD_TIME = "Cuba Standard Time";
        public const string US_EASTERN_STANDARD_TIME = "US Eastern Standard Time";
        public const string TURKS_AND_CAICOS_STANDARD_TIME = "Turks And Caicos Standard Time";
        public const string PARAGUAY_STANDARD_TIME = "Paraguay Standard Time";
        public const string ATLANTIC_STANDARD_TIME = "Atlantic Standard Time";
        public const string VENEZUELA_STANDARD_TIME = "Venezuela Standard Time";
        public const string CENTRAL_BRAZILIAN_STANDARD_TIME = "Central Brazilian Standard Time";
        public const string SA_WESTERN_STANDARD_TIME = "SA Western Standard Time";
        public const string PACIFIC_SA_STANDARD_TIME = "Pacific SA Standard Time";
        public const string NEWFOUNDLAND_STANDARD_TIME = "Newfoundland Standard Time";
        public const string TOCANTINS_STANDARD_TIME = "Tocantins Standard Time";
        public const string E_SOUTH_AMERICA_STANDARD_TIME = "E. South America Standard Time";
        public const string SA_EASTERN_STANDARD_TIME = "SA Eastern Standard Time";
        public const string ARGENTINA_STANDARD_TIME = "Argentina Standard Time";
        public const string MONTEVIDEO_STANDARD_TIME = "Montevideo Standard Time";
        public const string MAGALLANES_STANDARD_TIME = "Magallanes Standard Time";
        public const string SAINT_PIERRE_STANDARD_TIME = "Saint Pierre Standard Time";
        public const string BAHIA_STANDARD_TIME = "Bahia Standard Time";
        public const string UTC_02 = "UTC-02";
        public const string GREENLAND_STANDARD_TIME = "Greenland Standard Time";
        public const string MID_ATLANTIC_STANDARD_TIME = "Mid-Atlantic Standard Time";
        public const string AZORES_STANDARD_TIME = "Azores Standard Time";
        public const string CAPE_VERDE_STANDARD_TIME = "Cape Verde Standard Time";
        public const string UTC = "UTC";
        public const string GMT_STANDARD_TIME = "GMT Standard Time";
        public const string GREENWICH_STANDARD_TIME = "Greenwich Standard Time";
        public const string SAO_TOME_STANDARD_TIME = "Sao Tome Standard Time";
        public const string MOROCCO_STANDARD_TIME = "Morocco Standard Time";
        public const string W_EUROPE_STANDARD_TIME = "W. Europe Standard Time";
        public const string CENTRAL_EUROPE_STANDARD_TIME = "Central Europe Standard Time";
        public const string ROMANCE_STANDARD_TIME = "Romance Standard Time";
        public const string CENTRAL_EUROPEAN_STANDARD_TIME = "Central European Standard Time";
        public const string W_CENTRAL_AFRICA_STANDARD_TIME = "W. Central Africa Standard Time";
        public const string GTB_STANDARD_TIME = "GTB Standard Time";
        public const string MIDDLE_EAST_STANDARD_TIME = "Middle East Standard Time";
        public const string EGYPT_STANDARD_TIME = "Egypt Standard Time";
        public const string E_EUROPE_STANDARD_TIME = "E. Europe Standard Time";
        public const string WEST_BANK_STANDARD_TIME = "West Bank Standard Time";
        public const string SOUTH_AFRICA_STANDARD_TIME = "South Africa Standard Time";
        public const string FLE_STANDARD_TIME = "FLE Standard Time";
        public const string ISRAEL_STANDARD_TIME = "Israel Standard Time";
        public const string SOUTH_SUDAN_STANDARD_TIME = "South Sudan Standard Time";
        public const string KALININGRAD_STANDARD_TIME = "Kaliningrad Standard Time";
        public const string SUDAN_STANDARD_TIME = "Sudan Standard Time";
        public const string LIBYA_STANDARD_TIME = "Libya Standard Time";
        public const string NAMIBIA_STANDARD_TIME = "Namibia Standard Time";
        public const string JORDAN_STANDARD_TIME = "Jordan Standard Time";
        public const string ARABIC_STANDARD_TIME = "Arabic Standard Time";
        public const string SYRIA_STANDARD_TIME = "Syria Standard Time";
        public const string TURKEY_STANDARD_TIME = "Turkey Standard Time";
        public const string ARAB_STANDARD_TIME = "Arab Standard Time";
        public const string BELARUS_STANDARD_TIME = "Belarus Standard Time";
        public const string RUSSIAN_STANDARD_TIME = "Russian Standard Time";
        public const string E_AFRICA_STANDARD_TIME = "E. Africa Standard Time";
        public const string VOLGOGRAD_STANDARD_TIME = "Volgograd Standard Time";
        public const string IRAN_STANDARD_TIME = "Iran Standard Time";
        public const string ARABIAN_STANDARD_TIME = "Arabian Standard Time";
        public const string ASTRAKHAN_STANDARD_TIME = "Astrakhan Standard Time";
        public const string AZERBAIJAN_STANDARD_TIME = "Azerbaijan Standard Time";
        public const string RUSSIA_TIME_ZONE_3 = "Russia Time Zone 3";
        public const string MAURITIUS_STANDARD_TIME = "Mauritius Standard Time";
        public const string SARATOV_STANDARD_TIME = "Saratov Standard Time";
        public const string GEORGIAN_STANDARD_TIME = "Georgian Standard Time";
        public const string CAUCASUS_STANDARD_TIME = "Caucasus Standard Time";
        public const string AFGHANISTAN_STANDARD_TIME = "Afghanistan Standard Time";
        public const string WEST_ASIA_STANDARD_TIME = "West Asia Standard Time";
        public const string EKATERINBURG_STANDARD_TIME = "Ekaterinburg Standard Time";
        public const string PAKISTAN_STANDARD_TIME = "Pakistan Standard Time";
        public const string QYZYLORDA_STANDARD_TIME = "Qyzylorda Standard Time";
        public const string INDIA_STANDARD_TIME = "India Standard Time";
        public const string SRI_LANKA_STANDARD_TIME = "Sri Lanka Standard Time";
        public const string NEPAL_STANDARD_TIME = "Nepal Standard Time";
        public const string CENTRAL_ASIA_STANDARD_TIME = "Central Asia Standard Time";
        public const string BANGLADESH_STANDARD_TIME = "Bangladesh Standard Time";
        public const string OMSK_STANDARD_TIME = "Omsk Standard Time";
        public const string MYANMAR_STANDARD_TIME = "Myanmar Standard Time";
        public const string SE_ASIA_STANDARD_TIME = "SE Asia Standard Time";
        public const string ALTAI_STANDARD_TIME = "Altai Standard Time";
        public const string W_MONGOLIA_STANDARD_TIME = "W. Mongolia Standard Time";
        public const string NORTH_ASIA_STANDARD_TIME = "North Asia Standard Time";
        public const string N_CENTRAL_ASIA_STANDARD_TIME = "N. Central Asia Standard Time";
        public const string TOMSK_STANDARD_TIME = "Tomsk Standard Time";
        public const string CHINA_STANDARD_TIME = "China Standard Time";
        public const string NORTH_ASIA_EAST_STANDARD_TIME = "North Asia East Standard Time";
        public const string SINGAPORE_STANDARD_TIME = "Singapore Standard Time";
        public const string W_AUSTRALIA_STANDARD_TIME = "W. Australia Standard Time";
        public const string TAIPEI_STANDARD_TIME = "Taipei Standard Time";
        public const string ULAANBAATAR_STANDARD_TIME = "Ulaanbaatar Standard Time";
        public const string AUS_CENTRAL_W_STANDARD_TIME = "Aus Central W. Standard Time";
        public const string TRANSBAIKAL_STANDARD_TIME = "Transbaikal Standard Time";
        public const string TOKYO_STANDARD_TIME = "Tokyo Standard Time";
        public const string NORTH_KOREA_STANDARD_TIME = "North Korea Standard Time";
        public const string KOREA_STANDARD_TIME = "Korea Standard Time";
        public const string YAKUTSK_STANDARD_TIME = "Yakutsk Standard Time";
        public const string CEN_AUSTRALIA_STANDARD_TIME = "Cen. Australia Standard Time";
        public const string AUS_CENTRAL_STANDARD_TIME = "AUS Central Standard Time";
        public const string E_AUSTRALIA_STANDARD_TIME = "E. Australia Standard Time";
        public const string AUS_EASTERN_STANDARD_TIME = "AUS Eastern Standard Time";
        public const string WEST_PACIFIC_STANDARD_TIME = "West Pacific Standard Time";
        public const string TASMANIA_STANDARD_TIME = "Tasmania Standard Time";
        public const string VLADIVOSTOK_STANDARD_TIME = "Vladivostok Standard Time";
        public const string LORD_HOWE_STANDARD_TIME = "Lord Howe Standard Time";
        public const string BOUGAINVILLE_STANDARD_TIME = "Bougainville Standard Time";
        public const string RUSSIA_TIME_ZONE_10 = "Russia Time Zone 10";
        public const string MAGADAN_STANDARD_TIME = "Magadan Standard Time";
        public const string NORFOLK_STANDARD_TIME = "Norfolk Standard Time";
        public const string SAKHALIN_STANDARD_TIME = "Sakhalin Standard Time";
        public const string CENTRAL_PACIFIC_STANDARD_TIME = "Central Pacific Standard Time";
        public const string RUSSIA_TIME_ZONE_11 = "Russia Time Zone 11";
        public const string NEW_ZEALAND_STANDARD_TIME = "New Zealand Standard Time";
        public const string UTC_12 = "UTC+12";
        public const string FIJI_STANDARD_TIME = "Fiji Standard Time";
        public const string KAMCHATKA_STANDARD_TIME = "Kamchatka Standard Time";
        public const string CHATHAM_ISLANDS_STANDARD_TIME = "Chatham Islands Standard Time";
        public const string UTC_13 = "UTC+13";
        public const string TONGA_STANDARD_TIME = "Tonga Standard Time";
        public const string SAMOA_STANDARD_TIME = "Samoa Standard Time";
        public const string LINE_ISLANDS_STANDARD_TIME = "Line Islands Standard Time";
        #endregion
    }
}
